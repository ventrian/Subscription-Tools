'
' Subscription Tools for DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2007
'

Namespace Ventrian.SubscriptionTools.Entities

    Public Class Constants

#Region " Constants "

        Public Const ALLOW_MULTIPLE_SUBSCRIPTIONS As String = "AllowMultipleSubscriptions"
        Public Const USE_LIVE_PROCESSOR As String = "UseLiveProcessor"
        Public Const PROCESSOR_USER_ID As String = "ProcessorUserID"
        Public Const REQUIRE_SHIPPING_ADDRESS As String = "RequireShippingAddress"
        Public Const CURRENCY As String = "Currency"
        Public Const INTRO_TEXT As String = "IntroText"

        Public Const DEFAULT_ALLOW_MULTIPLE_SUBSCRIPTIONS As Boolean = False
        Public Const DEFAULT_USE_LIVE_PROCESSOR As Boolean = True
        Public Const DEFAULT_PROCESSOR_USER_ID As String = ""
        Public Const DEFAULT_REQUIRE_SHIPPING_ADDRESS As Boolean = False
        Public Const DEFAULT_CURRENCY As String = "USD"

        Public Const REMINDER_EMAIL As String = "ReminderEmail"
        Public Const REMINDER_PORTAL_NAME As String = "ReminderPortalName"

        Public Const REMINDER_PERIOD As String = "ReminderPeriod"
        Public Const REMINDER_PERIOD_MEASUREMENT As String = "ReminderPeriodMeasurement"
        Public Const REMINDER_FREQUENCY As String = "ReminderFrequency"
        Public Const REMINDER_FREQUENCY_MEASUREMENT As String = "ReminderFrequencyMeasurement"

        Public Const REMINDER_SUBJECT As String = "ReminderSubject"
        Public Const REMINDER_TEMPLATE As String = "ReminderTemplate"
        Public Const REMINDER_BCC As String = "ReminderBCC"

        Public Const REMINDER_PERIOD_DEFAULT As Integer = 28
        Public Const REMINDER_PERIOD_MEASUREMENT_DEFAULT As String = "d"
        Public Const REMINDER_FREQUENCY_DEFAULT As Integer = 7
        Public Const REMINDER_FREQUENCY_MEASUREMENT_DEFAULT As String = "d"

        Public Const REMINDER_DEFAULT_SUBJECT As String = "[PORTALNAME]: Subscription Renewal"
        Public Const REMINDER_DEFAULT_TEMPLATE As String = "" _
            & "Hi [FIRSTNAME] [LASTNAME]," & vbCrLf & vbCrLf _
            & "Your [SUBSCRIPTION] to [PORTALNAME] expires on the following date:-" & vbCrLf & vbCrLf _
            & "[EXPIRYDATE]" & vbCrLf & vbCrLf _
            & "To renew/extend your membership, please visit the following URL:-" & vbCrLf & vbCrLf _
            & "http://www.sampleurl.com/" & vbCrLf & vbCrLf _
            & "Yours Sincerely," & vbCrLf & vbCrLf _
            & "Your Name Here"

        Public Const REMINDER_SCHEDULER_ID As String = "ScheduleID"
        Public Const REMINDER_SCHEDULER_ENABLED As String = "SchedulerEnabled"
        Public Const REMINDER_SCHEDULER_TIME_LAPSE As String = "SchedulerTimeLapse"
        Public Const REMINDER_SCHEDULER_TIME_LAPSE_MEASUREMENT As String = "SchedulerTimeLapseMeasurement"
        Public Const REMINDER_SCHEDULER_RETRY_FREQUENCY As String = "SchedulerRetryFrequency"
        Public Const REMINDER_SCHEDULER_RETRY_FREQUENCY_MEASUREMENT As String = "SchedulerRetryFrequencyMeasurement"

        Public Const REMINDER_RECEIVE_NOTIFICATIONS As String = "ReceiveNotifications"

        Public Const REMINDER_SCHEDULER_ENABLED_DEFAULT As Boolean = False
        Public Const REMINDER_SCHEDULER_TIME_LAPSE_DEFAULT As Integer = 1
        Public Const REMINDER_SCHEDULER_TIME_LAPSE_MEASUREMENT_DEFAULT As String = "d"
        Public Const REMINDER_SCHEDULER_RETRY_FREQUENCY_DEFAULT As Integer = 2
        Public Const REMINDER_SCHEDULER_RETRY_FREQUENCY_MEASUREMENT_DEFAULT As String = "d"

        Public Const INVOICE_SEND As String = "InvoiceSend"
        Public Const INVOICE_TEMPLATE As String = "InvoiceTemplate"

        Public Const INVOICE_SEND_DEFAULT As Boolean = False
        Public Const INVOICE_TEMPLATE_DEFAULT As String = "" _
            & "Your Purchase was successful." & vbCrLf _
            & "Thank you! Your receipt is below." & vbCrLf & vbCrLf _
            & "------ HOW TO MANAGE YOUR MEMBERSHIP ------" & vbCrLf & vbCrLf _
            & "To manage all aspects of your Ventrian.com membership, visit our Account area at http://www.ventrian.com/subscribe.aspx" & vbCrLf & vbCrLf _
            & "------ CUSTOMER SUPPORT ------" & vbCrLf & vbCrLf _
            & "Have questions about Ventrian.com? You can find answers at http://www.ventrian.com/support.aspx" & vbCrLf & vbCrLf _
            & "------ INVOICE DETAILS ------" & vbCrLf & vbCrLf _
            & "Receipt #: [RECEIPTID]" & vbCrLf _
            & "Date:   [DATE]" & vbCrLf & vbCrLf _
            & "[FULLNAME]" & vbCrLf _
            & "[STREET] " & vbCrLf _
            & "[CITY], [REGION] [POSTALCODE]" & vbCrLf _
            & "[COUNTRY]" & vbCrLf & vbCrLf _
            & "Item: [SUBSCRIPTIONNAME]" & vbCrLf _
            & "Price: [SUBSCRIPTIONPRICE]" & vbCrLf _
            & "Status: [STATUS]*" & vbCrLf & vbCrLf _
            & "* If your status is pending, you may have paid with eCheque that requires a few days to clear."

#End Region

    End Class

End Namespace
