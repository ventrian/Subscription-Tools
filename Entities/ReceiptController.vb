'
' Subscription Tools for DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2006
' by Ventrian ( support@ventrian.com ) ( http://www.ventrian.com )
'

Imports System
Imports System.Data

Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Framework

Imports Ventrian.SubscriptionTools.Data

Namespace Ventrian.SubscriptionTools.Entities

    Public Class ReceiptController

#Region " Public Methods "

        Public Function [Get](ByVal receiptID As Integer) As ReceiptInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetReceipt(receiptID), GetType(ReceiptInfo)), ReceiptInfo)

        End Function

        Public Function List(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal userID As Integer, ByVal userName As String) As ArrayList

            Return CBO.FillCollection(DataProvider.Instance().ListReceipt(portalID, moduleID, userID, userName), GetType(ReceiptInfo))

        End Function

        Public Function ListExpired(ByVal portalID As Integer, ByVal moduleID As Integer, ByVal datePeriod As DateTime, ByVal dateFrequency As DateTime) As ArrayList

            Return CBO.FillCollection(DataProvider.Instance().ListReceiptExpired(portalID, moduleID, datePeriod, dateFrequency), GetType(ReceiptInfo))

        End Function

        Public Function Add(ByVal objReceipt As ReceiptInfo) As Integer

            Return CType(DataProvider.Instance().AddReceipt(objReceipt.PortalID, objReceipt.ModuleID, objReceipt.UserID, objReceipt.DateCreated, objReceipt.DateStart, objReceipt.DateEnd, objReceipt.Status, objReceipt.Name, objReceipt.Description, objReceipt.ServiceFee, objReceipt.BillingPeriod, objReceipt.BillingFrequency, objReceipt.Processor, objReceipt.ProcessorTxID, objReceipt.Currency, objReceipt.AutoRenew), Integer)

        End Function

        Public Sub Update(ByVal objReceipt As ReceiptInfo)

            DataProvider.Instance().UpdateReceipt(objReceipt.ReceiptID, objReceipt.PortalID, objReceipt.ModuleID, objReceipt.UserID, objReceipt.DateCreated, objReceipt.DateStart, objReceipt.DateEnd, objReceipt.Status, objReceipt.Name, objReceipt.ServiceFee, objReceipt.BillingPeriod, objReceipt.BillingFrequency, objReceipt.Processor, objReceipt.ProcessorTxID, objReceipt.Currency, objReceipt.AutoRenew)

        End Sub

        Public Sub Delete(ByVal receiptID As Integer)

            DataProvider.Instance().DeleteReceipt(receiptID)

        End Sub

#End Region

    End Class

End Namespace
