Imports System.Web.UI.WebControls

Imports DotNetNuke.Common
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Security
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Partial Public Class StatusViewOptions
        Inherits PortalModuleBase

#Region " Private Methods "

        Private Sub BindRoles()

            Dim objRoleController As New RoleController

            Localization.LocalizeDataGrid(grdRoles, Me.LocalResourceFile)

            grdRoles.DataSource = objRoleController.GetPortalRoles(Me.PortalId)
            grdRoles.DataBind()

        End Sub

#End Region

#Region " Protected Methods "

        Protected Function IsContentDefined(ByVal roleID As String) As String

            Dim objContentController As New ContentController
            Dim objContent As ContentInfo = objContentController.Get(Me.ModuleId, roleID)

            If (objContent Is Nothing) Then
                Return "False"
            Else
                If (objContent.SettingValue <> "") Then
                    Return "True"
                Else
                    Return "False"
                End If
            End If

        End Function

#End Region

#Region " Event Handlers "

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try
                If (IsPostBack = False) Then
                    BindRoles()
                End If
            Catch exc As Exception 'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

#End Region

    End Class

End Namespace