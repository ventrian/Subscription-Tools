Imports System.Web.UI.WebControls

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.Security
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Namespace Ventrian.SubscriptionTools

    Partial Public Class SignupRegister
        Inherits PortalModuleBase

#Region " Private Methods "

        Private Sub BindMessage()

            Dim message As String = Localization.GetString("Message", Me.LocalResourceFile)

            message = message.Replace("[REGISTERURL]", RegisterUrl)
            message = message.Replace("[LOGINURL]", LoginUrl)

            lblMessage.Text = message

        End Sub

        Private Function LoginUrl() As String

            If PortalSettings.LoginTabId <> -1 And Request.QueryString("override") Is Nothing Then
                ' user defined tab
                Return NavigateURL(PortalSettings.LoginTabId)
            Else
                ' admin tab
                Return NavigateURL("Login")
            End If

        End Function

        Private Function RegisterUrl() As String

            If PortalSettings.UserTabId <> -1 Then
                Return NavigateURL(PortalSettings.UserTabId)
            Else
                Return NavigateURL("Register")
            End If

        End Function

#End Region

#Region " Event Handlers "

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try

                BindMessage()

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

#End Region

    End Class

End Namespace