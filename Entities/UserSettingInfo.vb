Namespace Ventrian.SubscriptionTools.Entities

    Public Class UserSettingInfo

#Region " Private Members "

        Dim _userSettingID As Integer
        Dim _userID As Integer
        Dim _settingName As String
        Dim _settingValue As String

#End Region

#Region " Public Properties "

        Public Property UserSettingID() As Integer
            Get
                Return _userSettingID
            End Get
            Set(ByVal Value As Integer)
                _userSettingID = Value
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

        Public Property SettingName() As String
            Get
                Return _settingName
            End Get
            Set(ByVal Value As String)
                _settingName = Value
            End Set
        End Property

        Public Property SettingValue() As String
            Get
                Return _settingValue
            End Get
            Set(ByVal Value As String)
                _settingValue = Value
            End Set
        End Property

#End Region

    End Class

End Namespace
