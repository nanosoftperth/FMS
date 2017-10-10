﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MainLight.master" CodeBehind="CustomerDetailsMain.aspx.vb" Inherits="FMS.WEB.CustomerDetailsMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link href="../../Content/grid/bootstrap.css" rel="stylesheet">
<link href="../../Content/grid/grid.css" rel="stylesheet">
<script src="../../Content/javascript/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function OnPressureValuesClick(contentUrl) {
            document.getElementById("pMessage2").innerHTML = contentUrl;
            ShowLoginWindow2();
        }
     
        function ShowLoginWindow2() {
            pcLogin2.ShowAtPos(10, 10);
            pcLogin2.Show();
        }

    </script>
        <dx:ASPxGridView ID="CustomersGridView" runat="server" DataSourceID="odsCustomer" AutoGenerateColumns="False" 
            KeyFieldName="Cid" Width="550px"  Theme="SoftOrange" OnRowUpdating="CustomersGridView_RowUpdating" OnRowInserting="CustomersGridView_RowInserting">
            <ClientSideEvents SelectionChanged="function(s,e){ alert('xxxxx'); }" />
            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="CustomerID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CustomerName" VisibleIndex="9"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AddressLine1" VisibleIndex="10"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AddressLine2" VisibleIndex="11"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="State" VisibleIndex="12" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Suburb" VisibleIndex="13" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PostCode" VisibleIndex="14" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CustomerContactName" VisibleIndex="14" Visible="false"></dx:GridViewDataTextColumn>
            </Columns>
            <Settings ShowPreview="true" />
            <SettingsPager PageSize="10" />
            <%--<SettingsEditing Mode="PopupEditForm"/>
            <SettingsPopup>
                <EditForm  Modal="true" 
                    VerticalAlign="WindowCenter" 
                    HorizontalAlign="WindowCenter"/>                
            </SettingsPopup>--%>
            <Templates>
                <EditForm>
                    <div class="container">
                        <div style="display:none">
                            <dx:ASPxTextBox  id="txtCustomerID" runat="server" Text='<%# Eval("CustomerID") %>'></dx:ASPxTextBox>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblCustomerName" runat="server" Text="Customer&nbsp;Name:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtCustomerName" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("CustomerName") %>'></dx:ASPxTextBox>
                            </div>
                            <div class="col-md-1">
                                <dx:ASPxButton ID="btnViewSites" AutoPostBack="false" runat="server" Text="View Sites">
                                    <ClientSideEvents Click="function(s,e) { OnPressureValuesClick('you clicked me!');}" />
                                </dx:ASPxButton>
                            </div>
                            <div class="col-md-1">
                                <dx:ASPxTextBox ID="txtViewID" runat="server" Width="50px" Text='<%# Eval("CID") %>'></dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblAddressLine1" runat="server" Text="Address&nbsp;Line&nbsp;1:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtAddressLine1" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("AddressLine1") %>'></dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblAddressLine2" runat="server" Text="Address&nbsp;Line&nbsp;2:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtAddressLine2" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("AddressLine2") %>'></dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="row ">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblSuburb" runat="server" Text="Suburb:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-5">
                                <div class="container">
                                    <div class="row row-md-margin-top">
                                        <dx:ASPxTextBox ID="txtSuburb" runat="server" Width="111px" MaxLength="22" Text='<%# Eval("Suburb") %>'></dx:ASPxTextBox>&nbsp;
                                        <dx:ASPxLabel ID="lblState" runat="server" Text="State:"></dx:ASPxLabel>&nbsp;
                                        <dx:ASPxComboBox ID="cbState" DataSourceID="odsStates" runat="server" Width="112px" SelectedIndex='<%# Eval("state") - 1 %>'  Height="20px" TextField="StateDesc" ValueField="Sid"></dx:ASPxComboBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                        <dx:ASPxLabel ID="lblPCode" runat="server" Text="P/Code:"></dx:ASPxLabel>&nbsp;
                                        <dx:ASPxTextBox ID="txtPCode" runat="server" Width="50px" MaxLength="22" Text='<%# Eval("PostCode") %>'></dx:ASPxTextBox>&nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblCustCommencementDate" runat="server" Text="Customer&nbsp;Commencement&nbsp;Date:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-5">
                                <div class="container">
                                    <div class="row row-md-margin-top">
                                        <dx:ASPxDateEdit ID="dtCustCommencementDate" runat="server" Date='<%# Eval("CustomerCommencementDate") %>'></dx:ASPxDateEdit>&nbsp;
                                        <dx:ASPxLabel ID="lblYears" runat="server" Text="Years:"></dx:ASPxLabel>&nbsp;
                                        <dx:ASPxTextBox ID="txtYears" runat="server" Width="50px" MaxLength="22" Text="10"></dx:ASPxTextBox>&nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblCustomerRating" runat="server" Text="Customer&nbsp;Rating:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxComboBox ID="cbCustomerRating" DataSourceID="odsCustomerRating" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("CustomerRating") - 1 %>' TextField="CustomerRating" ValueField="Rid"></dx:ASPxComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblZone" runat="server" Text="Zone:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxComboBox ID="cbZone" DataSourceID="odsZones" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("ZoneSortOrder") - 1%>'  TextField="AreaDescription" ValueField="Aid"></dx:ASPxComboBox>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-3">
                                <dx:ASPxLabel ID="lblPerAnnumValue" runat="server" Text="Per Annum Value:" ForeColor="Blue" Font-Bold="true" Width="300px"></dx:ASPxLabel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblCustomerContactName" runat="server" Text="Customer&nbsp;Contact&nbsp;Name:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtCustomerContactName" runat="server" Width="170px" MaxLength="50" Text='<%# Eval("CustomerContactName") %>'></dx:ASPxTextBox>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtPerAnnumValue" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("CustomerValue") %>'></dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblCustomerPhone" runat="server" Text="Customer&nbsp;Phone:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtCustomerPhone" runat="server" Width="170px" MaxLength="50" Text='<%# Eval("CustomerPhone")%>'></dx:ASPxTextBox>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-3">
                                <dx:ASPxButton ID="btnUpdateValue" runat="server" Text="Update Value"></dx:ASPxButton>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblCustomerMobile" runat="server" Text="Customer&nbsp;Mobile:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtCustomerMobile" runat="server" Width="170px" MaxLength="50" Text='<%# Eval("CustomerMobile")%>'></dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblCustomerFax" runat="server" Text="Customer&nbsp;Fax:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtCustomerFax" runat="server" Width="170px" MaxLength="50" Text='<%# Eval("CustomerFax")%>'></dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblCustomerComments" runat="server" Text="Customer&nbsp;Comments:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtCustomerComments" runat="server" Width="270px" Height="100px" Text='<%# Eval("CustomerComments")%>'></dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblCustomerAgentName" runat="server" Text="Customer&nbsp;Agent&nbsp;Name:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxComboBox ID="cbCustomerAgentName" DataSourceID="odsCustomerAgents" runat="server" Width="260px" Height="20px" SelectedIndex='<%# Eval("AgentSortOrder") - 1%>' TextField="CustomerAgentName" ValueField="Aid"></dx:ASPxComboBox>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-1 col-md-1_5">
                                <dx:ASPxButton ID="btnAddNew" runat="server" Text="Add New"></dx:ASPxButton>
                            </div>
                             <div class="col-md-1 col-md-1_5">
                                <dx:ASPxButton ID="btnExit" runat="server" Text="Exit"></dx:ASPxButton>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblMYOBCustomerNumber" runat="server" Text="MYOB&nbsp;Customer&nbsp;Number:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtMYOBCustomerNumber" runat="server" Width="60px" MaxLength="50" Text='<%# Eval("MYOBCustomerNumber")%>'></dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 col-md-1_5">
                                <dx:ASPxLabel ID="lblInactiveCustomer" runat="server" Text="Inactive&nbsp;Customer:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-1 col-md-1_5">
                                <dx:ASPxCheckBox ID="chkInActiveCustomer" runat="server" Checked='<%# Eval("InactiveCustomer")%>'></dx:ASPxCheckBox>
                            </div>
                            <div class="col-md-1 col-md-1_5">
                                <dx:ASPxLabel ID="lblExcludeFuelLevy" runat="server" Text="Exclude&nbsp;Fuel&nbsp;Levy:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-1 col-md-1_5">
                                <dx:ASPxCheckBox ID="chkExcludeFuelLevy" runat="server" Checked='<%# Eval("chkCustomerExcludeFuelLevy")%>'></dx:ASPxCheckBox>
                            </div>
                            <div class="col-md-1 col-md-1_5">
                                <dx:ASPxLabel ID="lblRateIncrease" runat="server" Text="Rate&nbsp;Increase:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-1 col-md-1_5">
                                <dx:ASPxComboBox ID="cbRateIncrease" DataSourceID="odsRateIncreaseReference" runat="server" Width="100px" Height="20px" SelectedIndex='<%# Eval("RateIncreaseSortOrder") - 1%>'  TextField="RateIncreaseDescription" ValueField="Aid"></dx:ASPxComboBox>
                            </div>
                        </div>
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
    <dx:ASPxPopupControl ID="pcLogin2" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="Center" PopupVerticalAlign="Middle" ClientInstanceName="pcLogin2" 
        HeaderText="Fault Code Information" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="370px" >        
           
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="Panel2" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">             
                            <p id="pMessage2"></p>
                            <dx:ASPxGridView ID="SiteGridView" DataSourceID="odsSitesView" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="ZoneID" VisibleIndex="0"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Aid" VisibleIndex="1"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="AreaDescription" VisibleIndex="2"></dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
    <asp:ObjectDataSource ID="odsSitesView" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tbZone"></asp:ObjectDataSource>    
    <asp:ObjectDataSource ID="odsRateIncreaseReference" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRateIncreaseReference"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCustomerAgents" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblCustomerAgent"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsZones" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tbZone" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsStates" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblStates"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCustomerRating" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblCustomerRating"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCustomer" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblCustomers" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllWithZoneSortOrder" TypeName="FMS.Business.DataObjects.tblCustomers" UpdateMethod="Update"></asp:ObjectDataSource>
</asp:Content>
