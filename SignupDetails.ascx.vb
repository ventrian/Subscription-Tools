Imports System.Web.UI.WebControls

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.Security
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Partial Public Class SignupDetails
        Inherits ModuleBase

#Region " Protected Methods "

        Protected Function FormatAmount(ByVal obj As Object) As String

            Dim objReceipt As ReceiptInfo = CType(obj, ReceiptInfo)

            Dim symbol As String = "$"

            Dim currency As String = Me.Currency
            If (objReceipt.Currency <> "") Then
                currency = objReceipt.Currency
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

            Return symbol & objReceipt.ServiceFee.ToString("##0.00")

        End Function

        Protected Function FormatDetails(ByVal obj As Object) As String

            Dim objReceipt As ReceiptInfo = CType(obj, ReceiptInfo)

            Return objReceipt.Details

        End Function

        Protected Function FormatEndDate(ByVal obj As Object) As String

            Dim objReceipt As ReceiptInfo = CType(obj, ReceiptInfo)

            If Not (objReceipt Is Nothing) Then
                If (objReceipt.DateEnd = Null.NullDate) Then
                    Return ""
                Else
                    Return objReceipt.DateEnd.ToShortDateString()
                End If
            End If

            Return ""

        End Function

#End Region

#Region " Private Methods "

        Private Sub BindReceipts()

            Dim objReceiptController As New ReceiptController
            Dim objReceipts As ArrayList = objReceiptController.List(Me.PortalId, Me.ModuleId, Me.UserId, Null.NullString)


            For Each objReceiptItem As ReceiptInfo In objReceipts
                If (objReceiptItem.AutoRenew) Then

                End If
            Next

            grdReceipts.DataSource = objReceipts
            grdReceipts.DataBind()

            grdReceipts.Columns(6).Visible = Me.InvoiceSend

            If (objReceipts.Count > 0) Then

                Dim objReceipt As ReceiptInfo
                Dim subscribedTo As String = ""
                For Each objReceipt In objReceipts
                    If ((objReceipt.DateEnd > DateTime.Now Or objReceipt.DateEnd = Null.NullDate)) Then
                        If (subscribedTo = "") Then
                            subscribedTo = objReceipt.Name
                        Else
                            subscribedTo = subscribedTo & ", " & objReceipt.Name
                        End If
                    End If
                Next

                objReceipt = CType(objReceipts(0), ReceiptInfo)
                If ((objReceipt.DateEnd > DateTime.Now Or objReceipt.DateEnd = Null.NullDate)) Then
                    If (objReceipt.Status.ToLower() = "pending") Then
                        lblCurrent.Text = Localization.GetString("SubscribedTo", Me.LocalResourceFile).Replace("[PLANS]", subscribedTo) & " " & Localization.GetString("Pending", Me.LocalResourceFile)
                    Else
                        If (objReceipt.DateEnd = Null.NullDate) Then
                            lblCurrent.Text = "You are currently subscribed to " & subscribedTo & ". Your access never expires."
                        Else
                            If Me.AllowMultipleSubscriptions Then
                                lblCurrent.Text = Localization.GetString("SubscribedTo", Me.LocalResourceFile).Replace("[PLANS]", subscribedTo) & " " & Localization.GetString("Extend", Me.LocalResourceFile).Replace("[LINK]", NavigateURL(Me.TabId, Null.NullString, "signupType=plan"))
                            Else
                                lblCurrent.Text = Localization.GetString("SubscribedTo", Me.LocalResourceFile).Replace("[PLANS]", subscribedTo) & " " & Localization.GetString("ExpiresIn", Me.LocalResourceFile).Replace("[DAYS]", (DateDiff(DateInterval.Day, DateTime.Now, objReceipt.DateEnd) + 1).ToString()) & " " & Localization.GetString("Extend", Me.LocalResourceFile).Replace("[LINK]", NavigateURL(Me.TabId, Null.NullString, "signupType=plan"))
                            End If
                        End If
                    End If
                Else
                    lblCurrent.Text = Localization.GetString("Expired", Me.LocalResourceFile).Replace("[PLAN]", objReceipt.Name) & " " & Localization.GetString("Renew", Me.LocalResourceFile).Replace("[LINK]", NavigateURL(Me.TabId, Null.NullString, "signupType=plan"))
                End If
            Else
                If (Request("Pending") = "True") Then
                    lblCurrent.Text = Localization.GetString("Refresh", Me.LocalResourceFile).Replace("[LINK]", Request.RawUrl)
                Else
                    Response.Redirect(NavigateURL(Me.TabId), True)
                End If
            End If

        End Sub

        Private Sub BindReminder()

            Dim objUserSettingController As New UserSettingController


            Dim objUserSetting As UserSettingInfo = objUserSettingController.Get(Me.UserId, Constants.REMINDER_RECEIVE_NOTIFICATIONS)

            If (objUserSetting Is Nothing) Then
                chkReminder.Checked = True
            Else
                chkReminder.Checked = Convert.ToBoolean(objUserSetting.SettingValue)
            End If



        End Sub

#End Region

#Region " Protected Methods "

        Protected Function GetInvoiceUrl(ByVal receiptID As String) As String

            Return NavigateURL(Me.TabId, Null.NullString, "signupType=invoice", "ReceiptID=" & receiptID)

        End Function

#End Region

#Region " Event Handlers "

        Private Sub Page_Initialization(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            Try

                If (Request("Clear") = "1") Then
                    ' Clear Role Cookie
                    Response.Cookies.Remove("portalroles")
                    Response.Redirect(NavigateURL(Me.TabId, "", "signupType=Details", "Pending=True"), True)
                End If

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try

                If (Page.IsPostBack = False) Then
                    BindReceipts()
                    BindReminder()
                End If

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub chkReminder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReminder.CheckedChanged

            Try

                Dim objUserSettingController As New UserSettingController

                Dim objUserSetting As UserSettingInfo = objUserSettingController.Get(Me.UserId, Constants.REMINDER_RECEIVE_NOTIFICATIONS)

                If (objUserSetting Is Nothing) Then
                    objUserSetting = New UserSettingInfo
                    objUserSetting.UserID = Me.UserId
                    objUserSetting.SettingName = Constants.REMINDER_RECEIVE_NOTIFICATIONS
                    objUserSetting.SettingValue = chkReminder.Checked.ToString()
                    objUserSettingController.Add(objUserSetting)
                Else
                    objUserSetting.SettingValue = chkReminder.Checked.ToString()
                    objUserSettingController.Update(objUserSetting)
                End If

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

#End Region

    End Class

End Namespace