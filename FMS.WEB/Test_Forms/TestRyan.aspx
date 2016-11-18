<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.master" CodeBehind="TestRyan.aspx.vb" Inherits="FMS.WEB.TestRyan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentLeft" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="odsAppBooking">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="ApplicationBookingId" VisibleIndex="0"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ApplicationDriverID" VisibleIndex="1"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ApplicationId" VisibleIndex="2"></dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="ArrivalTime" VisibleIndex="3">
                <PropertiesDateEdit>
                    <TimeSectionProperties>
                        <TimeEditProperties>
                            <ClearButton Visibility="Auto"></ClearButton>
                        </TimeEditProperties>
                    </TimeSectionProperties>

                    <ClearButton Visibility="Auto"></ClearButton>
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="GeofenceLeave" VisibleIndex="4"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="GeofenceDestination" VisibleIndex="5"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CustomerName" VisibleIndex="6"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CustomerPhone" VisibleIndex="7"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CustomerEmail" VisibleIndex="8"></dx:GridViewDataTextColumn>
            <dx:GridViewDataCheckColumn FieldName="IsAlert5min" VisibleIndex="9"></dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn FieldName="IsAlertLeaveForPickup" VisibleIndex="10"></dx:GridViewDataCheckColumn>
        </Columns>
    </dx:ASPxGridView>

    this is a test 
    <asp:ObjectDataSource runat="server" ID="odsAppBooking" SelectMethod="GetAllBookingsForApplication" TypeName="FMS.Business.DataObjects.ApplicationBooking">
        <SelectParameters>
            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource runat="server" ID="odsApplicatoinBooking"></asp:ObjectDataSource>
    <asp:ObjectDataSource runat="server" ID="odsApplicationGeoFence" SelectMethod="GetAllApplicationGeoFences" TypeName="FMS.Business.DataObjects.ApplicationGeoFence">
        <SelectParameters>
            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
