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
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.UI.Utilities

Imports Ventrian.SubscriptionTools.Entities

Namespace Ventrian.SubscriptionTools

    Partial Public Class EditPlan
        Inherits ModuleBase

#Region " Private Members "

        Private _planID As Integer = Null.NullInteger

#End Region

#Region " Private Methods "

        Private Sub ReadQueryString()

            If Not (Request("PlanID") Is Nothing) Then
                _planID = Convert.ToInt32(Request("PlanID"))
            End If

        End Sub

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

            Dim objCrumbEditPlan As New CrumbInfo
            objCrumbEditPlan.Caption = Localization.GetString("EditPlan", LocalResourceFile)
            objCrumbEditPlan.Url = EditUrl("PlanID", _planID.ToString(), "EditPlan")
            crumbs.Add(objCrumbEditPlan)

            rptBreadCrumbs.DataSource = crumbs
            rptBreadCrumbs.DataBind()

        End Sub

        Private Sub BindCurrency()

            Dim ctlList As New Lists.ListController
            'Dim colCurrency As Lists.ListEntryInfoCollection = ctlList.GetListEntryInfoCollection("Currency", "")
            Dim colCurrency As IEnumerable(Of DotNetNuke.Common.Lists.ListEntryInfo) = ctlList.GetListEntryInfoItems("Currency", "")

            drpCurrency.DataSource = colCurrency
            drpCurrency.DataBind()

            drpCurrency.Items.Insert(0, New ListItem(Localization.GetString("DefaultCurrency", Me.LocalResourceFile), ""))

        End Sub

        Private Sub BindRoles()

            Dim objRoleController As New RoleController
            drpRole.DataSource = objRoleController.GetRoles(Me.PortalId)

            drpRole.DataBind()
            drpRole.Items.Insert(0, New ListItem(Localization.GetString("SelectRole.Text", Me.LocalResourceFile), "-1"))

            'rptRoles.DataSource = objRoleController.GetPortalRoles(Me.PortalId)
            rptRoles.DataSource = objRoleController.GetRoles(Me.PortalId)
            rptRoles.DataBind()

        End Sub

        Private Sub BindFrequency()

            For Each value As Integer In System.Enum.GetValues(GetType(FrequencyType))
                Dim li As New ListItem
                li.Value = value.ToString()
                li.Text = System.Enum.GetName(GetType(FrequencyType), value)
                drpPlanDuration.Items.Add(li)
            Next

            drpPlanDuration.Items.Insert(0, New ListItem(Localization.GetString("SelectPlanDuration.Text", Me.LocalResourceFile), "-1"))
            txtPlanDuration.Visible = False

        End Sub

        Private Sub BindPlan()

            If (_planID = Null.NullInteger) Then

                cmdDelete.Visible = False

            Else

                cmdDelete.Visible = True
                ' ClientAPI.AddButtonConfirm(cmdDelete, "Are You Sure You Want To Delete This Plan?")

                Dim objPlanController As New PlanController
                Dim objPlan As PlanInfo = objPlanController.Get(_planID)

                If Not (objPlan Is Nothing) Then
                    txtName.Text = objPlan.Name
                    txtDescription.Text = objPlan.Description
                    txtViewOrder.Text = objPlan.ViewOrder.ToString()
                    chkIsActive.Checked = objPlan.IsActive

                    If Not (drpRole.Items.FindByValue(objPlan.RoleID.ToString()) Is Nothing) Then
                        drpRole.SelectedValue = objPlan.RoleID.ToString()
                    End If

                    If Not (drpCurrency.Items.FindByValue(objPlan.Currency) Is Nothing) Then
                        drpCurrency.SelectedValue = objPlan.Currency
                    End If

                    txtServiceFee.Text = objPlan.ServiceFee.ToString("N2")

                    If Not (drpPlanDuration.Items.FindByValue(objPlan.BillingFrequency.ToString()) Is Nothing) Then
                        drpPlanDuration.SelectedValue = objPlan.BillingFrequency.ToString()
                        ShowPlanDuration()
                        If (txtPlanDuration.Visible) Then
                            txtPlanDuration.Text = objPlan.BillingPeriod.ToString()
                        End If
                    End If

                    If (objPlan.EndDate <> Null.NullDate) Then
                        txtEndDate.Text = objPlan.EndDate.ToShortDateString()
                    End If
                    chkAutoRenew.Checked = objPlan.AutoRenew

                Else

                    Response.Redirect(NavigateURL("EditPlan"), True)

                End If

            End If

        End Sub

        Private Sub ShowPlanDuration()

            txtPlanDuration.Visible = Not (drpPlanDuration.SelectedValue = "1" Or drpPlanDuration.SelectedValue = "6" Or drpPlanDuration.SelectedIndex = 0)
            valPlanDuration.Enabled = Not (drpPlanDuration.SelectedValue = "1" Or drpPlanDuration.SelectedValue = "6" Or drpPlanDuration.SelectedIndex = 0)
            valPlanDuration1.Enabled = Not (drpPlanDuration.SelectedValue = "1" Or drpPlanDuration.SelectedValue = "6" Or drpPlanDuration.SelectedIndex = 0)
            trEndDate.Visible = (drpPlanDuration.SelectedValue = "6")
            trAutoRenew.Visible = (drpPlanDuration.SelectedValue = "2" Or drpPlanDuration.SelectedValue = "3" Or drpPlanDuration.SelectedValue = "4" Or drpPlanDuration.SelectedValue = "5")

        End Sub

#End Region

#Region " Event Handlers "

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            cmdEndDate.NavigateUrl = DotNetNuke.Common.Utilities.Calendar.InvokePopupCal(txtEndDate)

            ReadQueryString()
            BindCrumbs()

            If (IsPostBack = False) Then

                BindRoles()
                BindFrequency()
                BindCurrency()
                BindPlan()

            End If

        End Sub

        Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click

            If (Page.IsValid) Then

                Dim objPlanController As New PlanController
                Dim objPlan As New PlanInfo

                objPlan.ModuleID = Me.ModuleId
                objPlan.PortalID = Me.PortalId

                objPlan.Name = txtName.Text
                objPlan.Description = txtDescription.Text
                If (txtViewOrder.Text <> "") Then
                    objPlan.ViewOrder = Convert.ToInt32(txtViewOrder.Text)
                Else
                    objPlan.ViewOrder = 0
                End If
                objPlan.IsActive = chkIsActive.Checked

                objPlan.RoleID = Convert.ToInt32(drpRole.SelectedValue)
                objPlan.ServiceFee = Convert.ToDecimal(txtServiceFee.Text)
                objPlan.BillingFrequency = Convert.ToInt32(drpPlanDuration.SelectedValue)
                If (txtPlanDuration.Text.Length > 0) Then
                    objPlan.BillingPeriod = Convert.ToInt32(txtPlanDuration.Text)
                End If
                objPlan.Currency = drpCurrency.SelectedValue
                If (trEndDate.Visible) Then
                    If (txtEndDate.Text <> "") Then
                        Try
                            objPlan.EndDate = Convert.ToDateTime(txtEndDate.Text)
                        Catch
                            objPlan.EndDate = Null.NullDate
                        End Try
                    Else
                        objPlan.EndDate = Null.NullDate
                    End If
                Else
                    objPlan.EndDate = Null.NullDate
                End If
                objPlan.AutoRenew = chkAutoRenew.Checked

                If (_planID = Null.NullInteger) Then
                    objPlan.PlanID = objPlanController.Add(objPlan)
                Else
                    objPlan.PlanID = _planID
                    objPlanController.Update(objPlan)
                End If

                Dim advancedPricing As String = ""

                For Each item As RepeaterItem In rptRoles.Items
                    If (item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem) Then

                        Dim txtServiceFee As TextBox = CType(item.FindControl("txtServiceFee"), TextBox)
                        Dim lblRoleName As Label = CType(item.FindControl("lblRoleName"), Label)

                        If (txtServiceFee IsNot Nothing And lblRoleName IsNot Nothing) Then
                            Dim roleName As String = lblRoleName.Text
                            If (txtServiceFee.Text <> "" AndAlso IsNumeric(txtServiceFee.Text)) Then
                                If (advancedPricing = "") Then
                                    advancedPricing = lblRoleName.Text & "-" & txtServiceFee.Text
                                Else
                                    advancedPricing = advancedPricing & ";" & lblRoleName.Text & "-" & txtServiceFee.Text
                                End If
                            End If
                        End If
                    End If
                Next

                If (advancedPricing <> "") Then
                    Dim objModuleController As New ModuleController()
                    objModuleController.UpdateModuleSetting(Me.ModuleId, "ST-" & objPlan.PlanID.ToString(), advancedPricing)
                End If

                Response.Redirect(EditUrl("EditPlans"), True)

            End If

        End Sub

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

            Response.Redirect(EditUrl("EditPlans"), True)

        End Sub

        Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

            Dim objPlanController As New PlanController
            objPlanController.Delete(_planID)

            Response.Redirect(EditUrl("EditPlans"), True)

        End Sub

        Private Sub drpPlanDuration_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles drpPlanDuration.SelectedIndexChanged

            ShowPlanDuration()

        End Sub

        Private Sub rptRoles_ItemDataBound(ByVal sender As System.Object, ByVal e As RepeaterItemEventArgs) Handles rptRoles.ItemDataBound

            If (_planID <> Null.NullInteger) Then

                If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then

                    Dim objRole As RoleInfo = CType(e.Item.DataItem, RoleInfo)

                    Dim txtServiceFee As TextBox = CType(e.Item.FindControl("txtServiceFee"), TextBox)

                    If (txtServiceFee IsNot Nothing) Then
                        If (Settings.Contains("ST-" & _planID.ToString())) Then
                            Dim pricing As String = Settings("ST-" & _planID.ToString()).ToString()

                            If (pricing <> "") Then
                                For Each p As String In pricing.Split(";"c)
                                    If (p.Split("-"c).Length = 2) Then
                                        If (objRole.RoleName.ToLower() = p.Split("-"c)(0).ToLower()) Then
                                            txtServiceFee.Text = p.Split("-"c)(1)
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    End If

                End If

            End If

        End Sub

#End Region

    End Class

End Namespace