<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CANData.aspx.vb" Inherits="FMS.WEB.CANData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>CANBUS Data (raw) from all devices so far</h1>

            <dx:ASPxGridView ID="ASPxGridView1" runat="server" Theme="SoftOrange" DataSourceID="odsCANBUS2" EnableTheming="True" AutoGenerateColumns="False">
                <SettingsPager Visible="False" Mode="ShowAllRecords"></SettingsPager>

                <Settings ShowFilterRow="True" ShowGroupPanel="True"></Settings>
                <SettingsDataSecurity AllowEdit="False" AllowInsert="False" AllowDelete="False"></SettingsDataSecurity>
                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="ArbritrationID" VisibleIndex="0"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Data" VisibleIndex="1"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DLC" VisibleIndex="2"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataCheckColumn FieldName="is_extended_id" VisibleIndex="3"></dx:GridViewDataCheckColumn>
                    <dx:GridViewDataCheckColumn FieldName="is_error_frame" VisibleIndex="4"></dx:GridViewDataCheckColumn>
                    <dx:GridViewDataCheckColumn FieldName="is_remote_frame" VisibleIndex="5"></dx:GridViewDataCheckColumn>
                    <dx:GridViewDataDateColumn   PropertiesDateEdit-DisplayFormatString="g" PropertiesDateEdit-EditFormat="DateTime" FieldName="TimeStamp" VisibleIndex="6">
                        <PropertiesDateEdit>
                            <TimeSectionProperties>
                                <TimeEditProperties>
                                    <ClearButton Visibility="Auto"></ClearButton>
                                </TimeEditProperties>
                            </TimeSectionProperties>

                            <ClearButton Visibility="Auto"></ClearButton>
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="Id" VisibleIndex="7"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DeviceID" VisibleIndex="8"></dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>

            <asp:ObjectDataSource runat="server" ID="odsCANBUS2" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.CANData"></asp:ObjectDataSource>

        </div>
    </form>
</body>
</html>
