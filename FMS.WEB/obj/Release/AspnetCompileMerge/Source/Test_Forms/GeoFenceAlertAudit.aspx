<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="GeoFenceAlertAudit.aspx.vb" Inherits="FMS.WEB.GeoFenceAlertAudit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
    <dx:ASPxGridView    ID="dgvAlertTypeOccurences" 
                        runat="server" 
                        KeyFieldName="AlertTypeOccuranceID" 
                        AutoGenerateColumns="False" 
                        DataSourceID="odsATOAlarmTypeOccurance" 
                        Theme="SoftOrange">

        <SettingsPager Visible="False"></SettingsPager>

        <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>

        <SettingsDataSecurity AllowDelete="False" AllowInsert="False" AllowEdit="False"></SettingsDataSecurity>
        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
        <Columns>
            <dx:GridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="0"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="AlertTypeOccuranceID" VisibleIndex="1" Visible="False"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="DriverName" VisibleIndex="2" Caption="Driver Name"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="SubscriberTypeStr" VisibleIndex="5" Caption="Subscriber Type"></dx:GridViewDataTextColumn>
            <%--<dx:GridViewDataTextColumn FieldName="GeoFenceCollisionID" VisibleIndex="2"></dx:GridViewDataTextColumn>--%>
            <dx:GridViewDataTextColumn FieldName="SubscriberTypeName" VisibleIndex="6" Caption="Subscriber Name(s)"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Emails" VisibleIndex="7" Caption="Email Addresses"></dx:GridViewDataTextColumn>
            <%--<dx:GridViewDataTextColumn FieldName="SubscriberNativeID" VisibleIndex="5"></dx:GridViewDataTextColumn>--%>
            <dx:GridViewDataTextColumn FieldName="Texts" VisibleIndex="8" Caption="Mobile Numbers"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="DateSent" VisibleIndex="9" Caption="Date/Time Alert Sent"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ApplicationGeoFenceName" VisibleIndex="4" Caption="Geo-Fence Name"></dx:GridViewDataTextColumn>
            <%--<dx:GridViewDataTextColumn FieldName="ApplicationGeoFenceID" VisibleIndex="9"></dx:GridViewDataTextColumn>--%>
            <dx:GridViewDataTextColumn FieldName="MessageContent" VisibleIndex="11" Caption="Message Content"></dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn FieldName="AlertTypeID" Caption="Alert Type" VisibleIndex="3">
                <PropertiesComboBox DataSourceID="odsATOAlertType" TextField="Action" ValueField="ApplicationAlertTypeID">
                    <ClearButton Visibility="Auto"></ClearButton>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>

        </Columns>
    </dx:ASPxGridView>

    <asp:ObjectDataSource runat="server" ID="odsATOAlarmTypeOccurance" SelectMethod="GetAllForApplication" TypeName="FMS.Business.DataObjects.AlertTypeOccurance">
        <SelectParameters>
            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="applicationID"></asp:SessionParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <asp:ObjectDataSource runat="server" ID="odsATOAlertType" SelectMethod="GetALLForApplication" TypeName="FMS.Business.DataObjects.AlertType">
        <SelectParameters>
            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
        </SelectParameters>
    </asp:ObjectDataSource>

        



</asp:Content>
