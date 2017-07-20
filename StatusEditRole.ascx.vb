Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Exceptions

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Partial Public Class StatusEditRole
        Inherits PortalModuleBase

#Region " Private Members "

        Private _roleName As String = Null.NullString

#End Region

#Region " Private Properties "

        Private ReadOnly Property Content() As DotNetNuke.UI.UserControls.TextEditor
            Get
                Return CType(teContent, DotNetNuke.UI.UserControls.TextEditor)
            End Get
        End Property

#End Region

#Region " Private Methods "

        Private Sub ReadQueryString()

            If Not (Request("Role") Is Nothing) Then
                _roleName = Request("Role")
            End If

        End Sub

#End Region

#Region "Event Handlers"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                ReadQueryString()
                If Page.IsPostBack = False Then
                    Dim objContentController As New ContentController
                    Dim objContentInfo As New ContentInfo

                    objContentInfo = objContentController.Get(Me.ModuleId, _roleName.ToString())

                    If Not (objContentInfo Is Nothing) Then
                        Content.Text = objContentInfo.SettingValue
                    End If
                End If

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
            Try
                Response.Redirect(EditUrl("ViewOptions"), True)
            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdUpdate.Click
            Try
                Dim objContentController As New ContentController
                Dim objContentInfo As New ContentInfo

                objContentInfo = objContentController.Get(Me.ModuleId, _roleName.ToString())

                If (objContentInfo Is Nothing) Then
                    objContentInfo = New ContentInfo
                    objContentInfo.ModuleID = Me.ModuleId
                    objContentInfo.SettingName = _roleName.ToString()
                    objContentInfo.SettingValue = Content.Text
                    objContentController.Add(objContentInfo)
                Else
                    objContentInfo.ModuleID = Me.ModuleId
                    objContentInfo.SettingName = _roleName.ToString()
                    objContentInfo.SettingValue = Content.Text
                    objContentController.Update(objContentInfo)
                End If

                Response.Redirect(EditUrl("ViewOptions"), True)
            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try
        End Sub

        Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click

            Try
                Dim objContentController As New ContentController
                Dim objContentInfo As ContentInfo = objContentController.Get(Me.ModuleId, _roleName.ToString())

                If Not (objContentInfo Is Nothing) Then
                    objContentInfo.SettingValue = ""
                    objContentController.Update(objContentInfo)
                End If

                Response.Redirect(EditUrl("ViewOptions"), True)
            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

#End Region

    End Class

End Namespace