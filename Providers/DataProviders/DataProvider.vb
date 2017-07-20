'
' Subscription Tools for DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2006
' by Ventrian ( sales@ventrian.com ) ( http://www.ventrian.com )
'

Imports System
Imports System.Web.Caching
Imports System.Reflection

Imports DotNetNuke
Imports DotNetNuke.Common.Utilities

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools.Data

    Public MustInherit Class DataProvider

#Region " Shared/Static Methods "

        ' singleton reference to the instantiated object 
        Private Shared objProvider As DataProvider = Nothing

        ' constructor
        Shared Sub New()
            CreateProvider()
        End Sub

        ' dynamically create provider
        Private Shared Sub CreateProvider()
            objProvider = CType(Framework.Reflection.CreateObject("data", "Ventrian.SubscriptionTools", "Ventrian.SubscriptionTools"), DataProvider)
        End Sub

        ' return the provider
        Public Shared Shadows Function Instance() As DataProvider
            Return objProvider
        End Function

#End Region

#Region " Abstract Methods "

#Region " Plan Methods "

        Public MustOverride Function ListPlan(ByVal portalID As Integer, ByVal moduleID As Integer) As IDataReader
        Public MustOverride Function GetPlan(ByVal planID As Integer) As IDataReader
        Public MustOverride Function AddPlan(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal roleID As Integer, ByVal name As String, ByVal description As String, ByVal isActive As Boolean, ByVal viewOrder As Integer, ByVal serviceFee As Decimal, ByVal billingFrequency As Integer, ByVal billingPeriod As Integer, ByVal currency As String, ByVal endDate As DateTime, ByVal autoRenew As Boolean) As Integer
        Public MustOverride Sub UpdatePlan(ByVal planID As Integer, ByVal portalID As Integer, ByVal moduleID As Integer, ByVal roleID As Integer, ByVal name As String, ByVal description As String, ByVal isActive As Boolean, ByVal viewOrder As Integer, ByVal serviceFee As Decimal, ByVal billingFrequency As Integer, ByVal billingPeriod As Integer, ByVal currency As String, ByVal endDate As DateTime, ByVal autoRenew As Boolean)
        Public MustOverride Sub DeletePlan(ByVal planID As Integer)

#End Region

#Region " Receipt Methods "

        Public MustOverride Function GetReceipt(ByVal receiptID As Integer) As IDataReader
        Public MustOverride Function ListReceipt(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal userID As Integer, ByVal userName As String) As IDataReader
        Public MustOverride Function ListReceiptExpired(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal datePeriod As DateTime, ByVal dateFrequency As DateTime) As IDataReader
        Public MustOverride Function AddReceipt(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal userID As Integer, ByVal dateCreated As DateTime, ByVal dateStart As DateTime, ByVal dateEnd As DateTime, ByVal status As String, ByVal name As String, ByVal description As String, ByVal serviceFee As Decimal, ByVal billingPeriod As Integer, ByVal billingFrequency As Integer, ByVal processor As String, ByVal processorTxID As String, ByVal currency As String, ByVal autoRenew As Boolean) As Integer
        Public MustOverride Sub UpdateReceipt(ByVal receiptID As Integer, ByVal portalID As Integer, ByVal moduleID As Integer, ByVal userID As Integer, ByVal dateCreated As DateTime, ByVal dateStart As DateTime, ByVal dateEnd As DateTime, ByVal status As String, ByVal name As String, ByVal serviceFee As Decimal, ByVal billingPeriod As Integer, ByVal billingFrequency As Integer, ByVal processor As String, ByVal processorTxID As String, ByVal currency As String, ByVal autoRenew As Boolean)
        Public MustOverride Sub DeleteReceipt(ByVal receiptID As Integer)

#End Region

#Region " Content Methods "

        Public MustOverride Function GetContent(ByVal moduleID As Integer, ByVal settingName As String) As IDataReader
        Public MustOverride Function AddContent(ByVal moduleID As Integer, ByVal settingName As String, ByVal settingValue As String) As Integer
        Public MustOverride Sub UpdateContent(ByVal contentID As Integer, ByVal moduleID As Integer, ByVal settingName As String, ByVal settingValue As String)

#End Region

#Region " Order Methods "

        Public MustOverride Function GetOrder(ByVal orderID As Integer) As IDataReader
        Public MustOverride Function AddOrder(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal userID As Integer) As Integer

#End Region

#Region " OrderItem Methods "

        Public MustOverride Function ListOrderItem(ByVal orderID As Integer) As IDataReader
        Public MustOverride Function AddOrderItem(ByVal orderID As Integer, ByVal planID As Integer, ByVal serviceFee As Decimal) As Integer

#End Region

#Region " Report Methods "

        Public MustOverride Function ListReport(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal reportType As ReportType, ByVal dateStart As DateTime, ByVal dateEnd As DateTime) As IDataReader

#End Region

#Region " Scheduler Methods "

        Public MustOverride Sub AddScheduleItemSetting(ByVal scheduleID As Integer, ByVal name As String, ByVal value As String)

#End Region

#Region " Reminder Methods "

        Public MustOverride Sub AddReminder(ByVal receiptID As Integer, ByVal dateSent As DateTime)

#End Region

#Region " UserSetting Methods "

        Public MustOverride Function GetUserSetting(ByVal userID As Integer, ByVal settingName As String) As IDataReader
        Public MustOverride Function AddUserSetting(ByVal userID As Integer, ByVal settingName As String, ByVal settingValue As String) As Integer
        Public MustOverride Sub UpdateUserSetting(ByVal userID As Integer, ByVal settingName As String, ByVal settingValue As String)

#End Region

#End Region

    End Class

End Namespace