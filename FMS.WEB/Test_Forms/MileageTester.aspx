<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MileageTester.aspx.vb" Inherits="FMS.WEB.MileageTester" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <style type="text/css">
                .floatRight {
                    float: right;
                }

                table td {
                    padding: 5px;
                }

                .floatLeft {
                    float: left;
                    width: 269px;
                }
            </style>

            <h3>Mileage Tester</h3>

            <table class="floatLeft">
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="VIn Number:"></dx:ASPxLabel>
                    </td>
                    <td>

                        <dx:ASPxTextBox ID="txtVINNumber" runat="server" Width="170px"></dx:ASPxTextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Start Date:"></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="dateEditStartDate" runat="server"></dx:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="End Date:"></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="dateEditEndDate" runat="server"></dx:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dx:ASPxButton CssClass="floatRight" ID="btnSWearch" runat="server" Text="Search" Height="16px" Width="116px"></dx:ASPxButton>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="*Please note that you want to have the start date to be AFTER an odomoter reading or an exception will be caused. You probably want to have the start and end date 24 hrs apart."></dx:ASPxLabel>
                    </td>
                </tr>

                <%--<dx:ASPxTextBox ID="txtAnswer" Enabled="false" runat="server" Width="170px"></dx:ASPxTextBox>--%>
            </table>

            <dx:ASPxMemo CssClass="floatLeft" ID="memoAnswer" Enabled="false" runat="server" Height="329px" Width="615px"></dx:ASPxMemo>

        </div>
    </form>
</body>
</html>
