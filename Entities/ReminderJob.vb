Imports System.Threading

Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Portals
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Mail

Namespace Ventrian.SubscriptionTools.Entities

    Public Class ReminderJob
        Inherits DotNetNuke.Services.Scheduling.SchedulerClient

#Region " Private Members "

        Private _portalName As String = ""

#End Region

#Region " Constructors "

        Public Sub New(ByVal objScheduleHistoryItem As DotNetNuke.Services.Scheduling.ScheduleHistoryItem)

            MyBase.new()
            Me.ScheduleHistoryItem = objScheduleHistoryItem

        End Sub

#End Region

        Public Overrides Sub DoWork()
            Try
                'notification that the event is progressing
                Me.Progressing()    'OPTIONAL
                SendReminders()
                Me.ScheduleHistoryItem.Succeeded = True    'REQUIRED

            Catch exc As Exception    'REQUIRED

                Me.ScheduleHistoryItem.Succeeded = False    'REQUIRED
                Me.ScheduleHistoryItem.AddLogNote("Subscription Tools -> Send Reminder failed. " + exc.ToString)     'OPTIONAL
                'notification that we have errored
                Me.Errored(exc)    'REQUIRED
                'log the exception
                LogException(exc)    'OPTIONAL

            End Try
        End Sub

        Private Sub SendReminders()

            Dim portalID As Integer = Convert.ToInt32(Me.ScheduleHistoryItem.GetSetting("PortalID"))
            Dim moduleID As Integer = Convert.ToInt32(Me.ScheduleHistoryItem.GetSetting("ModuleID"))

            Dim objModuleController As New ModuleController

            Dim settings As Hashtable = objModuleController.GetModuleSettings(moduleID)

            Dim period As Integer = Null.NullInteger
            If (settings.Contains(Constants.REMINDER_PERIOD)) Then
                period = Convert.ToInt32(settings(Constants.REMINDER_PERIOD).ToString())
            Else
                Throw New Exception("Subscription Tools -> Period not found.")
            End If

            Dim periodMeasurement As String = ""
            If (settings.Contains(Constants.REMINDER_PERIOD_MEASUREMENT)) Then
                periodMeasurement = settings(Constants.REMINDER_PERIOD_MEASUREMENT).ToString()
            Else
                Throw New Exception("Subscription Tools -> Period Measurement not found.")
            End If

            Dim frequency As Integer = Null.NullInteger
            If (settings.Contains(Constants.REMINDER_FREQUENCY)) Then
                frequency = Convert.ToInt32(settings(Constants.REMINDER_FREQUENCY).ToString())
            Else
                Throw New Exception("Subscription Tools -> Frequency not found.")
            End If

            Dim frequencyMeasurement As String = ""
            If (settings.Contains(Constants.REMINDER_FREQUENCY_MEASUREMENT)) Then
                frequencyMeasurement = settings(Constants.REMINDER_FREQUENCY_MEASUREMENT).ToString()
            Else
                Throw New Exception("Subscription Tools -> Frequency Measurement not found.")
            End If

            Dim datePeriod As DateTime = DateTime.Now

            Select Case periodMeasurement.ToLower()
                Case "d"
                    datePeriod = datePeriod.AddDays(Convert.ToInt32(period))
                    Exit Select

                Case "h"
                    datePeriod = datePeriod.AddHours(Convert.ToInt32(period))
                    Exit Select

                Case "m"
                    datePeriod = datePeriod.AddMinutes(Convert.ToInt32(period))
                    Exit Select

                Case "s"
                    datePeriod = datePeriod.AddSeconds(Convert.ToInt32(period))
                    Exit Select
            End Select

            Dim dateFrequency As DateTime = DateTime.Now

            Select Case frequencyMeasurement.ToLower()
                Case "d"
                    dateFrequency = dateFrequency.AddDays(Convert.ToInt32(frequency) * -1)
                    Exit Select

                Case "h"
                    dateFrequency = dateFrequency.AddHours(Convert.ToInt32(frequency) * -1)
                    Exit Select

                Case "m"
                    dateFrequency = dateFrequency.AddMinutes(Convert.ToInt32(frequency) * -1)
                    Exit Select

                Case "s"
                    dateFrequency = dateFrequency.AddSeconds(Convert.ToInt32(frequency) * -1)
                    Exit Select
            End Select

            Dim email As String = ""
            If (settings.Contains(Constants.REMINDER_EMAIL)) Then
                email = settings(Constants.REMINDER_EMAIL).ToString()
            Else
                Throw New Exception("Subscription Tools -> Portal Email not found.")
            End If

            If (settings.Contains(Constants.REMINDER_PORTAL_NAME)) Then
                _portalName = settings(Constants.REMINDER_PORTAL_NAME).ToString()
            End If

            Dim subject As String = ""
            If (settings.Contains(Constants.REMINDER_SUBJECT)) Then
                subject = settings(Constants.REMINDER_SUBJECT).ToString()
            Else
                Throw New Exception("Subscription Tools -> Subject not found.")
            End If

            Dim template As String = ""
            If (settings.Contains(Constants.REMINDER_TEMPLATE)) Then
                template = settings(Constants.REMINDER_TEMPLATE).ToString()
            Else
                Throw New Exception("Subscription Tools -> Template/Body not found.")
            End If

            Dim bccEmail As String = ""
            If (settings.Contains(Constants.REMINDER_BCC)) Then
                bccEmail = settings(Constants.REMINDER_BCC).ToString().Trim()
            End If

            Dim objReceiptController As New ReceiptController
            Dim objReceipts As ArrayList = objReceiptController.ListExpired(portalID, moduleID, datePeriod, dateFrequency)

            Me.ScheduleHistoryItem.AddLogNote(objReceipts.Count & " expiring subscriptions found.")    'OPTIONAL

            Dim emailsSent As Integer = 0

            For Each objReceipt As ReceiptInfo In objReceipts
                If (objReceipt.Email <> "" And objReceipt.AutoRenew = False) Then

                    Dim objUserSettingController As New UserSettingController

                    Dim objUserSetting As UserSettingInfo = objUserSettingController.Get(objReceipt.UserID, Constants.REMINDER_RECEIVE_NOTIFICATIONS)

                    Dim skipEmail As Boolean = False
                    If Not (objUserSetting Is Nothing) Then
                        skipEmail = Not Convert.ToBoolean(objUserSetting.SettingValue)
                    End If

                    If (skipEmail) Then
                        Me.ScheduleHistoryItem.AddLogNote("[SKIPPING] " & objReceipt.FirstName & " " & objReceipt.LastName & " has elected no notifications.")
                    Else
                        Dim sendTo As String = objReceipt.Email
                        Dim sendFrom As String = email

                        Dim emailSubject As String = FormatTokens(subject, objReceipt)
                        Dim emailBody As String = FormatTokens(template, objReceipt)

                        Try
                            DotNetNuke.Services.Mail.Mail.SendMail(sendFrom, sendTo, bccEmail, emailSubject, emailBody, "", "", "", "", "", "")
                            Data.DataProvider.Instance().AddReminder(objReceipt.ReceiptID, DateTime.Now())
                            emailsSent = emailsSent + 1
                            Me.ScheduleHistoryItem.AddLogNote("[PROCESSING] " & objReceipt.FirstName & " " & objReceipt.LastName & " has been sent a reminder.")
                        Catch
                            Me.ScheduleHistoryItem.AddLogNote("[FAILURE] " & objReceipt.FirstName & " " & objReceipt.LastName & " has thrown an exception when trying to send a reminder.")
                        End Try
                    End If

                Else
                    Me.ScheduleHistoryItem.AddLogNote("[SKIPPING] " & objReceipt.FirstName & " " & objReceipt.LastName & " has no email set.")
                End If
            Next

            Me.ScheduleHistoryItem.AddLogNote(emailsSent.ToString() & " emails sent.")

        End Sub

        Private Function FormatTokens(ByVal text As String, ByVal objReceipt As ReceiptInfo) As String

            Dim formattedText As String = text

            formattedText = formattedText.Replace("[PORTALNAME]", _portalName)
            formattedText = formattedText.Replace("[FIRSTNAME]", objReceipt.FirstName)
            formattedText = formattedText.Replace("[LASTNAME]", objReceipt.LastName)
            formattedText = formattedText.Replace("[USERNAME]", objReceipt.UserName)
            formattedText = formattedText.Replace("[EMAIL]", objReceipt.Email)
            formattedText = formattedText.Replace("[DISPLAYNAME]", objReceipt.DisplayName)
            formattedText = formattedText.Replace("[SUBSCRIPTION]", objReceipt.Name)
            formattedText = formattedText.Replace("[EXPIRYDATE]", objReceipt.DateEnd.ToString("MMMM, dd yyyy"))

            Return formattedText

        End Function

    End Class

End Namespace
