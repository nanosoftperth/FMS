<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CAN_Explorer.aspx.vb" Inherits="FMS.WEB.CAN_Explorer" %>

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

                        <%--APPLICATION--%>

                        <dx:ASPxComboBox ID="comboApplications" AutoPostBack="true" ValueField="ApplicationID" TextField="ApplicationName" runat="server" DataSourceID="odsApplications"></dx:ASPxComboBox>

                        <asp:ObjectDataSource runat="server" ID="odsApplications" SelectMethod="GetAllApplications" TypeName="FMS.Business.DataObjects.Application"></asp:ObjectDataSource>

                        <%--VEHICLE--%>

                        <dx:ASPxComboBox ID="comboVehicles" runat="server" AutoPostBack="true" ValueField="DeviceID" TextField="Name" DataSourceID="odsVehicles"></dx:ASPxComboBox>

                        <asp:ObjectDataSource runat="server" ID="odsVehicles" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.ApplicationVehicle">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="comboApplications" PropertyName="Value" DbType="Guid" Name="appplicationID"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>

                        <%--PGN--%>
                        <dx:ASPxComboBox ID="comboPGNs" ValueField="Standard_SPN" AutoPostBack="true" runat="server" Width="500px" DataSourceID="odsPGNs">
                            <Columns>
                                <dx:ListBoxColumn FieldName="Standard"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="PGN"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="Parameter_Group_Label"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="Acronym"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="SPN"></dx:ListBoxColumn>
                                <dx:ListBoxColumn FieldName="Description"></dx:ListBoxColumn>
                            </Columns>

                        </dx:ASPxComboBox>

                        <asp:ObjectDataSource runat="server" ID="odsPGNs" SelectMethod="GetCANMessageDefinitions" TypeName="FMS.Business.DataObjects.Device">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="comboVehicles" PropertyName="Value" Name="deviceID" Type="String"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>

                        <%--SPN--%>

                        <dx:ASPxGridView ID="gvCANValues" runat="server" DataSourceID="odsCANValues" AutoGenerateColumns="False">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="RawValue" VisibleIndex="1"></dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="ValueStr" VisibleIndex="2"></dx:GridViewDataTextColumn>

                                <dx:GridViewDataDateColumn FieldName="Time" VisibleIndex="0">
                                    <PropertiesDateEdit>
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>

                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>

                            </Columns>
                        </dx:ASPxGridView>


                        <asp:ObjectDataSource runat="server" ID="odsCANValues" SelectMethod="GetLast100Values" TypeName="FMS.Business.DataObjects.CanDataPoint">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="comboPGNs" PropertyName="Value" Name="Standard_SPN" Type="String"></asp:ControlParameter>
                                <asp:ControlParameter ControlID="comboVehicles" PropertyName="Value" Name="deviceID" Type="String"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </table>


        </div>
    </form>
</body>
</html>
