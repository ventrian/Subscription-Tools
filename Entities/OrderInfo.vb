Namespace Ventrian.SubscriptionTools.Entities

    Public Class OrderInfo

#Region " Private Members "

        Dim _orderID As Integer
        Dim _portalID As Integer
        Dim _moduleID As Integer
        Dim _userID As Integer

#End Region

#Region " Public Properties "

        Public Property OrderID() As Integer
            Get
                Return _orderID
            End Get
            Set(ByVal Value As Integer)
                _orderID = Value
            End Set
        End Property

        Public Property PortalID() As Integer
            Get
                Return _portalID
            End Get
            Set(ByVal Value As Integer)
                _portalID = Value
            End Set
        End Property

        Public Property ModuleID() As Integer
            Get
                Return _moduleID
            End Get
            Set(ByVal Value As Integer)
                _moduleID = Value
            End Set
        End Property

        Public Property UserID() As Integer
            Get
                Return _userID
            End Get
            Set(ByVal Value As Integer)
                _userID = Value
            End Set
        End Property

#End Region

    End Class

End Namespace
