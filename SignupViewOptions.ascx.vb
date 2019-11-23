Imports System.Web.UI.WebControls

Imports DotNetNuke.Common
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Security
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Partial Public Class SignupViewOptions
        Inherits ModuleBase

#Region " Private Properties "

        Private ReadOnly Property Introduction() As DotNetNuke.UI.UserControls.TextEditor
            Get
                Return CType(txtIntro, DotNetNuke.UI.UserControls.TextEditor)
            End Get
        End Property

#End Region

#Region " Private Methods "

        Private Sub BindCrumbs()

            Dim crumbs As New ArrayList

            Dim objCrumbMain As New CrumbInfo
            objCrumbMain.Caption = Localization.GetString("Main", LocalResourceFile)
            objCrumbMain.Url = NavigateURL()
            crumbs.Add(objCrumbMain)

            Dim objEditReceipts As New CrumbInfo
            objEditReceipts.Caption = Localization.GetString("ViewOptions", LocalResourceFile)
            objEditReceipts.Url = EditUrl("ViewOptions")
            crumbs.Add(objEditReceipts)

            rptBreadCrumbs.DataSource = crumbs
            rptBreadCrumbs.DataBind()

        End Sub

        Private Sub BindCurrency()

            Dim ctlList As New Lists.ListController
            'Dim colCurrency As Lists.ListEntryInfoCollection = ctlList.GetListEntryInfoCollection("Currency", "")
            Dim colCurrency As IEnumerable(Of DotNetNuke.Common.Lists.ListEntryInfo) = ctlList.GetListEntryInfoItems("Currency", "")

            drpCurrency.DataSource = colCurrency
            drpCurrency.DataBind()

        End Sub

        Private Sub BindSettings()

            chkAllowMultipleSubscriptions.Checked = Me.AllowMultipleSubscriptions
            chkRequireShippingAddress.Checked = Me.RequireShippingAddress
            chkUseLiveProcessor.Checked = Me.UseLiveProcessor
            txtProcessorUserID.Text = Me.ProcessorUserID
            If Not (drpCurrency.Items.FindByValue(Me.Currency) Is Nothing) Then
                drpCurrency.SelectedValue = Me.Currency
            End If
            Introduction.Text = Me.IntroText

            chkSendInvoice.Checked = Me.InvoiceSend
            txtInvoice.Text = Me.InvoiceTemplate

        End Sub

        Private Sub SaveSettings()

            Dim objModuleController As New ModuleController

            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.ALLOW_MULTIPLE_SUBSCRIPTIONS, chkAllowMultipleSubscriptions.Checked.ToString())
            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REQUIRE_SHIPPING_ADDRESS, chkRequireShippingAddress.Checked.ToString())
            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.USE_LIVE_PROCESSOR, chkUseLiveProcessor.Checked.ToString())
            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.PROCESSOR_USER_ID, txtProcessorUserID.Text)
            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.CURRENCY, drpCurrency.SelectedValue)

            Dim objContentController As New ContentController
            Dim objContent As ContentInfo = objContentController.Get(Me.ModuleId, Constants.INTRO_TEXT)

            If (objContent Is Nothing) Then
                objContent = New ContentInfo
                objContent.ModuleID = Me.ModuleId
                objContent.SettingName = Constants.INTRO_TEXT
                objContent.SettingValue = Introduction.Text
                objContentController.Add(objContent)
            Else
                objContent.SettingValue = Introduction.Text
                objContentController.Update(objContent)
            End If

            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.INVOICE_SEND, chkSendInvoice.Checked.ToString())
            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.INVOICE_TEMPLATE, txtInvoice.Text)

        End Sub

#End Region

#Region " Event Handlers "

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try

                BindCrumbs()

                If Not (Page.IsPostBack) Then
                    BindCurrency()
                    BindSettings()
                End If

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click

            Try

                SaveSettings()
                Response.Redirect(NavigateURL(), True)

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

#End Region

    End Class

End Namespace