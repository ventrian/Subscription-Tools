Namespace Ventrian.SubscriptionTools.Entities

    Public Class ReceiptInfo

#Region " Private Members "

        Dim _receiptID As Integer
        Dim _portalID As Integer
        Dim _moduleID As Integer
        Dim _userID As Integer
        Dim _dateCreated As DateTime
        Dim _dateStart As DateTime
        Dim _dateEnd As DateTime
        Dim _status As String
        Dim _name As String
        Dim _description As String
        Dim _serviceFee As Decimal
        Dim _billingPeriod As Integer
        Dim _billingFrequency As Integer
        Dim _processor As String
        Dim _processorTxID As String
        Dim _currency As String
        Dim _autoRenew As Boolean

        Dim _firstName As String
        Dim _lastName As String
        Dim _userName As String
        Dim _email As String
        Dim _displayName As String

#End Region

#Region " Public Properties "

        Public Property ReceiptID() As Integer
            Get
                Return _receiptID
            End Get
            Set(ByVal Value As Integer)
                _receiptID = Value
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

        Public Property DateCreated() As DateTime
            Get
                Return _dateCreated
            End Get
            Set(ByVal Value As DateTime)
                _dateCreated = Value
            End Set
        End Property

        Public Property DateStart() As DateTime
            Get
                Return _dateStart
            End Get
            Set(ByVal Value As DateTime)
                _dateStart = Value
            End Set
        End Property

        Public Property DateEnd() As DateTime
            Get
                Return _dateEnd
            End Get
            Set(ByVal Value As DateTime)
                _dateEnd = Value
            End Set
        End Property

        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal Value As String)
                _status = Value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal Value As String)
                _description = Value
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

        Public Property BillingPeriod() As Integer
            Get
                Return _billingPeriod
            End Get
            Set(ByVal Value As Integer)
                _billingPeriod = Value
            End Set
        End Property

        Public ReadOnly Property BillingFrequencyType() As FrequencyType
            Get
                Return CType(System.Enum.Parse(GetType(FrequencyType), _billingFrequency.ToString()), FrequencyType)
            End Get
        End Property

        Public Property BillingFrequency() As Integer
            Get
                Return _billingFrequency
            End Get
            Set(ByVal Value As Integer)
                _billingFrequency = Value
            End Set
        End Property

        Public Property AutoRenew() As Boolean
            Get
                Return _autoRenew
            End Get
            Set(ByVal Value As Boolean)
                _autoRenew = Value
            End Set
        End Property

        Public ReadOnly Property Details() As String
            Get
                Select Case BillingFrequencyType
                    Case FrequencyType.OneTimeFee
                        Return "Permanent access."
                    Case FrequencyType.Day
                        Return BillingPeriod.ToString() & " " & BillingFrequencyType.ToString() & "(s) access."
                    Case FrequencyType.Week
                        Return BillingPeriod.ToString() & " " & BillingFrequencyType.ToString() & "(s) access."
                    Case FrequencyType.Month
                        Return BillingPeriod.ToString() & " " & BillingFrequencyType.ToString() & "(s) access."
                    Case FrequencyType.Year
                        Return BillingPeriod.ToString() & " " & BillingFrequencyType.ToString() & "(s) access."
                End Select
                Return ""
            End Get
        End Property

        Public Property Processor() As String
            Get
                Return _processor
            End Get
            Set(ByVal Value As String)
                _processor = Value
            End Set
        End Property

        Public Property ProcessorTxID() As String
            Get
                Return _processorTxID
            End Get
            Set(ByVal Value As String)
                _processorTxID = Value
            End Set
        End Property

        Public Property Currency() As String
            Get
                Return _currency
            End Get
            Set(ByVal value As String)
                _currency = value
            End Set
        End Property

        Public Property FirstName() As String
            Get
                Return _firstName
            End Get
            Set(ByVal Value As String)
                _firstName = Value
            End Set
        End Property

        Public Property LastName() As String
            Get
                Return _lastName
            End Get
            Set(ByVal Value As String)
                _lastName = Value
            End Set
        End Property

        Public Property UserName() As String
            Get
                Return _userName
            End Get
            Set(ByVal Value As String)
                _userName = Value
            End Set
        End Property

        Public Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal Value As String)
                _email = Value
            End Set
        End Property

        Public Property DisplayName() As String
            Get
                Return _displayName
            End Get
            Set(ByVal Value As String)
                _displayName = Value
            End Set
        End Property

#End Region

    End Class

End Namespace
