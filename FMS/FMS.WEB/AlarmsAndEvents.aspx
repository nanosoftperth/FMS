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
            <dx:TabPage Name="Custom Alerts" Text="Custom Alerts">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div style="float:left">
                                            <dx:ASPxLabel ID="lblVehicle" runat="server" Text="Vehicle"></dx:ASPxLabel>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div style="float:left">
                                            <asp:DropDownList ID="ddlVehicles" DataSourceId="odsGetAllVehicleNames" runat="server" Width="120px">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div style="float:left">
                                            <dx:ASPxLabel ID="lblComparison" runat="server" Text="Compare"></dx:ASPxLabel>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div style="float:left">
                                            <asp:DropDownList ID="ddlComparison" runat="server" Width="120px" CssClass="ddlCenter">
                                                <asp:ListItem Text=""></asp:ListItem>
                                                <asp:ListItem Text="<"></asp:ListItem>
                                                <asp:ListItem Text=">"></asp:ListItem>
                                                <asp:ListItem Text="<="></asp:ListItem>
                                                <asp:ListItem Text=">="></asp:ListItem>
                                                <asp:ListItem Text="!="></asp:ListItem>
                                                <asp:ListItem Text="Like"></asp:ListItem>
                                                <asp:ListItem Text="="></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div style="float:left">
                                            <dx:ASPxLabel ID="lblText" runat="server" Text="Text"></dx:ASPxLabel>
                                        </div>  
                                    </div>
                                    <div class="col-md-3">
                                        <div style="float:left">
                                            <dx:ASPxTextBox ID="txtTextSearch" runat="server" Width="120px"></dx:ASPxTextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-3">
                                <div class="row">
                                    <div class="col-md-3"></div>
                                    <div class="col-md-3">
                                        <div style="float:left">
                                            <dx:ASPxButton ID="btnRefresh" runat="server" Text="Refresh" >
                                                <ClientSideEvents Click="function(s, e) {
	                                                gridEvents.Refresh();
                                                }" />
                                            </dx:ASPxButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <br />

                        <dx:ASPxGridView ID="dgvEvents" runat="server" DataSourceID="odsEvents" ClientInstanceName="gridEvents" AutoGenerateColumns="False">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="Standard" VisibleIndex="0"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="PGN" VisibleIndex="1"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SPN" VisibleIndex="2"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Acronym" VisibleIndex="3"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="4"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Resolution" VisibleIndex="5"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Units" VisibleIndex="6"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="offset" VisibleIndex="7"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="pos" VisibleIndex="8"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SPN_Length" VisibleIndex="9"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="PGN_Length" VisibleIndex="10"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Data_Range" VisibleIndex="11"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Resolution_Multiplier" ReadOnly="True" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="pos_start" ReadOnly="True" VisibleIndex="13"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="pos_end" ReadOnly="True" VisibleIndex="14"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CanValue" ReadOnly="True" VisibleIndex="15"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataCheckColumn FieldName="SendMail" Caption="Email" VisibleIndex="16"></dx:GridViewDataCheckColumn>
                            <dx:GridViewDataCheckColumn FieldName="SendText" Caption="Text" VisibleIndex="17"></dx:GridViewDataCheckColumn>
                        </Columns>
                    </dx:ASPxGridView>
                        <asp:ObjectDataSource ID="odsEvents" runat="server" SelectMethod="GetAllForVehicleSPN" TypeName="FMS.Business.DataObjects.EventType">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlVehicles" PropertyName="SelectedValue" DefaultValue="VehicleName" Name="VehicleName" Type="String"></asp:ControlParameter>
                                <asp:ControlParameter ControlID="ddlComparison" PropertyName="SelectedValue" DefaultValue="" Name="Comparison" Type="String"></asp:ControlParameter>
                                <asp:ControlParameter ControlID="txtTextSearch" PropertyName="Text" Name="TextSearch" Type="String"></asp:ControlParameter>
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
            <dx:TabPage Name="Custom Events" Text="Custom Events">
                <ContentCollection>
                    <dx:ContentControl runat="server"></dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</asp:Content>

