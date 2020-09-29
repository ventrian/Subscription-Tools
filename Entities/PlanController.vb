'
' Subscription Tools for DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2006
' by Ventrian ( support@ventrian.com ) ( http://www.ventrian.com )
'

Imports System
Imports System.Data

Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Framework

Imports Ventrian.SubscriptionTools.Data

Namespace Ventrian.SubscriptionTools.Entities

    Public Class PlanController

#Region " Public Methods "

        Public Function List(ByVal portalID As Integer, ByVal moduleID As Integer) As ArrayList

            Return CBO.FillCollection(DataProvider.Instance().ListPlan(portalID, moduleID), GetType(PlanInfo))

        End Function

        Public Function [Get](ByVal planID As Integer) As PlanInfo

            'Return CType(CBO.FillObject(DataProvider.Instance().GetPlan(planID), GetType(PlanInfo)), PlanInfo)
            Return CBO.FillObject(Of PlanInfo)(DataProvider.Instance().GetPlan(planID))

        End Function

        Public Function Add(ByVal objPlan As PlanInfo) As Integer

            Return CType(DataProvider.Instance().AddPlan(objPlan.PortalID, objPlan.ModuleID, objPlan.RoleID, objPlan.Name, objPlan.Description, objPlan.IsActive, objPlan.ViewOrder, objPlan.ServiceFee, objPlan.BillingFrequency, objPlan.BillingPeriod, objPlan.Currency, objPlan.EndDate, objPlan.AutoRenew), Integer)

        End Function

        Public Sub Update(ByVal objPlan As PlanInfo)

            DataProvider.Instance().UpdatePlan(objPlan.PlanID, objPlan.PortalID, objPlan.ModuleID, objPlan.RoleID, objPlan.Name, objPlan.Description, objPlan.IsActive, objPlan.ViewOrder, objPlan.ServiceFee, objPlan.BillingFrequency, objPlan.BillingPeriod, objPlan.Currency, objPlan.EndDate, objPlan.AutoRenew)

        End Sub

        Public Sub Delete(ByVal planID As Integer)

            DataProvider.Instance().DeletePlan(planID)

        End Sub

#End Region

    End Class

End Namespace
