'
' Subscription Tools for DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2006
'
Imports DotNetNuke.Services.Localization

Namespace Ventrian.SubscriptionTools.Entities

    Public Class PlanInfo

#Region " Private Members "

        Dim _planID As Integer
        Dim _roleID As Integer
        Dim _portalID As Integer
        Dim _moduleID As Integer
        Dim _name As String
        Dim _description As String
        Dim _currency As String
        Dim _isActive As Boolean
        Dim _viewOrder As Integer
        Dim _serviceFee As Decimal
        Dim _billingFrequency As Integer
        Dim _billingPeriod As Integer
        Dim _endDate As DateTime
        Dim _autoRenew As Boolean

#End Region

#Region " Public Properties "

        Public Property PlanID() As Integer
            Get
                Return _planID
            End Get
            Set(ByVal Value As Integer)
                _planID = Value
            End Set
        End Property

        Public Property RoleID() As Integer
            Get
                Return _roleID
            End Get
            Set(ByVal Value As Integer)
                _roleID = Value
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

        Public Property Currency() As String
            Get
                Return _currency
            End Get
            Set(ByVal Value As String)
                _currency = Value
            End Set
        End Property

        Public Property IsActive() As Boolean
            Get
                Return _isActive
            End Get
            Set(ByVal Value As Boolean)
                _isActive = Value
            End Set
        End Property

        Public Property ViewOrder() As Integer
            Get
                Return _viewOrder
            End Get
            Set(ByVal Value As Integer)
                _viewOrder = Value
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

        Public Property BillingFrequency() As Integer
            Get
                Return _billingFrequency
            End Get
            Set(ByVal Value As Integer)
                _billingFrequency = Value
            End Set
        End Property

        Public ReadOnly Property BillingFrequencyType() As FrequencyType
            Get
                Return CType(System.Enum.Parse(GetType(FrequencyType), _billingFrequency.ToString()), FrequencyType)
            End Get
        End Property

        Public Property BillingPeriod() As Integer
            Get
                Return _billingPeriod
            End Get
            Set(ByVal Value As Integer)
                _billingPeriod = Value
            End Set
        End Property

        Public Property EndDate() As DateTime
            Get
                Return _endDate
            End Get
            Set(ByVal Value As DateTime)
                _endDate = Value
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

                Dim localResourceFile As String = DotNetNuke.Common.Globals.ApplicationPath & "/DesktopModules/SubscriptionSignup/App_LocalResources/signupPlan"
                Dim _details As String = ""

                If (BillingFrequencyType = FrequencyType.OneTimeFee) Then
                    _details = Localization.GetString("PermanentAccess", localResourceFile)
                    If (_details.Length > 0) Then
                        _details = _details.Replace("[FEE]", ServiceFee.ToString("##0.00"))
                    End If
                Else
                    If (BillingFrequencyType = FrequencyType.FixedEndDate) Then
                        _details = Localization.GetString("FixedEndDate", localResourceFile)
                        If (_details.Length > 0) Then
                            _details = _details.Replace("[FEE]", ServiceFee.ToString("##0.00"))
                            _details = _details.Replace("[PERIOD]", EndDate.ToString("MMM, dd yyyy"))
                        End If
                    Else
                        If (BillingPeriod = 1) Then
                            _details = Localization.GetString("FrequencyAccessSingular", localResourceFile)
                        Else
                            _details = Localization.GetString("FrequencyAccessPlural", localResourceFile)
                        End If
                        If (_details.Length > 0) Then
                            _details = _details.Replace("[FEE]", ServiceFee.ToString("##0.00"))
                            _details = _details.Replace("[PERIOD]", BillingPeriod.ToString())
                            _details = _details.Replace("[FREQUENCY]", BillingFrequencyType.ToString())
                        End If
                    End If
                End If

                    Return _details
            End Get
        End Property

#End Region

    End Class

End Namespace
