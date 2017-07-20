<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Reports.ascx.vb" Inherits="Ventrian.SubscriptionTools.Reports" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
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
<table class="Settings" cellspacing="2" cellpadding="2" summary="Site Log Design Table" border="0" width="450">
	<tr vAlign="top">
    <td class="SubHead" width="150"><dnn:label id="plReportType" runat="server" controlname="cboReportType" suffix=":"></dnn:label></td>
		<td class="NormalBold" align="left" width="325">
			<asp:DropDownList ID="cboReportType" Runat="server" DataValueField="value" DataTextField="text" CssClass="NormalTextBox" AutoPostBack="True"></asp:DropDownList>
		</td>
	</tr>
	<tr runat="server" id="trStartDate">
    <td class="SubHead" width="150"><dnn:label id="plStartDate" runat="server" controlname="txtStartDate" suffix=":"></dnn:label></td>
		<td class="NormalBold" align="left" width="325">
			<asp:TextBox id="txtStartDate" CssClass="NormalTextBox" runat="server" width="120" Columns="20"></asp:TextBox>&nbsp;
			<asp:HyperLink id="cmdStartCalendar" resourcekey="Calendar" Runat="server" CssClass="CommandButton">Calendar</asp:HyperLink>
		</td>
	</tr>
	<tr runat="server" id="trEndDate">
    <td class="SubHead" width="150"><dnn:label id="plEndDate" runat="server" controlname="txtEndDate" suffix=":"></dnn:label></td>
		<td class="NormalBold" align="left" width="325">
			<asp:TextBox id="txtEndDate" CssClass="NormalTextBox" runat="server" width="120" Columns="20"></asp:TextBox>&nbsp;
			<asp:HyperLink id="cmdEndCalendar" resourcekey="Calendar" Runat="server" CssClass="CommandButton">Calendar</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td class="NormalBold" vAlign="top" align="center" colspan="2">
			<asp:LinkButton id="cmdDisplay" resourcekey="cmdDisplay" cssclass="CommandButton" Text="Display" runat="server" />&nbsp;&nbsp;
			<asp:LinkButton id="cmdCancel" resourcekey="cmdCancel" CssClass="CommandButton" runat="server">Cancel</asp:LinkButton>
		</td>
	</tr>
</table>
<br />
<asp:datagrid id="grdLog" Width="750" Runat="server" Border="0" CellPadding="4" CellSpacing="4" AutoGenerateColumns="true" HeaderStyle-CssClass="NormalBold" ItemStyle-CssClass="Normal" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" BorderStyle="None" BorderWidth="0px" GridLines="None">
	<ItemStyle HorizontalAlign="Center" CssClass="Normal" />
	<HeaderStyle HorizontalAlign="Center" CssClass="NormalBold" />
</asp:datagrid>
<div align="center">
	<asp:LinkButton ID="btnExport" Runat="server" Text="Export" ResourceKey="Export" CssClass="CommandButton" Visible="False" />
</div>
