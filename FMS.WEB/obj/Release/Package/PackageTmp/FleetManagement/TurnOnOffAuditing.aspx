<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TurnOnOffAuditing.aspx.vb" Inherits="FMS.WEB.TurnOnOffAuditing" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <dx:ASPxCheckBox ID="cbxOnOff" runat="server" Text="Turn Off Auditing"></dx:ASPxCheckBox>
                </td>                
            </tr>
            <tr>
                <td>
                    <br />
                    <dx:ASPxButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"></dx:ASPxButton>
                </td>                
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
