Imports System.Web
Imports System.Web.Util
Imports System.Web.UI.WebControls

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Security
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.Services.Localization

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Partial Public Class SignupPlan
        Inherits ModuleBase

#Region " Private Methods "

        Private Sub BindIntro()

            litContent.Text = Server.HtmlDecode(Me.IntroText)

        End Sub

        Private Sub BindPlans()

            Dim objPlanController As New PlanController

            Dim objPlans As ArrayList = objPlanController.List(Me.PortalId, Me.ModuleId)

            For Each objPlan As PlanInfo In objPlans

                If (objPlan.IsActive) Then
                    Dim symbol As String = "$"

                    Dim currency As String = Me.Currency
                    If (objPlan.Currency <> "") Then
                        currency = objPlan.Currency
                    End If

                    Select Case currency.ToUpper()
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

                    Dim _details As String = ""
                    Dim serviceFee As Decimal = objPlan.ServiceFee

                    If (Settings.Contains("ST-" & objPlan.PlanID.ToString())) Then
                        Dim pricing As String = Settings("ST-" & objPlan.PlanID.ToString()).ToString()
                        For Each p As String In pricing.Split(";"c)
                            If (p.Split("-"c).Length = 2) Then
                                If (UserInfo.IsInRole(p.Split("-"c)(0))) Then
                                    Dim feeToCompare As Decimal = Convert.ToDecimal(p.Split("-"c)(1))
                                    If (feeToCompare < serviceFee) Then
                                        serviceFee = feeToCompare
                                    End If
                                End If
                            End If
                        Next
                    End If

                    If (objPlan.BillingFrequencyType = FrequencyType.OneTimeFee) Then
                        _details = Localization.GetString("PermanentAccess", LocalResourceFile)
                        If (_details.Length > 0) Then
                            _details = _details.Replace("[FEE]", symbol & serviceFee.ToString("##0.00"))
                        End If
                    Else

                        If (objPlan.BillingFrequencyType = FrequencyType.FixedEndDate) Then
                            _details = Localization.GetString("FixedEndDate", LocalResourceFile)
                            If (_details.Length > 0) Then
                                _details = _details.Replace("[FEE]", symbol & serviceFee.ToString("##0.00"))
                                _details = _details.Replace("[PERIOD]", objPlan.EndDate.ToString("MMM, dd yyyy"))
                            End If
                        Else
                            If (objPlan.BillingPeriod = 1) Then
                                _details = Localization.GetString("FrequencyAccessSingular", LocalResourceFile)
                            Else
                                _details = Localization.GetString("FrequencyAccessPlural", LocalResourceFile)
                            End If
                            If (_details.Length > 0) Then
                                _details = _details.Replace("[FEE]", symbol & serviceFee.ToString("##0.00"))
                                _details = _details.Replace("[PERIOD]", objPlan.BillingPeriod.ToString())
                                _details = _details.Replace("[FREQUENCY]", Localization.GetString(objPlan.BillingFrequencyType.ToString(), Me.LocalResourceFile))
                            End If
                        End If
                    End If

                    If (Me.AllowMultipleSubscriptions) Then
                        valPlansSelected.Enabled = True
                        lstPlansMultiple.Items.Add(New ListItem(objPlan.Name & " - " & _details, objPlan.PlanID.ToString()))
                    Else
                        valPlansSelected.Enabled = False
                        lstPlans.Items.Add(New ListItem(objPlan.Name & " - " & _details, objPlan.PlanID.ToString()))
                    End If
                End If
            Next

            If (lstPlans.Items.Count = 1) Then
                lstPlans.Items(0).Selected = True
            End If

            If (lstPlans.Items.Count = 0 And lstPlansMultiple.Items.Count = 0) Then
                pnlPlans.Visible = False
                lblNoPlans.Visible = True
            End If

        End Sub

        Private Sub BindProcessors()

            For Each value As Integer In System.Enum.GetValues(GetType(PaymentType))
                Dim li As New ListItem
                li.Value = value.ToString()
                li.Text = System.Enum.GetName(GetType(PaymentType), value)
                lstProcessors.Items.Add(li)
            Next

            If (lstProcessors.Items.Count = 1) Then
                lstProcessors.Items(0).Selected = True
            End If

        End Sub

        Private Sub SetVisibility()

            lstPlans.Visible = Not Me.AllowMultipleSubscriptions
            lstPlansMultiple.Visible = Me.AllowMultipleSubscriptions

            valPlans.Enabled = Not Me.AllowMultipleSubscriptions

        End Sub

#End Region

#Region " Event Handlers "

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            SetVisibility()

            If (IsPostBack = False) Then
                BindIntro()
                BindPlans()
                BindProcessors()
            End If

        End Sub

        Private Sub cmdSubscribe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubscribe.Click

            If (Page.IsValid) Then

                Dim objOrder As New OrderInfo

                objOrder.ModuleID = Me.ModuleId
                objOrder.PortalID = Me.PortalId
                objOrder.UserID = Me.UserId

                Dim objOrderController As New OrderController
                Dim objOrderItemController As New OrderItemController

                objOrder.OrderID = objOrderController.Add(objOrder)

                Dim objPlanController As New PlanController
                Dim subscription As String = ""
                Dim serviceFee As Decimal = 0

                Dim autoRenew As Boolean = False
                Dim frequency As String = ""

                Dim currency As String = ""

                If (Me.AllowMultipleSubscriptions) Then

                    For Each li As ListItem In lstPlansMultiple.Items
                        If (li.Selected) Then
                            Dim objOrderItem As New OrderItemInfo

                            objOrderItem.OrderID = objOrder.OrderID
                            objOrderItem.PlanID = Convert.ToInt32(li.Value)

                            Dim objPlan As PlanInfo = objPlanController.Get(objOrderItem.PlanID)

                            If Not (objPlan Is Nothing) Then
                                If (subscription = "") Then
                                    subscription = objPlan.Name
                                Else
                                    subscription = subscription & ", " & objPlan.Name
                                End If

                                If (Settings.Contains("ST-" & objPlan.PlanID.ToString())) Then
                                    Dim pricing As String = Settings("ST-" & objPlan.PlanID.ToString()).ToString()
                                    For Each p As String In pricing.Split(";"c)
                                        If (p.Split("-"c).Length = 2) Then
                                            If (UserInfo.IsInRole(p.Split("-"c)(0))) Then
                                                Dim feeToCompare As Decimal = Convert.ToDecimal(p.Split("-"c)(1))
                                                If (feeToCompare < objPlan.ServiceFee) Then
                                                    objPlan.ServiceFee = feeToCompare
                                                End If
                                            End If
                                        End If
                                    Next
                                End If

                                serviceFee = serviceFee + objPlan.ServiceFee
                                objOrderItem.ServiceFee = serviceFee
                                currency = objPlan.Currency

                                objOrderItemController.Add(objOrderItem)
                            End If

                        End If
                    Next

                Else
                    Dim objOrderItem As New OrderItemInfo

                    objOrderItem.OrderID = objOrder.OrderID
                    objOrderItem.PlanID = Convert.ToInt32(lstPlans.SelectedValue)

                    Dim objPlan As PlanInfo = objPlanController.Get(objOrderItem.PlanID)

                    If Not (objPlan Is Nothing) Then
                        subscription = objPlan.Name

                        If (Settings.Contains("ST-" & objPlan.PlanID.ToString())) Then
                            Dim pricing As String = Settings("ST-" & objPlan.PlanID.ToString()).ToString()
                            For Each p As String In pricing.Split(";"c)
                                If (p.Split("-"c).Length = 2) Then
                                    If (UserInfo.IsInRole(p.Split("-"c)(0))) Then
                                        Dim feeToCompare As Decimal = Convert.ToDecimal(p.Split("-"c)(1))
                                        If (feeToCompare < objPlan.ServiceFee) Then
                                            objPlan.ServiceFee = feeToCompare
                                        End If
                                    End If
                                End If
                            Next
                        End If

                        serviceFee = serviceFee + objPlan.ServiceFee
                        objOrderItem.ServiceFee = serviceFee
                        currency = objPlan.Currency
                        objOrderItemController.Add(objOrderItem)

                        autoRenew = objPlan.AutoRenew
                        Select Case objPlan.BillingFrequencyType

                            Case FrequencyType.Day
                                frequency = "D"
                                Exit Select

                            Case FrequencyType.Week
                                frequency = "W"
                                Exit Select

                            Case FrequencyType.Month
                                frequency = "M"
                                Exit Select

                            Case FrequencyType.Year
                                frequency = "Y"
                                Exit Select

                        End Select
                    End If

                End If

                Dim strPayPalURL As String = ""

                If (Me.UseLiveProcessor) Then
                    strPayPalURL += "https://www.paypal.com/cgi-bin/webscr?business=" & HTTPPOSTEncode(Me.ProcessorUserID)
                Else
                    strPayPalURL += "https://www.sandbox.paypal.com/cgi-bin/webscr?business=" & HTTPPOSTEncode(Me.ProcessorUserID)
                End If

                If (autoRenew) Then
                    strPayPalURL += "&cmd=_xclick-subscriptions"
                    strPayPalURL += "&src=1"
                Else
                    strPayPalURL += "&cmd=_xclick"
                End If

                strPayPalURL += "&item_name=" & HTTPPOSTEncode(PortalSettings.PortalName & " - " & subscription)
                strPayPalURL += "&item_number=" & HTTPPOSTEncode(objOrder.OrderID.ToString)
                strPayPalURL += "&invoice=" & Me.UserInfo.Username & " " & HTTPPOSTEncode(objOrder.OrderID.ToString)

                If (autoRenew) Then
                    Dim enFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US")
                    Dim strService As String = String.Format(enFormat.NumberFormat, "{0:#####0.00}", serviceFee)

                    strPayPalURL += "&a3=" & strService ' price
                    strPayPalURL += "&p3=1" ' Payment cycle
                    strPayPalURL += "&t3=" & frequency ' Frequency cycle
                Else
                    If (Me.RequireShippingAddress) Then
                        strPayPalURL += "&no_shipping=2"
                    End If
                    strPayPalURL += "&no_note=1"
                    strPayPalURL += "&quantity=1"

                    Dim enFormat As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US")
                    Dim strService As String = String.Format(enFormat.NumberFormat, "{0:#####0.00}", serviceFee)

                    strPayPalURL += "&amount=" & strService
                End If

                If (currency = "") Then
                    currency = Me.Currency
                End If
                strPayPalURL += "&currency_code=" & HTTPPOSTEncode(currency)

                strPayPalURL += "&custom=" & HTTPPOSTEncode(Me.UserId.ToString & "," & Me.UseLiveProcessor.ToString() & "," & Me.ModuleId.ToString())

                Dim returnUrl As String = NavigateURL(Me.TabId, "", "signupType=Details", "Pending=True", "Clear=1")
                If (returnUrl.ToLower().StartsWith("http://") Or returnUrl.ToLower().StartsWith("https://")) = False Then
                    returnUrl = AddHTTP(System.Web.HttpContext.Current.Request.Url.Host & returnUrl)
                End If

                Dim cancelUrl As String = NavigateURL(Me.TabId, "", "signupType=Details")
                If (cancelUrl.ToLower().StartsWith("http://") Or cancelUrl.ToLower().StartsWith("https://")) = False Then
                    cancelUrl = AddHTTP(System.Web.HttpContext.Current.Request.Url.Host & cancelUrl)
                End If

                strPayPalURL += "&return=" & HTTPPOSTEncode(returnUrl)
                strPayPalURL += "&cancel_return=" & HTTPPOSTEncode(cancelUrl)

                strPayPalURL += "&notify_url=" & HTTPPOSTEncode(AddHTTP(GetDomainName(Request)) & "/DesktopModules/SubscriptionSignup/Tools/IPNHandler.aspx")
                strPayPalURL += "&sra=1"       ' reattempt on failure

                Dim strAcceptLanguage As String
                Dim strSetLanguage As String

                'A language tag is a string that begins with the two-character 
                'language code that identifies the language. If necessary to
                'distinguish regional differences in language, the language tag
                'may also contain a country code, which is another two-character
                'string. The language code and country code are separated by
                'a hyphen. For example, the language tag used to identify the
                'British English locale is "en-gb". I'm guesing that "gb" stands
                'for "Great Britain".

                strAcceptLanguage = Mid(Request.ServerVariables("HTTP_ACCEPT_LANGUAGE"), 4, 2)

                'If there is no Language available in the browser we will set default to 'us'

                If Len(strAcceptLanguage) > 0 Then
                    strSetLanguage = strAcceptLanguage
                Else
                    strSetLanguage = "us"
                End If

                ' PayPal only handles uppercase chars now and a limited subset.
                ' "US" = English
                ' "FR" = French
                ' "DE" = German
                ' "IT" = Italian
                ' "ES" = Spanish
                ' "CN" = Chinese
                ' "AU" = Australian
                ' "GB" = United Kingdom
                ' "JP" = Japanese 

                Select Case strSetLanguage.ToLower()
                    Case "us"
                        strSetLanguage = "US"
                    Case "fr"
                        strSetLanguage = "FR"
                    Case "de"
                        strSetLanguage = "DE"
                    Case "it"
                        strSetLanguage = "IT"
                    Case "es"
                        strSetLanguage = "ES"
                    Case "cn"
                        strSetLanguage = "CN"
                    Case "au"
                        strSetLanguage = "AU"
                    Case "gb"
                        strSetLanguage = "GB"
                    Case "jp"
                        strSetLanguage = "JP"
                    Case Else
                        strSetLanguage = ""
                End Select

                If (strSetLanguage <> "") Then
                    strPayPalURL += "&lc=" & strSetLanguage
                End If

                Redirect(strPayPalURL)

            End If

        End Sub

        Public Sub Redirect(ByVal url As String)

            If (url Is Nothing) Then
                Throw New ArgumentNullException("Url is Nothing!")
            End If

            If (url.IndexOf(ChrW(10)) >= 0) Then
                Throw New ArgumentException("Cannot Redirect To Newline")
            End If

            Response.Clear()

            Response.StatusCode = 302
            Response.RedirectLocation = url

            Response.Write("<html><head><title>Object moved</title></head><body>" & ChrW(13) & ChrW(10))
            Response.Write(("<h2>Object moved to <a href='" & HttpUtility.HtmlEncode(url) & "'>here</a>.</h2>" & ChrW(13) & ChrW(10)))
            Response.Write("</body></html>" & ChrW(13) & ChrW(10))

            Response.End()

        End Sub

        Private Sub valPlansSelected_ServerValidate(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles valPlansSelected.ServerValidate

            For Each item As ListItem In lstPlansMultiple.Items
                If (item.Selected) Then
                    args.IsValid = True
                    Return
                End If
            Next

            args.IsValid = False
        End Sub

#End Region

    End Class

End Namespace