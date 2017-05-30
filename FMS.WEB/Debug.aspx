<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="Debug.aspx.vb" Inherits="FMS.WEB.Debug" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <style type="text/css">
        .debugtabletd {
            padding-left: 5px;
            padding-bottom: 20px;
        }

        div.dxb {
            padding-bottom: 2px !important;
            padding-left: 2px !important;
            padding-right: 2px !important;
            padding-top: 0px !important;
        }
    </style>


    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0">
        <TabPages>
            <dx:TabPage Text="All device messages received">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table class="debugtable">
                            <tr>

                                <td class="debugtabletd">StartDate</td>
                                <td class="debugtabletd">
                                    <dx:ASPxDateEdit ID="dateStart" runat="server" EditFormat="DateTime" Theme="SoftOrange">
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>

                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </dx:ASPxDateEdit>
                                </td>

                                <td class="debugtabletd">EndDate</td>
                                <td class="debugtabletd">
                                    <dx:ASPxDateEdit ID="dateEnd" runat="server" EditFormat="DateTime" Theme="SoftOrange">
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>

                                        <ClearButton Visibility="Auto"></ClearButton>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <%--<div style="position: absolute; top: 45px; right: 75px;">--%>
                                    <div style="padding-left: 15px; margin-top: -22px;">
                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Query Logs" Theme="SoftOrange" ></dx:ASPxButton>
                                    </div>
                                </td>
                            </tr>
                        </table>


                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="odsAuditObjs">
                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                            <SettingsDataSecurity AllowEdit="False" AllowInsert="False" AllowDelete="False"></SettingsDataSecurity>
                            <SettingsPager PageSize="1000"></SettingsPager>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="ApplicationName" VisibleIndex="0" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DeviceID" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TruckName" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Message" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="Time" VisibleIndex="1">
                                    <PropertiesDateEdit DisplayFormatString="G" EditFormat="DateTime">
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                            </Columns>
                            <GroupSummary>
                                <dx:ASPxSummaryItem FieldName="DeviceID" SummaryType="Custom" />
                            </GroupSummary>
                        </dx:ASPxGridView>
                        <asp:ObjectDataSource runat="server" ID="odsAuditObjs"
                            SelectMethod="GetAllBetweenDates"
                            TypeName="FMS.Business.DataObjects.LogEntry">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="dateStart" PropertyName="Value" Name="startdtae" Type="DateTime"></asp:ControlParameter>
                                <asp:ControlParameter ControlID="dateEnd" PropertyName="Value" Name="enddate" Type="DateTime"></asp:ControlParameter>
                                <asp:SessionParameter DbType="Guid" Name="applicationid" SessionField="ApplicationID" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="External bug tracker access">
                <ContentCollection>
                    <dx:ContentControl Width="100%" Height="100%" runat="server">
                        <iframe marginwidth="0" marginheight="0" frameborder="0" overflow-y="scroll" overflow-x="hidden" id="bugtrackerIframe"
                            width="50px" height="50px">please wait...</iframe>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>

</asp:Content>
