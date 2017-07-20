<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="SignupPlan.ascx.vb" Inherits="Ventrian.SubscriptionTools.SignupPlan" %>
<asp:Panel ID="pnlPlans" Runat="server">
	<asp:Label id="litContent" Runat="server" CssClass="Normal"></asp:Label>
	<BR>
	<DIV class="SubHead">
		<asp:Label id="lblSelectPlan" Runat="server" CssClass="SubHead" ResourceKey="SelectPlan" EnableViewState="False"></asp:Label>
		<asp:RequiredFieldValidator id="valPlans" Runat="server" CssClass="NormalRed" ControlToValidate="lstPlans" ErrorMessage="You must select a Plan"
			ResourceKey="valPlans.ErrorMessage" Display="Dynamic"></asp:RequiredFieldValidator>
		<asp:CustomValidator ID="valPlansSelected" Runat="server" CssClass="NormalRed" ErrorMessage="You must select at least 1 Plan"	
			ResourceKey="valPlansSelected.ErrorMessage" Runat="server" Display="Dynamic" />
	</DIV>
	<asp:RadioButtonList id="lstPlans" Runat="server" CssClass="Normal"></asp:RadioButtonList>
	<asp:CheckBoxList id="lstPlansMultiple" Runat="server" CssClass="Normal"></asp:CheckBoxList>
	<BR>
	<DIV class="SubHead">
		<asp:Label id="lblSelectPaymentProcessor" Runat="server" CssClass="SubHead" ResourceKey="SelectPaymentProcessor"
			EnableViewState="False"></asp:Label>
		<asp:RequiredFieldValidator id="valProcessor" Runat="server" CssClass="NormalRed" ControlToValidate="lstProcessors"
			ErrorMessage="You must select a Processor" ResourceKey="valProcessor.ErrorMessage" Display="Dynamic"></asp:RequiredFieldValidator>
	</DIV>
	<asp:RadioButtonList id="lstProcessors" Runat="server" CssClass="Normal"></asp:RadioButtonList>
	<P align="center">
		<asp:linkbutton id="cmdSubscribe" resourcekey="cmdSubscribe" runat="server" cssclass="CommandButton"
			text="Subscribe" borderstyle="none"></asp:linkbutton></P>
</asp:Panel>
<asp:Label ID="lblNoPlans" Runat="server" CssClass="Normal" Visible="False" ResourceKey="NoPlans"></asp:Label>
