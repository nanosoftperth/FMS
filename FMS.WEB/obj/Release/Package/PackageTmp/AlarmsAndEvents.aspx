<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="AlarmsAndEvents.aspx.vb" Inherits="FMS.WEB.AlarmsAndEvents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Content/javascript/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
           
        })

        //Cesar: Use for Delete Dialog Box
        var visibleIndex;
        function OnCustomButtonClick(s, e, item) {
            visibleIndex = e.visibleIndex;

            if (item == 'EventConfig') {
                popupDelete_EventConfig.SetHeaderText("Delete Item");
                popupDelete_EventConfig.Show();
            }
            if (item == 'AlertConfig') {
                popupDelete_AlertConfig.SetHeaderText("Delete Item");
                popupDelete_AlertConfig.Show();
            }
            //if (item == 'RunSite') {
            //    popupDelete_RunSite.SetHeaderText("Delete Item");
            //    popupDelete_RunSite.Show();
            //}
            //if (item == 'SiteTab') {
            //    popupDelete_SiteTab.SetHeaderText("Delete Item");
            //    popupDelete_SiteTab.Show();
            //}
            //if (item == 'SiteDoc') {
            //    popupDelete_SiteDoc.SetHeaderText("Delete Item");
            //    popupDelete_SiteDoc.Show();
            //}
        }
        function OnClickYes(s, e, item) {
            if (item == 'EventConfig') {
                cltEventConfig.DeleteRow(visibleIndex);
                popupDelete_EventConfig.Hide();
            }
            if (item == 'AlertConfig') {
                cltgvAlertConfiguration.DeleteRow(visibleIndex);
                popupDelete_AlertConfig.Hide();
            }
            //if (item == 'RunSite') {
            //    cltRunSiteGridView.DeleteRow(visibleIndex);
            //    popupDelete_RunSite.Hide();
            //}
            //if (item == 'SiteTab') {
            //    cltSiteDetailsGridView.DeleteRow(visibleIndex);
            //    popupDelete_SiteTab.Hide();
            //}
            //if (item == 'SiteDoc') {
            //    DocGridView.DeleteRow(visibleIndex);
            //    popupDelete_SiteDoc.Hide();
            //}
        }
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
                            KeyFieldName="CAN_EventDefinitionID" Width="550px" OnRowValidating="ASPxGridView1_RowValidating" Theme="SoftOrange"
                            OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize" ClientInstanceName="cltEventConfig">
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <ClientSideEvents CustomButtonClick="function(s, e)
                            {
                                OnCustomButtonClick(s, e, 'EventConfig');
                            }" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="btnDelete_EventConfig" Text="Delete" />
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="CAN_EventDefinitionID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="VehicleID" VisibleIndex="9"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Metric" VisibleIndex="10"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Comparison" VisibleIndex="11"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="QueryType" VisibleIndex="12" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TextValue" VisibleIndex="13" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MetricValue" VisibleIndex="14" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="VehicleText" VisibleIndex="14" Visible="false"></dx:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowPreview="true" />
                            <SettingsPager PageSize="10" />
                            <EditFormLayoutProperties ColCount="8" >
                                <Items>
                                    <dx:GridViewColumnLayoutItem ColumnName="VehicleID" ColSpan="2" Caption="Vehicle" RequiredMarkDisplayMode="Required" >
                                        <Template>
                                            <dx:ASPxComboBox ID="ddlVehicleList" runat="server" Width="80px" DataSourceID="odsVehicles" Value='<%# Bind("VehicleID")%>' TextField="name" ValueField="name"  
                                                ClientSideEvents-TextChanged="function(s, e) { 
                                                    ddlMetric.PerformCallback(s.GetValue().toString());
                                                }"/>
                                        </Template>
                                    </dx:GridViewColumnLayoutItem>

                                    <dx:GridViewColumnLayoutItem ColumnName="Metric" ColSpan="2" Caption="Parameter" RequiredMarkDisplayMode="Required" Visible="true">
                                        <Template>
                                            <dx:ASPxComboBox ID="ddlMetric" runat="server" Width="100px" DataSourceID="odsMetric" Value='<%# Bind("Metric")%>' 
                                                TextField="canMetricText" ValueField="CanMetricValue" ClientInstanceName="ddlMetric" OnCallback="ddlMetric_Callback" ClientSideEvents-Init="function(se,e){
                                                    ddlMetric.PerformCallback('Metric_Callback');
                                                }" />
                                        </Template>
                                    </dx:GridViewColumnLayoutItem>

                                    <dx:GridViewColumnLayoutItem ColumnName="QueryType" ColSpan="2" Caption="Query Type" RequiredMarkDisplayMode="Required">
                                        <Template>
                                            <dx:ASPxComboBox ID="ddlQueryType" runat="server" Width="50px" Value='<%# Bind("QueryType")%>' ClientInstanceName="xxx">
                                                <Items>
                                                    <dx:ListEditItem Text="" Value=""/>
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
                                    <dx:GridViewColumnLayoutItem ColumnName="VehicleText" Width="0px" CssClass="Hide"></dx:GridViewColumnLayoutItem>
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
                        <dx:ASPxGridView ID="gvEventOccurances" DataSourceID="odsEventOccurances" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridOccurances" Width="700px" Theme="SoftOrange">
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <Columns>                                
                                <dx:GridViewDataTextColumn FieldName="EventType" VisibleIndex="4"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="StartTime" VisibleIndex="5" PropertiesTextEdit-DisplayFormatString="d/MM/yyyy HH:mm"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="EndTime" VisibleIndex="6" PropertiesTextEdit-DisplayFormatString="d/MM/yyyy HH:mm"></dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsEventOccurances" runat="server" SelectMethod="GetCanEventOccuranceList" TypeName="FMS.Business.DataObjects.Can_EventOccurance">
                        </asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="AlertConfiguration" Text="Alert Configuration">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxGridView ID="gvAlertConfiguration" DataSourceID="odsAlertConfiguration" 
                            runat="server" AutoGenerateColumns="False" KeyFieldName="CanAlertDefinitionIDUnique" 
                            Theme="SoftOrange" ClientInstanceName="cltgvAlertConfiguration">
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <ClientSideEvents CustomButtonClick="function(s, e)
                            {
                                OnCustomButtonClick(s, e, 'AlertConfig');
                            }" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="btnDelete_AlertConfig" Text="Delete" />
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="CAN_AlertDefinitionID" VisibleIndex="9" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CAN_EventDefinitionID" VisibleIndex="8" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SubscriberNativeID" VisibleIndex="7" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataCheckColumn FieldName="SendEmail" VisibleIndex="3"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="SendText" VisibleIndex="4"></dx:GridViewDataCheckColumn>
                                <dx:GridViewDataDateColumn FieldName="TimePeriod" VisibleIndex="5" Visible="false">
                                    <PropertiesDateEdit>
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>
                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="ValueDate" Caption="Time Period" VisibleIndex="1"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="EventType" VisibleIndex="0"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MessageDestination" VisibleIndex="2"></dx:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowPreview="true" />
                            <SettingsPager PageSize="10" />
                            <EditFormLayoutProperties ColCount="8" >
                                <Items>
                                    <dx:GridViewColumnLayoutItem ColumnName="EventType" ColSpan="2" Caption="Event Type" RequiredMarkDisplayMode="Required" >
                                        <Template>
                                            <dx:ASPxComboBox ID="ddlEventType" runat="server" Width="100px" DataSourceID="odsEventType" Value='<%# Bind("EventType")%>' TextField="FieldText" ValueField="FieldValue" />
                                        </Template>
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColumnName="TimePeriod" ColSpan="2" Caption="Time Period" RequiredMarkDisplayMode="Required" >
                                        <Template>
                                            <dx:ASPxTimeEdit ID="teTimePeriod" runat="server" EditFormat="Custom" Value='<%# Bind("TimePeriod")%>' 
		                                        EditFormatString="HH:mm:ss" DateTime="01/01/2001 00:00:00">
	                                        </dx:ASPxTimeEdit>
                                        </Template>
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColumnName="MessageDestination" ColSpan="2" Caption="Message Destination" RequiredMarkDisplayMode="Required" >
                                        <Template>
                                            <dx:ASPxComboBox ID="ddlMessageDestination" runat="server" Width="100px" DataSourceID="odsMessageDestination" Value='<%# Bind("MessageDestination")%>' TextField="FieldText" ValueField="FieldValue" />
                                        </Template>
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColumnName="SendEmail" ColSpan="2" Caption="Send Email">
                                        <Template>
                                            <dx:ASPxCheckBox value='<%# Bind("SendEmail")%>' ID="cbSendMail" runat="server"></dx:ASPxCheckBox>
                                        </Template>
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:GridViewColumnLayoutItem ColumnName="SendText" ColSpan="2" Caption="Send Text">
                                        <Template>
                                            <dx:ASPxCheckBox value='<%# Bind("SendText")%>' ID="cbSendText" runat="server"></dx:ASPxCheckBox>
                                        </Template>
                                    </dx:GridViewColumnLayoutItem>
                                    <dx:EditModeCommandLayoutItem ColSpan="7" HorizontalAlign="Right" />
                                </Items>
                            </EditFormLayoutProperties>
                        </dx:ASPxGridView>
                       <asp:ObjectDataSource ID="odsAlertConfiguration" runat="server" SelectMethod="GetCanAlertDefinitionList" TypeName="FMS.Business.DataObjects.Can_AlertDefinition" DataObjectTypeName="FMS.Business.DataObjects.Can_AlertDefinition" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="ApplicationId" DbType="Guid" Name="applicationId"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsEventType" runat="server" SelectMethod="GetEventDefintionList" TypeName="FMS.Business.DataObjects.Can_AlertDefinition"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsMessageDestination" runat="server" SelectMethod="GetSubscribersList" TypeName="FMS.Business.DataObjects.Can_AlertDefinition">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="ApplicationId" DbType="Guid" Name="applicationId"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="AlertOccurances" Text="Alert Occurances">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxGridView ID="dvAlertOccurances" DataSourceID="odsAlertOccurances" runat="server" AutoGenerateColumns="False" Theme="SoftOrange">                            
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="CAN_EventOccuranceAlertID" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CAN_EventOccuranceID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CAN_AlertDefinition" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="SentDate" VisibleIndex="7" Width="120" PropertiesDateEdit-DisplayFormatString="d/MM/yyyy HH:mm" Caption="Data/Time Sent">
                                    <PropertiesDateEdit>
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>
                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="AlertType" VisibleIndex="3" Width="250"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="TimePeriod" VisibleIndex="9" Visible="false">
                                    <PropertiesDateEdit>
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>
                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="StartTime" VisibleIndex="4" PropertiesTextEdit-DisplayFormatString="d/MM/yyyy HH:mm" Width="120px"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="EndTime" VisibleIndex="5" PropertiesTextEdit-DisplayFormatString="d/MM/yyyy HH:mm" Width="120px"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SubscriberNativeID" VisibleIndex="10" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="EmailAddress" VisibleIndex="6" Width="150" Caption="Email Addresses"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MessageContent" VisibleIndex="8" Width="300"></dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsAlertOccurances" runat="server" SelectMethod="GetCanEventOccuranceList" TypeName="FMS.Business.DataObjects.Can_EventOccuranceAlert">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="ApplicationId" DbType="Guid" Name="ApplicationId"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <dx:ASPxPopupControl ID="DeleteDialog_EventConfig" runat="server" Text="Are you sure you want to delete this?"
            ClientInstanceName="popupDelete_EventConfig" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <dx:ASPxButton ID="yesButton_EventConfig" runat="server" Text="Yes" AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e)
                            {
                                OnClickYes(s, e, 'EventConfig');
                            }" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="noButton_EventConfig" runat="server" Text="No" AutoPostBack="false">
                        <ClientSideEvents Click="function(){ popupDelete_EventConfig.Hide(); }" />
                    </dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="DeleteDialog_AlertConfig" runat="server" Text="Are you sure you want to delete this?"
            ClientInstanceName="popupDelete_AlertConfig" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <dx:ASPxButton ID="yesButton_AlertConfig" runat="server" Text="Yes" AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e)
                            {
                                OnClickYes(s, e, 'AlertConfig');
                            }" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="noButton_AlertConfig" runat="server" Text="No" AutoPostBack="false">
                        <ClientSideEvents Click="function(){ popupDelete_AlertConfig.Hide(); }" />
                    </dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
</asp:Content>

