'
' Subscription Tools for DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2007
'

Imports System
Imports System.Configuration
Imports System.Data

Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Common.Utilities

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Public Class ModuleBase

        Inherits PortalModuleBase

#Region " Properties "

        Public ReadOnly Property AllowMultipleSubscriptions() As Boolean
            Get
                If (Settings.Contains(Constants.ALLOW_MULTIPLE_SUBSCRIPTIONS)) Then
                    Return Convert.ToBoolean(Settings(Constants.ALLOW_MULTIPLE_SUBSCRIPTIONS).ToString())
                Else
                    Return Constants.DEFAULT_ALLOW_MULTIPLE_SUBSCRIPTIONS
                End If
            End Get
        End Property

        Public ReadOnly Property RequireShippingAddress() As Boolean
            Get
                If (Settings.Contains(Constants.REQUIRE_SHIPPING_ADDRESS)) Then
                    Return Convert.ToBoolean(Settings(Constants.REQUIRE_SHIPPING_ADDRESS).ToString())
                Else
                    Return Constants.DEFAULT_REQUIRE_SHIPPING_ADDRESS
                End If
            End Get
        End Property

        Public ReadOnly Property UseLiveProcessor() As Boolean
            Get
                If (Settings.Contains(Constants.USE_LIVE_PROCESSOR)) Then
                    Return Convert.ToBoolean(Settings(Constants.USE_LIVE_PROCESSOR).ToString())
                Else
                    Return Constants.DEFAULT_USE_LIVE_PROCESSOR
                End If
            End Get
        End Property

        Public ReadOnly Property ProcessorUserID() As String
            Get
                If (Settings.Contains(Constants.PROCESSOR_USER_ID)) Then
                    Return Settings(Constants.PROCESSOR_USER_ID).ToString()
                Else
                    Return Constants.DEFAULT_PROCESSOR_USER_ID
                End If
            End Get
        End Property

        Public ReadOnly Property Currency() As String
            Get
                If (Settings.Contains(Constants.CURRENCY)) Then
                    Return Settings(Constants.CURRENCY).ToString()
                Else
                    Return Constants.DEFAULT_CURRENCY
                End If
            End Get
        End Property

        Public ReadOnly Property IntroText() As String
            Get
                Dim objContentController As New ContentController
                Dim objContent As ContentInfo = objContentController.Get(Me.ModuleId, Constants.INTRO_TEXT)

                If Not (objContent Is Nothing) Then
                    Return objContent.SettingValue
                Else
                    Return ""
                End If
            End Get
        End Property

        Public ReadOnly Property ReminderScheduleID() As Integer
            Get
                If (Settings.Contains(Constants.REMINDER_SCHEDULER_ID)) Then
                    Return Convert.ToInt32(Settings(Constants.REMINDER_SCHEDULER_ID).ToString())
                Else
                    Return Null.NullInteger
                End If
            End Get
        End Property

        Public ReadOnly Property ReminderPeriod() As Integer
            Get
                If (Settings.Contains(Constants.REMINDER_PERIOD)) Then
                    Return Convert.ToInt32(Settings(Constants.REMINDER_PERIOD).ToString())
                Else
                    Return Constants.REMINDER_PERIOD_DEFAULT
                End If
            End Get
        End Property

        Public ReadOnly Property ReminderPeriodMeasurement() As String
            Get
                If (Settings.Contains(Constants.REMINDER_PERIOD_MEASUREMENT)) Then
                    Return Settings(Constants.REMINDER_PERIOD_MEASUREMENT).ToString()
                Else
                    Return Constants.REMINDER_PERIOD_MEASUREMENT_DEFAULT
                End If
            End Get
        End Property

        Public ReadOnly Property ReminderFrequency() As Integer
            Get
                If (Settings.Contains(Constants.REMINDER_FREQUENCY)) Then
                    Return Convert.ToInt32(Settings(Constants.REMINDER_FREQUENCY).ToString())
                Else
                    Return Constants.REMINDER_FREQUENCY_DEFAULT
                End If
            End Get
        End Property

        Public ReadOnly Property ReminderFrequencyMeasurement() As String
            Get
                If (Settings.Contains(Constants.REMINDER_FREQUENCY_MEASUREMENT)) Then
                    Return Settings(Constants.REMINDER_FREQUENCY_MEASUREMENT).ToString()
                Else
                    Return Constants.REMINDER_FREQUENCY_MEASUREMENT_DEFAULT
                End If
            End Get
        End Property

        Public ReadOnly Property ReminderSubject() As String
            Get
                If (Settings.Contains(Constants.REMINDER_SUBJECT)) Then
                    Return Settings(Constants.REMINDER_SUBJECT).ToString()
                Else
                    Return Constants.REMINDER_DEFAULT_SUBJECT
                End If
            End Get
        End Property

        Public ReadOnly Property ReminderTemplate() As String
            Get
                If (Settings.Contains(Constants.REMINDER_TEMPLATE)) Then
                    Return Settings(Constants.REMINDER_TEMPLATE).ToString()
                Else
                    Return Constants.REMINDER_DEFAULT_TEMPLATE
                End If
            End Get
        End Property

        Public ReadOnly Property ReminderBCC() As String
            Get
                If (Settings.Contains(Constants.REMINDER_BCC)) Then
                    Return Settings(Constants.REMINDER_BCC).ToString()
                Else
                    Return ""
                End If
            End Get
        End Property

        Public ReadOnly Property ReminderSchedulerEnabled() As Boolean
            Get
                If (Settings.Contains(Constants.REMINDER_SCHEDULER_ENABLED)) Then
                    Return Convert.ToBoolean(Settings(Constants.REMINDER_SCHEDULER_ENABLED).ToString())
                Else
                    Return Constants.REMINDER_SCHEDULER_ENABLED_DEFAULT
                End If
            End Get
        End Property

        Public ReadOnly Property ReminderSchedulerTimeLapse() As Integer
            Get
                If (Settings.Contains(Constants.REMINDER_SCHEDULER_TIME_LAPSE)) Then
                    Return Convert.ToInt32(Settings(Constants.REMINDER_SCHEDULER_TIME_LAPSE).ToString())
                Else
                    Return Constants.REMINDER_SCHEDULER_TIME_LAPSE_DEFAULT
                End If
            End Get
        End Property

        Public ReadOnly Property ReminderSchedulerTimeLapseMeasurement() As String
            Get
                If (Settings.Contains(Constants.REMINDER_SCHEDULER_TIME_LAPSE_MEASUREMENT)) Then
                    Return Settings(Constants.REMINDER_SCHEDULER_TIME_LAPSE_MEASUREMENT).ToString()
                Else
                    Return Constants.REMINDER_SCHEDULER_TIME_LAPSE_MEASUREMENT_DEFAULT
                End If
            End Get
        End Property

        Public ReadOnly Property ReminderSchedulerRetryFrequency() As Integer
            Get
                If (Settings.Contains(Constants.REMINDER_SCHEDULER_RETRY_FREQUENCY)) Then
                    Return Convert.ToInt32(Settings(Constants.REMINDER_SCHEDULER_RETRY_FREQUENCY).ToString())
                Else
                    Return Constants.REMINDER_SCHEDULER_RETRY_FREQUENCY_DEFAULT
                End If
            End Get
        End Property

        Public ReadOnly Property ReminderSchedulerRetryFrequencyMeasurement() As String
            Get
                If (Settings.Contains(Constants.REMINDER_SCHEDULER_RETRY_FREQUENCY_MEASUREMENT)) Then
                    Return Settings(Constants.REMINDER_SCHEDULER_RETRY_FREQUENCY_MEASUREMENT).ToString()
                Else
                    Return Constants.REMINDER_SCHEDULER_RETRY_FREQUENCY_MEASUREMENT_DEFAULT
                End If
            End Get
        End Property

        Public ReadOnly Property InvoiceSend() As Boolean
            Get
                If (Settings.Contains(Constants.INVOICE_SEND)) Then
                    Return Convert.ToBoolean(Settings(Constants.INVOICE_SEND).ToString())
                Else
                    Return Constants.INVOICE_SEND_DEFAULT
                End If
            End Get
        End Property

        Public ReadOnly Property InvoiceTemplate() As String
            Get
                If (Settings.Contains(Constants.INVOICE_TEMPLATE)) Then
                    Return Settings(Constants.INVOICE_TEMPLATE).ToString()
                Else
                    Return Constants.INVOICE_TEMPLATE_DEFAULT
                End If
            End Get
        End Property

#End Region

    End Class

End Namespace
