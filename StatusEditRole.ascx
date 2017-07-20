<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="StatusEditRole.ascx.vb" Inherits="Ventrian.SubscriptionTools.StatusEditRole" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>
<table cellspacing="2" cellpadding="2" summary="Edit HTML Design Table" border="0">
	<tr valign="top">
		<td colspan="2"><dnn:texteditor id="teContent" runat="server" height="400" width="700"></dnn:texteditor></td>
	</tr>
	<tr>
		<td><asp:Label ID="lblTokens" Runat="server" ResourceKey="Tokens" CssClass="Normal" /></td>
	</tr>
</table>
<p>
	<asp:linkbutton class="CommandButton" id="cmdUpdate" resourcekey="cmdUpdate" runat="server" borderstyle="none"
		text="Update"></asp:linkbutton>&nbsp;
	<asp:linkbutton class="CommandButton" id="cmdCancel" resourcekey="cmdCancel" runat="server" borderstyle="none"
		text="Cancel" causesvalidation="False"></asp:linkbutton>&nbsp;
	<asp:linkbutton class="CommandButton" id="cmdClear" resourcekey="cmdClear" runat="server" borderstyle="none"
		text="Clear" causesvalidation="False"></asp:linkbutton>&nbsp;
</p>
