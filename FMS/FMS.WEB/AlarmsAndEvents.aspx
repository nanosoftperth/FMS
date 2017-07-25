<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="AlarmsAndEvents.aspx.vb" Inherits="FMS.WEB.AlarmsAndEvents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ctl00_MainPane_Content_ASPxRoundPanel1_MainContent_ASPxPageControl1_ddlVehicles").change(function () {                
                $("#ctl00_ctl00_MainPane_Content_ASPxRoundPanel1_MainContent_ASPxPageControl1_btnRefresh").click();
            });

            if ($('#hdnValidate').value == "true")
                alert('true');
        })


    </script>
    <style>
        .ddlCenter {
            padding-left: 20px;
        }
        .Hide { display:none; }
    </style>    
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0">
        <TabPages>
            <dx:TabPage Name="EventConfiguration" Text="Event Configuration">
                <ContentCollection>
                    <dx:ContentControl runat="server">     
                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" DataSourceID="odsEventConfiguration" AutoGenerateColumns="False" 
                            KeyFieldName="CAN_EventDefinitionID" Width="550px" OnRowValidating="ASPxGridView1_RowValidating"
                            OnStartRowEditing="ASPxGridView1_StartRowEditing">
                            <Columns>
                                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="CAN_EventDefinitionID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                
                                <dx:GridViewDataTextColumn FieldName="VehicleID" VisibleIndex="9"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Metric" VisibleIndex="10"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Comparison" VisibleIndex="11"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="QueryType" VisibleIndex="12" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TextValue" VisibleIndex="13" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MetricValue" VisibleIndex="14" Visible="false"></dx:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowPreview="true" />
                            <SettingsPager PageSize="5" />
                            <EditFormLayoutProperties ColCount="8" >
                                <Items>
                                    <dx:GridViewColumnLayoutItem ColumnName="VehicleID" ColSpan="2" Caption="Vehicle" RequiredMarkDisplayMode="Required" >
                                        <Template>
                                            <dx:ASPxComboBox ID="ddlVehicleList" runat="server" Width="80px" DataSourceID="odsVehicles" Value='<%# Bind("VehicleID")%>' TextField="name" ValueField="name" />
                                        </Template>
                                    </dx:GridViewColumnLayoutItem>

                                    <dx:GridViewColumnLayoutItem ColumnName="Metric" ColSpan="2" Caption="Parameter" RequiredMarkDisplayMode="Required" >
                                        <Template>
                                            <dx:ASPxComboBox ID="ddlMetric" runat="server" Width="100px" DataSourceID="odsMetric" Value='<%# Bind("Metric")%>' TextField="canMetricText" ValueField="CanMetricValue" />
                                        </Template>
                                    </dx:GridViewColumnLayoutItem>

                                    <dx:GridViewColumnLayoutItem ColumnName="QueryType" ColSpan="2" Caption="Query Type" RequiredMarkDisplayMode="Required">
                                        <Template>
                                            <dx:ASPxComboBox ID="ddlQueryType" runat="server" Width="50px" Value='<%# Bind("QueryType")%>' ClientInstanceName="xxx">
                                                <Items>
                                                    <dx:ListEditItem Text=""   Value=""/>
                                                    <dx:ListEditItem Text="<" Value="<"/>
                                                    <dx:ListEditItem Text=">" Value=">"/>
                                                    <dx:ListEditItem Text="<=" Value="<="/>
                                                    <dx:ListEditItem Text=">=" Value=">="/>
                                                    <dx:ListEditItem Text="!=" Value="!="/>
                                                    <dx:ListEditItem Text="Like" Value="Like"/>
                                                    <dx:ListEditItem Text="=" Value="="/>
                                                </Items>
                                            </dx:ASPxComboBox>
                                        </Template>
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColumnName="TextValue" Width="80px" Caption="" Visible="true"></dx:GridViewColumnLayoutItem>    
                                    <dx:GridViewColumnLayoutItem ColumnName="MetricValue" Width="0px" CssClass="Hide"></dx:GridViewColumnLayoutItem>
                                    <dx:EditModeCommandLayoutItem ColSpan="7" HorizontalAlign="Right" />
                                </Items>
                            </EditFormLayoutProperties>
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsEventConfiguration" runat="server" DataObjectTypeName="FMS.Business.DataObjects.Can_EventDefinition" DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetCanEventDefinition" TypeName="FMS.Business.DataObjects.Can_EventDefinition" UpdateMethod="Update"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsVehicles" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.ApplicationVehicle">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="ApplicationId" DbType="Guid" Name="appplicationID"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsMetric" runat="server" SelectMethod="GetCanMessageList" TypeName="FMS.Business.DataObjects.Can_EventDefinition"></asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="EventOccurances" Text="Event Occurances">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxTextBox ID="txtSearch" runat="server" Width="170px">
                            <ClientSideEvents TextChanged="function(s, e) {
	                                                gridOccurances.Refresh();
                                                }" />
                        </dx:ASPxTextBox>
                        <dx:ASPxGridView ID="gvEventOccurances" DataSourceID="odsEventOccurances" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridOccurances" Width="700px" >
                            <Columns>                                
                                <dx:GridViewDataTextColumn FieldName="EventType" VisibleIndex="4"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="StartTime" VisibleIndex="5"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="EndTime" VisibleIndex="6"></dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsEventOccurances" runat="server" SelectMethod="GetCanEventOccuranceList" TypeName="FMS.Business.DataObjects.Can_EventOccurance">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtSearch" PropertyName="Text" Name="SearchParam" Type="String"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </dx:ContentControl>
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

