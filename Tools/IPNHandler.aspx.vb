Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Web.Mail
Imports System.Collections.Specialized

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Log.EventLog

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools.Tools
    Partial Public Class IPNHandler
        Inherits Framework.PageBase

#Region " Private Methods "

        Private Function VerifyIPN(ByVal useLiveProcessor As Boolean) As Boolean

            Dim strFormValues As String = Request.Form.ToString()
            Dim strNewValue As String
            Dim strResponse As String
            Dim serverURL As String = ""

            If useLiveProcessor Then
                serverURL = "https://www.paypal.com/cgi-bin/webscr"
            Else
                serverURL = "https://www.sandbox.paypal.com/cgi-bin/webscr"
            End If

            ' Create the request back
            Dim req As HttpWebRequest = CType(WebRequest.Create(serverURL), HttpWebRequest)

            ' Set values for the request back
            req.Method = "POST"
            req.ContentType = "application/x-www-form-urlencoded"
            strNewValue = strFormValues & "&cmd=_notify-validate"
            req.ContentLength = strNewValue.Length

            ' Write the request back IPN strings
            Dim stOut As StreamWriter = New StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII)
            stOut.Write(strNewValue)
            stOut.Close()

            ' Do the request to PayPal and get the response
            Dim httpWebResponse As HttpWebResponse = DirectCast(req.GetResponse(), HttpWebResponse)
            Using streamReader As New StreamReader(httpWebResponse.GetResponseStream())
                strResponse = streamReader.ReadToEnd()
                streamReader.Close()
            End Using

            ' Confirm whether the IPN was VERIFIED or INVALID. If INVALID, just ignore the IPN
            Return (httpWebResponse.StatusCode = HttpStatusCode.OK)

        End Function

#End Region

#Region " Event Handlers "

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim status As String = Request.Form("payment_status").ToString()
            Dim txn_type As String = Request.Form("txn_type")

            Dim values As String = ""
            For Each key As String In Request.Form.Keys
                values += (key & ":" & Request.Form(key) & vbCrLf)
            Next

            ' DotNetNuke.Services.Mail.Mail.SendMail("smcculloch@iinet.net.au", "smcculloch@iinet.net.au", "", "Form-Post", values, "", "", "", "", "", "")

            If (Request.Form("custom").ToString().Split(Convert.ToChar(",")).Length < 2) Then
                Return
            End If

            Dim transactionID As String = Request.Form("txn_id").ToString()
            Dim userID As Integer = Convert.ToInt32(Request.Form("custom").Split(Convert.ToChar(","))(0))
            Dim useLiveProcessor As Boolean = Convert.ToBoolean(Request.Form("custom").Split(Convert.ToChar(","))(1))
            Dim moduleID As Integer = Convert.ToInt32(Request.Form("custom").Split(Convert.ToChar(","))(2))
            Dim orderID As Integer = Convert.ToInt32(Request.Form("item_number").ToString())
            Dim amount As String = Request.Form("mc_gross").ToString()

            If (VerifyIPN(useLiveProcessor)) Then

                If (status.ToLower() = "subscr_cancel") Then

                    Dim objOrderController As New OrderController
                    Dim objOrder As OrderInfo = objOrderController.Get(orderID)

                    If Not (objOrder Is Nothing) Then
                        Dim objReceiptController As New ReceiptController
                        Dim objReceipts As ArrayList = objReceiptController.List(objOrder.PortalID, objOrder.ModuleID, userID, Null.NullString)

                        For Each objReceipt As ReceiptInfo In objReceipts
                            objReceipt.AutoRenew = False
                            objReceiptController.Update(objReceipt)
                        Next
                    End If
                    Return

                End If

                If amount <> String.Empty Then

                    Dim objOrderController As New OrderController
                    Dim objOrder As OrderInfo = objOrderController.Get(orderID)

                    If Not (objOrder Is Nothing) Then

                        Dim objReceiptsProcessed As New ArrayList

                        Dim objOrderItemController As New OrderItemController
                        Dim objOrderItems As ArrayList = objOrderItemController.List(objOrder.OrderID)

                        For Each objOrderItem As OrderItemInfo In objOrderItems

                            Dim objReceiptController As New ReceiptController
                            Dim objReceipts As ArrayList = objReceiptController.List(objOrder.PortalID, objOrder.ModuleID, userID, Null.NullString)

                            Dim isUpdate As Boolean = False
                            Dim objReceipt As New ReceiptInfo

                            For Each objRec As ReceiptInfo In objReceipts
                                If (objRec.Processor = "PayPal" And objRec.ProcessorTxID = transactionID) Then
                                    objRec.Status = status
                                    objReceiptController.Update(objRec)
                                    objReceipt = objRec
                                    isUpdate = True
                                End If
                            Next

                            Dim objPlanController As New PlanController
                            Dim objPlan As PlanInfo = objPlanController.Get(objOrderItem.PlanID)

                            If (isUpdate = False) Then

                                If (status.ToLower() = "completed" And (txn_type.ToLower() = "web_accept" Or txn_type.ToLower() = "send_money" Or txn_type.ToLower() = "subscr_payment")) Then

                                    objReceipt.PortalID = objOrder.PortalID
                                    objReceipt.ModuleID = objOrder.ModuleID
                                    objReceipt.UserID = userID
                                    objReceipt.Status = status
                                    objReceipt.DateCreated = DateTime.Now
                                    objReceipt.Name = objPlan.Name
                                    objReceipt.Description = objPlan.Description
                                    objReceipt.ServiceFee = objOrderItem.ServiceFee
                                    objReceipt.BillingPeriod = objPlan.BillingPeriod
                                    objReceipt.BillingFrequency = objPlan.BillingFrequency
                                    objReceipt.Processor = "PayPal"
                                    objReceipt.ProcessorTxID = transactionID
                                    objReceipt.DateStart = DateTime.Now
                                    objReceipt.AutoRenew = objPlan.AutoRenew

                                    If (objPlan.Currency <> "") Then
                                        objReceipt.Currency = objPlan.Currency
                                    Else
                                        'Dim objModuleController As New ModuleController
                                        'Dim settings As Hashtable = objModuleController.GetModuleSettings(moduleID)
                                        Dim settings As Hashtable = ModuleController.Instance.GetModule(moduleID, -1, False).ModuleSettings

                                        Dim currency As String = ""
                                        If (settings.Contains(Constants.CURRENCY)) Then
                                            currency = settings(Constants.CURRENCY).ToString()
                                        Else
                                            currency = Constants.DEFAULT_CURRENCY
                                        End If

                                        objReceipt.Currency = currency
                                    End If

                                    Dim objUser As UserInfo = UserController.GetUser(objOrder.PortalID, userID, True)
                                    If (objUser IsNot Nothing) Then
                                        objReceipt.DisplayName = objUser.DisplayName
                                        objReceipt.FirstName = objUser.FirstName
                                        objReceipt.LastName = objUser.LastName
                                    End If

                                    Dim objRoleController As New RoleController
                                    Dim objUserRole As UserRoleInfo = objRoleController.GetUserRole(objReceipt.PortalID, objReceipt.UserID, objPlan.RoleID)

                                    If Not (objUserRole Is Nothing) Then
                                        If (objUserRole.ExpiryDate <> Null.NullDate) Then
                                            If (objUserRole.ExpiryDate > DateTime.Now) Then
                                                objReceipt.DateStart = objUserRole.ExpiryDate
                                            End If
                                        End If
                                    End If

                                    Select Case objPlan.BillingFrequencyType
                                        Case FrequencyType.OneTimeFee
                                            objReceipt.DateEnd = Null.NullDate
                                            Exit Select
                                        Case FrequencyType.Day
                                            objReceipt.DateEnd = objReceipt.DateStart.AddDays(objPlan.BillingPeriod)
                                            Exit Select
                                        Case FrequencyType.Week
                                            objReceipt.DateEnd = objReceipt.DateStart.AddDays(objPlan.BillingPeriod * 7)
                                            Exit Select
                                        Case FrequencyType.Month
                                            objReceipt.DateEnd = objReceipt.DateStart.AddMonths(objPlan.BillingPeriod)
                                            Exit Select
                                        Case FrequencyType.Year
                                            objReceipt.DateEnd = objReceipt.DateStart.AddYears(objPlan.BillingPeriod)
                                            Exit Select
                                        Case FrequencyType.FixedEndDate
                                            objReceipt.DateEnd = objPlan.EndDate
                                            Exit Select
                                    End Select

                                    objReceipt.AutoRenew = objPlan.AutoRenew

                                    objReceipt.ReceiptID = objReceiptController.Add(objReceipt)
                                    objReceiptsProcessed.Add(objReceipt)

                                End If


                            End If

                            If (objReceipt.Status.ToLower() <> "pending") Then
                                If (isUpdate) Then
                                    Dim ts As TimeSpan = DateTime.Now.Subtract(objReceipt.DateStart)
                                    objReceipt.DateStart = objReceipt.DateStart.Add(ts)
                                    If (objReceipt.DateEnd <> Null.NullDate) Then
                                        objReceipt.DateEnd = objReceipt.DateEnd.Add(ts)
                                    End If
                                    objReceiptController.Update(objReceipt)
                                End If

                                Dim objRoleController As New RoleController
                                objRoleController.AddUserRole(objReceipt.PortalID, objReceipt.UserID, objPlan.RoleID, objReceipt.DateEnd)
                            End If

                        Next

                        If (objReceiptsProcessed.Count > 0) Then
                            SendInvoice(objReceiptsProcessed, moduleID)
                        End If

                    End If

                End If

            End If

        End Sub

        Private Sub SendInvoice(ByVal objReceipts As ArrayList, ByVal moduleID As Integer)

            Dim objUserController As New UserController
            Dim objUser As UserInfo = objUserController.GetUser(CType(objReceipts(0), ReceiptInfo).PortalID, CType(objReceipts(0), ReceiptInfo).UserID)

            If (objUser Is Nothing) Then
                Response.Redirect(NavigateURL, True)
            End If

            Dim objReceipt As ReceiptInfo = CType(objReceipts(0), ReceiptInfo)

            'Dim objModuleController As New ModuleController
            'Dim settings As Hashtable = objModuleController.GetModuleSettings(moduleID)
            Dim settings As Hashtable = ModuleController.Instance.GetModule(moduleID, -1, False).ModuleSettings

            Dim currency As String = ""
            If (settings.Contains(Constants.CURRENCY)) Then
                currency = settings(Constants.CURRENCY).ToString()
            Else
                currency = Constants.DEFAULT_CURRENCY
            End If

            Dim symbol As String = "$"
            Select Case currency
                Case "USD"
                    symbol = "$"

                Case "GBP"
                    symbol = "£"

                Case "EUR"
                    symbol = "€"

                Case "CAD"
                    symbol = "$"

                Case "JPY"
                    symbol = "¥"
            End Select

            Dim invoice As String = ""
            If (settings.Contains(Constants.INVOICE_TEMPLATE)) Then
                invoice = settings(Constants.INVOICE_TEMPLATE).ToString()
            Else
                invoice = Constants.INVOICE_TEMPLATE_DEFAULT
            End If

            Dim subscriptionName As String = ""
            Dim subscriptionPrice As Decimal = Null.NullDecimal
            For Each objReceipt2 As ReceiptInfo In objReceipts
                If (subscriptionName = "") Then
                    subscriptionName = objReceipt2.Name
                Else
                    subscriptionName = subscriptionName & ", " & objReceipt2.Name
                End If

                If (subscriptionPrice = Null.NullDecimal) Then
                    subscriptionPrice = objReceipt2.ServiceFee
                Else
                    subscriptionPrice = subscriptionPrice + objReceipt2.ServiceFee
                End If
            Next

            Dim invoiceSend As Boolean = Constants.INVOICE_SEND_DEFAULT
            If (settings.Contains(Constants.INVOICE_SEND)) Then
                invoiceSend = Convert.ToBoolean(settings(Constants.INVOICE_SEND).ToString())
            End If

            invoice = invoice.Replace("[CITY]", objUser.Profile.City)
            invoice = invoice.Replace("[COUNTRY]", objUser.Profile.Country)
            invoice = invoice.Replace("[DATE]", objReceipt.DateCreated.ToString("MMMM, dd yyyy"))
            invoice = invoice.Replace("[DISPLAYNAME]", objUser.DisplayName)
            If (objReceipt.DateEnd <> Null.NullDate) Then
                invoice = invoice.Replace("[EXPIRYDATE]", objReceipt.DateEnd.ToString("MMMM, dd yyyy"))
            Else
                invoice = invoice.Replace("[EXPIRYDATE]", "")
            End If
            invoice = invoice.Replace("[FIRSTNAME]", objReceipt.FirstName)
            invoice = invoice.Replace("[FULLNAME]", objReceipt.FirstName & " " & objReceipt.LastName)
            invoice = invoice.Replace("[LASTNAME]", objReceipt.LastName)
            invoice = invoice.Replace("[DISPLAYNAME]", objReceipt.DisplayName)
            invoice = invoice.Replace("[PORTALNAME]", Me.PortalSettings.PortalName)
            invoice = invoice.Replace("[POSTALCODE]", objUser.Profile.PostalCode)
            invoice = invoice.Replace("[QTY]", "")
            invoice = invoice.Replace("[RECEIPTID]", objReceipt.ReceiptID.ToString())
            invoice = invoice.Replace("[REGION]", objUser.Profile.Region)
            invoice = invoice.Replace("[STATUS]", objReceipt.Status)
            invoice = invoice.Replace("[STREET]", objUser.Profile.Street)
            invoice = invoice.Replace("[SUBSCRIPTIONNAME]", objReceipt.Name)
            invoice = invoice.Replace("[SUBSCRIPTIONDESCRIPTION]", objReceipt.Description)
            invoice = invoice.Replace("[SUBSCRIPTIONPRICE]", symbol & objReceipt.ServiceFee.ToString("##0.00"))
            invoice = invoice.Replace("[USERNAME]", objUser.Username)

            'Dim sendTo As String = objUser.Membership.Email
            Dim sendTo As String = objUser.Email
            Dim sendFrom As String = PortalSettings.Email

            Dim emailSubject As String = PortalSettings.PortalName & " Subscription Invoice"
            Dim emailBody As String = invoice

            If (invoiceSend) Then
                DotNetNuke.Services.Mail.Mail.SendMail(sendFrom, sendTo, "", emailSubject, emailBody, "", "", "", "", "", "")
            End If

        End Sub

#End Region

    End Class
End Namespace