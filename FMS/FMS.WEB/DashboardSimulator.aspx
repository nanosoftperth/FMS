<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DashboardSimulator.aspx.vb" Inherits="FMS.WEB.DashboardSimulator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard Value Simulator Entry</title>
    <link href="Content/font_style.css" rel="stylesheet" />
    <style>
        .Label_style {
            text-align: right;
            font-family:"Windows Command Prompt Regular";
            width: 50%;
        }
        .Entry_text_style {
            text-align: left;
            font-family:"Windows Command Prompt Regular";
            width: 50%;
        }
        .tbl_style {
            width: 500px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="tbl_style">
            <tr>
                <td class="Label_style"><asp:Label ID="lblDevID" runat="server" Text="Device ID"></asp:Label></td>                
                <td class="Entry_text_style"><asp:textbox id="txtDevID" runat="server" Width="300px"></asp:textbox></td>
            </tr>  
            <tr>
                <td class="Label_style"><asp:Label ID="lblParking" runat="server" Text="Parking"></asp:Label></td>                
                <td class="Entry_text_style"><asp:textbox id="txtParking" runat="server" Width="300px"></asp:textbox></td>
            </tr>            
            <tr>
                <td class="Label_style"><asp:Label ID="lblDriving" runat="server" Text="Driving"></asp:Label></td>                
                <td class="Entry_text_style"><asp:textbox id="txtDriving" runat="server" Width="300px"></asp:textbox></td>
            </tr>
            <%--<tr>
                <td class="Label_style"><asp:Label ID="lblIFM" runat="server" Text="IFM"></asp:Label></td>                
                <td class="Entry_text_style"><asp:textbox id="txtIFM" runat="server" Width="300px"></asp:textbox></td>
            </tr>--%>            <%--<tr>
                <td class="Label_style"><asp:Label ID="lblCAN" runat="server" Text="CAN"></asp:Label></td>                
                <td class="Entry_text_style"><asp:textbox id="txtCAN" runat="server" Width="300px"></asp:textbox></td>
            </tr>--%>            <%--<trtxtLCD_Speed
                <td class="Label_style"><asp:Label ID="lblAlign" runat="server" Text="Alignment"></asp:Label></td>                
                <td class="Entry_text_style"><asp:textbox id="txtAlign" runat="server" Width="300px"></asp:textbox></td>
            </tr>--%>            <%--<tr>
                <td class="Label_style"><asp:Label ID="lblWarning" runat="server" Text="Warning"></asp:Label></td>                
                <td class="Entry_text_style"><asp:textbox id="txtWarning" runat="server" Width="300px"></asp:textbox></td>
            </tr>--%>            <%--<tr>
                <td class="Label_style"><asp:Label ID="lblStop" runat="server" Text="STOP"></asp:Label></td>                
                <td class="Entry_text_style"><asp:textbox id="txtStop" runat="server" Width="300px"></asp:textbox></td>
            </tr>--%>
            <tr>
                <td class="Label_style"><asp:Label ID="lblSpeed" runat="server" Text="Speed"></asp:Label></td>                
                <td class="Entry_text_style"><asp:textbox id="txtSpeed" runat="server" Width="300px"></asp:textbox></td>
            </tr>
            <tr>
                <td class="Label_style">
                    <asp:Label ID="lblbatery" runat="server" Text="Battery Voltage"></asp:Label><br />
                    <asp:Label ID="lblVoltageRange" runat="server" Text="(78.50 to 85)"></asp:Label>
                </td>                
                <td class="Entry_text_style"><asp:textbox id="txtBattery" runat="server" Width="300px"></asp:textbox></td>
            </tr>
        </table>

        <table class="tbl_style">
            <tr>
                <td colspan="2" style="text-align:center; background-color: black; color: white;">
                    <asp:Label ID="lvlLCDTitleTable" runat="server" Text="LCD"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Label_style"><asp:Label ID="lblLCD_Speed" runat="server" Text="Speed in cm/sec"></asp:Label></td>                
                <td class="Entry_text_style"><asp:textbox id="txtLCD_Speed" runat="server" Width="300px"></asp:textbox></td>
            </tr>
            <tr>
                <td class="Label_style"><asp:Label ID="lblLCD_hour" runat="server" Text="Operating hour counter"></asp:Label></td>                
                <td class="Entry_text_style"><asp:textbox id="txtLCD_hour" runat="server" Width="300px"></asp:textbox></td>
            </tr>
            <tr>
                <td class="Label_style"><asp:Label ID="lblLCD_SteeringProgram" runat="server" Text="Active steering program"></asp:Label></td>                
                <td class="Entry_text_style"><asp:textbox id="txtLCD_SteeringProgram" runat="server" Width="300px"></asp:textbox></td>
            </tr>
            <tr>
                <td class="Label_style">
                    <asp:Label ID="lblLCD_FaultCode" runat="server" Text="Fault Code"></asp:Label>
                    <%--<br />--%>                    <%--<asp:Label ID="lblDelimeter" runat="server" Text="(Delimeter ',' if more than one(1) fault codes)"></asp:Label>--%>
                </td>                
                <td class="Entry_text_style"><asp:textbox id="txtFaultCode" runat="server" Width="300px"></asp:textbox></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right;">
                    <asp:Button ID="btnGetDataFromAPI" runat="server" Text="Get Data From API" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click"/>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>

</html>
