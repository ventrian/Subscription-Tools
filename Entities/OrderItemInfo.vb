Namespace Ventrian.SubscriptionTools.Entities

    Public Class OrderItemInfo

#Region " Private Members "

        Dim _orderItemID As Integer
        Dim _orderID As Integer
        Dim _planID As Integer
        Dim _serviceFee As Decimal

#End Region

#Region " Public Properties "

        Public Property OrderItemID() As Integer
            Get
                Return _orderItemID
            End Get
            Set(ByVal Value As Integer)
                _orderItemID = Value
            End Set
        End Property

        Public Property OrderID() As Integer
            Get
                Return _orderID
            End Get
            Set(ByVal Value As Integer)
                _orderID = Value
            End Set
        End Property

        Public Property PlanID() As Integer
            Get
                Return _planID
            End Get
            Set(ByVal Value As Integer)
                _planID = Value
            End Set
        End Property

        Public Property ServiceFee() As Decimal
            Get
                Return _serviceFee
            End Get
            Set(ByVal Value As Decimal)
                _serviceFee = Value
            End Set
        End Property

#End Region

    End Class

End Namespace
