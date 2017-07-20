Imports System.Web.UI.WebControls

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Security
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Partial Public Class EditPlans
        Inherits ModuleBase


#Region " Private Members "

        Private _plans As New ArrayList

#End Region

#Region " Private Methods "

        Private Sub BindCrumbs()

            Dim crumbs As New ArrayList

            Dim objCrumbMain As New CrumbInfo
            objCrumbMain.Caption = Localization.GetString("Main", LocalResourceFile)
            objCrumbMain.Url = NavigateURL()
            crumbs.Add(objCrumbMain)

            Dim objCrumbEditPlans As New CrumbInfo
            objCrumbEditPlans.Caption = Localization.GetString("EditPlans", LocalResourceFile)
            objCrumbEditPlans.Url = EditUrl("EditPlans")
            crumbs.Add(objCrumbEditPlans)

            rptBreadCrumbs.DataSource = crumbs
            rptBreadCrumbs.DataBind()

        End Sub

        Private Sub BindPlans()

            Dim objPlanController As New PlanController

            DotNetNuke.Services.Localization.Localization.LocalizeDataGrid(grdPlans, Me.LocalResourceFile)

            _plans = objPlanController.List(Me.PortalId, Me.ModuleId)

            If (_plans.Count > 1) Then
                If (CType(_plans(0), PlanInfo).ViewOrder = 0 And CType(_plans(1), PlanInfo).ViewOrder = 0) Then
                    ' Sort Order must be messed up, fix now..
                    For i As Integer = 0 To _plans.Count - 1
                        Dim objPlan As PlanInfo = CType(_plans(i), PlanInfo)
                        objPlan.ViewOrder = i
                        objPlanController.Update(objPlan)
                    Next
                End If
            End If

            grdPlans.DataSource = _plans
            grdPlans.DataBind()

            If (grdPlans.Items.Count > 0) Then
                grdPlans.Visible = True
                lblNoPlans.Visible = False
            Else
                grdPlans.Visible = False
                lblNoPlans.Visible = True
                lblNoPlans.Text = DotNetNuke.Services.Localization.Localization.GetString("NoPlansMessage.Text", LocalResourceFile)
            End If

        End Sub

#End Region

#Region " Protected Methods "

        Protected Function GetEditUrl(ByVal planID As String) As String

            Return EditUrl("PlanID", planID, "EditPlan")

        End Function

#End Region

#Region " Event Handlers "

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try

                BindCrumbs()
                If (Page.IsPostBack = False) Then
                    BindPlans()
                End If

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub cmdAddPlan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddPlan.Click

            Try

                Response.Redirect(EditUrl("EditPlan"), True)

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub cmdReturnToSignup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReturnToSignup.Click

            Try

                Response.Redirect(NavigateURL(), True)

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub grdPlans_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdPlans.ItemDataBound

            If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then

                Dim btnUp As ImageButton = CType(e.Item.FindControl("btnUp"), ImageButton)
                Dim btnDown As ImageButton = CType(e.Item.FindControl("btnDown"), ImageButton)

                Dim objPlan As PlanInfo = CType(e.Item.DataItem, PlanInfo)

                If Not (btnUp Is Nothing And btnDown Is Nothing) Then

                    If (objPlan.PlanID = CType(_plans(0), PlanInfo).PlanID) Then
                        btnUp.Visible = False
                    End If

                    If (objPlan.PlanID = CType(_plans(_plans.Count - 1), PlanInfo).PlanID) Then
                        btnDown.Visible = False
                    End If

                    btnUp.CommandArgument = objPlan.PlanID.ToString()
                    btnUp.CommandName = "Up"

                    btnDown.CommandArgument = objPlan.PlanID.ToString()
                    btnDown.CommandName = "Down"

                End If

                Dim btnPublished As ImageButton = CType(e.Item.FindControl("btnPublished"), ImageButton)

                If Not (btnPublished Is Nothing) Then
                    If (objPlan.IsActive) Then
                        btnPublished.ImageUrl = "~/images/checked.gif"
                        btnPublished.CommandName = "PublishedChecked"
                    Else
                        btnPublished.ImageUrl = "~/images/cancel.gif"
                        btnPublished.CommandName = "PublishedCancel"
                    End If

                    btnPublished.CommandArgument = objPlan.PlanID.ToString()
                End If

            End If

        End Sub

        Private Sub grdPlans_ItemCommand(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdPlans.ItemCommand

            Dim objPlanController As New PlanController
            _plans = objPlanController.List(Me.PortalId, Me.ModuleId)

            Dim planID As Integer = Convert.ToInt32(e.CommandArgument)

            For i As Integer = 0 To _plans.Count - 1

                Dim objPlan As PlanInfo = CType(_plans(i), PlanInfo)

                If (planID = objPlan.PlanID) Then
                    If (e.CommandName = "Up") Then

                        Dim objPlanToSwap As PlanInfo = CType(_plans(i - 1), PlanInfo)

                        Dim sortOrder As Integer = objPlan.ViewOrder
                        Dim sortOrderPrevious As Integer = objPlanToSwap.ViewOrder

                        objPlan.ViewOrder = sortOrderPrevious
                        objPlanToSwap.ViewOrder = sortOrder

                        objPlanController.Update(objPlan)
                        objPlanController.Update(objPlanToSwap)

                    End If

                    If (e.CommandName = "Down") Then

                        Dim objPlanToSwap As PlanInfo = CType(_plans(i + 1), PlanInfo)

                        Dim sortOrder As Integer = objPlan.ViewOrder
                        Dim sortOrderNext As Integer = objPlanToSwap.ViewOrder

                        objPlan.ViewOrder = sortOrderNext
                        objPlanToSwap.ViewOrder = sortOrder

                        objPlanController.Update(objPlan)
                        objPlanController.Update(objPlanToSwap)

                    End If

                    If (e.CommandName = "PublishedChecked") Then
                        objPlan.IsActive = False
                        objPlanController.Update(objPlan)
                    End If

                    If (e.CommandName = "PublishedCancel") Then
                        objPlan.IsActive = True
                        objPlanController.Update(objPlan)
                    End If
                End If

            Next

            Response.Redirect(Request.RawUrl, True)

        End Sub

#End Region

    End Class

End Namespace