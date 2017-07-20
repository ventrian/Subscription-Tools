<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="SignupDetails.ascx.vb" Inherits="Ventrian.SubscriptionTools.SignupDetails" %>
<br />
<asp:Label ID="lblCurrent" runat="server" CssClass="Normal" /><br /><br />
<asp:Label ID="lblReceipts" Runat="server" CssClass="SubHead" ResourceKey="Receipts"></asp:Label>
<asp:datagrid id="grdReceipts" Border="0" CellPadding="4" CellSpacing="0" Width="100%" AutoGenerateColumns="false" runat="server" summary="Receipts Design Table">
	<Columns>
		<asp:BoundColumn HeaderText="Start Date" DataField="DateStart" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-Width="75px" DataFormatString="{0:d}" />
		<asp:TemplateColumn>
			<HeaderStyle CssClass="NormalBold" />
			<ItemStyle CssClass="Normal" Width="75px" />
			<HeaderTemplate>
				End Date
			</HeaderTemplate>
			<ItemTemplate>
				<%# FormatEndDate(Container.DataItem) %>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn HeaderText="Name" DataField="Name" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-Width="100px" />
		<asp:TemplateColumn HeaderText="Details" HeaderStyle-CssClass="NormalBold" ItemStyle-CssClass="Normal">
			<ItemTemplate>
				<%#FormatDetails(Container.DataItem)%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Amount" HeaderStyle-CssClass="NormalBold" ItemStyle-CssClass="Normal" ItemStyle-Width="75px">
			<ItemTemplate>
				<%#FormatAmount(Container.DataItem)%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn HeaderText="Status" DataField="Status" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" ItemStyle-Width="75px" />
		<asp:TemplateColumn HeaderText="View" HeaderStyle-CssClass="NormalBold" ItemStyle-CssClass="Normal">
			<ItemTemplate>
				<asp:HyperLink ID="HyperLink1" Runat="server" NavigateUrl='<%# GetInvoiceUrl(DataBinder.Eval(Container.DataItem, "ReceiptID").ToString()) %>'>
					<asp:Label ID="lblViewInvoice" Runat="server" ResourceKey="ViewInvoice" CssClass="Normal" />
				</asp:HyperLink>
			</ItemTemplate>
		</asp:TemplateColumn>

	</Columns>
</asp:datagrid>
<br />
<asp:Label ID=lblReminders Runat="server" CssClass="SubHead" ResourceKey="Reminders"></asp:Label><br />
<asp:CheckBox ID="chkReminder" Runat="server" AutoPostBack="True" Text="Do you want to be notified when a subscription is close to expiration?" CssClass="Normal" ResourceKey="ReminderNotification" />