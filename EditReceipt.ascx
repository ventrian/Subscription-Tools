<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="EditReceipt.ascx.vb" Inherits="Ventrian.SubscriptionTools.EditReceipt" %>
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

<asp:Panel ID="pnlAddReceipt" Runat="server">
<dnn:sectionhead id="dshReceipt" cssclass="Head" runat="server" text="Receipt Settings" section="tblReceipt"
	resourcekey="ReceiptSettings" includerule="True"></dnn:sectionhead>
<TABLE id="tblReceipt" cellSpacing="0" cellPadding="2" width="525" summary="Receipt Settings Design Table"
	border="0" runat="server">
	<TR>
		<TD colSpan="3">
			<asp:label id="lblReceiptSettingsHelp" cssclass="Normal" runat="server" resourcekey="ReceiptSettingsDescription"
				enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
		<TD class="SubHead" width="150">
			<dnn:label id="plUserName" runat="server" resourcekey="UserName" suffix=":" controlname="txtUserName"></dnn:label></TD>
		<TD align="left" width="325">
			<asp:textbox id="txtUserName" cssclass="NormalTextBox" runat="server" maxlength="255" columns="30"
				width="325"></asp:textbox>
			<asp:requiredfieldvalidator id="valUserName" cssclass="NormalRed" runat="server" resourcekey="valUserName.ErrorMessage" controltovalidate="txtUserName"
				errormessage="<br>You Must Enter a Valid UserName" display="Dynamic"></asp:requiredfieldvalidator>
			<asp:CustomValidator ID="valUserNameExists" CssClass="NormalRed" Runat="server" ResourceKey="ValUserNameExists.ErrorMessage" ControlToValidate="txtUserName" 
				errormessage="<br>UserName does not exist" Display="Dynamic" />		
		</TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
		<TD class="SubHead" width="150">
			<dnn:label id="plSelectPlan" runat="server" resourcekey="SelectPlan" suffix=":" controlname="drpPlan"></dnn:label></TD>
		<TD align="left" width="325">
			<asp:dropdownlist id="drpSelectPlan" cssclass="NormalTextBox" runat="server" DataTextField="Name" DataValueField="PlanID" 
				width="325"></asp:dropdownlist></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
		<TD class="SubHead" width="150">
			<dnn:label id="plAddToRole" runat="server" resourcekey="AddToRole" suffix=":" controlname="chkAddToRole"></dnn:label></TD>
		<TD align="left" width="325">
			<asp:checkbox id="chkAddToRole" runat="server" Checked="True"></asp:checkbox>
		</TD>
	</TR>
	<tr valign="top">
		<td width="25"><img height="1" src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width="25" /></td>
		<td colspan="2">
		    <dnn:sectionhead id="dshAdvanced" cssclass="Head" runat="server" text="Advanced Settings" section="tblAddReceiptAdvanced"
	            resourcekey="AdvancedSettings" includerule="True"></dnn:sectionhead>
            <table id="tblAddReceiptAdvanced" cellspacing="0" cellpadding="2" width="525" summary="Advanced Receipt Settings Design Table" border="0" runat="server">
	        <tr>
		        <td class="SubHead" width="150">
			        <dnn:label id="plStartDate2" runat="server" resourcekey="plStartDate2" suffix=":" controlname="txtStartDate"></dnn:label></td>
		        <td align="left" width="325">    
		            <asp:textbox id="txtStartDate" CssClass="NormalTextBox" runat="server" width="150" maxlength="15"></asp:textbox>
	                <asp:hyperlink id="cmdStartDate" CssClass="CommandButton" runat="server" resourcekey="Calendar">Calendar</asp:hyperlink>&nbsp;<br /><asp:Label ID="Label1" Runat="server" EnableViewState="False" ResourceKey="StartDate2.Help"
		                CssClass="Normal" />
	                <asp:comparevalidator id="valStartDate" cssclass="NormalRed" runat="server" controltovalidate="txtStartDate"
		                errormessage="<br>Invalid start date!" operator="DataTypeCheck" type="Date" display="Dynamic" ResourceKey="valStartDate.ErrorMessage"></asp:comparevalidator>
                </td>
		    </tr>
		    <tr>
		        <td class="SubHead" width="150">
			        <dnn:label id="plProcessor2" runat="server" resourcekey="plProcessor" suffix=":" controlname="txtProcessor"></dnn:label></td>
		        <td align="left" width="325">    
		            <asp:textbox id="txtProcessor" CssClass="NormalTextBox" runat="server" width="150"></asp:textbox>
	            </td>
		    </tr>
		    <tr>
		        <td class="SubHead" width="150">
			        <dnn:label id="plTransactionID2" runat="server" resourcekey="plTransactionID" suffix=":" controlname="txtTransactionID"></dnn:label></td>
		        <td align="left" width="325">    
		            <asp:textbox id="txtTransactionID" CssClass="NormalTextBox" runat="server" width="150"></asp:textbox>
	            </td>
		    </tr>
		    </table>
		</td>
	</TR>
</TABLE>
</asp:Panel>

<asp:panel ID="pnlDetail" Runat="server">
	<dnn:sectionhead id="dshReceiptDetail" cssclass="Head" runat="server" text="Receipt Detail" section="tblReceiptDetail"
		resourcekey="ReceiptDetail" includerule="True"></dnn:sectionhead>
	<TABLE id="tblReceiptDetail" cellSpacing="0" cellPadding="2" width="525" summary="Receipt Detail Design Table"
		border="0" runat="server">
		<TR>
			<TD colSpan="3">
				<asp:label id="lblReceiptDetail" cssclass="Normal" runat="server" resourcekey="ReceiptDetailDescription"
					enableviewstate="False"></asp:label></TD>
		</TR>
		<TR vAlign="top">
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<TD class="SubHead" width="150">
				<dnn:label id="plUserName2" runat="server" resourcekey="UserName2" suffix=":" controlname="lblUserName"></dnn:label></TD>
			<TD align="left" width="325">
				<asp:Label ID="lblUserName" Runat="server" CssClass="Normal" />
			</TD>
		</TR>
		<TR vAlign="top">
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<TD class="SubHead" width="150">
				<dnn:label id="plPlan" runat="server" resourcekey="Plan" suffix=":" controlname="lblPlan"></dnn:label></TD>
			<TD align="left" width="325">
				<asp:Label ID="lblPlan" Runat="server" CssClass="Normal" />
			</TD>
		</TR>
		<TR vAlign="top">
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<TD class="SubHead" width="150">
				<dnn:label id="plCreateDate" runat="server" resourcekey="CreateDate" suffix=":" controlname="lblCreateDate"></dnn:label></TD>
			<TD align="left" width="325">
				<asp:Label ID="lblCreateDate" Runat="server" CssClass="Normal" />
			</TD>
		</TR>
		<TR vAlign="top">
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<TD class="SubHead" width="150">
				<dnn:label id="plStartDate" runat="server" resourcekey="StartDate" suffix=":" controlname="lblStartDate"></dnn:label></TD>
			<TD align="left" width="325">
				<asp:Label ID="lblStartDate" Runat="server" CssClass="Normal" />
			</TD>
		</TR>
		<TR vAlign="top">
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<TD class="SubHead" width="150">
				<dnn:label id="plEndDate" runat="server" resourcekey="EndDate" suffix=":" controlname="lblEndDate"></dnn:label></TD>
			<TD align="left" width="325">
				<asp:Label ID="lblEndDate" Runat="server" CssClass="Normal" />
			</TD>
		</TR>
		<TR vAlign="top">
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<TD class="SubHead" width="150">
				<dnn:label id="plProcessor" runat="server" resourcekey="Processor" suffix=":" controlname="lblProcessor"></dnn:label></TD>
			<TD align="left" width="325">
				<asp:Label ID="lblProcessor" Runat="server" CssClass="Normal" />
			</TD>
		</TR>
		<TR vAlign="top">
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<TD class="SubHead" width="150">
				<dnn:label id="plTransactionID" runat="server" resourcekey="TransactionID" suffix=":" controlname="lblTransactionID"></dnn:label></TD>
			<TD align="left" width="325">
				<asp:HyperLink ID="lnkTransactionID" runat="server" CssClass="Normal" Visible="false"></asp:HyperLink>
				<asp:Label ID="lblTransactionID" runat="server" CssClass="Normal" Visible="false"></asp:Label>
			</TD>
		</TR>
		<TR vAlign="top">
			<TD width="25"><IMG height=1 src='<%= Page.ResolveUrl("~/Images/Spacer.gif") %>' width=25></TD>
			<TD class="SubHead" width="150">
				<dnn:label id="plServiceFee" runat="server" resourcekey="ServiceFee" suffix=":" controlname="lblServiceFee"></dnn:label></TD>
			<TD align="left" width="325">
				<asp:Label ID="lblServiceFee" Runat="server" CssClass="Normal" />
			</TD>
		</TR>
	</TABLE>
</asp:panel>
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
