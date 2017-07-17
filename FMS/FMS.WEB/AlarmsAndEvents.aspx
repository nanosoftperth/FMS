<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="AlarmsAndEvents.aspx.vb" Inherits="FMS.WEB.AlarmsAndEvents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ctl00_MainPane_Content_ASPxRoundPanel1_MainContent_ASPxPageControl1_ddlVehicles").change(function () {
                var start = this.value;
                $("#ctl00_ctl00_MainPane_Content_ASPxRoundPanel1_MainContent_ASPxPageControl1_btnRefresh").click();
            });
        })
    </script>
        
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0">
        <TabPages>
            <dx:TabPage Name="Custom Events" Text="Custom Events">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <asp:DropDownList ID="ddlVehicles" DataSourceId="odsGetAllVehicleNames" runat="server">
                        </asp:DropDownList>
                        <dx:ASPxButton ID="btnRefresh" runat="server" Text="Refresh" >
                            <ClientSideEvents Click="function(s, e) {
	                            gridEvents.Refresh();
                            }" />
                        </dx:ASPxButton>
                        <dx:ASPxGridView ID="dgvEvents" runat="server" DataSourceID="odsEvents" ClientInstanceName="gridEvents">                            
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsEvents" runat="server" SelectMethod="GetAllForVehicleSPN" TypeName="FMS.Business.DataObjects.EventType">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlVehicles" PropertyName="SelectedValue" DefaultValue="VehicleName" Name="VehicleName" Type="String"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsGetAllVehicleNames" runat="server" SelectMethod="GetAllVehicleNames" TypeName="FMS.Business.DataObjects.EventType">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="applicationId"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="Custom Alerts" Text="Custom Alerts">
                <ContentCollection>
                    <dx:ContentControl runat="server"></dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</asp:Content>

