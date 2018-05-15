<%@ Control Language="vb" AutoEventWireup="true" Inherits="FMS.WEB.ReminderForm" Codebehind="ReminderForm.ascx.vb" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web"	TagPrefix="dx" %>

<table class="dxscBorderSpacing" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(Me, 0, 0) %> style="width:100%; padding-bottom:15px;">
	<tr><td> 
		 <dx:ASPxListBox ID="lbItems" runat="server" Width="100%" style="padding-bottom:15px;"></dx:ASPxListBox>
	</td></tr>
</table>
<table class="dxscButtonTable" style="width: 100%" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(Me, 0, 0) %>>
	<tr>
		<td style="width:100%;"><dx:ASPxButton ID="btnDismissAll" runat="server" AutoPostBack="false"></dx:ASPxButton></td>
		<td class="dx-ar" style="width:80px;" <%= DevExpress.Web.Internal.RenderUtils.GetAlignAttributes(Me, "right", Nothing)%>>
            <dx:ASPxButton ID="btnDismiss" runat="server" Width="80px" AutoPostBack="false"></dx:ASPxButton></td>
	</tr>
	<tr>
		<td colspan="2" style="padding:8px 0 4px 0;"><dx:ASPxLabel ID="lblClickSnooze" runat="server"></dx:ASPxLabel></td>
	</tr>
	<tr>
		<td style="width:100%;padding-right:20px;"><dx:ASPxComboBox ID="cbSnooze" runat="server" Width="100%">
		</dx:ASPxComboBox></td>
		<td style="width:80px;"><dx:ASPxButton ID="btnSnooze" runat="server" Width="80px" AutoPostBack="false"></dx:ASPxButton></td>
	</tr>
</table>