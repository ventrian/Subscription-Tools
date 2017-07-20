<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EditPlan.ascx.vb" Inherits="Ventrian.SubscriptionTools.EditPlan" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<div align="left">
<table cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td align="left">
			<asp:Repeater ID="rptBreadCrumbs" Runat="server" EnableViewState="false">
				<ItemTemplate>
					<a href='<%# DataBinder.Eval(Container.DataItem, "Url") %>' class="NormalBold"><%# DataBinder.Eval(Container.DataItem, "Caption") %></a>
				</ItemTemplate>
				<SeparatorTemplate>
					&#187;
				</SeparatorTemplate>
			</asp:Repeater>
		</td>
	</tr>
	<tr>
		<td height="5" colspan="2"></td>
	</tr>
</table>
<table class="Settings" cellspacing="2" cellpadding="2" summary="Edit Plan Design Table"
	border="0">
	<tr>
		<td width="560" valign="top">
			<asp:panel id="pnlSettings" runat="server" cssclass="WorkPanel" visible="True">
				<dnn:sectionhead id="dshPlan" cssclass="Head" runat="server" text="Plan Settings" section="tblPlan"
					resourcekey="PlanSettings" includerule="True"></dnn:sectionhead>
				<TABLE id="tblPlan" cellSpacing="0" cellPadding="2" width="525" summary="Plan Settings Design Table"
					border="0" runat="server">
					<TR>
						<TD colSpan="2">
							<asp:label id="lblPlanSettingsHelp" cssclass="Normal" runat="server" resourcekey="PlanSettingsDescription"
								enableviewstate="False"></asp:label></TD>
					</TR>
					<TR vAlign="top">
						<TD class="SubHead" width="150">
							<dnn:label id="plName" runat="server" resourcekey="Name" suffix=":" controlname="txtName"></dnn:label></TD>
						<TD align="left" width="325">
							<asp:textbox id="txtName" cssclass="NormalTextBox" runat="server" maxlength="255" columns="30"
								width="325"></asp:textbox>
							<asp:requiredfieldvalidator id="valName" cssclass="NormalRed" runat="server" resourcekey="valName" controltovalidate="txtName"
								errormessage="<br>You Must Enter a Valid Name" display="Dynamic"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD class="SubHead" width="150">
							<dnn:label id="plDescription" runat="server" resourcekey="Description" suffix=":" controlname="txtDescription"></dnn:label></TD>
						<TD width="325">
							<asp:textbox id="txtDescription" cssclass="NormalTextBox" runat="server" maxlength="1000" columns="30"
								width="325" textmode="MultiLine" height="84px"></asp:textbox></TD>
					</TR>
					<TR>
						<TD class="SubHead" width="160">
							<dnn:label id="plViewOrder" runat="server" resourcekey="ViewOrder" suffix=":" controlname="txtViewOrder"></dnn:label></TD>
						<TD width="365">
							<asp:textbox id="txtViewOrder" cssclass="NormalTextBox" runat="server" maxlength="3" columns="30"
								width="300"></asp:textbox>
							<asp:regularexpressionvalidator id="valViewOrder" cssclass="NormalRed" runat="server" resourcekey="valViewOrder.ErrorMessage"
								controltovalidate="txtViewOrder" errormessage="View Order must be a Number or an Empty String" display="Dynamic"
								validationexpression="^\d*$"></asp:regularexpressionvalidator></TD>
					</TR>
					<tr valign="top">
						<td class="SubHead" width="150">
							<dnn:label id="plIsActive" runat="server" resourcekey="IsActive" suffix=":" controlname="chkIsActive"></dnn:label></td>
						<td width="325">
							<asp:CheckBox id="chkIsActive" Checked="True" Runat="server"></asp:CheckBox></td>
					</tr>
					<tr valign="top">
						<td class="SubHead" width="150">
							<dnn:label id="plCurrency" runat="server" resourcekey="Currency" suffix=":" controlname="drpCurrency"></dnn:label></td>
						<td width="325">
							<asp:dropdownlist id="drpCurrency" cssclass="NormalTextBox" datavaluefield="value" datatextfield="text"
					            width="150" runat="server" /></td>
					</tr>
				</TABLE>
				<br />
			</asp:panel>
			<asp:panel id="pnlSubscription" runat="server" cssclass="WorkPanel" visible="True">
				<dnn:sectionhead id="dshSubscription" cssclass="Head" runat="server" text="Subscription Settings"
					section="tblSubscription" resourcekey="SubscriptionSettings" includerule="True"></dnn:sectionhead>
				<TABLE id="tblSubscription" cellSpacing="0" cellPadding="2" width="525" summary="Subscription Settings Design Table"
					border="0" runat="server">
					<TR vAlign="top">
						<TD class="SubHead" width="150">
							<dnn:label id="plRole" runat="server" resourcekey="Role" suffix=":" controlname="drpRole"></dnn:label></TD>
						<TD align="left" width="325">
							<asp:DropDownList id="drpRole" Runat="server" DataValueField="RoleID" DataTextField="RoleName" CssClass="NormalTextBox"></asp:DropDownList>
							<asp:requiredfieldvalidator id="valRole" cssclass="NormalRed" runat="server" resourcekey="valRole" controltovalidate="drpRole"
								errormessage="<br>You Must Select A Role" display="Dynamic" InitialValue="-1"></asp:requiredfieldvalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD class="SubHead" width="150">
							<dnn:label id="plServiceFee" runat="server" resourcekey="ServiceFee" suffix=":" controlname="txtServiceFee"></dnn:label></TD>
						<TD width="325">
							<asp:textbox id="txtServiceFee" cssclass="NormalTextBox" runat="server" maxlength="50" columns="30"
								width="100"></asp:textbox>
							<asp:requiredfieldvalidator id="valServiceFee" cssclass="NormalRed" runat="server" resourcekey="valServiceFee"
								controltovalidate="txtServiceFee" errormessage="<br>Service Fee Value Is Required" display="Dynamic"></asp:requiredfieldvalidator>
							<asp:comparevalidator id="valServiceFee1" cssclass="NormalRed" runat="server" resourcekey="valServiceFee1"
								controltovalidate="txtServiceFee" errormessage="<br>Service Fee Value Entered Is Not Valid" display="Dynamic"
								type="Currency" operator="DataTypeCheck"></asp:comparevalidator>
							<asp:comparevalidator id="valServiceFee2" cssclass="NormalRed" runat="server" resourcekey="valServiceFee2"
								controltovalidate="txtServiceFee" errormessage="<br>Service Fee Must Be Greater Than or Equal to Zero"
								display="Dynamic" operator="GreaterThanEqual" valuetocompare="0"></asp:comparevalidator></TD>
					</TR>
					<TR vAlign="top">
						<TD class="SubHead" width="150">
							<dnn:label id="plPlanDuration" runat="server" resourcekey="PlanDuration" suffix=":" controlname="txtPlanDuration"></dnn:label></TD>
						<TD width="325">
							<asp:textbox id="txtPlanDuration" cssclass="NormalTextBox" runat="server" maxlength="50" columns="30"
								width="100"></asp:textbox>
							<asp:dropdownlist id="drpPlanDuration" cssclass="NormalTextBox" runat="server" width="200px" AutoPostBack="True" />
							<asp:requiredfieldvalidator id="valPlanDuration" cssclass="NormalRed" runat="server" resourcekey="valPlanDuration"
								controltovalidate="txtPlanDuration" errormessage="<br>Plan Duration Value Entered Is Not Valid" display="Dynamic"></asp:requiredfieldvalidator>
							<asp:requiredfieldvalidator id="valPlanDuration3" cssclass="NormalRed" runat="server" resourcekey="valPlanDuration"
								controltovalidate="drpPlanDuration" errormessage="<br>Plan Duration Value Entered Is Not Valid" display="Dynamic"
								InitialValue="-1"></asp:requiredfieldvalidator>
							<asp:comparevalidator id="valPlanDuration1" cssclass="NormalRed" runat="server" resourcekey="valPlanDuration1"
								controltovalidate="txtPlanDuration" errormessage="<br>Plan Duration Value Entered Is Not Valid" display="Dynamic"
								type="Integer" operator="DataTypeCheck"></asp:comparevalidator>
							<asp:comparevalidator id="valPlanDuration2" cssclass="NormalRed" runat="server" resourcekey="valPlanDuration2"
								controltovalidate="txtPlanDuration" errormessage="<br>Plan Duration Must Be Greater Than or Equal to Zero"
								display="Dynamic" operator="GreaterThan" valuetocompare="0"></asp:comparevalidator></TD>
					</TR>
					<tr valign="top" runat="server" id="trAutoRenew" visible="false">
						<td class="SubHead" width="150">
							<dnn:label id="plAutoRenew" runat="server" resourcekey="AutoRenew" suffix=":" controlname="chkAutoRenew"></dnn:label></td>
						<td width="325">
							<asp:CheckBox id="chkAutoRenew" Checked="False" Runat="server"></asp:CheckBox></td>
					</tr>
					<tr valign="top" runat="server" id="trEndDate" visible="false">
						<td class="SubHead" width="150">
							<dnn:label id="plEndDate" runat="server" resourcekey="EndDate" suffix=":" controlname="txtEndDate"></dnn:label></td>
						<td align="left" width="325">
						    <asp:textbox id="txtEndDate" CssClass="NormalTextBox" runat="server" width="150" maxlength="15"></asp:textbox>
	                        <asp:hyperlink id="cmdEndDate" CssClass="CommandButton" runat="server" resourcekey="Calendar">Calendar</asp:hyperlink>
	                        <asp:requiredfieldvalidator id="valEndDateRequired" cssclass="NormalRed" runat="server" resourcekey="valEndDateRequired"
								controltovalidate="txtEndDate" errormessage="<br>End Date Required" display="Dynamic"></asp:requiredfieldvalidator>
	                        <asp:comparevalidator id="valEndDate" cssclass="NormalRed" runat="server" controltovalidate="txtEndDate"
		                        errormessage="<br>Invalid end date!" operator="DataTypeCheck" type="Date" display="Dynamic" ResourceKey="valEndDate.ErrorMessage"></asp:comparevalidator>
						</td>
					</tr>
				</TABLE>
				<br />
				<dnn:sectionhead id="dshAdvancedPricing" cssclass="Head" runat="server" text="Advanced Pricing"
					section="tblAdvancedPricing" resourcekey="AdvancedPricing" includerule="True" isexpanded="True"></dnn:sectionhead>
				<table id="tblAdvancedPricing" cellspacing="0" cellpadding="2" width="525" summary="Advanced Pricing Design Table"
					border="0" runat="server">
					<tr>
					    <td colspan="2"><asp:Label ID="lblAdvancedPricing" runat="server" resourceKey="AdvancedPricingHelp" CssClass="Normal" /></td>
					</tr>
					<tr valign="top">
						<td class="SubHead" width="150" valign="middle">
							<dnn:label id="plPricing" runat="server" resourcekey="Pricing" suffix=":" controlname="drpRole"></dnn:label></td>
						<td align="left" width="325">
							<asp:Repeater ID="rptRoles" runat="server">
							    <HeaderTemplate>
							        <table width="100%">
							        <tr>
							            <td><asp:Label ID="lblRole" runat="server" ResourceKey="Role" CssClass="NormalBold" /></td>
							            <td><asp:Label ID="lblServiceFee" runat="server" ResourceKey="ServiceFee" CssClass="NormalBold" /></td>
							        </tr>
							    </HeaderTemplate>
							    <ItemTemplate>
							        <tr>
							            <td class="Normal"><%#DataBinder.Eval(Container.DataItem, "RoleName")%></td>
							            <td>
							                <asp:Label ID="lblRoleName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RoleName") %>' Visible="false" /> 
							                <asp:textbox id="txtServiceFee" cssclass="NormalTextBox" runat="server" maxlength="50" columns="30" width="100"></asp:textbox>
							            </td>
							        </tr>
							    </ItemTemplate>
							    <FooterTemplate>
							        </table>
							    </FooterTemplate>
							</asp:Repeater>
						</td>
					</tr>
				</table>
			</asp:panel>
		</td>
	</tr>
</table>
<p align="center">
	<asp:linkbutton id="cmdUpdate" resourcekey="cmdUpdate" runat="server" cssclass="CommandButton" text="Update"
		borderstyle="none" />
	&nbsp;
	<asp:linkbutton id="cmdCancel" resourcekey="cmdCancel" runat="server" cssclass="CommandButton" text="Cancel"
		causesvalidation="False" borderstyle="none" />
	&nbsp;
	<asp:linkbutton id="cmdDelete" resourcekey="cmdDelete" runat="server" cssclass="CommandButton" text="Delete"
		causesvalidation="False" borderstyle="none" />
</p>
</div>
