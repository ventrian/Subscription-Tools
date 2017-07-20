Namespace Ventrian.SubscriptionTools.Entities

    Public Class ContentInfo

#Region " Private Members "

        Dim _contentID As Integer
        Dim _moduleID As Integer
        Dim _settingName As String
        Dim _settingValue As String

#End Region

#Region " Public Properties "

        Public Property ContentID() As Integer
            Get
                Return _contentID
            End Get
            Set(ByVal Value As Integer)
                _contentID = Value
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
