Imports System
Imports System.Data

Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Framework

Imports Ventrian.SubscriptionTools.Data

Namespace Ventrian.SubscriptionTools.Entities

    Public Class OrderItemController

#Region " Public Methods "

        Public Function List(ByVal orderID As Integer) As ArrayList

            Return CBO.FillCollection(DataProvider.Instance().ListOrderItem(orderID), GetType(OrderItemInfo))

        End Function

        Public Function Add(ByVal objOrderItem As OrderItemInfo) As Integer

            Return CType(DataProvider.Instance().AddOrderItem(objOrderItem.OrderID, objOrderItem.PlanID, objOrderItem.ServiceFee), Integer)

        End Function

#End Region

    End Class

End Namespace
