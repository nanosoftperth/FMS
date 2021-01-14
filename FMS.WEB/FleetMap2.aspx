﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Main.master" CodeBehind="FleetMap2.aspx.vb" Inherits="FMS.WEB.FleetMap2" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxwschsc" %>


<asp:Content ID="ContentLeft" ContentPlaceHolderID="ContentLeft" runat="server">

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.36&key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g&libraries=places,visualization"></script>

    <script src="Content/javascript/firstRun.js?version=<%=WebVersion%>"></script>
    <script src="Content/javascript/accordian.js?version=<%=WebVersion%>"></script>
    <script src="Content/javascript/markerWithLabel.js?version=<%=WebVersion%>"></script>
    <script src="Content/javascript/FleetMap.js?version=<%=WebVersion%>"></script>

    <script src="Content/javascript/pathDrawer.js?version=<%=WebVersion%>"></script>
    <script src="Content/javascript/GeoFencing.js?version=<%=WebVersion%>"></script>
    <script src="Content/javascript/FleetMap_ClockLogic.js?version=<%=WebVersion%>"></script>

    <link href="Content/Accoridan.css" rel="stylesheet" />
    <script src="Content/javascript/numtowords.js"></script>


    <!-- CSS -->

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">


    <link rel="stylesheet" type="text/css"  href="Content/css/bootstrap.min.css?version=<%=WebVersion%>">
    <link rel="stylesheet" type="text/css" href="Content/navbar_left.css?version=<%=WebVersion%>">

    <!-- custom scrollbar for navbox on left (and other places if need be) -->
    <link rel="stylesheet" href="Content/css/jquery.mCustomScrollbar.min.css?version=<%=WebVersion%>">

       <!-- EXTERNAL JS LIBRARIES-->
    <script src="Content/javascript/jquery-1.12.4.js?version=<%=WebVersion%>"></script>
    <script src="Content/javascript/jquery-ui.js?version=<%=WebVersion%>"></script>
    <script src="Content/javascript/bootstrap.min.js?version=<%=WebVersion%>"></script>

    <!-- jQuery Custom Scroller CDN -->
    <script src="Content/javascript/jquery.mCustomScrollbar.concat.min.js?version=<%=WebVersion%>"></script>


    <style type="text/css">
        .dxgvSelectedRow_SoftOrange {
            background: #EBEBEB !important;
        }

        .dxgvInlineEditRow_SoftOrange, .dxgvDataRow_SoftOrange {
            background: #FFFFFF !important;
        }

        .clsImageShow {
            width: 25px;
            float: right;
        }

        .clsImageHide {
            display: none;
        }
    </style>

    <script>
        sessionStorage.removeItem('startupseq');
    </script>

    <!--
            ASPXPANEL ID = 'LEFT PANE' WAS TAKEN FROM HERE.    
            GET IT BACK FROM FLEETMAP.ASPX UNEDITED
     -->

    <div style="display: none;">

        <dx:ASPxPanel DefaultButton="ASPxButton1"
            ID="LeftPane"
            runat="server"
            FixedPosition="WindowLeft"
            ClientInstanceName="leftPane"
            CssClass="leftPane"
            Collapsible="True">

            <SettingsAdaptivity CollapseAtWindowInnerWidth="1" />
            <Styles>
                <Panel CssClass="panel"></Panel>
            </Styles>

            <PanelCollection>
                <dx:PanelContent runat="server" ID="test1" SupportsDisabledAttribute="True" Width="100%">
                    <section id="accordion">


                        <div class="sidebartop1">
                        </div>

                        <div class="sidebartop1">
                            <input class="chkbox" type="checkbox" id="check-3" />

                            <label class="accordianTitle" for="check-3">Activity Viewer</label>

                            <article>
                                <br />
                                <div style="font-weight: bold;">
                                </div>
                                <br />
                            </article>
                        </div>

                        <!--- Vehicle Viewer tab  -->

                        <!---End Vehicle Viewer tab  -->


                        <div>

                            <label class="accordianTitle" for="check-4">Geo-Fencing and Alerts</label>

                            <article>

                                <div style="padding: 5px;">
                                </div>
                            </article>
                        </div>

                        <div>
                            <dx:ASPxButton CssClass="floatright" ID="ASPxButton1" AutoPostBack="false"
                                ClientInstanceName="btnViewCurrentMapTime"
                                runat="server"
                                Text="View Current Map Time"
                                Theme="Office2010Black"
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
    </div>

    <dx:ASPxPopupControl ClientInstanceName="popupGeoCaching" Width="500px" Height="340px" MaxWidth="800px" MaxHeight="800px"
        MinHeight="340px"
        MinWidth="500px"
        ID="pcMain" 
        ShowFooter="True"
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
        CloseAction="CloseButton" 
        PopupAnimationType="Fade" 
        CloseAnimationType="Fade" 
        PopupHorizontalAlign="LeftSides"
        PopupVerticalAlign="Below">


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
            border: 2px solid black;
            white-space: nowrap;
            opacity: 0.9 !important;
            /*margin-top:39px !important;*/
        }

        .labels-hidden {
            display: none !important;
        }

        .labels:hover {
            /*opacity: 0.1 !Important;*/
            background-color: none !Important;
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
            opacity: 0.9 !important;
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
                                <CalendarProperties Style-CssClass="popupDateEdit" ShowClearButton="False" ShowTodayButton="False">
                                    <%--<ControlStyle CssClass="popupDateEdit"></ControlStyle>--%>

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
        PopupElementID=""
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

    <div class="wrapper">


        <nav id="sidebar" style="display: block;">

            <div class="sidebar-header">
                <img style="width: 100%; height: auto;" src="Content/Images/logo/logo-white.png">
            </div>

            <ul class="list-unstyled components">
                <%--<p>Map Options</p>--%>

                <li>

                    <input type="checkbox" class="chkbox" id="check-2" />

                    <p>
                        <input id="pac-input" type="text" placeholder="Search Box">
                    </p>

                </li>

                <li class="">

                    <a href="#selectDateTime" data-toggle="collapse" aria-expanded="false" class="collapsed">Select Date/Time</a>

                    <ul class="list-unstyled collapse" id="selectDateTime" aria-expanded="false" style="height: 0px;">

                        <li>
                            <input type="checkbox" class="chkbox" id="check-1" />

                            <span class="dateTimePicker" style="margin-left: 2px;">Enter the time you wish to view:</span>

                            <dx:ASPxDateEdit ID="ASPxDateEdit1"
                                runat="server"
                                Date="01/20/2016 00:06:00"
                                EditFormat="DateTime"
                                CssClass="topZindex"
                                Width="100%"
                                ClientInstanceName="dateViewThisDateTime"
                                EnableTheming="True"
                                Height="28px"
                                Theme="Office2010Black">

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
                                    KeyDown="function(s,e){dateedit_Click(s,e);e.returnValue = false;e.cancel = true;}" />

                                <Buttons>
                                    <dx:EditButton Visible="false" Text="view on map">
                                    </dx:EditButton>
                                </Buttons>
                                <ClearButton Visibility="Auto">
                                </ClearButton>

                                <Paddings PaddingLeft="10px"></Paddings>

                            </dx:ASPxDateEdit>
                        </li>
                    </ul>
                </li>

                <li class="">

                    <a href="#activityViewer" data-toggle="collapse" aria-expanded="false" class="collapsed">Activity Viewer</a>

                    <ul class="list-unstyled collapse" id="activityViewer" aria-expanded="false" style="height: 0px;">

                        <li>
                            <table class="activityViewerTable" style="width: 100% !important; display: table-cell !important;">
                                <tr>
                                    <td>
                                        <span class="dateTimePicker" style="margin-left: 2px; margin-top: 5px;">Vehicle / Driver:</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxDropDownEdit
                                            Width="100%"
                                            EnableTheming="True"
                                            Height="28px"
                                            Theme="Office2010Black"
                                            AutoPostBack="false" ClientInstanceName="checkComboBox" ID="ddlTrucks" runat="server" AnimationType="None">
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
                                            <ClientSideEvents TextChanged="SynchronizeListBoxValues"
                                                DropDown="SynchronizeListBoxValues" />
                                        </dx:ASPxDropDownEdit>
                                    </td>
                                </tr>


                                <tr>
                                    <td><span class="dateTimePicker" style="margin-left: 2px; margin-top: 5px;">Start Time:</span></td>
                                </tr>

                                <tr>
                                    <td>
                                        <dx:ASPxDateEdit ID="heatMapStartTime"
                                            AutoPostBack="false"
                                            ClientInstanceName="heatMapStartTime"
                                            ClientIDMode="Predictable"
                                            ClientEnabled="true"
                                            runat="server"
                                            Date="01/20/2016 00:06:00"
                                            EditFormat="DateTime"
                                            Width="100%"
                                            EnableTheming="True"
                                            Height="28px"
                                            Theme="Office2010Black">
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
                                            Width="100%"
                                            EnableTheming="True"
                                            Height="28px"
                                            Theme="Office2010Black">

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
                                    <td style="padding-top: 10px;">

                                        <li class="active">

                                            <a href="#activityViewerOptions" data-toggle="collapse" aria-expanded="false" class="collapsed subitem">* advanced options</a>

                                            <ul class="list-unstyled collapse" id="activityViewerOptions" aria-expanded="false" style="height: 0px;">

                                                <li>

                                                    <table id="tblHeatMapOptions" style="width: 100%;">

                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="cbExcludeCars" runat="server" ClientInstanceName="cbExcludeCars" TextAlign="Left" EnableTheming="True" Theme="Office2010Black" Text="Hide vehicles not selected" CheckState="Unchecked">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	cbExcludeCars_CheckChanged(s, e);
}" />
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="cb" runat="server" ClientInstanceName="cbGradientToggle" TextAlign="Left" EnableTheming="True" Theme="Office2010Black" Text="Gradient">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	changeGradient();
}" />
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="cbOpiacy" runat="server" ClientInstanceName="cbOpiacyToggle" TextAlign="Left" EnableTheming="True" Theme="Office2010Black" Text="Opiacy" Checked="true">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	changeOpacity();
}" />
                                                                </dx:ASPxCheckBox>

                                                            </td>
                                                        </tr>
                                                        <tr>


                                                            <td>
                                                                <dx:ASPxCheckBox ID="cbRadius" ClientInstanceName="cdRadiusToggle" runat="server" TextAlign="Left" EnableTheming="True" Theme="Office2010Black" Text="Radius" Checked="true">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	changeRadius();
}" />
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="ASPxCheckBox1" Checked="true" ClientInstanceName="cbShowjourney" runat="server" TextAlign="Left" EnableTheming="True" Theme="Office2010Black" Text="Show Journey">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	showJourneyTogle()
}" />
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="ASPxCheckBox2" ClientInstanceName="cbShowHeatmap" runat="server" TextAlign="Left" EnableTheming="True" Theme="Office2010Black" Text="Show Heatmap" Checked="true">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	toggleHeatmap()
}" />
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="ASPxCheckBox5" runat="server" ClientInstanceName="cbFollowTruck" TextAlign="Left" EnableTheming="True" Theme="Office2010Black" Text="Follow Vehicle" Checked="true">
                                                                </dx:ASPxCheckBox>

                                                            </td>
                                                        </tr>


                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="ASPxCheckBox3" ClientInstanceName="cbHeatmapAutoUpdate" runat="server" TextAlign="Left" EnableTheming="True" Theme="Office2010Black" Text="Real-time update" Checked="false">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	cbHeatmapAutoUpdate_CheckChanged(s,e)
}" />
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="cbSnapToRoad" ClientInstanceName="cbSnapToRoad" runat="server" TextAlign="Left" EnableTheming="True" Theme="Office2010Black" Text="Snap to road" Checked="true">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	cbSnapToRoad_CheckChanged(s,e)
}" />
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>




                                                    </table>


                                                </li>
                                            </ul>

                                        </li>


                                    </td>

                                </tr>

                                <tr>

                                    <td>

                                        <table>
                                            <tr>
                                                <td colspan="4">
                                                    <span id="autoplayJourney">Auto-play journey?
                                                    </span>
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

                                <tr>
                                    <td style="padding: 15px 0px 5px 0px; text-align: right;">
                                        <dx:ASPxButton ID="btnHeatMapSearch"
                                            ClientInstanceName="btnHeatMapSearch"
                                            runat="server"
                                            AutoPostBack="false"
                                            EnableTheming="True"
                                            Theme="Office2010Black"
                                            Text="View Activity">

                                            <ClientSideEvents Click="function(){btnHeatMapSearch_Click();}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>

                        </li>
                    </ul>

                </li>


                <li class="vehicle_viewer">

                    <a href="#vehicle_viewer" data-toggle="collapse" aria-expanded="false" class="collapsed">Vehicle Visibility</a>

                    <ul class="list-unstyled collapse" id="vehicle_viewer" aria-expanded="false" style="height: 0px;">

                        <li>


                            <input class="chkbox" type="checkbox" id="check-5" />

                            <dx:ASPxGridView ID="dgvVehicles"
                                ClientInstanceName="dgvVehicles"
                                runat="server"
                                AutoGenerateColumns="False"
                                DataSourceID="odsVehicles"
                                EnableTheming="True"
                                KeyFieldName="ApplicationVehileID"
                                SettingsBehavior-ConfirmDelete="true"
                                Theme="Office2010Black"
                                OnDataBound="dgvVehicles_DataBound"
                                Style="width: 230px"
                                OnPreRender="dgvVehicles_PreRender">

                                <Settings ShowFilterRow="True" />

                                <SettingsAdaptivity>
                                    <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                                </SettingsAdaptivity>

                                <SettingsPager PageSize="5"></SettingsPager>

                                <SettingsBehavior ConfirmDelete="True"></SettingsBehavior>

                                <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="DeviceID" VisibleIndex="0" Caption="Show">
                                        <FilterTemplate>
                                            <div style="margin-left: 5px">
                                                <input type="checkbox" id="chkHeaderShow" onchange="GetAllSelected(this);" class="chkHead" />
                                            </div>
                                        </FilterTemplate>
                                        <DataItemTemplate>
                                            <div>
                                                <input type="checkbox" id="chkShow" class="chk" value='<%# Eval("DeviceID")%>' onchange="GetID(this.value, this)" runat="server" />
                                            </div>
                                        </DataItemTemplate>
                                        <Settings AllowAutoFilter="False" />

                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="2" Caption="Vehicle">
                                        <DataItemTemplate>
                                            <div style="width: 100%">
                                                <div style="width: 30px; float: left;">
                                                    <asp:Image ID="imgVehicle" ImageUrl='<%#"ImageHandler.ashx?imgID="& Convert.ToString(Eval("ApplicationImageID"))%>' runat="server" Height="26px" Width="26px" />
                                                </div>
                                                <div style="width: 70%; padding-top: 5px; font-size: 12px; float: left;">
                                                    <%#Eval("Name")%>
                                                </div>
                                            </div>
                                        </DataItemTemplate>
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <ClientSideEvents EndCallback="function(s, e) { GetSelectedRows(s,e);}" />

                            </dx:ASPxGridView>

                            <!-- DataSources  -->
                            <asp:ObjectDataSource ID="odsMapMarker" runat="server" SelectMethod="GetAllApplicationImages" TypeName="FMS.Business.DataObjects.ApplicationImage">
                                <SelectParameters>
                                    <asp:SessionParameter DbType="Guid" Name="applicationid" SessionField="ApplicationID" />
                                    <asp:Parameter Name="type" Type="String" DefaultValue="vehicle" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:ObjectDataSource ID="odsVehicles" runat="server" DataObjectTypeName="FMS.Business.DataObjects.ApplicationVehicle" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.ApplicationVehicle" UpdateMethod="Update">
                                <SelectParameters>
                                    <asp:SessionParameter DbType="Guid" Name="appplicationID" SessionField="ApplicationID" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:ObjectDataSource ID="odsTrucks"
                                runat="server"
                                SelectMethod="GetExampleFleetNow"
                                TypeName="FMS.Business.Truck">

                                <SelectParameters>
                                    <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appid"></asp:SessionParameter>
                                </SelectParameters>
                            </asp:ObjectDataSource>

                        </li>
                    </ul>

                </li>


                <li class="">

                    <a href="#geofences" data-toggle="collapse" aria-expanded="false" class="collapsed">Geo-Fences</a>

                    <input class="chkbox" type="checkbox" id="check-4" />

                    <ul class="list-unstyled collapse" id="geofences" aria-expanded="false" style="height: 0px;">

                        <li>

                            <dx:ASPxCheckBox ClientInstanceName="cbViewGeoFences"
                                ID="cbViewGeoFences"
                                Checked="false"
                                runat="server"
                                EnableTheming="True"
                                TextAlign="Left"
                                AutoPostBack="false"
                                Text="show geo-fences">
                                <ClientSideEvents CheckedChanged="function(s, e) {
	cbViewGeoFences_checkChanged(e);
}" />
                            </dx:ASPxCheckBox>

                        </li>
                        <li>

                            <dx:ASPxCheckBox ClientInstanceName="cbViewGeoFencesWithBooking"
                                ID="cbViewGeoFencesWithBooking"
                                Checked="false"
                                runat="server"
                                TextAlign="Left"
                                EnableTheming="True"
                                AutoPostBack="false"
                                Text="include bookings">
                                <ClientSideEvents CheckedChanged="function(s, e) {
	cbViewGeoFencesWithBooking_checkChanged(e);
}" />
                            </dx:ASPxCheckBox>

                        </li>

                        <li>
                            <dx:ASPxCheckBox ClientInstanceName="cbViewGeoFenceLabels"
                                ID="cbViewGeoFenceLabels"
                                Checked="True"
                                runat="server"
                                EnableTheming="True"
                                TextAlign="Left"
                                Text="show labels"
                                CheckState="Checked">

                                <ClientSideEvents CheckedChanged="function(s, e) {
	cbViewGeoFenceLabels_checkChanged(e);
}" />
                            </dx:ASPxCheckBox>
                        </li>
                        <li>
                            <dx:ASPxButton CssClass="floatright"
                                ID="btnViewGeofencing"
                                AutoPostBack="false"
                                ClientInstanceName="btnViewGeofencing"
                                runat="server"
                                Text="Configure Geo-Fences"
                                Theme="Office2010Black"
                                EnableTheming="True">

                                <ClientSideEvents Click="function(s, e) {
	                                                SetPCVisible (true);
                                                }"></ClientSideEvents>


                            </dx:ASPxButton>
                        </li>

                    </ul>

                </li>


            </ul>

            <hr>


            <ul class="list-unstyled in indented">

                <p>Quick Links</p>
                <li>
                    <a target="_blank" href="AlarmsAndAlerts.aspx"> Record of Geo-Fence events </a>
                </li>
                <li></li>
                <li>
                    <a target="_blank" href="AlarmsAndEvents.aspx">Alarms and Events</a>                    
                </li>
                <li></li>
                <li>
                    <a target="_blank" href="ReportGenerator.aspx">Reporting System </a>
                </li>
                <li></li>
                <li>
                    <a target="_blank" href="Dashboarding.aspx">Dashboard </a>
                </li>
                <li></li>
            </ul>

            <hr>

            <ul class="list-unstyled CTAs">
                <li><a href="javascript:SetPCVisible(true);" class="article">Geo-Fence Explorer</a></li>
                <li><a href="javascript:SetPCVisible(true);" class="article">Map Date &amp; Time</a></li>
            </ul>
        </nav>

        <div class="content" id="googleMap" style="position: relative; overflow: hidden;">
        </div>

    </div>

    <div id="closeButtoncontent" style="display: none;">
        <button type="button" id="sidebarCollapse" class="navbar-btn">
            <span></span>
            <span></span>
            <span></span>
        </button>

    </div>


    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1"
        runat="server">
        <ClientSideEvents ControlsInitialized="function(s,e){FleetMap_ControlsInitialized();}" />

    </dx:ASPxGlobalEvents>

    <script type="text/javascript"> 
        var VehicleSelectedArrfilter = [];
        // Get All Selected Vehicles
        $(document).ready(function () {
            $('.chkHead').prop('checked', true);
        });

        function GetID(_Value, element) {

            if (element.checked) {
                // To Show vehicle on Map
                $(element).parent().parent().parent().removeClass("dxgvSelectedRow_SoftOrange");
                $(element).parent().parent().parent().addClass("dxgvInlineEditRow_SoftOrange dxgvDataRow_SoftOrange");

                for (var i = 0; i < markers.length; i++) {
                    if (markers[i].ID == _Value) {
                        markers[i].setMap(map);
                        //localStorage.setItem('IsHiddenVehcile', 'false');
                    }
                }

                var isFind = VehicleSelectedArr.filter(function (name) { return name == _Value });
                if (isFind == '')
                    VehicleSelectedArr.push(_Value);
                //VehicleSelectedArr.push(_Value);
                //debugger;
                //if ($.inArray(markers[i].ID, VehicleSelectedArr) > -1) {
                //    //do something    
                //}
                //else {
                //    VehicleSelectedArr.push(markers[i].ID);
                //}

                if (markers.length == VehicleSelectedArr.length) {
                    $('.chkHead').prop('checked', true);
                }
            }
            else {
                $(element).parent().parent().parent().removeClass("dxgvInlineEditRow_SoftOrange dxgvDataRow_SoftOrange");
                $(element).parent().parent().parent().addClass("dxgvSelectedRow_SoftOrange");
                // To hide vehicle on Map
                for (var i = 0; i < markers.length; i++) {
                    if (markers[i].ID == _Value) {
                        markers[i].setMap(null);
                        //localStorage.setItem('IsHiddenVehcile', 'true');
                    }

                }
                var i = VehicleSelectedArr.indexOf(_Value);
                if (i != -1) {
                    VehicleSelectedArr.splice(i, 1);
                }

                $('.chkHead').prop('checked', false);
            }

            //  console.log(VehicleSelectedArr);
        }
        function GetIcon(id) {
            if ($("#lblVehicle").hasClass("showHide")) {
                $("#lblVehicle").removeClass("showHide");
                $("#img").attr("src", "");
                $("#img").removeClass("clsImageShow");
                $("#img").addClass("clsImageHide");
            }
            else {
                $("#lblVehicle").addClass("showHide");
                if (markers.length > VehicleSelectedArr.length) {
                    $("#img").attr("src", "Content/image.png");
                    $("#img").removeClass("clsImageHide");
                    $("#img").addClass("clsImageShow");
                    if ((markers.length - VehicleSelectedArr.length) == 1) {
                        $("#img").attr("title", "Warning! " + numinwrd(markers.length - VehicleSelectedArr.length) + " <%= GetCompanyName()%>" + " vehicle is hidden.");
                    }
                    else {
                        $("#img").attr("title", "Warning! " + numinwrd(markers.length - VehicleSelectedArr.length) + " <%= GetCompanyName()%>" + " vehicles are hidden.");
                    }
                }
                else {
                    $("#img").attr("src", "");
                    $("#img").removeClass("clsImageShow");
                    $("#img").addClass("clsImageHide");
                }
            }
        }

        function GetAllSelected(element) {
            //console.log("Before Filter" + VehicleSelectedArr);
            VehicleSelectedArrfilter = [];
            if (element.checked) {
                //var index = 0;
                $("table#ctl00_ctl00_MainPane_Content_ContentLeft_LeftPane_dgvVehicles_DXMainTable > tbody > tr").each(function (index, element) {
                    if (index > 1) {
                        $(this).removeClass("dxgvDataRow_SoftOrange");
                        $(this).addClass("dxgvInlineEditRow_SoftOrange dxgvSelectedRow_SoftOrange");

                        var item = $(element.children[0]).find('div').children()[0];

                        VehicleSelectedArrfilter.push(item.value);
                    }
                    //index = index + 1;
                });
                $('.chk').prop('checked', true);
                for (var i = 0; i < markers.length; i++) {

                    var IsSelected = VehicleSelectedArrfilter.filter(function (name) { return name == markers[i].ID });

                    if (IsSelected != '') {
                        markers[i].setMap(map);

                        var isFind = VehicleSelectedArr.filter(function (name) { return name == markers[i].ID });
                        if (isFind == '')
                            VehicleSelectedArr.push(markers[i].ID);
                    }
                }
            }
            else {
                //var index = 0;
                $("table#ctl00_ctl00_MainPane_Content_ContentLeft_LeftPane_dgvVehicles_DXMainTable > tbody > tr").each(function (index, element) {
                    if (index > 1) {
                        $(this).removeClass("dxgvInlineEditRow_SoftOrange dxgvDataRow_SoftOrange");
                        $(this).addClass("dxgvSelectedRow_SoftOrange");

                        var item = $(element.children[0]).find('div').children()[0];

                        VehicleSelectedArrfilter.push(item.value);
                    }
                    //index = index + 1;
                });
                $('.chk').prop('checked', false);
                for (var i = 0; i < markers.length; i++) {

                    var IsUnSelected = VehicleSelectedArrfilter.filter(function (name) { return name == markers[i].ID });

                    if (IsUnSelected != '') {
                        markers[i].setMap(null);

                        var item = VehicleSelectedArr.indexOf(markers[i].ID);
                        if (item != -1) {
                            VehicleSelectedArr.splice(item, 1);
                        }
                    }
                }
            }

        }

        function GetSelectedRows(s, e) {
            //var count = { checked: 0, row: 0 };
            $.each($("table#ctl00_ctl00_MainPane_Content_ContentLeft_LeftPane_dgvVehicles_DXMainTable > tbody > tr"), function (index, element) {
                if (index > 1) {
                    //count.row++;
                    var item = $(element.children[0]).find('div').children()[0];
                    var isFind = VehicleSelectedArr.filter(function (name) { return name == item.value });
                    if (isFind != '') {
                        // count.checked++;
                        $(item).prop('checked', true);
                    }
                }
            });
            if (markers.length == VehicleSelectedArr.length)

                $('.chkHead').prop('checked', true);

        }
    </script>

    <script type="text/javascript">

        /*
                 MAKE THE SIDEBAR SCROLLABLE AND NOT GROW WHEN IT GETS MORE CONTENT, STRETCHING THE PAGE.
        */

        $(document).ready(function () {

            $("#sidebar").mCustomScrollbar({
                theme: "minimal"
            });

            $('#sidebarCollapse').on('click', function () {
                // open or close navbar
                $('#sidebar').toggleClass('active');
                // close dropdowns
                $('.collapse.in').toggleClass('in');
                // and also adjust aria-expanded attributes we use for the open/closed arrows
                // in our CSS
                $('a[aria-expandjavascript:showModalAndRefreshContent();ed=true]').attr('aria-expanded', 'false');
            });

        });


    </script>

</asp:Content>