
Imports System.Web.UI.WebControls

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Security
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Entities.Tabs

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Partial Public Class Status
        Inherits PortalModuleBase
        Implements IActionable

#Region " Private Methods "

        Private Sub BindRoleMessage()

            Dim objContentController As New ContentController
            Dim objContentInfo As ContentInfo

            Dim literal As New literal

            If (Request.IsAuthenticated = False) Then

                objContentInfo = objContentController.Get(Me.ModuleId, "Generic")

                If Not (objContentInfo Is Nothing) Then
                    literal.Text = Server.HtmlDecode(objContentInfo.SettingValue)
                Else
                    literal.Text = ""
                End If

                phControls.Controls.Add(literal)
                Return

            End If

            Dim objRoleController As New RoleController

            Dim lRoles As IList(Of DotNetNuke.Entities.Users.UserRoleInfo) = objRoleController.GetUserRoles(Me.UserInfo, True)
            For Each role As DotNetNuke.Entities.Users.UserRoleInfo In lRoles
                If PortalSecurity.IsInRole(role.RoleName) Then
                    objContentInfo = objContentController.Get(Me.ModuleId, role.RoleName)

                    If Not (objContentInfo Is Nothing) Then
                        If (objContentInfo.SettingValue <> "") Then
                            Dim val As String = Server.HtmlDecode(objContentInfo.SettingValue)

                            If (Request.IsAuthenticated) Then
                                val = val.Replace("[FULLNAME]", Me.UserInfo.DisplayName)
                                val = val.Replace("[USERNAME]", Me.UserInfo.Username)
                            End If

                            literal.Text = val
                            phControls.Controls.Add(literal)
                            Return
                        End If
                    End If
                End If
            Next

            'Dim arrRoles As String() = objRoleController.GetRolesByUser(Me.UserId, Me.PortalId)
            'For Each role As String In arrRoles
            '    If PortalSecurity.IsInRole(role) Then
            '        objContentInfo = objContentController.Get(Me.ModuleId, role)

            '        If Not (objContentInfo Is Nothing) Then
            '            If (objContentInfo.SettingValue <> "") Then
            '                Dim val As String = Server.HtmlDecode(objContentInfo.SettingValue)

            '                If (Request.IsAuthenticated) Then
            '                    val = val.Replace("[FULLNAME]", Me.UserInfo.DisplayName)
            '                    val = val.Replace("[USERNAME]", Me.UserInfo.Username)
            '                End If

            '                literal.Text = val
            '                phControls.Controls.Add(literal)
            '                Return
            '            End If
            '        End If
            '    End If
            'Next

            objContentInfo = objContentController.Get(Me.ModuleId, "Generic")

            If Not (objContentInfo Is Nothing) Then
                literal.Text = Server.HtmlDecode(objContentInfo.SettingValue)
            Else
                literal.Text = ""
            End If
            phControls.Controls.Add(literal)
            Return

        End Sub

#End Region

#Region " Event Handlers "

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            BindRoleMessage()

        End Sub

#End Region

#Region " Optional Interfaces "

        Public ReadOnly Property ModuleActions() As DotNetNuke.Entities.Modules.Actions.ModuleActionCollection Implements IActionable.ModuleActions
            Get
                Dim Actions As New ModuleActionCollection
                Actions.Add(GetNextActionID, "View Options", ModuleActionType.ContentOptions, "", "", EditUrl("ViewOptions"), False, SecurityAccessLevel.Edit, True, False)
                Return Actions
            End Get
        End Property

#End Region

    End Class

End Namespace