<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CANBusPropertyDisplay2.aspx.vb" Inherits="FMS.WEB.CANBusPropertyDisplay2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Repeater ID="repeater_main" runat="server" DataSourceID="odsCanMessages">

                <HeaderTemplate>
                    <table>
                </HeaderTemplate>


                <ItemTemplate>

                    <tr id="<%# Eval("Standard_SPN")%>">
                        <td class="Description"  > <%# Eval("Description")%> </td>
                        <td class="Description"  > <%# Eval("Standard_SPN")%> </td>
                        <td class="Value"  > Loading... </td>
                        <td class="Date"  > Loading... </td>
                    </tr>

                </ItemTemplate>


                <FooterTemplate></table></FooterTemplate>

            </asp:Repeater>

            <asp:ObjectDataSource runat="server" ID="odsCanMessages" SelectMethod="GetEmaxiCANMessages" TypeName="FMS.WEB.Controllers.VehicleController">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="DeviceID" Name="deviceID" Type="String"></asp:QueryStringParameter>
                </SelectParameters>
            </asp:ObjectDataSource>

        </div>
    </form>
</body>
</html>

