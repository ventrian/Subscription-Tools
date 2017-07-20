'
' Subscription Tools for DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2006
' by Scott McCulloch ( support@ventrian.com ) ( http://www.smcculloch.net )
'

Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

Imports DotNetNuke
Imports DotNetNuke.Common.Utilities
Imports Microsoft.ApplicationBlocks.Data

Imports Ventrian.SubscriptionTools.Data
Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Public Class SqlDataProvider

        Inherits DataProvider

#Region " Private Members "

        Private Const ProviderType As String = "data"

        Private _providerConfiguration As Framework.Providers.ProviderConfiguration = Framework.Providers.ProviderConfiguration.GetProviderConfiguration(ProviderType)
        Private _connectionString As String
        Private _providerPath As String
        Private _objectQualifier As String
        Private _databaseOwner As String

#End Region

#Region " Constructors "

        Public Sub New()

            ' Read the configuration specific information for this provider
            Dim objProvider As Framework.Providers.Provider = CType(_providerConfiguration.Providers(_providerConfiguration.DefaultProvider), Framework.Providers.Provider)

            ' Read the attributes for this provider
            _connectionString = Config.GetConnectionString()

            _providerPath = objProvider.Attributes("providerPath")

            _objectQualifier = objProvider.Attributes("objectQualifier")
            If _objectQualifier <> "" And _objectQualifier.EndsWith("_") = False Then
                _objectQualifier += "_"
            End If

            _databaseOwner = objProvider.Attributes("databaseOwner")
            If _databaseOwner <> "" And _databaseOwner.EndsWith(".") = False Then
                _databaseOwner += "."
            End If

        End Sub

#End Region

#Region " Properties "

        Public ReadOnly Property ConnectionString() As String
            Get
                Return _connectionString
            End Get
        End Property

        Public ReadOnly Property ProviderPath() As String
            Get
                Return _providerPath
            End Get
        End Property

        Public ReadOnly Property ObjectQualifier() As String
            Get
                Return _objectQualifier
            End Get
        End Property

        Public ReadOnly Property DatabaseOwner() As String
            Get
                Return _databaseOwner
            End Get
        End Property

#End Region

#Region " Public Methods "

        Private Function GetNull(ByVal Field As Object) As Object
            Return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value)
        End Function

#Region " Plan Methods "

        Public Overrides Function ListPlan(ByVal portalID As Integer, ByVal moduleID As Integer) As IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_PlanList", portalID, moduleID), IDataReader)
        End Function

        Public Overrides Function GetPlan(ByVal planID As Integer) As IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_PlanGet", planID), IDataReader)
        End Function

        Public Overrides Function AddPlan(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal roleID As Integer, ByVal name As String, ByVal description As String, ByVal isActive As Boolean, ByVal viewOrder As Integer, ByVal serviceFee As Decimal, ByVal billingFrequency As Integer, ByVal billingPeriod As Integer, ByVal currency As String, ByVal endDate As DateTime, ByVal autoRenew As Boolean) As Integer
            Return CType(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_PlanAdd", portalID, GetNull(moduleID), roleID, name, GetNull(description), isActive, viewOrder, serviceFee, billingFrequency, billingPeriod, GetNull(currency), GetNull(endDate), autoRenew), Integer)
        End Function

        Public Overrides Sub UpdatePlan(ByVal planID As Integer, ByVal portalID As Integer, ByVal moduleID As Integer, ByVal roleID As Integer, ByVal name As String, ByVal description As String, ByVal isActive As Boolean, ByVal viewOrder As Integer, ByVal serviceFee As Decimal, ByVal billingFrequency As Integer, ByVal billingPeriod As Integer, ByVal currency As String, ByVal endDate As DateTime, ByVal autoRenew As Boolean)
            SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_PlanUpdate", planID, portalID, GetNull(moduleID), roleID, name, GetNull(description), isActive, viewOrder, serviceFee, billingFrequency, billingPeriod, GetNull(currency), GetNull(endDate), autoRenew)
        End Sub

        Public Overrides Sub DeletePlan(ByVal planID As Integer)
            SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_PlanDelete", planID)
        End Sub

#End Region

#Region " Receipt Methods "

        Public Overrides Function GetReceipt(ByVal receiptID As Integer) As IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_ReceiptGet", receiptID), IDataReader)
        End Function

        Public Overrides Function ListReceipt(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal userID As Integer, ByVal userName As String) As IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_ReceiptList", portalID, GetNull(moduleID), GetNull(userID), GetNull(userName)), IDataReader)
        End Function

        Public Overrides Function ListReceiptExpired(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal datePeriod As DateTime, ByVal dateFrequency As DateTime) As IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_ReceiptListExpired", portalID, moduleID, datePeriod, dateFrequency), IDataReader)
        End Function

        Public Overrides Function AddReceipt(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal userID As Integer, ByVal dateCreated As DateTime, ByVal dateStart As DateTime, ByVal dateEnd As DateTime, ByVal status As String, ByVal name As String, ByVal description As String, ByVal serviceFee As Decimal, ByVal billingPeriod As Integer, ByVal billingFrequency As Integer, ByVal processor As String, ByVal processorTxID As String, ByVal currency As String, ByVal autoRenew As Boolean) As Integer
            Return CType(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_ReceiptAdd", portalID, moduleID, userID, dateCreated, dateStart, GetNull(dateEnd), status, name, GetNull(description), serviceFee, billingPeriod, billingFrequency, processor, processorTxID, GetNull(currency), autoRenew), Integer)
        End Function

        Public Overrides Sub UpdateReceipt(ByVal receiptID As Integer, ByVal portalID As Integer, ByVal moduleID As Integer, ByVal userID As Integer, ByVal dateCreated As DateTime, ByVal dateStart As DateTime, ByVal dateEnd As DateTime, ByVal status As String, ByVal name As String, ByVal serviceFee As Decimal, ByVal billingPeriod As Integer, ByVal billingFrequency As Integer, ByVal processor As String, ByVal processorTxID As String, ByVal currency As String, ByVal autoRenew As Boolean)
            SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_ReceiptUpdate", receiptID, portalID, moduleID, userID, dateCreated, dateStart, GetNull(dateEnd), status, name, serviceFee, billingPeriod, billingFrequency, processor, processorTxID, GetNull(currency), autoRenew)
        End Sub

        Public Overrides Sub DeleteReceipt(ByVal receiptID As Integer)
            SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_ReceiptDelete", receiptID)
        End Sub

#End Region

#Region " Content Methods "

        Public Overrides Function GetContent(ByVal moduleID As Integer, ByVal settingName As String) As IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_ContentGet", moduleID, settingName), IDataReader)
        End Function

        Public Overrides Function AddContent(ByVal moduleID As Integer, ByVal settingName As String, ByVal settingValue As String) As Integer
            Return CType(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_ContentAdd", moduleID, settingName, settingValue), Integer)
        End Function

        Public Overrides Sub UpdateContent(ByVal contentID As Integer, ByVal moduleID As Integer, ByVal settingName As String, ByVal settingValue As String)
            SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_ContentUpdate", contentID, moduleID, settingName, settingValue)
        End Sub

#End Region

#Region " Order Methods "

        Public Overrides Function GetOrder(ByVal orderID As Integer) As IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_OrderGet", orderID), IDataReader)
        End Function

        Public Overrides Function AddOrder(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal userID As Integer) As Integer
            Return CType(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_OrderAdd", portalID, moduleID, userID), Integer)
        End Function

#End Region

#Region " Order Item Methods "

        Public Overrides Function ListOrderItem(ByVal orderID As Integer) As IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_OrderItemList", orderID), IDataReader)
        End Function

        Public Overrides Function AddOrderItem(ByVal orderID As Integer, ByVal planID As Integer, ByVal serviceFee As Decimal) As Integer
            Return CType(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_OrderItemAdd", orderID, planID, GetNull(serviceFee)), Integer)
        End Function


#End Region

#Region " Report Methods "

        Public Overrides Function ListReport(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal reportType As ReportType, ByVal dateStart As DateTime, ByVal dateEnd As DateTime) As IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_GetReport" & CType(reportType, Integer).ToString(), portalID, moduleID, GetNull(dateStart), GetNull(dateEnd)), IDataReader)
        End Function

#End Region

#Region " Schedule Methods "

        Public Overrides Sub AddScheduleItemSetting(ByVal scheduleID As Integer, ByVal name As String, ByVal value As String)
            SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_AddScheduleItemSetting", scheduleID, name, value)
        End Sub

#End Region

#Region " Reminder Methods "

        Public Overrides Sub AddReminder(ByVal receiptID As Integer, ByVal dateSent As DateTime)
            SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_ReminderAdd", receiptID, dateSent)
        End Sub

#End Region

#Region " UserSetting Methods "

        Public Overrides Function GetUserSetting(ByVal userID As Integer, ByVal settingName As String) As IDataReader
            Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_UserSettingGet", userID, settingName), IDataReader)
        End Function

        Public Overrides Function AddUserSetting(ByVal userID As Integer, ByVal settingName As String, ByVal settingValue As String) As Integer
            Return CType(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_UserSettingAdd", userID, settingName, settingValue), Integer)
        End Function

        Public Overrides Sub UpdateUserSetting(ByVal userID As Integer, ByVal settingName As String, ByVal settingValue As String)
            SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & "DnnForge_SubscriptionTools_UserSettingUpdate", userID, settingName, settingValue)
        End Sub

#End Region

#End Region

    End Class

End Namespace