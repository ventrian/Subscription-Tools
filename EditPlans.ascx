<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EditPlans.ascx.vb" Inherits="Ventrian.SubscriptionTools.EditPlans" %>
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
<asp:datagrid id="grdPlans" Border="0" CellPadding="4" CellSpacing="0" Width="100%" AutoGenerateColumns="false" runat="server" summary="Plans Design Table" GridLines="None">
	<Columns>
		<asp:TemplateColumn ItemStyle-Width="20">
			<ItemTemplate>
				<asp:HyperLink NavigateUrl='<%# GetEditUrl(DataBinder.Eval(Container.DataItem,"PlanID").ToString()) %>' runat="server" ID="Hyperlink1">
				<asp:Image ImageUrl="~/images/edit.gif" AlternateText="Edit" runat="server" ID="Hyperlink1Image" resourcekey="Edit"/></asp:HyperLink>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn HeaderText="Name" DataField="Name" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
		<asp:BoundColumn HeaderText="Fee" DataField="Details" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
		<asp:TemplateColumn>
			<HeaderTemplate>
				<asp:Label ID="lblPublished" Runat="server" ResourceKey="Published.Header" CssClass="NormalBold" />
			</HeaderTemplate>
			<ItemStyle Width="60px" HorizontalAlign="Center" />
			<ItemTemplate>
				<asp:ImageButton ID="btnPublished" Runat="server" ImageUrl="~/Images/checked.gif" />
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn>
			<HeaderTemplate>
				<asp:Label ID="lblSortOrder" Runat="server" ResourceKey="SortOrder.Header" CssClass="NormalBold" />
			</HeaderTemplate>
			<ItemStyle Width="60px" HorizontalAlign="Center" />
			<ItemTemplate>
				<table cellpadding="0" cellspacing="0">
				<tr>
					<td width="16px"><asp:ImageButton ID="btnDown" Runat="server" ImageUrl="~/Images/dn.gif" /></td>
					<td width="16px"><asp:ImageButton ID="btnUp" Runat="server" ImageUrl="~/Images/up.gif" /></td>
				</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:datagrid>
<asp:Label ID="lblNoPlans" Runat="server" CssClass="Normal" />
<p>
	<asp:linkbutton id="cmdAddPlan" resourcekey="AddPlan" runat="server" cssclass="CommandButton" text="Add Plan" causesvalidation="False" borderstyle="none" />
	&nbsp;
	<asp:linkbutton id="cmdReturnToSignup" resourcekey="ReturnToSignup" runat="server" cssclass="CommandButton" text="Return To Signup" causesvalidation="False" borderstyle="none" />
</p>
