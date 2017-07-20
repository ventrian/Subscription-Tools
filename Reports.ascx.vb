Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Security
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.UI.Utilities

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Partial Public Class Reports
        Inherits ModuleBase

#Region " Private Methods "

        Private Sub BindCrumbs()

            Dim crumbs As New ArrayList

            Dim objCrumbMain As New CrumbInfo
            objCrumbMain.Caption = Localization.GetString("Main", LocalResourceFile)
            objCrumbMain.Url = NavigateURL()
            crumbs.Add(objCrumbMain)

            Dim objEditReceipts As New CrumbInfo
            objEditReceipts.Caption = Localization.GetString("ViewReports", LocalResourceFile)
            objEditReceipts.Url = EditUrl("ViewReports")
            crumbs.Add(objEditReceipts)

            rptBreadCrumbs.DataSource = crumbs
            rptBreadCrumbs.DataBind()

        End Sub

        Private Sub BindReportTypes()

            For Each value As Integer In System.Enum.GetValues(GetType(ReportType))
                Dim li As New ListItem
                li.Value = value.ToString()
                li.Text = Localization.GetString(System.Enum.GetName(GetType(ReportType), value), Me.LocalResourceFile)
                cboReportType.Items.Add(li)
            Next

        End Sub

        Private Sub BindCriteria()

            Dim objReportType As ReportType = CType(System.Enum.Parse(GetType(ReportType), cboReportType.SelectedValue), ReportType)

            If (objReportType = ReportType.SubscriptionsActive Or objReportType = ReportType.SubscriptionsExpiring) Then
                trStartDate.Visible = False
                trEndDate.Visible = False
                txtStartDate.Text = ""
                txtEndDate.Text = ""
            Else
                trStartDate.Visible = True
                trEndDate.Visible = True
            End If

        End Sub

        Public Shared Sub OutputHtmlTextWriterToExcel(ByVal fileName As String, ByVal htmlWriter As HtmlTextWriter)
            If Not fileName.EndsWith(".xls") Then
                fileName = String.Concat(fileName, ".xls")
            End If

            System.Web.HttpContext.Current.Response.Clear()
            System.Web.HttpContext.Current.Response.Charset = ""
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" & fileName)
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"

            'output the html
            System.Web.HttpContext.Current.Response.Write(htmlWriter.InnerWriter.ToString)
            System.Web.HttpContext.Current.Response.End()

        End Sub

#End Region

#Region " Event Handlers "

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try

                cmdStartCalendar.NavigateUrl = Common.Utilities.Calendar.InvokePopupCal(txtStartDate)
                cmdEndCalendar.NavigateUrl = Common.Utilities.Calendar.InvokePopupCal(txtEndDate)

                BindCrumbs()

                If (IsPostBack = False) Then

                    BindReportTypes()
                    BindCriteria()

                End If

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub cmdDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDisplay.Click

            Try

                Dim objReportType As ReportType = CType(System.Enum.Parse(GetType(ReportType), cboReportType.SelectedValue), ReportType)

                Dim objReportController As New ReportController

                Dim dateStart As DateTime = Null.NullDate
                Dim dateEnd As DateTime = Null.NullDate

                If (txtStartDate.Text.Length > 0) Then
                    dateStart = Convert.ToDateTime(txtStartDate.Text)
                End If

                If (txtEndDate.Text.Length > 0) Then
                    dateEnd = Convert.ToDateTime(txtEndDate.Text)
                End If

                Dim dr As IDataReader = objReportController.List(PortalId, ModuleId, objReportType, dateStart, dateEnd)
                grdLog.DataSource = dr
                grdLog.DataBind()
                dr.Close()

                btnExport.Visible = True

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

            Try

                Response.Redirect(NavigateURL(), True)

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub cboReportType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboReportType.SelectedIndexChanged

            BindCriteria()

        End Sub

        Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click

            Dim objReportType As ReportType = CType(System.Enum.Parse(GetType(ReportType), cboReportType.SelectedValue), ReportType)

            Me.grdLog.Visible = True
            Dim htmlWriter As New System.Web.UI.HtmlTextWriter(New System.IO.StringWriter)

            Me.grdLog.RenderControl(htmlWriter)
            Me.grdLog.Visible = False

            OutputHtmlTextWriterToExcel(objReportType.ToString() & ".xls", htmlWriter)

        End Sub

#End Region

    End Class

End Namespace