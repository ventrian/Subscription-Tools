Imports System.Web.UI.WebControls

Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Partial Public Class EditReceipts
        Inherits ModuleBase

#Region " Private Members "

        Dim _currentPage As Integer = 1
        Dim _pageRecords As Integer = Null.NullInteger

        Dim _receipts As ArrayList

#End Region

#Region " Private Methods "

        Private Sub ReadQueryString()

            If Not (Request("CurrentPage") Is Nothing) Then
                _currentPage = Convert.ToInt32(Request("CurrentPage"))
            End If

            If Not (Request("PageRecords") Is Nothing) Then
                _pageRecords = Convert.ToInt32(Request("PageRecords"))
            End If

            If (_pageRecords <> Null.NullInteger) Then
                If Not (drpRecordsPerPage.Items.FindByValue(_pageRecords.ToString()) Is Nothing) Then
                    drpRecordsPerPage.SelectedValue = _pageRecords.ToString()
                End If
            End If

            If Not (Request("UserName") Is Nothing) Then
                txtUserName.Text = Server.UrlDecode(Request("UserName").Trim())
            End If

        End Sub

        Private Sub BindCrumbs()

            Dim crumbs As New ArrayList

            Dim objCrumbMain As New CrumbInfo
            objCrumbMain.Caption = Localization.GetString("Main", LocalResourceFile)
            objCrumbMain.Url = NavigateURL()
            crumbs.Add(objCrumbMain)

            Dim objEditReceipts As New CrumbInfo
            objEditReceipts.Caption = Localization.GetString("EditReceipts", LocalResourceFile)
            objEditReceipts.Url = EditUrl("EditReceipts")
            crumbs.Add(objEditReceipts)

            rptBreadCrumbs.DataSource = crumbs
            rptBreadCrumbs.DataBind()

        End Sub

        Private Sub BindReceipts()

            Dim PageSize As Integer = Convert.ToInt32(drpRecordsPerPage.SelectedItem.Value)

            Dim objReceiptController As New ReceiptController

            Localization.LocalizeDataGrid(grdReceipts, Me.LocalResourceFile)

            _receipts = objReceiptController.List(Me.PortalId, Me.ModuleId, Null.NullInteger, txtUserName.Text.Trim())

            Dim objPagedDataSource As New PagedDataSource

            objPagedDataSource.AllowPaging = True
            objPagedDataSource.DataSource = _receipts
            If (_pageRecords = Null.NullInteger) Then
                objPagedDataSource.PageSize = PageSize
            Else
                objPagedDataSource.PageSize = _pageRecords
            End If
            objPagedDataSource.CurrentPageIndex = _currentPage - 1

            If (_receipts.Count > 0) Then

                grdReceipts.DataSource = objPagedDataSource
                grdReceipts.DataBind()

                grdReceipts.Visible = True
                lblNoReceipts.Visible = False
                ctlPagingControl.Visible = True

                ctlPagingControl.Visible = True
                ctlPagingControl.TotalRecords = _receipts.Count
                ctlPagingControl.PageSize = PageSize
                ctlPagingControl.CurrentPage = _currentPage

                ctlPagingControl.QuerystringParams = "ctl=EditReceipts&mid=" & Me.ModuleId.ToString()

                If (Request("UserName") <> "") Then
                    ctlPagingControl.QuerystringParams = ctlPagingControl.QuerystringParams & "&UserName=" + Server.UrlEncode(Request("UserName"))
                End If

                If (drpRecordsPerPage.SelectedIndex <> 0) Then
                    ctlPagingControl.QuerystringParams = ctlPagingControl.QuerystringParams & "&PageRecords=" & drpRecordsPerPage.SelectedValue
                End If

                ctlPagingControl.TabID = TabId
            Else
                grdReceipts.Visible = False
                lblNoReceipts.Visible = True
                ctlPagingControl.Visible = False
            End If

        End Sub

#End Region

#Region " Protected Methods "

        Protected Function GetDateEnd(ByVal obj As Object) As String

            Dim objReceipt As ReceiptInfo = CType(obj, ReceiptInfo)

            If Not (objReceipt Is Nothing) Then
                If (objReceipt.DateEnd <> Null.NullDate) Then
                    Return objReceipt.DateEnd.ToShortDateString()
                End If
            End If

            Return ""

        End Function

        Protected Function GetServiceFee(ByVal obj As Object) As String

            Dim objReceipt As ReceiptInfo = CType(obj, ReceiptInfo)

            If Not (objReceipt Is Nothing) Then

                Dim currency As String = Me.Currency
                If (objReceipt.Currency <> "") Then
                    currency = objReceipt.Currency
                End If

                Dim symbol As String = "$"
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
            End If

            Return ""

        End Function

#End Region

#Region " Event Handlers "

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try

                If (Page.IsPostBack = False) Then
                    ReadQueryString()
                    BindCrumbs()
                    BindReceipts()
                End If

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub drpRecordsPerPage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles drpRecordsPerPage.SelectedIndexChanged

            Try

                Response.Redirect(EditUrl("UserName", Server.UrlEncode(txtUserName.Text.Trim()), "EditReceipts", "PageRecords=" & drpRecordsPerPage.SelectedValue), True)

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub cmdAddReceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddReceipt.Click

            Try

                Response.Redirect(EditUrl("EditReceipt"), True)

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub cmdReturnToModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReturnToModule.Click

            Try

                Response.Redirect(NavigateURL(), True)

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click

            Try

                Response.Redirect(EditUrl("UserName", Server.UrlEncode(txtUserName.Text.Trim()), "EditReceipts"), True)

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

            Try

                Response.Redirect(EditUrl("EditReceipts"), True)

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

#End Region

    End Class

End Namespace