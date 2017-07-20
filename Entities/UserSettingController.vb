Imports System
Imports System.Data

Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Framework

Imports Ventrian.SubscriptionTools.Data

Namespace Ventrian.SubscriptionTools.Entities

    Public Class UserSettingController

#Region " Public Methods "

        Public Function [Get](ByVal userID As Integer, ByVal settingName As String) As UserSettingInfo

            Return CType(CBO.FillObject(DataProvider.Instance().GetUserSetting(userID, settingName), GetType(UserSettingInfo)), UserSettingInfo)

        End Function

        Public Function Add(ByVal objUserSetting As UserSettingInfo) As Integer

            Return CType(DataProvider.Instance().AddUserSetting(objUserSetting.UserID, objUserSetting.SettingName, objUserSetting.SettingValue), Integer)

        End Function

        Public Sub Update(ByVal objUserSetting As UserSettingInfo)

            DataProvider.Instance().UpdateUserSetting(objUserSetting.UserID, objUserSetting.SettingName, objUserSetting.SettingValue)

        End Sub

#End Region

    End Class

End Namespace
