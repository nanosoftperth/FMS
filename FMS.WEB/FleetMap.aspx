<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.master" CodeBehind="FleetMap.aspx.vb" Inherits="FMS.WEB.FleetMap" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxwschsc" %>


<asp:Content ID="ContentLeft" ContentPlaceHolderID="ContentLeft" runat="server">


    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.24&key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g&libraries=places,visualization"></script>


    <script src="Content/javascript/firstRun.js?version=<%=WebVersion%>"></script>
    <script src="Content/javascript/accordian.js?version=<%=WebVersion%>"></script>
    <script src="Content/javascript/markerWithLabel.js?version=<%=WebVersion%>"></script>
    <script src="Content/javascript/FleetMap.js?version=<%=WebVersion%>"></script>

    <script src="Content/javascript/pathDrawer.js?version=<%=WebVersion%>"></script>
    <script src="Content/javascript/GeoFencing.js?version=<%=WebVersion%>"></script>
    <script src="Content/javascript/FleetMap_ClockLogic.js?version=<%=WebVersion%>"></script>

    <link href="Content/Accoridan.css" rel="stylesheet" />

    <dx:ASPxPanel DefaultButton="ASPxButton1" 
                    ID="LeftPane" 
                    runat="server" 
                    FixedPosition="WindowLeft" 
                    ClientInstanceName="leftPane" 
                    CssClass="leftPane" 
                    Collapsible="True">

        <SettingsAdaptivity CollapseAtWindowInnerWidth="1023" />
        <Styles>
            <Panel CssClass="panel"></Panel>
        </Styles>

        <PanelCollection>

            <dx:PanelContent runat="server" ID="test1" SupportsDisabledAttribute="True" Width="100%">


                <section id="accordion">

                    <div>

                        <input type="checkbox" class="chkbox" id="check-1" />
                        <label class="accordianTitle" for="check-1">Show specific date/time</label>

                        <article>
                            <br />
                            <div style="padding-left: 15px; font-weight: bold;">
                                <span class="dateTimePicker" style="margin-left: 2px;">Enter the time you wish to view:</span>

                                <dx:ASPxDateEdit ID="ASPxDateEdit1"
                                    runat="server"
                                    Date="01/20/2016 00:06:00"
                                    EditFormat="DateTime"
                                    ClientInstanceName="dateViewThisDateTime"
                                    EnableTheming="True"
                                    Height="22px"
                                    Paddings-PaddingLeft="10px"
                                    padding-right="10px"
                                    Theme="MetropolisBlue"
                                    Width="198px">

                                    <TimeSectionProperties Visible="True">
                                        <TimeEditProperties>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                    <ClientSideEvents DateChanged="function(s, e) {
	dateedit_Click(s,e);
}"
                                        CalendarCustomDisabledDate="function(s, e) {  }" 
                                        KeyDown="function(s,e){dateedit_Click(s,e);e.returnValue = false;e.cancel = true;}"
                                        />

                                    <Buttons>
                                        <dx:EditButton Visible="false" Text="view on map">
                                        </dx:EditButton>
                                    </Buttons>
                                    <ClearButton Visibility="Auto">
                                    </ClearButton>

                                    <Paddings PaddingLeft="10px"></Paddings>
                                </dx:ASPxDateEdit>
                            </div>
                            <br />
                        </article>
                    </div>
                    <div>
                        <input type="checkbox" class="chkbox" id="check-2" />
                        <label class="accordianTitle" for="check-2">Address search</label>
                        <article>
                            <p>
                                <b>Search for address</b>
                                <input id="pac-input" type="text" placeholder="Search Box">
                            </p>
                        </article>
                    </div>
                    <div>
                        <input class="chkbox" type="checkbox" id="check-3" />

                        <label class="accordianTitle" for="check-3">Activity Viewer</label>

                        <article>
                            <br />
                            <div style="padding-left: 15px; font-weight: bold;">
                                <table>
                                    <tr>
                                        <td>
                                            <span class="dateTimePicker" style="margin-left: 2px; margin-top: 5px;">Vehicle / Driver:</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxDropDownEdit AutoPostBack="false"  ClientInstanceName="checkComboBox" ID="ddlTrucks" Height="22px" Width="198px" runat="server" AnimationType="None">

                                                <DropDownWindowStyle BackColor="#EDEDED" />
                                                <DropDownWindowTemplate>

                                                    <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="checkListBox" SelectionMode="CheckColumn" runat="server">
                                                        <Border BorderStyle="None" />
                                                        <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                        <Items>
                                                            <dx:ListEditItem Text="(Select all)" />
                                                        </Items>
                                                        <ClientSideEvents SelectedIndexChanged="OnListBoxSelectionChanged" />
                                                    </dx:ASPxListBox>
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="padding: 4px">
                                                                <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                                    <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </DropDownWindowTemplate>

                                                <ClientSideEvents   TextChanged="SynchronizeListBoxValues" 
                                                                    DropDown="SynchronizeListBoxValues" 
                                                                     />
                                            </dx:ASPxDropDownEdit>
                                        </td>
                                    </tr>

                                    <%-- <tr>
                                        <td>
                                            <dx:ASPxComboBox ID="cboSelectedTruck"
                                                ClientInstanceName="cboSelectedTruck"
                                                ClientEnabled="true"
                                                ClientIDMode="Predictable"
                                                ValueField="ID"
                                                TextField="ComboBoxDisplay"
                                                runat="server"
                                                ValueType="System.String"
                                                DataSourceID="odsTrucks"
                                                Theme="Metropolis">

                                                <ClearButton Visibility="Auto"></ClearButton>


                                            </dx:ASPxComboBox>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td><span class="dateTimePicker" style="margin-left: 2px; margin-top: 5px;">Start Time:</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxDateEdit ID="heatMapStartTime" AutoPostBack="false" ClientInstanceName="heatMapStartTime" ClientIDMode="Predictable" ClientEnabled="true" runat="server" Date="01/20/2016 00:06:00" EditFormat="DateTime"
                                                EnableTheming="True" Height="22px" Paddings-PaddingLeft="10px" padding-right="10px"
                                                Theme="MetropolisBlue"
                                                Width="198px">
                                                <TimeSectionProperties Visible="True">
                                                    <TimeEditProperties>
                                                        <ClearButton Visibility="Auto">
                                                        </ClearButton>
                                                    </TimeEditProperties>
                                                </TimeSectionProperties>
                                                <ClientSideEvents ButtonClick="function(s, e) {
	dateedit_Click(s,e);
}"
                                                    CalendarCustomDisabledDate="function(s, e) {
	
}" />
                                                <Buttons>
                                                    <dx:EditButton Visible="false" Text="View">
                                                    </dx:EditButton>
                                                </Buttons>
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>

                                                <Paddings PaddingLeft="10px"></Paddings>
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="dateTimePicker" style="margin-left: 2px; margin-top: 5px;">End Time:</span>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxDateEdit 
                                                AutoPostBack="false"
                                                ID="heatMapEndTime"
                                                ClientInstanceName="heatMapEndTime"
                                                ClientEnabled="true"
                                                ClientIDMode="Predictable"
                                                runat="server"
                                                Date="01/20/2016 00:06:00"
                                                EditFormat="DateTime"
                                                EnableTheming="True"
                                                Height="22px"
                                                Paddings-PaddingLeft="10px"
                                                padding-right="10px"
                                                Theme="MetropolisBlue"
                                                Width="198px">

                                                <TimeSectionProperties Visible="True">
                                                    <TimeEditProperties>
                                                        <ClearButton Visibility="false">
                                                        </ClearButton>
                                                    </TimeEditProperties>
                                                </TimeSectionProperties>
                                                <ClientSideEvents ButtonClick="function(s, e) {
	dateedit_Click(s,e);
}"
                                                    CalendarCustomDisabledDate="function(s, e) {
	
}" />
                                                <Buttons>
                                                    <dx:EditButton Visible="false" Text="View">
                                                    </dx:EditButton>
                                                </Buttons>
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>

                                                <Paddings PaddingLeft="10px"></Paddings>
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 5px;">

                                            <table id="tblHeatMapOptions" style="float: right;">
                                                <tr>
                                                    <td style="padding: 2px;">
                                                        <dx:ASPxButton ID="btnHeatMapSearch" 
                                                            ClientInstanceName="btnHeatMapSearch" 
                                                            runat="server"
                                                             AutoPostBack="false" 
                                                            Text="View Activity">

                                                            <ClientSideEvents Click="function(){btnHeatMapSearch_Click();}" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="cb" runat="server" ClientInstanceName="cbGradientToggle" EnableTheming="True" Text="Gradient" Theme="SoftOrange">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
	changeGradient();
}" />
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="cbOpiacy" runat="server" ClientInstanceName="cbOpiacyToggle" EnableTheming="True" Text="Opiacy" Checked="true" Theme="SoftOrange">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
	changeOpacity();
}" />
                                                        </dx:ASPxCheckBox>

                                                    </td>
                                                </tr>
                                                <tr>


                                                    <td>
                                                        <dx:ASPxCheckBox ID="cbRadius" ClientInstanceName="cdRadiusToggle" runat="server" EnableTheming="True" Text="Radius" Checked="true" Theme="SoftOrange">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
	changeRadius();
}" />
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="ASPxCheckBox1" Checked="true" ClientInstanceName="cbShowjourney" runat="server" EnableTheming="True" Text="Show Journey" Theme="SoftOrange">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
	showJourneyTogle()
}" />
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="ASPxCheckBox2" ClientInstanceName="cbShowHeatmap" runat="server" EnableTheming="True" Text="Show Heatmap" Checked="true" Theme="SoftOrange">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
	toggleHeatmap()
}" />
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="ASPxCheckBox5" runat="server" ClientInstanceName="cbFollowTruck" EnableTheming="True" Text="Follow Vehicle" Checked="true" Theme="SoftOrange">
                                                        </dx:ASPxCheckBox>

                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="ASPxCheckBox3" ClientInstanceName="cbHeatmapAutoUpdate" runat="server" EnableTheming="True" Text="Real-time update" Checked="false" Theme="SoftOrange">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
	cbHeatmapAutoUpdate_CheckChanged(s,e)
}" />
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="cbSnapToRoad" ClientInstanceName="cbSnapToRoad" runat="server" EnableTheming="True" Text="Snap to road" Checked="true" Theme="SoftOrange">
                                                            <ClientSideEvents CheckedChanged="function(s, e) {
	cbSnapToRoad_CheckChanged(s,e)
}" />
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                </tr>


                                            </table>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td>

                                            <table>
                                                <tr>
                                                    <td colspan="4">
                                                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Vehicle update options" Font-Bold="True"></dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>

                                                    <td>
                                                        <dx:ASPxSpinEdit ID="spinTimeSeconds" ClientInstanceName="spinTimeSeconds" runat="server" Number="3" Theme="Default" EnableTheming="True" Width="45px">
                                                            <ClearButton Visibility="Auto"></ClearButton>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td colspan="2">
                                                        <dx:ASPxComboBox ID="comboTimeSecondsMultiplier" ClientInstanceName="comboTimeSecondsMultiplier" runat="server" Theme="Default" EnableTheming="True" SelectedIndex="1" Width="100%">
                                                            <Items>
                                                                <dx:ListEditItem Text="Seconds" Value="1"></dx:ListEditItem>
                                                                <dx:ListEditItem Text="Minutes" Value="60" Selected="True"></dx:ListEditItem>
                                                                <dx:ListEditItem Text="Hours" Value="3600"></dx:ListEditItem>
                                                            </Items>

                                                            <ClearButton Visibility="Auto"></ClearButton>

                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td style="padding-left: 3px; padding-right: 2px;">
                                                        <dx:ASPxCheckBox ID="ASPxCheckBox4" ClientInstanceName="cbAutoIncrement" runat="server">
                                                            <ClientSideEvents CheckedChanged="function(s,e){cbAutoIncrement_CheckedChanged(s,e);}" />

                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                </tr>


                                            </table>

                                        </td>

                                    </tr>
                                </table>

                            </div>

                            <br />
                        </article>
                    </div>


                    <asp:ObjectDataSource ID="odsTrucks"
                        runat="server"
                        SelectMethod="GetExampleFleetNow"
                        TypeName="FMS.Business.Truck">

                        <SelectParameters>
                            <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appid"></asp:SessionParameter>
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <div>
                        <input class="chkbox" type="checkbox" id="check-4" />
                        <label class="accordianTitle" for="check-4">Geo-Fencing and Alerts</label>

                        <article>

                            <div style="padding: 5px;">
                                <table>
                                    <tr>
                                        <td style="padding-top: 7px;">
                                            <span class="dateTimePicker" style="margin-left: 2px; margin-top: 5px;">Create boundaries and alerts for your vehicles:</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 7px; padding-left: 68px;">
                                            <div style="float: left;">
                                                <dx:ASPxCheckBox ClientInstanceName="cbViewGeoFences"
                                                    ID="cbViewGeoFences"
                                                    Checked="false"
                                                    runat="server"
                                                    EnableTheming="True"
                                                    AutoPostBack="false"
                                                    Text="show geo-fences">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	cbViewGeoFences_checkChanged(e);
}" />
                                                </dx:ASPxCheckBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 7px; padding-left: 68px;">
                                            <div style="float: left;">
                                                <dx:ASPxCheckBox ClientInstanceName="cbViewGeoFencesWithBooking"
                                                    ID="cbViewGeoFencesWithBooking"
                                                    Checked="false"
                                                    runat="server"
                                                    EnableTheming="True"
                                                    AutoPostBack="false"
                                                    Text="include bookings">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	cbViewGeoFences_checkChanged(e);
}" />
                                                </dx:ASPxCheckBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 7px; padding-left: 68px;">
                                            <div style="float: left;">
                                                <dx:ASPxCheckBox ClientInstanceName="cbViewGeoFenceLabels" ID="cbViewGeoFenceLabels" Checked="false" runat="server" EnableTheming="True" Text="show labels">
                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	cbViewGeoFenceLabels_checkChanged(e);
}" />
                                                </dx:ASPxCheckBox>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 7px; padding-left: 40px; padding-bottom: 10px;">
                                            <dx:ASPxButton CssClass="floatright" ID="btnViewGeofencing" AutoPostBack="false"
                                                ClientInstanceName="btnViewGeofencing"
                                                runat="server"
                                                Text="Configure Geo-Fences"
                                                EnableTheming="True">

                                                <ClientSideEvents Click="function(s, e) {
	                                                SetPCVisible (true);
                                                }"></ClientSideEvents>

                                            </dx:ASPxButton>
                                        </td>
                                    </tr>

                                </table>

                            </div>
                        </article>
                    </div>

                    <div>
                        <dx:ASPxButton CssClass="floatright" ID="ASPxButton1" AutoPostBack="false"
                            ClientInstanceName="btnViewCurrentMapTime"
                            runat="server"
                            Text="View Current Map Time"
                            EnableTheming="True">

                            <ClientSideEvents Click="function(s, e) {
	                                                btnViewCurrentMapTime_Click();
                                                }"></ClientSideEvents>

                        </dx:ASPxButton>
                    </div>

                    <div>

                        <dx:ASPxLabel ID="ASPxLabel10" runat="server" ClientInstanceName="errormessage" Text=""></dx:ASPxLabel>
                    </div>

                </section>


            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>



    <dx:ASPxPopupControl ClientInstanceName="popupGeoCaching" Width="500px" Height="340px" MaxWidth="800px" MaxHeight="800px"
        MinHeight="340px"
        MinWidth="500px"
        ID="pcMain" ShowFooter="True"
        FooterText="(c) nanosoft.com.au"
        PopupElementID="btnViewGeofencing"
        HeaderText="Area of Countries"
        runat="server"
        EnableViewState="False"
        AllowDragging="True"
        AllowResize="True"
        DragElement="Window"
        ResizingMode="Postponed"
        ShowSizeGrip="True"
        CloseAction="CloseButton" PopupAnimationType="Fade" CloseAnimationType="Fade" PopupHorizontalAlign="LeftSides" PopupVerticalAlign="Below">

        <ClientSideEvents Closing="function(s,e){popupGeoCaching_Closing(s,e);}" />

        <HeaderTemplate>

            <div style="float: left;">

                <img src="Content/Images/location-icon.png" style="width: 48px; vertical-align: bottom; height: 48px" />
            </div>
            <div style="float: left;">
                <h2 style="margin-bottom: 2px; margin-left: 10px; margin-top: 10px;">Geo-Fencing Configuration</h2>

            </div>

            <div style="float: right; margin-top: 2px; margin-right: 2px;">
                <dx:ASPxImage ID="popupcloseImg" runat="server" ImageUrl="~/Content/Images/close_icon.png" Height="14px" Width="14px" Cursor="pointer">
                    <ClientSideEvents Click="function(s, e){
                        popupcloseImg_Click();
                    }" />
                </dx:ASPxImage>
            </div>

        </HeaderTemplate>

        <ContentCollection>
            <dx:PopupControlContentControl CssClass="pnel1Background" runat="server">
                <asp:Panel ID="Panel1" runat="server">
                    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" ActiveTabIndex="2">
                        <TabPages>
                            <dx:TabPage Text="Summary">
                                <ContentCollection>
                                    <dx:ContentControl runat="server">
                                        <dx:ASPxGridView
                                            KeyFieldName="ApplicationGeoFenceID"
                                            ID="dgvGeoFenceSummary"
                                            runat="server"
                                            AutoGenerateColumns="False"
                                            DataSourceID="odsGeoFences"
                                            ClientInstanceName="dgvGeoFenceSummary">

                                            <ClientSideEvents
                                                CustomButtonClick="function(s,e){dgvGeoFenceSummary_CustomButtonClick(s,e);}" />

                                            <SettingsPager PageSize="6">
                                            </SettingsPager>

                                            <SettingsDataSecurity AllowEdit="False" AllowInsert="False" AllowDelete="False"></SettingsDataSecurity>
                                            <SettingsSearchPanel Visible="True" />
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="ApplicationGeoFenceID" ShowInCustomizationForm="True" VisibleIndex="0" Visible="False">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ApplicationID" ShowInCustomizationForm="True" VisibleIndex="1" Visible="False">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True" VisibleIndex="3">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataDateColumn FieldName="DateCreated" ShowInCustomizationForm="True" VisibleIndex="5">
                                                    <PropertiesDateEdit>
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
                                                <dx:GridViewDataComboBoxColumn FieldName="UserID" VisibleIndex="4" Caption="Author">
                                                    <PropertiesComboBox DataSourceID="odsGeoFencesUsers" TextField="UserName" ValueField="UserID">
                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>
                                                <dx:GridViewCommandColumn ButtonType="Link" ShowInCustomizationForm="True" VisibleIndex="6">
                                                    <CustomButtons>
                                                        <dx:GridViewCommandColumnCustomButton ID="btnView" Text="view">
                                                        </dx:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>
                                                </dx:GridViewCommandColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                        <asp:ObjectDataSource ID="odsGeoFences" runat="server" DataObjectTypeName="FMS.Business.DataObjects.ApplicationGeoFence" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllApplicationGeoFences" TypeName="FMS.Business.DataObjects.ApplicationGeoFence" UpdateMethod="Update">
                                            <SelectParameters>
                                                <asp:SessionParameter DbType="Guid" Name="appID" SessionField="ApplicationID" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <asp:ObjectDataSource ID="odsGeoFencesUsers" runat="server" SelectMethod="GetAllUsersForApplication" TypeName="FMS.Business.DataObjects.User">
                                            <SelectParameters>
                                                <asp:SessionParameter DbType="Guid" Name="applicationid" SessionField="ApplicationID" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>

                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Create New">
                                <ContentCollection>
                                    <dx:ContentControl runat="server">
                                        <div>
                                            <table class="colourpicker" style="width: 370px;">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="GeoFence Name">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" ClientInstanceName="geofenceName" Width="100%">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Description">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxMemo ID="ASPxMemo1" runat="server" CssClass="nope" ClientInstanceName="geoFenceDescription" Height="98px" Width="315px">
                                                        </dx:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Colour">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxColorEdit ID="ASPxColorEdit1" runat="server" ClientInstanceName="goefenceColour" Width="100%">
                                                            <ClientSideEvents ColorChanged="function(){goefenceColour_ColorChanged();}" />
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </dx:ASPxColorEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Options">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <div style="float: right;">
                                                            <table>
                                                                <tr>
                                                                    <td>

                                                                        <dx:ASPxComboBox ID="combogeoFenceTypeSelection"
                                                                            ClientInstanceName="combogeoFenceTypeSelection"
                                                                            runat="server"
                                                                            SelectedIndex="0"
                                                                            Theme="Metropolis"
                                                                            Width="80px">

                                                                            <ClientSideEvents SelectedIndexChanged="function(s,e){combogeoFenceTypeSelection_SelectedIndexChanged(s,e);}" />

                                                                            <Items>
                                                                                <dx:ListEditItem Selected="True" Text="Polygon" Value="Polygon"></dx:ListEditItem>
                                                                                <dx:ListEditItem Text="Circle" Value="Circle"></dx:ListEditItem>
                                                                            </Items>

                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnPlaceOnMap" AutoPostBack="false" runat="server" Text="Place new on map" Width="100%">
                                                                            <ClientSideEvents Click="function(s, e) {
	btnPlaceOnMap_Click()
}" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnClearFromMap" AutoPostBack="false" runat="server" Text="Clear from map" Width="100%">
                                                                            <ClientSideEvents Click="function(s, e) {
	btnClearFromMap_Click()
}" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td></td>
                                                                    <td>
                                                                        <%--<dx:ASPxButton ID="btnUpdate" AutoPostBack="false" runat="server" Text="Update" Width="100%">
                                                                            <ClientSideEvents Click="function(s, e) {
	btnUpdate_Click()
}" />
                                                                        </dx:ASPxButton>--%>
                                                                        <dx:ASPxButton ID="btnSave" AutoPostBack="false" runat="server" Text="Save" Width="100%">
                                                                            <ClientSideEvents Click="function(s, e) {
	btnSave_Click()
}" />
                                                                        </dx:ASPxButton>
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td colspan="2">
                                                        <h4 style="float: right;" id="messageTag"></h4>
                                                    </td>
                                                </tr>

                                            </table>
                                        </div>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Edit">
                                <ContentCollection>
                                    <dx:ContentControl runat="server">
                                        <div>
                                            <table class="colourpicker" style="width: 370px;">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="GeoFence Name">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox TextFormatString="{0}" ID="cbGeoEditGeoFences"
                                                            ClientInstanceName="cbGeoEditGeoFences"
                                                            runat="server"
                                                            ValueType="System.String"
                                                            DataSourceID="ObjectDataSource1"
                                                            Theme="SoftOrange">

                                                            <Columns>
                                                                <dx:ListBoxColumn FieldName="Name" />
                                                                <dx:ListBoxColumn FieldName="Description" />
                                                                <dx:ListBoxColumn FieldName="ApplicationGeoFenceID" Width="0px" />
                                                                <dx:ListBoxColumn FieldName="Colour" Width="0px" />
                                                            </Columns>

                                                            <ClearButton Visibility="Auto"></ClearButton>
                                                            <ClientSideEvents SelectedIndexChanged="function(s,e){cbGeoEditGeoFences_SelectedIndexChanges(s,e);}" />
                                                        </dx:ASPxComboBox>
                                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllApplicationGeoFences" TypeName="FMS.Business.DataObjects.ApplicationGeoFence">
                                                            <SelectParameters>
                                                                <asp:SessionParameter DbType="Guid" Name="appID" SessionField="ApplicationID" />
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Description">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxMemo ID="memoGeoEditDescription" runat="server" CssClass="nope" ClientInstanceName="memoGeoEditDescription" Height="98px" Width="315px">
                                                        </dx:ASPxMemo>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Colour">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxColorEdit ID="colorGeoEditSelct" runat="server" ClientInstanceName="colorGeoEditSelct" Width="100%">
                                                            <ClientSideEvents ColorChanged="function(){colorGeoEditSelct_ColorChanged();}" />
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </dx:ASPxColorEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Options">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <div style="float: right;">
                                                            <table>
                                                                <tr>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnEditDelete" AutoPostBack="false" ClientInstanceName="btnEditDelete" runat="server" Text="Delete" Width="100px">
                                                                            <ClientSideEvents Click="function(s, e) {
	btnEditDelete_Click()
}" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnEditSave" ClientInstanceName="btnEditSave" AutoPostBack="false" runat="server" Text="Save" Width="100px">
                                                                            <ClientSideEvents Click="function(s, e) {
	btnEditSave_Click()
}" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <%-- <tr>
                                                    <td colspan="2">
                                                        <h4 style="float:right;" id="editMessageTag"></h4>
                                                    </td>
                                                </tr>--%>
                                            </table>
                                        </div>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                </asp:Panel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <%--<ClientSideEvents CloseUp="function(s, e) { SetImageState(false); }" PopUp="function(s, e) { SetImageState(true); }" />--%>
    </dx:ASPxPopupControl>


</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" style="float: left;" runat="server">



    <style type="text/css">
        .labels {
            color: black;
            background-color: white;
            font-family: "Lucida Grande", "Arial", sans-serif;
            font-size: 10px;
            font-weight: bold;
            text-align: center;
            /*width: 60px;*/
            border: 2px solid black;
            white-space: nowrap;
            opacity: 1 !Important;
        }

        .labelsPloygon {
            color: black;
            /*background-color: white;*/
            font-family: "Lucida Grande", "Arial", sans-serif;
            font-size: 18px;
            font-weight: bold;
            text-align: center;
            /*width: 60px;*/
            /*border: 2px solid black;*/
            white-space: nowrap;
        }
    </style>


    <div style="position: absolute; top: 159px; right: 15px; z-index: 99;" id="viewInRealTimeCheckbox">
        <dx:ASPxCheckBox ID="autoUpdate" runat="server"
            EnableTheming="True"
            Text="view in real-time"
            ClientInstanceName="autoUpdate"
            Theme="SoftOrange" Checked="True" Font-Bold="true" CheckState="Checked">

            <ClientSideEvents CheckedChanged="function(s, e) {
	autoUpdate_CheckedChanged(s,e);
}" />
        </dx:ASPxCheckBox>
    </div>


    <dx:ASPxPopupControl ClientInstanceName="popupDate"
        Width="432px"
        Height="290px"
        PopupVerticalOffset="196"
        PopupHorizontalOffset="257"
        ID="popupDate"
        ShowFooter="True"
        FooterText=""
        PopupElementID="btnViewGeofencing"
        HeaderText="Map Time"
        runat="server"
        EnableViewState="False"
        AllowDragging="True"
        AllowResize="False"
        DragElement="Window"
        ResizingMode="Postponed"
        ShowSizeGrip="False"
        CloseAction="CloseButton"
        PopupAnimationType="Fade"
        CloseAnimationType="Fade"
        PopupHorizontalAlign="LeftSides"
        PopupVerticalAlign="Below">

        <ClientSideEvents />

        <ContentCollection>


            <dx:PopupControlContentControl runat="server">

                <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="200px" Height="106px">

                    <PanelCollection>

                        <dx:PanelContent runat="server">

                            <dx:ASPxDateEdit ID="ASPxDateEdit2"
                                TimeSectionProperties-Visible="true"
                                ClientInstanceName="dateShow"
                                runat="server"
                                Date="03/22/2016 07:03:36"
                                ClearButton-DisplayMode="Never"
                                CalendarProperties-ShowTodayButton="false"
                                CalendarProperties-ShowClearButton="false"
                                EditFormat="Time"
                                EnableTheming="True"
                                ShowShadow="false"
                                Theme="SoftOrange">

                                <ClientSideEvents DropDown="function(s,e){ dateEdit_DropDown(s,e);}"
                                    QueryCloseUp="function(s,e){ dateEdit_QueryCloseUp(s,e);}" />
                                <CalendarProperties ControlStyle-CssClass="popupDateEdit" ShowClearButton="False" ShowTodayButton="False">
                                    <ControlStyle CssClass="popupDateEdit"></ControlStyle>

                                    <Style CssClass="popupDateEdit"></Style>
                                </CalendarProperties>
                                <TimeSectionProperties ShowOkButton="false" ShowCancelButton="false" ShowSecondHand="true">
                                    <TimeEditProperties EditFormatString="T">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </TimeEditProperties>
                                </TimeSectionProperties>

                                <ClearButton Visibility="Auto">
                                </ClearButton>
                            </dx:ASPxDateEdit>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>

            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <%--BELOW POPUP CONTROL IS USED FOR SHOWING AN EMBEDED REPORT FOM THE REPORT HANDLER--%>

    <dx:ASPxPopupControl ID="popupReport"
        runat="server"
        ClientInstanceName="popupReport"
        Width="820px"
        Height="680px"
        MaxHeight="850px"
        MinWidth="820px"
        ShowFooter="False"
        FooterText="(c) nanosoft.com.au"
        PopupElementID="btnViewGeofencing"
        HeaderText="Area of Countries"
        EnableViewState="False"
        AllowDragging="True"
        AllowResize="True"
        DragElement="Window"
        ResizingMode="Postponed"
        ShowSizeGrip="True"
        CloseAction="CloseButton"
        PopupAnimationType="Fade"
        CloseAnimationType="Fade"
        PopupHorizontalAlign="LeftSides"
        PopupVerticalAlign="Below">

        <ClientSideEvents />


    </dx:ASPxPopupControl>

    <div style="padding: 10px 10px 10px 10px;">
        <div id="googleMap" style="width: 100%; height: 100%; background-color: gray;"></div>
    </div>

    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1"
        runat="server">
        <ClientSideEvents ControlsInitialized="function(s,e){FleetMap_ControlsInitialized();}" />

    </dx:ASPxGlobalEvents>

</asp:Content>
