Imports System.Web.UI.WebControls

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Security
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Services.Scheduling
Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.UI.Utilities

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Partial Public Class SendReminders
        Inherits ModuleBase

#Region " Private Methods "

        Private Sub BindCrumbs()

            Dim crumbs As New ArrayList

            Dim objCrumbMain As New CrumbInfo
            objCrumbMain.Caption = Localization.GetString("Main", LocalResourceFile)
            objCrumbMain.Url = NavigateURL()
            crumbs.Add(objCrumbMain)

            Dim objEditReceipts As New CrumbInfo
            objEditReceipts.Caption = Localization.GetString("SendReminders", LocalResourceFile)
            objEditReceipts.Url = EditUrl("Reminders")
            crumbs.Add(objEditReceipts)

            rptBreadCrumbs.DataSource = crumbs
            rptBreadCrumbs.DataBind()

        End Sub

        Private Sub BindDefaults()

            txtReminderPeriod.Text = Me.ReminderPeriod.ToString()
            If Not (drpReminderPeriodMeasurement.Items.FindByValue(Me.ReminderPeriodMeasurement) Is Nothing) Then
                drpReminderPeriodMeasurement.SelectedValue = Me.ReminderPeriodMeasurement
            End If

            txtReminderFrequency.Text = Me.ReminderFrequency.ToString()
            If Not (drpReminderFrequencyMeasurement.Items.FindByValue(Me.ReminderFrequencyMeasurement) Is Nothing) Then
                drpReminderFrequencyMeasurement.SelectedValue = Me.ReminderFrequencyMeasurement
            End If

            txtSubject.Text = Me.ReminderSubject
            txtTemplate.Text = Me.ReminderTemplate
            txtBCCEmail.Text = Me.ReminderBCC

            chkEnabled.Checked = Me.ReminderSchedulerEnabled
            txtTimeLapse.Text = Me.ReminderSchedulerTimeLapse.ToString()
            If Not (drpTimeLapseMeasurement.Items.FindByValue(Me.ReminderSchedulerTimeLapseMeasurement) Is Nothing) Then
                drpTimeLapseMeasurement.SelectedValue = Me.ReminderSchedulerTimeLapseMeasurement
            End If
            txtRetryTimeLapse.Text = Me.ReminderSchedulerRetryFrequency.ToString()
            If Not (drpRetryTimeLapseMeasurement.Items.FindByValue(Me.ReminderSchedulerRetryFrequencyMeasurement) Is Nothing) Then
                drpRetryTimeLapseMeasurement.SelectedValue = Me.ReminderSchedulerRetryFrequencyMeasurement
            End If

        End Sub

        Private Sub BindHistory()

            If (Me.ReminderScheduleID <> Null.NullInteger) Then

                Dim arrSchedule As ArrayList = SchedulingProvider.Instance.GetScheduleHistory(Me.ReminderScheduleID)

                If (arrSchedule.Count > 0) Then

                    arrSchedule.Sort(New ScheduleHistorySortStartDate)

                    'Localize Grid
                    Services.Localization.Localization.LocalizeDataGrid(dgScheduleHistory, Me.LocalResourceFile)

                    dgScheduleHistory.DataSource = arrSchedule
                    dgScheduleHistory.DataBind()

                    lblNoHistory.Visible = False
                    dgScheduleHistory.Visible = True
                Else
                    lblNoHistory.Visible = True
                    dgScheduleHistory.Visible = False
                End If

            Else

                lblNoHistory.Visible = True
                dgScheduleHistory.Visible = False

            End If

        End Sub

#End Region

#Region " Event Handlers "

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try

                BindCrumbs()

                If (IsPostBack = False) Then

                    BindDefaults()
                    BindHistory()

                End If

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click

            Try

                If (Page.IsValid) Then

                    Dim objModuleController As New ModuleController

                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_EMAIL, PortalSettings.Email)
                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_PORTAL_NAME, PortalSettings.PortalName)

                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_PERIOD, txtReminderPeriod.Text)
                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_PERIOD_MEASUREMENT, drpReminderPeriodMeasurement.SelectedValue)
                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_FREQUENCY, txtReminderFrequency.Text)
                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_FREQUENCY_MEASUREMENT, drpReminderFrequencyMeasurement.SelectedValue)
                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_SUBJECT, txtSubject.Text)
                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_TEMPLATE, txtTemplate.Text)
                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_BCC, txtBCCEmail.Text)

                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_SCHEDULER_ENABLED, chkEnabled.Checked.ToString())
                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_SCHEDULER_TIME_LAPSE, txtTimeLapse.Text)
                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_SCHEDULER_TIME_LAPSE_MEASUREMENT, drpTimeLapseMeasurement.SelectedValue)
                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_SCHEDULER_RETRY_FREQUENCY, txtRetryTimeLapse.Text)
                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_SCHEDULER_RETRY_FREQUENCY_MEASUREMENT, drpRetryTimeLapseMeasurement.SelectedValue)

                    Dim objScheduleItem As New ScheduleItem

                    objScheduleItem.TypeFullName = "Ventrian.SubscriptionTools.Entities.ReminderJob, Ventrian.SubscriptionTools"

                    If txtTimeLapse.Text = "" Or txtTimeLapse.Text = "0" Or txtTimeLapse.Text = "-1" Then
                        objScheduleItem.TimeLapse = Null.NullInteger
                    Else
                        objScheduleItem.TimeLapse = Convert.ToInt32(txtTimeLapse.Text)
                    End If

                    objScheduleItem.TimeLapseMeasurement = drpTimeLapseMeasurement.SelectedItem.Value

                    If txtRetryTimeLapse.Text = "" Or txtRetryTimeLapse.Text = "0" Or txtRetryTimeLapse.Text = "-1" Then
                        objScheduleItem.RetryTimeLapse = Null.NullInteger
                    Else
                        objScheduleItem.RetryTimeLapse = Convert.ToInt32(txtRetryTimeLapse.Text)
                    End If

                    objScheduleItem.RetryTimeLapseMeasurement = drpRetryTimeLapseMeasurement.SelectedItem.Value
                    objScheduleItem.RetainHistoryNum = 10
                    objScheduleItem.AttachToEvent = ""
                    objScheduleItem.CatchUpEnabled = False
                    objScheduleItem.Enabled = chkEnabled.Checked
                    objScheduleItem.ObjectDependencies = ""
                    objScheduleItem.Servers = ""

                    If (Me.ReminderScheduleID <> Null.NullInteger) Then

                        If (SchedulingProvider.Instance().GetSchedule(Me.ReminderScheduleID) Is Nothing) Then
                            objScheduleItem.ScheduleID = SchedulingProvider.Instance().AddSchedule(objScheduleItem)
                        Else
                            objScheduleItem.ScheduleID = Me.ReminderScheduleID
                            SchedulingProvider.Instance().UpdateSchedule(objScheduleItem)
                        End If

                    Else

                        objScheduleItem.ScheduleID = SchedulingProvider.Instance().AddSchedule(objScheduleItem)

                    End If

                    Data.DataProvider.Instance().AddScheduleItemSetting(objScheduleItem.ScheduleID, "PortalID", Me.PortalSettings.PortalId.ToString())
                    Data.DataProvider.Instance().AddScheduleItemSetting(objScheduleItem.ScheduleID, "ModuleID", Me.ModuleId.ToString())

                    Data.DataProvider.Instance().AddScheduleItemSetting(objScheduleItem.ScheduleID, "Period", txtReminderPeriod.Text)
                    Data.DataProvider.Instance().AddScheduleItemSetting(objScheduleItem.ScheduleID, "PeriodMeasurement", drpReminderPeriodMeasurement.SelectedValue)

                    Data.DataProvider.Instance().AddScheduleItemSetting(objScheduleItem.ScheduleID, "Frequency", txtReminderFrequency.Text)
                    Data.DataProvider.Instance().AddScheduleItemSetting(objScheduleItem.ScheduleID, "FrequencyMeasurement", drpReminderFrequencyMeasurement.SelectedValue)

                    objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.REMINDER_SCHEDULER_ID, objScheduleItem.ScheduleID.ToString())

                    Response.Redirect(NavigateURL(), True)

                End If

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