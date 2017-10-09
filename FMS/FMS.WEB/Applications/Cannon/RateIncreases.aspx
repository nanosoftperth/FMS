<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="RateIncreases.aspx.vb" Inherits="FMS.WEB.RateIncreases" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxGridView ID="RateIncreasesGridView" KeyFieldName="RateIncreaseID" DataSourceID="odsRateIncreases" runat="server">
        <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
        <Settings ShowPreview="true" />
        <SettingsPager PageSize="10" />
        <Columns>
            <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="RateIncreaseID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="RateIncreaseDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
            <dx:GridViewDataCheckColumn FieldName="AnnualIncreaseApplies" VisibleIndex="3"></dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn FieldName="AlreadyDoneThisYear" VisibleIndex="3"></dx:GridViewDataCheckColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:ObjectDataSource ID="odsRateIncreases" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRateIncreaseReference" DataObjectTypeName="FMS.Business.DataObjects.tblRateIncreaseReference" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
</asp:Content>
