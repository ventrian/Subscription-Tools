<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="SignupViewOptions.ascx.vb" Inherits="Ventrian.SubscriptionTools.SignupViewOptions" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>
<table cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td align="left">
			<asp:Repeater ID="rptBreadCrumbs" Runat="server">
				<ItemTemplate>
					<a href='<%# DataBinder.Eval(Container.DataItem, "Url") %>' class="NormalBold">
						<%# DataBinder.Eval(Container.DataItem, "Caption") %>
					</a>
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
<div align="left">
	<dnn:sectionhead id="dshSubscriptionSettings" cssclass="Head" runat="server" text="Subscription Settings"
		section="tblSubscriptionSettings" resourcekey="SubscriptionSettings" includerule="True"></dnn:sectionhead>
	<TABLE id="tblSubscriptionSettings" cellSpacing="0" cellPadding="2" width="100%" summary="Subscription Settings Details Design Table"
		border="0" runat="server">
		<TR>
			<TD colSpan="3">
				<asp:label id="lblSubscriptionSettingsHelp" cssclass="Normal" runat="server" resourcekey="SubscriptionSettingsHelp"
					enableviewstate="False"></asp:label></TD>
		</TR>
		<tr>
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<td class="SubHead" width="200"><dnn:label id="plAllowMultiSubscriptions" text="Multiple Subscriptions?" runat="server" controlname="chkAllowMultipleSubscriptions"></dnn:label></td>
			<td valign="top"><asp:checkbox id="chkAllowMultipleSubscriptions" Runat="server" CssClass="NormalTextBox"></asp:checkbox></td>
		</tr>
	</TABLE>
	<br>
	<dnn:sectionhead id="dshPaymentSettings" cssclass="Head" runat="server" text="Payment Settings" section="tblPaymentSettings"
		resourcekey="PaymentSettings" includerule="True"></dnn:sectionhead>
	<TABLE id="tblPaymentSettings" cellSpacing="0" cellPadding="2" width="100%" summary="Payment Settings Details Design Table"
		border="0" runat="server">
		<TR>
			<TD colSpan="3">
				<asp:label id="plPaymentSettingsHelp" cssclass="Normal" runat="server" resourcekey="PaymentSettingsHelp"
					enableviewstate="False"></asp:label></TD>
		</TR>
		<tr>
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<td class="SubHead" width="200"><dnn:label id="plUseLiveProcessor" text="Use Live Processor?" runat="server" controlname="chkUseLiveProcessor"></dnn:label></td>
			<td valign="top"><asp:checkbox id="chkUseLiveProcessor" Runat="server" CssClass="NormalTextBox"></asp:checkbox></td>
		</tr>
		<tr>
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<td class="SubHead" width="200"><dnn:label id="plProcessorUserID" text="Processor UserID" runat="server" controlname="txtProcessorUserID"></dnn:label></td>
			<td valign="top"><asp:TextBox id="txtProcessorUserID" Runat="server" CssClass="NormalTextBox" Width="300px"></asp:TextBox></td>
		</tr>
		<tr>
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<td class="SubHead" width="200"><dnn:label id="plCurrency" text="Currency:" controlname="drpCurrency" runat="server" /></td>
			<td><asp:dropdownlist id="drpCurrency" cssclass="NormalTextBox" datavaluefield="value" datatextfield="text"
					width="150" runat="server"></asp:dropdownlist></td>
		</tr>
		<tr>
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<td class="SubHead" width="200"><dnn:label id="plRequireShippingAddress" text="Require Shipping Address?" runat="server" controlname="chkRequireShippingAddress"></dnn:label></td>
			<td valign="top"><asp:checkbox id="chkRequireShippingAddress" Runat="server" CssClass="NormalTextBox"></asp:checkbox></td>
		</tr>
	</TABLE>
	<br>
	<dnn:sectionhead id="dshContentSettings" cssclass="Head" runat="server" text="Content Settings" section="tblContentSettings"
		resourcekey="ContentSettings" includerule="True"></dnn:sectionhead>
	<TABLE id="tblContentSettings" cellSpacing="0" cellPadding="2" width="100%" summary="Content Settings Details Design Table"
		border="0" runat="server">
		<TR>
			<TD colSpan="3">
				<asp:label id="plContentSettingsHelp" cssclass="Normal" runat="server" resourcekey="ContentSettingsHelp"
					enableviewstate="False"></asp:label></TD>
		</TR>
		<tr>
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<td class="SubHead" width="200"><dnn:label id="plIntro" text="Intro Text" runat="server" controlname="txtIntro"></dnn:label></td>
			<td valign="top"><dnn:texteditor id="txtIntro" runat="server" width="500px" height="400px"></dnn:texteditor></td>
		</tr>
	</TABLE>
	<br>
	<dnn:sectionhead id="dshInvoiceSettings" cssclass="Head" runat="server" text="Invoice Settings" section="tblInvoiceSettings"
		resourcekey="InvoiceSettings" includerule="True"></dnn:sectionhead>
	<TABLE id="tblInvoiceSettings" cellSpacing="0" cellPadding="2" width="100%" summary="Invoice Settings Details Design Table"
		border="0" runat="server">
		<TR>
			<TD colSpan="3">
				<asp:label id="lblInvoiceSettingsHelp" cssclass="Normal" runat="server" resourcekey="InvoiceSettingsHelp"
					enableviewstate="False"></asp:label></TD>
		</TR>
		<tr>
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<td class="SubHead" width="200"><dnn:label id="plSendInvoice" text="SendInvoice?" runat="server" controlname="chkSendInvoice"></dnn:label></td>
			<td valign="top"><asp:checkbox id="chkSendInvoice" Runat="server" CssClass="NormalTextBox"></asp:checkbox></td>
		</tr>
		<tr>
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<td class="SubHead" width="200"><dnn:label id="plInvoice" text="Invoice Template" runat="server" controlname="txtInvoice"></dnn:label></td>
			<td valign="top"><asp:textbox id="txtInvoice" runat="server" cssclass="NormalTextBox" width="500" rows="20" textmode="MultiLine"></asp:textbox></td>
		</tr>
	</TABLE>
	<br>
	<dnn:sectionhead id="dshInvoiceTokens" runat="server" cssclass="Head" includerule="True" isExpanded="false"
		resourcekey="InvoiceTokens" section="tblInvoiceTokens" text="InvoiceTokens" />
	<table id="tblInvoiceTokens" runat="server" cellspacing="0" cellpadding="2" width="525"
		summary="Invoice Tokens Design Table" border="0">
		<tr>
			<td colspan="3"><asp:label id="lblInvoiceTokensHelp" resourcekey="InvoiceTokensHelp" cssclass="Normal" runat="server"
					enableviewstate="False"></asp:label></td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[CITY]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblCity" resourcekey="City" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[COUNTRY]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblCountry" resourcekey="Country" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[DATE]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblDate" resourcekey="Date" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[DISPLAYNAME]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblDisplayName" resourcekey="DisplayName" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[EXPIRYDATE]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblExpiryDate" resourcekey="ExpiryDate" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[FIRSTNAME]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblFirstName" resourcekey="FirstName" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[FULLNAME]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblFullName" resourcekey="FullName" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[LASTNAME]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblLastName" resourcekey="LastName" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[PORTALNAME]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblPortalName" resourcekey="PortalName" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[POSTALCODE]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblPostalCode" resourcekey="PostalCode" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[QTY]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblQty" resourcekey="Qty" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[RECEIPTID]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblReceiptID" resourcekey="ReceiptID" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[REGION]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblRegion" resourcekey="Region" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[STATUS]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblStatus" resourcekey="Status" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[STREET]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblStreet" resourcekey="Street" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[SUBSCRIPTIONDESCRIPTION]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblSubscriptionDescription" resourcekey="SubscriptionDescription" cssclass="Normal" runat="server"
					enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[SUBSCRIPTIONNAME]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblSubscriptionName" resourcekey="SubscriptionName" cssclass="Normal" runat="server"
					enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[SUBSCRIPTIONPRICE]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblSubscriptionPrice" resourcekey="SubscriptionPrice" cssclass="Normal" runat="server"
					enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr valign="top">
			<TD width="25"></TD>
			<TD class="SubHead" width="200">[USERNAME]</TD>
			<td class="Normal" align="left" width="325">
				<asp:label id="lblUsername" resourcekey="Username" cssclass="Normal" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
	</table>
	<p align="center">
		<asp:linkbutton class="CommandButton" id="cmdUpdate" resourcekey="cmdUpdate" runat="server" text="Update"></asp:linkbutton>&nbsp;&nbsp;
		<asp:linkbutton class="CommandButton" id="cmdCancel" resourcekey="cmdCancel" runat="server" text="Cancel"></asp:linkbutton>
	</p>
</div>
