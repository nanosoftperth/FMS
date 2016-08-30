<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebForm1.aspx.vb" Inherits="FMS.WEB.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" Height="295px" Width="339px"><ContentCollection>
<dx:PopupControlContentControl runat="server">
    <dx:ASPxTimeEdit ID="ASPxTimeEdit1" runat="server" EnableTheming="True" Theme="SoftOrange">
        <ClearButton DisplayMode="Never" Visibility="Auto">
        </ClearButton>
    </dx:ASPxTimeEdit>
            </dx:PopupControlContentControl>
</ContentCollection>
        </dx:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
