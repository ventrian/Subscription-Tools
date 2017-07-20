Imports System.Web.UI.WebControls

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Security
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.UI.Utilities

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Partial Public Class EditReceipt
        Inherits ModuleBase

#Region " Private Members "

        Private _receiptID As Integer = Null.NullInteger

#End Region

#Region " Private Methods "

        Private Sub ReadQueryString()

            If Not (Request("ReceiptID") Is Nothing) Then
                _receiptID = Convert.ToInt32(Request("ReceiptID"))
            End If

        End Sub

        Private Sub BindCrumbs()

            Dim crumbs As New ArrayList

            Dim objCrumbMain As New CrumbInfo
            objCrumbMain.Caption = Localization.GetString("Main", LocalResourceFile)
            objCrumbMain.Url = NavigateURL()
            crumbs.Add(objCrumbMain)

            Dim objCrumbEditReceipts As New CrumbInfo
            objCrumbEditReceipts.Caption = Localization.GetString("EditReceipts", LocalResourceFile)
            objCrumbEditReceipts.Url = EditUrl("EditReceipts")
            crumbs.Add(objCrumbEditReceipts)

            Dim objCrumbEditReceipt As New CrumbInfo
            If (_receiptID = Null.NullInteger) Then
                objCrumbEditReceipt.Caption = Localization.GetString("AddReceipt", LocalResourceFile)
            Else
                objCrumbEditReceipt.Caption = Localization.GetString("EditReceipt", LocalResourceFile)
            End If
            objCrumbEditReceipt.Url = EditUrl("ReceiptID", _receiptID.ToString(), "EditReceipt")
            crumbs.Add(objCrumbEditReceipt)

            rptBreadCrumbs.DataSource = crumbs
            rptBreadCrumbs.DataBind()

        End Sub

        Private Sub BindPlans()

            Dim objPlanController As New PlanController

            Dim objPlans As ArrayList = objPlanController.List(Me.PortalId, Me.ModuleId)

            For Each objPlan As PlanInfo In objPlans

                If (objPlan.IsActive) Then
                    Dim symbol As String = "$"
                    Select Case Me.Currency
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

                    If (objPlan.BillingFrequencyType = FrequencyType.OneTimeFee) Then
                        _details = Localization.GetString("PermanentAccess", LocalResourceFile)
                        If (_details.Length > 0) Then
                            _details = _details.Replace("[FEE]", symbol & objPlan.ServiceFee.ToString("##0.00"))
                        End If
                    Else
                        If (objPlan.BillingFrequencyType = FrequencyType.FixedEndDate) Then
                            _details = Localization.GetString("FixedEndDate", LocalResourceFile)
                            If (_details.Length > 0) Then
                                _details = _details.Replace("[FEE]", symbol & objPlan.ServiceFee.ToString("##0.00"))
                                _details = _details.Replace("[PERIOD]", objPlan.EndDate.ToString("MMM, dd yyyy"))
                            End If
                        Else
                            If (objPlan.BillingPeriod = 1) Then
                                _details = Localization.GetString("FrequencyAccessSingular", LocalResourceFile)
                            Else
                                _details = Localization.GetString("FrequencyAccessPlural", LocalResourceFile)
                            End If
                            If (_details.Length > 0) Then
                                _details = _details.Replace("[FEE]", symbol & objPlan.ServiceFee.ToString("##0.00"))
                                _details = _details.Replace("[PERIOD]", objPlan.BillingPeriod.ToString())
                                _details = _details.Replace("[FREQUENCY]", Localization.GetString(objPlan.BillingFrequencyType.ToString(), Me.LocalResourceFile))
                            End If
                        End If
                    End If

                    drpSelectPlan.Items.Add(New ListItem(objPlan.Name & " - " & _details, objPlan.PlanID.ToString()))
                End If
            Next

        End Sub

        Private Sub BindReceipt()

            If (_receiptID = Null.NullInteger) Then

                txtProcessor.Text = Localization.GetString("Manual", Me.LocalResourceFile)

                pnlAddReceipt.Visible = True
                pnlDetail.Visible = False
                cmdDelete.Visible = False

            Else

                pnlAddReceipt.Visible = False
                pnlDetail.Visible = True
                cmdUpdate.Visible = False
                cmdDelete.Visible = True
                cmdDelete.Attributes.Add("onClick", "javascript:return confirm('" & Localization.GetString("Confirmation", LocalResourceFile) & "');")

                Dim objReceiptController As New ReceiptController
                Dim objReceipt As ReceiptInfo = objReceiptController.Get(_receiptID)

                If Not (objReceipt Is Nothing) Then
                    lblUserName.Text = objReceipt.UserName
                    lblPlan.Text = objReceipt.Name
                    lblCreateDate.Text = objReceipt.DateCreated.ToShortDateString()
                    lblStartDate.Text = objReceipt.DateStart.ToShortDateString()
                    If (objReceipt.DateEnd <> Null.NullDate) Then
                        lblEndDate.Text = objReceipt.DateEnd.ToShortDateString()
                    End If
                    lblProcessor.Text = objReceipt.Processor
                    If (objReceipt.Processor = "PayPal") Then
                        lnkTransactionID.Text = objReceipt.ProcessorTxID
                        lnkTransactionID.Target = "_blank"
                        If (Me.UseLiveProcessor) Then
                            lnkTransactionID.NavigateUrl = "https://www.paypal.com/vst/id=" & objReceipt.ProcessorTxID
                        Else
                            lnkTransactionID.NavigateUrl = "https://www.sandbox.paypal.com/vst/id=" & objReceipt.ProcessorTxID
                        End If
                        lnkTransactionID.Visible = True
                    Else
                        lblTransactionID.Text = objReceipt.ProcessorTxID
                        lblTransactionID.Visible = True
                    End If
                    lblServiceFee.Text = objReceipt.ServiceFee.ToString("##0.00")
                Else
                    Response.Redirect(NavigateURL("EditReceipt"), True)
                End If

            End If

        End Sub

#End Region

#Region " Event Handlers "

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try

                ReadQueryString()
                BindCrumbs()
                cmdStartDate.NavigateUrl = DotNetNuke.Common.Utilities.Calendar.InvokePopupCal(txtStartDate)

                If (IsPostBack = False) Then

                    BindPlans()
                    BindReceipt()

                End If

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click

            If (Page.IsValid) Then

                Dim objReceipt As New ReceiptInfo

                objReceipt.ModuleID = Me.ModuleId
                objReceipt.PortalID = Me.PortalId
                objReceipt.Status = "Complete"
                objReceipt.Processor = txtProcessor.Text
                objReceipt.ProcessorTxID = txtTransactionID.Text

                Dim objPlanController As New PlanController
                Dim objPlan As PlanInfo = objPlanController.Get(Convert.ToInt32(drpSelectPlan.SelectedValue))

                objReceipt.BillingFrequency = objPlan.BillingFrequency
                objReceipt.BillingPeriod = objPlan.BillingPeriod
                objReceipt.ServiceFee = objPlan.ServiceFee
                objReceipt.Name = objPlan.Name

                objReceipt.DateCreated = DateTime.Now
                If (txtStartDate.Text <> "") Then
                    objReceipt.DateStart = Convert.ToDateTime(txtStartDate.Text)
                Else
                    objReceipt.DateStart = DateTime.Now
                End If

                Dim objUser As UserInfo = UserController.GetUserByName(Me.PortalId, txtUserName.Text)

                objReceipt.UserID = objUser.UserID

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

                Dim objReceiptController As New ReceiptController
                objReceiptController.Add(objReceipt)

                If (chkAddToRole.Checked) Then
                    objRoleController.AddUserRole(objReceipt.PortalID, objReceipt.UserID, objPlan.RoleID, objReceipt.DateEnd)
                End If

                Response.Redirect(EditUrl("EditReceipts"), True)

            End If

        End Sub

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

            Response.Redirect(EditUrl("EditReceipts"), True)

        End Sub

        Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

            Dim objReceiptController As New ReceiptController
            objReceiptController.Delete(_receiptID)

            Response.Redirect(EditUrl("EditReceipts"), True)

        End Sub

        Private Sub valUserNameExists_ServerValidate(ByVal source As System.Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles valUserNameExists.ServerValidate

            Dim objUser As UserInfo = UserController.GetUserByName(PortalId, txtUserName.Text)

            If (objUser Is Nothing) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If

        End Sub

#End Region

    End Class

End Namespace