<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DriverDetails.aspx.vb" Inherits="FMS.WEB.DriverDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Driver Details</title>
    <script src="../Content/javascript/jquery-1.10.2.min.js" ></script>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Driver Details" Font-Bold="True" Font-Size="Large"></dx:ASPxLabel>      
        </div>
        <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="false" 
            KeyFieldName="usersecID" DataSourceID="odsUsers" Width="100%">

        </dx:ASPxGridView>
    </form>
</body>
</html>
