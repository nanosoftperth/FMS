<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CanBusDisplayProperty.aspx.vb" Inherits="FMS.WEB.CanBusDisplayProperty" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>CAN Value Viewer</title>
    <style>
        table {
            border-collapse: collapse;
        }

        table, td, th {
            border: 1px solid #C1C5C8;
        }

        .tdFont{
            font-family: "Segoe UI", Helvetica, "Droid Sans", Tahoma, Geneva, sans-serif;
            font-size:12px;
            font-stretch:normal;
            font-weight:normal;
            font-style:normal;
            padding-top: .1cm;
        }

        .Description {
            width:100px;
        }
        .Value {
            width:195px
        }
        .Date {
            width:112px
        }
    </style>
    <script src="Content/javascript/jquery-3.1.0.min.js"></script>    
    <script src="Content/javascript/CanBusDisplayProperty.js"></script>
    <script src="Content/javascript/DateFormat.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Repeater ID="repeater_main" runat="server" DataSourceID="odsCanMessages">

                <HeaderTemplate>
                    <table id="contentTable">
                </HeaderTemplate>


                <ItemTemplate>

                    <tr id="<%# Eval("Standard_SPN")%>" class="<%# Eval("Standard")%><%# Eval("spn")%>">
                        <td class="Description tdFont"> <%# Eval("Description")%> </td>
                        <td class="Value tdFont"  > Loading... </td>
                        <td class="Date tdFont"  > Loading... </td>
                    </tr>

                </ItemTemplate>

                <FooterTemplate></table></FooterTemplate>

            </asp:Repeater>

            <asp:ObjectDataSource runat="server" ID="odsCanMessages" SelectMethod="GetSortedEmaxiCANMessages" TypeName="FMS.WEB.Controllers.VehicleController">
                <SelectParameters>
                    <asp:QueryStringParameter QueryStringField="DeviceID" Name="deviceID" Type="String"></asp:QueryStringParameter>
                </SelectParameters>
            </asp:ObjectDataSource>

        </div>
        <div>
            <dx:ASPxPopupControl ID="myPopup" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="myPopup"
                HeaderText="Fault Code Information" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="370px" >        
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                            <PanelCollection>
                                <dx:PanelContent runat="server">             
                                    <p id="pMessage"></p>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>                
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>
        </div>
        <input type="hidden" id="deviceId"  value="<%= DeviceID%>"/>
        <input type="hidden" id="faultDesc"  value=""/>
    </form>
</body>
</html>
