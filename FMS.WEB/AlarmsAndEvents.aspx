<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="AlarmsAndEvents.aspx.vb" Inherits="FMS.WEB.AlarmsAndEvents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ctl00_MainPane_Content_ASPxRoundPanel1_MainContent_ASPxPageControl1_ddlVehicles").change(function () {                
                $("#ctl00_ctl00_MainPane_Content_ASPxRoundPanel1_MainContent_ASPxPageControl1_btnRefresh").click();
            });
        })
    </script>
    <style>
        .ddlCenter {
            padding-left: 20px;
        }
    </style>
        
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0">
        <TabPages>
            <dx:TabPage Name="EventConfiguration" Text="Event Configuration">
                <ContentCollection>
                    <dx:ContentControl runat="server">                        
                        <dx:ASPxGridView ID="dgvEventConfiguration" DataSourceID="odsEventConfiguration" runat="server" AutoGenerateColumns="False" KeyFieldName="CAN_EventDefinitionID">
                            <Columns>
                                <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowDeleteButton="true" ShowEditButton="true" VisibleIndex="0" />
                                <dx:GridViewDataComboBoxColumn FieldName="VehicleID" VisibleIndex="0">
                                    <PropertiesComboBox DataSourceID="odsVehicles"  TextField="Name" ValueField="Name">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="Metric" VisibleIndex="1">
                                    <PropertiesComboBox DataSourceID="odsMetric" TextField="canMetric" ValueField="canMetric" >
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="Comparison" VisibleIndex="2">
                                    <PropertiesComboBox >
                                        <Items>
                                            <dx:ListEditItem Text="" />
                                            <dx:ListEditItem Text="<" />
                                            <dx:ListEditItem Text=">" />
                                            <dx:ListEditItem Text="<=" />
                                            <dx:ListEditItem Text=">=" />
                                            <dx:ListEditItem Text="!=" />
                                            <dx:ListEditItem Text="Like" />
                                            <dx:ListEditItem Text="=" />
                                        </Items>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn FieldName="TextValueField" Caption="Text" Visible="false">
                                    <EditFormSettings  Visible="True" VisibleIndex="3" />
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <div>
                            <asp:ObjectDataSource ID="odsEventConfiguration" runat="server" SelectMethod="GetCanEventDefinition" TypeName="FMS.Business.DataObjects.Can_EventDefinition">
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="odsVehicles" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.ApplicationVehicle">
                                <SelectParameters>
                                    <asp:SessionParameter SessionField="ApplicationId" DbType="Guid" Name="appplicationID"></asp:SessionParameter>
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="odsMetric" runat="server" SelectMethod="GetCanMessageList" TypeName="FMS.Business.DataObjects.Can_EventDefinition"></asp:ObjectDataSource>
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="EventOccurances" Text="Event Occurances">
                <ContentCollection>
                    <dx:ContentControl runat="server"></dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="AlertConfiguration" Text="Alert Configuration">
                <ContentCollection>
                    <dx:ContentControl runat="server"></dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="AlertOccurances" Text="Alert Occurances">
                <ContentCollection>
                    <dx:ContentControl runat="server"></dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</asp:Content>

