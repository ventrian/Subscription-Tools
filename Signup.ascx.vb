
Imports System.Web.UI.WebControls

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.Security
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Services.Localization

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Partial Public Class Signup
        Inherits PortalModuleBase
        Implements IActionable

#Region " Private Members "

        Private _controlToLoad As String

#End Region

#Region " Private Methods "

        Private Sub ReadQueryString()

            If (Request.IsAuthenticated = False OrElse Me.UserId = Null.NullInteger) Then
                _controlToLoad = "signupRegister.ascx"
                Return
            End If

            If Not (Request("signupType") Is Nothing) Then

                ' Load the appropriate Control
                '
                Select Case Request("signupType").ToLower()

                    Case "details"
                        _controlToLoad = "signupDetails.ascx"

                    Case "invoice"
                        _controlToLoad = "ViewInvoice.ascx"

                    Case "plan"
                        _controlToLoad = "signupPlan.ascx"

                    Case Else

                        If (Me.Request.IsAuthenticated) Then

                            Dim objReceiptController As New ReceiptController
                            Dim objReceipts As ArrayList = objReceiptController.List(Me.PortalId, Me.ModuleId, Me.UserId, Null.NullString)

                            If (objReceipts.Count > 0) Then
                                _controlToLoad = "signupDetails.ascx"
                            Else
                                _controlToLoad = "signupPlan.ascx"
                            End If

                        Else
                            _controlToLoad = "signupRegister.ascx"
                        End If

                End Select

            Else

                ' Type parameter not found
                '
                If (Me.Request.IsAuthenticated) Then
                    Dim objReceiptController As New ReceiptController
                    Dim objReceipts As ArrayList = objReceiptController.List(Me.PortalId, Me.ModuleId, Me.UserId, Null.NullString)

                    If (objReceipts.Count > 0) Then
                        _controlToLoad = "signupDetails.ascx"
                    Else
                        _controlToLoad = "signupPlan.ascx"
                    End If
                Else
                    _controlToLoad = "signupRegister.ascx"
                End If

            End If

        End Sub

        Private Sub LoadControlType()

            Dim objPortalModuleBase As PortalModuleBase = CType(Me.LoadControl(_controlToLoad), PortalModuleBase)

            If Not (objPortalModuleBase Is Nothing) Then

                objPortalModuleBase.ModuleConfiguration = Me.ModuleConfiguration
                objPortalModuleBase.ID = System.IO.Path.GetFileNameWithoutExtension(_controlToLoad)
                phControls.Controls.Add(objPortalModuleBase)

            End If

        End Sub

#End Region

#Region " Event Handlers "

        Private Sub Page_Initialize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

            ReadQueryString()
            LoadControlType()

        End Sub

#End Region

#Region " Optional Interfaces "

        Public ReadOnly Property ModuleActions() As DotNetNuke.Entities.Modules.Actions.ModuleActionCollection Implements IActionable.ModuleActions
            Get
                Dim Actions As New ModuleActionCollection
                Actions.Add(GetNextActionID, Localization.GetString("EditPlans", Me.LocalResourceFile), ModuleActionType.ContentOptions, "", "", EditUrl("EditPlans"), False, SecurityAccessLevel.Edit, True, False)
                Actions.Add(GetNextActionID, Localization.GetString("EditReceipts", Me.LocalResourceFile), ModuleActionType.ContentOptions, "", "", EditUrl("EditReceipts"), False, SecurityAccessLevel.Edit, True, False)
                Actions.Add(GetNextActionID, Localization.GetString("SendReminders", Me.LocalResourceFile), ModuleActionType.ContentOptions, "", "", EditUrl("Reminders"), False, SecurityAccessLevel.Edit, True, False)
                Actions.Add(GetNextActionID, Localization.GetString("ViewOptions", Me.LocalResourceFile), ModuleActionType.ContentOptions, "", "", EditUrl("ViewOptions"), False, SecurityAccessLevel.Edit, True, False)
                Actions.Add(GetNextActionID, Localization.GetString("ViewReports", Me.LocalResourceFile), ModuleActionType.ContentOptions, "", "", EditUrl("ViewReports"), False, SecurityAccessLevel.Edit, True, False)
                Return Actions
            End Get
        End Property

#End Region

    End Class

End Namespace