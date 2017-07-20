<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EditReceipts.ascx.vb" Inherits="Ventrian.SubscriptionTools.EditReceipts" %>
<%@ Register TagPrefix="dnnsc" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<table cellpadding="0" cellspacing="0" width="100%">
<tr>
	<td align="left">
		<asp:Repeater ID="rptBreadCrumbs" Runat="server">
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
	<td height="5px" colspan="2"></td>
</tr>
</table>
<table width="100%" border="0">
	<tr>
		<td align="left" class="Normal">
			<asp:Label ID="lblSearch" Runat="server" resourcekey="Search" CssClass="SubHead">Search:</asp:Label><br>
			<asp:TextBox ID="txtUserName" Runat="server" />&nbsp;<asp:LinkButton ID="btnGo" Runat="server" ResourceKey="Go" CssClass="CommandButton" />&nbsp;<asp:LinkButton ID="btnClear" Runat="server" ResourceKey="Clear" CssClass="CommandButton" />
		</td>
		<td align="right" class="Normal">
			<asp:Label ID="lblRecordsPerPage" Runat="server" resourcekey="RecordsPage" CssClass="SubHead">Records Per Page:</asp:Label><br>
			<asp:DropDownList id="drpRecordsPerPage" Runat="server" AutoPostBack="True" CssClass="NormalTextBox">
				<asp:ListItem Value="10">10</asp:ListItem>
				<asp:ListItem Value="25">25</asp:ListItem>
				<asp:ListItem Value="50">50</asp:ListItem>
				<asp:ListItem Value="100">100</asp:ListItem>
				<asp:ListItem Value="250">250</asp:ListItem>
			</asp:DropDownList>
		</td>
	</tr>
</table>
<asp:datagrid id="grdReceipts" Border="0" CellPadding="4" width="100%" AutoGenerateColumns="false"
	runat="server" summary="Receipts Table" BorderStyle="None" BorderWidth="0px"
	GridLines="None">
	<Columns>
		<asp:TemplateColumn ItemStyle-Width="20">
			<ItemTemplate>
				<asp:HyperLink NavigateUrl='<%# EditUrl("ReceiptID", DataBinder.Eval(Container.DataItem, "ReceiptID").ToString(), "EditReceipt") %>' runat="server" ID="Hyperlink1">
				<asp:Image ImageUrl="~/images/edit.gif" AlternateText="Edit" runat="server" ID="Hyperlink1Image" resourcekey="Edit"/></asp:HyperLink>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn HeaderText="User" DataField="Username" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="100px" />
		<asp:BoundColumn HeaderText="Plan" DataField="Name" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="100px" />
		<asp:BoundColumn HeaderText="DateCreated" DataField="DateCreated" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:d}" ItemStyle-Width="100px" />
		<asp:BoundColumn HeaderText="DateStart" DataField="DateStart" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:d}" ItemStyle-Width="100px" />
		<asp:TemplateColumn>
			<ItemStyle Width="100px" />
			<HeaderTemplate>
				<asp:Label ID="lblDateEnd" Runat="server" ResourceKey="DateEnd.Header" CssClass="NormalBold" />
			</HeaderTemplate>
			<ItemTemplate>
				<span class="Normal"><%# GetDateEnd(Container.DataItem) %></span>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn HeaderText="Processor" DataField="Processor" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="100px" />
		<asp:TemplateColumn>
			<HeaderTemplate>
				<asp:Label ID="lblFee" Runat="server" ResourceKey="Fee.Header" CssClass="NormalBold" />
			</HeaderTemplate>
			<ItemTemplate>
				<span class="Normal"><%# GetServiceFee(Container.DataItem) %></span>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:datagrid>
<div align="center"><asp:Label ID="lblNoReceipts" Runat="server" CssClass="Normal" ResourceKey="NoReceipts" EnableViewState="False" Visible="False" /></div>
<dnnsc:PagingControl id="ctlPagingControl" runat="server"></dnnsc:PagingControl>
<p align="center">
	<asp:linkbutton id="cmdAddReceipt" resourcekey="AddReceipt" runat="server" cssclass="CommandButton" text="Add Receipt" causesvalidation="False" borderstyle="none" />
	&nbsp;
	<asp:linkbutton id="cmdReturnToModule" resourcekey="ReturnToModule" runat="server" cssclass="CommandButton" text="Return To Module" causesvalidation="False" borderstyle="none" />
</p>
