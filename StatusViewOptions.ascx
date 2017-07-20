<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="StatusViewOptions.ascx.vb" Inherits="Ventrian.SubscriptionTools.StatusViewOptions" %>
<%@ Import Namespace="DotNetNuke.Common" %>
<%@ Import Namespace="DotNetNuke.Common.Utilities" %>
<asp:Label Runat="server" ID="lblRoleInformation" ResourceKey="RoleInformation" CssClass="Normal" />
<asp:datagrid id="grdRoles" Border="0" CellPadding="4" CellSpacing="0" Width="100%" AutoGenerateColumns="false" EnableViewState="false" runat="server" summary="Pages Design Table">
	<Columns>
		<asp:TemplateColumn ItemStyle-Width="20">
			<ItemTemplate>
				<asp:HyperLink NavigateUrl='<%# NavigateURL(Me.TabID, "EditRole", "mid=" & Me.ModuleID.ToString(), "Role=" & DataBinder.Eval(Container.DataItem, "RoleName").ToString()) %>' runat="server" ID="Hyperlink1">
				<asp:Image ImageUrl="~/images/edit.gif" AlternateText="Edit" runat="server" ID="Hyperlink1Image" resourcekey="Edit"/></asp:HyperLink>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn HeaderText="Role" DataField="RoleName" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
		<asp:TemplateColumn ItemStyle-CssClass="Normal" HeaderText="Contains Content" HeaderStyle-Cssclass="NormalBold">
			<ItemTemplate>
				<%# IsContentDefined(DataBinder.Eval(Container.DataItem, "RoleName").ToString()) %>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:datagrid>
<hr>

<asp:Label Runat="server" ID="lblGenericInformation" ResourceKey="GenericInformation" CssClass="Normal" />
<p class="Normal" align="left">
	<a href='<%= NavigateURL(Me.TabID, "EditRole", "mid=" & Me.ModuleID.ToString(), "Role=Generic") %>'><asp:Image ImageUrl="~/images/edit.gif" AlternateText="Edit" runat="server" ID="Image1" resourcekey="Edit"/></a>
	&nbsp;&nbsp;<asp:Label Runat="server" ID="Label1" ResourceKey="GenericMessage" CssClass="Normal" />
</p>