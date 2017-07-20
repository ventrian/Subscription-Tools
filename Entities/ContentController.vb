Imports System
Imports System.Data

Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Framework

Imports Ventrian.SubscriptionTools.Data

Namespace Ventrian.SubscriptionTools.Entities

    Public Class ContentController

#Region " Public Methods "

        Public Function [Get](ByVal moduleID As Integer, ByVal settingName As String) As ContentInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetContent(moduleID, settingName), GetType(ContentInfo)), ContentInfo)

        End Function


        Public Function Add(ByVal objContent As ContentInfo) As Integer

            Return CType(DataProvider.Instance().AddContent(objContent.ModuleID, objContent.SettingName, objContent.SettingValue), Integer)

        End Function

        Public Sub Update(ByVal objContent As ContentInfo)

            DataProvider.Instance().UpdateContent(objContent.ContentID, objContent.ModuleID, objContent.SettingName, objContent.SettingValue)

        End Sub

#End Region

    End Class

End Namespace
