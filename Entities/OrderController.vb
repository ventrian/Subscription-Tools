Imports System
Imports System.Data

Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Framework

Imports Ventrian.SubscriptionTools.Data

Namespace Ventrian.SubscriptionTools.Entities

    Public Class OrderController

#Region " Public Methods "

        Public Function [Get](ByVal orderID As Integer) As OrderInfo

            'Return CType(CBO.FillObject(DataProvider.Instance().GetOrder(orderID), GetType(OrderInfo)), OrderInfo)
            Return CBO.FillObject(Of OrderInfo)(DataProvider.Instance().GetOrder(orderID))

        End Function


        Public Function Add(ByVal objOrder As OrderInfo) As Integer

            Return CType(DataProvider.Instance().AddOrder(objOrder.PortalID, objOrder.ModuleID, objOrder.UserID), Integer)

        End Function

#End Region

    End Class

End Namespace
