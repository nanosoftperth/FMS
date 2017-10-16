<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="SiteDetailsMain.aspx.vb" Inherits="FMS.WEB.SiteDetailsMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link href="../../Content/grid/bootstrap.css" rel="stylesheet">
<link href="../../Content/grid/grid.css" rel="stylesheet">
<script src="../../Content/javascript/jquery-1.10.2.min.js"></script>
<style>
    .container {
        width: 910px;
    }
</style>
<script>
    function ShowLoginWindow() {
        //viewPopup.ShowAtPos(100, 10);
        viewPopup.Show();
    }
    function ExecuteLinkIndustryGroup() {
   
        ShowLoginWindow();
        
        $("#ifrIndustryGroup").attr("src", "http://localhost:18351/Applications/Cannon/IndustryGroupPopup.aspx");
        
        //$("#ifrIndustryGroup").click();
    }
</script>
    <a id="lnkIndustryGroup" href="#" target="ifrIndustryGroup"></a>
    <dx:ASPxGridView ID="SiteDetailsGridView" runat="server" DataSourceID="odsSiteDetails" AutoGenerateColumns="False" 
        KeyFieldName="Cid" Width="550px"  Theme="SoftOrange" OnRowUpdating="SiteDetailsGridView_RowUpdating" OnRowInserting="SiteDetailsGridView_RowInserting">
        <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
        <Columns>
            <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="SiteID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Cid" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="SiteName" VisibleIndex="3"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Customer" VisibleIndex="4"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="AddressLine1" VisibleIndex="5"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Suburb" VisibleIndex="6" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="State" VisibleIndex="7" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PostCode" VisibleIndex="8" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PhoneNo" VisibleIndex="9" Visible="false"></dx:GridViewDataTextColumn>
        </Columns>
        <Settings ShowPreview="true" />
        <SettingsPager PageSize="10" />
        <SettingsEditing Mode="PopupEditForm"/>
        <SettingsPopup>
            <EditForm  Modal="true" 
                VerticalAlign="WindowCenter" 
                HorizontalAlign="WindowCenter"/>                
        </SettingsPopup>
        <Templates>
            <EditForm>
                <div class="container">
                    <div style="display:none">
                        <dx:ASPxTextBox  id="txtSiteID" ClientInstanceName="siteID" runat="server" Text='<%# Eval("SiteID") %>'></dx:ASPxTextBox>
                    </div>
                    <div class="row"></div>
                    <dx:ASPxPageControl ID="SiteDetailsPageControl" runat="server">
                        <TabPages>
                            <dx:TabPage Name="SiteDetails" Text="Site Details">
                                <ContentCollection>
                                    <dx:ContentControl runat="server">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <dx:ASPxLabel ID="lblSiteName" runat="server" Text="Site&nbsp;Name:" Width="100px"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-md-3">
                                                <dx:ASPxTextBox ID="txtSiteName" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("SiteName") %>'></dx:ASPxTextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <dx:ASPxLabel ID="lblAddressLine1" runat="server" Text="Address&nbsp;Line&nbsp;1:" Width="100px"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-md-3">
                                                <dx:ASPxTextBox ID="txtAddressLine1" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("AddressLine1") %>'></dx:ASPxTextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <dx:ASPxLabel ID="lblAddressLine2" runat="server" Text="Address&nbsp;Line&nbsp;2:" Width="100px"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-md-3">
                                                <dx:ASPxTextBox ID="txtAddressLine2" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("AddressLine2")%>'></dx:ASPxTextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <dx:ASPxLabel ID="lblAddressLine3" runat="server" Text="Address&nbsp;Line&nbsp;3:" Width="100px"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-md-3">
                                                <dx:ASPxTextBox ID="txtAddressLine3" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("AddressLine3")%>'></dx:ASPxTextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <dx:ASPxLabel ID="lblAddressLine4" runat="server" Text="Address&nbsp;Line&nbsp;4:" Width="100px"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-md-3">
                                                <dx:ASPxTextBox ID="txtAddressLine4" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("AddressLine4")%>'></dx:ASPxTextBox>
                                            </div>
                                        </div>
                                        <div class="row ">
                                            <div class="col-md-2">
                                                <dx:ASPxLabel ID="lblSuburb" runat="server" Text="Suburb:" Width="100px"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-md-7">
                                                <div class="container">
                                                    <div class="row row-md-margin-top">
                                                        <dx:ASPxTextBox ID="txtSuburb" runat="server" Width="111px" MaxLength="22" Text='<%# Eval("Suburb") %>'></dx:ASPxTextBox>&nbsp;
                                                        <dx:ASPxLabel ID="lblState" runat="server" Text="State:"></dx:ASPxLabel>&nbsp;
                                                        <dx:ASPxComboBox ID="cbState" DataSourceID="odsStates" runat="server" Width="112px" SelectedIndex='<%# Eval("StateSortOrder") - 1 %>'  Height="20px" TextField="StateDesc" ValueField="Sid"></dx:ASPxComboBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="lblPCode" runat="server" Text="P/Code:"></dx:ASPxLabel>&nbsp;
                                                        <dx:ASPxTextBox ID="txtPCode" runat="server" Width="50px" MaxLength="22" Text='<%# Eval("PostCode") %>'></dx:ASPxTextBox>&nbsp;
                                                        <dx:ASPxLabel ID="lblZone" runat="server" Text="Zone:"></dx:ASPxLabel>&nbsp;
                                                        <dx:ASPxComboBox ID="cbZone" DataSourceID="odsZones" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("ZoneSortOrder") - 1%>' TextField="AreaDescription" ValueField="Aid"></dx:ASPxComboBox>
                                                        <%--<dx:ASPxComboBox ID="cbZone" DataSourceID="odsZones" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("ZoneSortOrder") - 1%>'  TextField="AreaDescription" ValueField="Aid"></dx:ASPxComboBox>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <dx:ASPxLabel ID="lblCustomer" runat="server" Text="Customer:" Width="100px"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-md-3">
                                                <%--<dx:ASPxTextBox ID="txtCustomer" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("Customer")%>'></dx:ASPxTextBox>--%>
                                                <dx:ASPxComboBox ID="cbCustomer" DataSourceID="odsCustomers" runat="server" Width="260px" Height="20px" 
                                                     CallbackPageSize="30" SelectedIndex='<%# Eval("CustomerSortOrder") - 1%>'  TextField="CustomerName" ValueField="Cid">
                                                    <Columns>                                   
                                                        <dx:ListBoxColumn FieldName="CustomerName" Width="130px" />
                                                        <dx:ListBoxColumn FieldName="Suburb" Width="100%" />
                                                    </Columns>
                                                </dx:ASPxComboBox>
                                            </div>
                                            <div class="col-md-1">
                                                <dx:ASPxButton ID="btnCustomerDetails" ClientInstanceName="btnCustomerDetails" AutoPostBack="false" runat="server" Text="Customer Details">
                                                    <%--<ClientSideEvents Click="function(s,e) {
                                                            ViewSitesClick(txtViewID.GetText());
                                                        }" />--%>
                                                </dx:ASPxButton>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-md-2">
                                                <dx:ASPxLabel ID="lblIndustryGroup" runat="server" Text="Industry Group:" Width="100px"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-md-2">
                                                <dx:ASPxComboBox ID="cbIndustryGroup" DataSourceID="odsIndustryGroups" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("IndustrySortOrder") - 1%>' TextField="IndustryDescription" ValueField="Aid"></dx:ASPxComboBox>
                                            </div>
                                            <div class="col-md-1">
                                                <dx:ASPxButton ID="btnIndustryGroup" ClientInstanceName="btnIndustryGroup" AutoPostBack="false" runat="server" Text="Industry Group">
                                                    <ClientSideEvents Click="function(s,e) {
                                                            ExecuteLinkIndustryGroup();
                                                        }" />
                                                </dx:ASPxButton>
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <dx:ASPxLabel ID="lblSiteContactName" runat="server" Text="Site&nbsp;Contact&nbsp;Name:" Width="100px"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-md-3">
                                                <dx:ASPxTextBox ID="txtContactName" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("SiteContactName")%>'></dx:ASPxTextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <dx:ASPxLabel ID="lblSitePhone" runat="server" Text="Site&nbsp;Phone:" Width="100px"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-md-3">
                                                <dx:ASPxTextBox ID="txtSitePhone" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("SiteContactPhone")%>'></dx:ASPxTextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <dx:ASPxLabel ID="lblSiteFax" runat="server" Text="Site&nbsp;Fax:" Width="100px"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-md-3">
                                                <dx:ASPxTextBox ID="txtSiteFax" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("SiteContactFax")%>'></dx:ASPxTextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <dx:ASPxLabel ID="lblSiteMobile" runat="server" Text="Site&nbsp;Mobile:" Width="100px"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-md-3">
                                                <dx:ASPxTextBox ID="txtSiteContactMobile" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("SiteContactMobile")%>'></dx:ASPxTextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <dx:ASPxLabel ID="lblSiteEmail" runat="server" Text="Site&nbsp;Email:" Width="100px"></dx:ASPxLabel>
                                            </div>
                                            <div class="col-md-3">
                                                <dx:ASPxTextBox ID="txtSiteEmail" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("SiteContactEmail")%>'></dx:ASPxTextBox>
                                            </div>
                                        </div>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Name="SiteContractDetails" Text="Site Contract Details">
                            </dx:TabPage>
                            <dx:TabPage Name="SiteInvoicingDetails" Text="Site Invoicing Details">
                            </dx:TabPage>
                            <dx:TabPage Name="SiteServices" Text="Site Services">
                            </dx:TabPage>
                            <dx:TabPage Name="CIRHistory" Text="CIR History">
                            </dx:TabPage>
                            <dx:TabPage Name="Comments" Text="Comments">
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                </div>
                <div style="text-align: right; padding: 2px">
                    <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                        runat="server">
                    </dx:ASPxGridViewTemplateReplacement>
                    <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                        runat="server">
                    </dx:ASPxGridViewTemplateReplacement>
                </div>
            </EditForm>
        </Templates>
    </dx:ASPxGridView>

    <dx:ASPxPopupControl ID="viewPopup" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="viewPopup" 
        HeaderText="Site List" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="300px" Height="300px" >        
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="Panel2" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">             
                            <iframe id="ifrIndustryGroup" name="ifrIndustryGroup" src="#" style="width:460px; height:450px;"></iframe>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>

    <asp:ObjectDataSource ID="odsIndustryGroups" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblIndustryGroups"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCustomers" runat="server" SelectMethod="GetAllWithZoneSortOrder" TypeName="FMS.Business.DataObjects.tblCustomers"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsZones" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tbZone" ></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsStates" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblStates"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsSiteDetails" runat="server" SelectMethod="GetAllWithZoneSortOrder" TypeName="FMS.Business.DataObjects.tblSites"></asp:ObjectDataSource>
    
</asp:Content>
