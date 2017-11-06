<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerDetailsMain.aspx.vb" Inherits="FMS.WEB.CustomerDetailsMain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


<link href="../../Content/grid/bootstrap.css" rel="stylesheet" />
<link href="../../Content/grid/grid.css" rel="stylesheet" />
<script src="../../Content/javascript/jquery-1.10.2.min.js"></script>
<script src="../../Content/javascript/jquery-1.10.2.min.js"></script>
<style>
    .container {
        width: 700px;
    }
</style>
<style>
    .dxeMemoEditAreaSys{
        border-width:1px !Important;
    }
</style>
<script type="text/javascript">
    function ViewSitesClick(custID) {
        hdnCID.SetText(custID);
        SiteGridView.Refresh();
        ShowLoginWindow();
    }

    function ShowLoginWindow() {
        //viewPopup.ShowAtPos(100, 10);
        viewPopup.Show();
    }

    function CalculateCommencementDate(comm) {
        if (comm != "") {
            var date1 = new Date(comm);
            var dateNow = new Date();
            var dateNowToDays = new Date(dateNow.toLocaleDateString()) 
            var diff = new Date(dateNowToDays - date1);
            var diffByDays = diff / 1000 / 60 / 60 / 24;
            var commencement = Math.round(diffByDays) / 365.25;
            if (commencement.toFixed(2) != -0.00) {
                txtYears.SetText(commencement.toFixed(2));
            } else {
                txtYears.SetText('');
            }
        }
    }

    function getDataFromServer(_cid) {
        $.ajax({
            type: "POST",
            url: 'CustomerDetailsMain.aspx/UpdateValue',
            dataType: "json",
            data: JSON.stringify({ Cid: _cid }),
            contentType: "application/json",
            crossDomain: true,
            success: function (data) {
                if (data.d != null) {
                    txtPerAnnumValue.SetText(data.d.TotalAmount);
                } else {
                    txtPerAnnumValue.SetText('0');
                }
            }
        });
    }

</script>
       
</head>
<body>
        <form id="form1" runat="server">
    <dx:ASPxGridView ID="CustomersGridView" runat="server" DataSourceID="odsCustomer" AutoGenerateColumns="False" 
            KeyFieldName="Cid" Width="550px"  Theme="SoftOrange" OnRowUpdating="CustomersGridView_RowUpdating" OnRowInserting="CustomersGridView_RowInserting">
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
            <SettingsEditing Mode="PopupEditForm"/>
            <SettingsPopup>
                <EditForm  Modal="true" 
                    VerticalAlign="WindowCenter" 
                    HorizontalAlign="WindowCenter" width="200px"/>                
            </SettingsPopup>
            <Templates>
                <EditForm>
                    <div class="container">
                        <div style="display:none">
                            <dx:ASPxTextBox  id="txtCustomerID" ClientInstanceName="custID" runat="server" Text='<%# Eval("CustomerID") %>'></dx:ASPxTextBox>
                        </div>
                        <div class="row">
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblCustomerName" runat="server" Text="Customer&nbsp;Name:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-4">
                                <dx:ASPxTextBox ID="txtCustomerName" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("CustomerName") %>'></dx:ASPxTextBox>
                            </div>
                            <div class="col-md-1">
                                <dx:ASPxButton ID="btnViewSites" ClientInstanceName="btnViewSites" AutoPostBack="false" runat="server" Text="View Sites">
                                    <ClientSideEvents Click="function(s,e) {
                                         ViewSitesClick(txtViewID.GetText());
                                        }" />
                                </dx:ASPxButton>
                            </div>
                            <div class="col-md-1">
                                <dx:ASPxTextBox ID="txtViewID" ClientInstanceName="txtViewID" runat="server" Width="50px" Text='<%# Eval("CID") %>'></dx:ASPxTextBox>
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
                                <dx:ASPxTextBox ID="txtAddressLine2" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("AddressLine2") %>'></dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="row ">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblSuburb" runat="server" Text="Suburb:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-6">
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
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblCustCommencementDate" runat="server" Text="Customer&nbsp;Commencement Date:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-5">
                                <div class="container">
                                    <div class="row row-md-margin-top">
                                        <dx:ASPxDateEdit ID="dtCustCommencementDate" ClientInstanceName="dtCustCommencementDate" runat="server" Date='<%# Eval("CustomerCommencementDate") %>'>
                                            <ClientSideEvents LostFocus="function(s,e){
                                                    CalculateCommencementDate(dtCustCommencementDate.GetText());
                                                }" />
                                        </dx:ASPxDateEdit>&nbsp;
                                        <dx:ASPxLabel ID="lblYears" runat="server" Text="Years:"></dx:ASPxLabel>&nbsp;
                                        <dx:ASPxTextBox ID="txtYears" ClientInstanceName="txtYears" runat="server" Width="50px" MaxLength="22" Text=""></dx:ASPxTextBox>&nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblCustomerRating" runat="server" Text="Customer&nbsp;Rating:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxComboBox ID="cbCustomerRating" DataSourceID="odsCustomerRating" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("CustomerRating") - 1 %>' TextField="CustomerRating" ValueField="Rid"></dx:ASPxComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblZone" runat="server" Text="Zone:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxComboBox ID="cbZone" DataSourceID="odsZones" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("ZoneSortOrder") - 1%>'  TextField="AreaDescription" ValueField="Aid"></dx:ASPxComboBox>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxLabel ID="lblPerAnnumValue" runat="server" Text="Per Annum Value:" ForeColor="Blue" Font-Bold="true" Width="300px"></dx:ASPxLabel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblCustomerContactName" runat="server" Text="Customer&nbsp;Contact&nbsp;Name:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtCustomerContactName" runat="server" Width="170px" MaxLength="50" Text='<%# Eval("CustomerContactName") %>'></dx:ASPxTextBox>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtPerAnnumValue" ClientInstanceName="txtPerAnnumValue" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("CustomerValue") %>'></dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblCustomerPhone" runat="server" Text="Customer&nbsp;Phone:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtCustomerPhone" runat="server" Width="170px" MaxLength="50" Text='<%# Eval("CustomerPhone")%>'></dx:ASPxTextBox>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxButton ID="btnUpdateValue" runat="server" AutoPostBack="false" Text="Update Value">
                                    <ClientSideEvents Click="function(s,e){
                                            getDataFromServer(txtViewID.GetText());
                                        }" />
                                </dx:ASPxButton>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblCustomerMobile" runat="server" Text="Customer&nbsp;Mobile:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtCustomerMobile" runat="server" Width="170px" MaxLength="50" Text='<%# Eval("CustomerMobile")%>'></dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblCustomerFax" runat="server" Text="Customer&nbsp;Fax:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtCustomerFax" runat="server" Width="170px" MaxLength="50" Text='<%# Eval("CustomerFax")%>'></dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblCustomerComments" runat="server" Text="Customer&nbsp;Comments:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxMemo ID="txtCustomerComments" runat="server" Height="100px" Width="270px" class="dxeMemoEditAreaSys"  Text='<%# Eval("CustomerComments") %>'></dx:ASPxMemo>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblCustomerAgentName" runat="server" Text="Customer&nbsp;Agent&nbsp;Name:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxComboBox ID="cbCustomerAgentName" DataSourceID="odsCustomerAgents" runat="server" Width="260px" Height="20px" SelectedIndex='<%# Eval("AgentSortOrder") - 1%>' TextField="CustomerAgentName" ValueField="Aid"></dx:ASPxComboBox>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-1 col-md-1_5">
                            </div>
                             <div class="col-md-1 col-md-1_5">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblMYOBCustomerNumber" runat="server" Text="MYOB&nbsp;Customer&nbsp;Number:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtMYOBCustomerNumber" runat="server" Width="60px" MaxLength="50" Text='<%# Eval("MYOBCustomerNumber")%>'></dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblRateIncrease" runat="server" Text="Rate&nbsp;Increase:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxComboBox ID="cbRateIncrease" DataSourceID="odsRateIncreaseReference"  runat="server" Width="100px" Height="20px" SelectedIndex='<%# Eval("RateIncreaseSortOrder") - 1%>'  TextField="RateIncreaseDescription" ValueField="Aid"></dx:ASPxComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblInactiveCustomer" runat="server" Text="Inactive&nbsp;Customer:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxCheckBox ID="chkInActiveCustomer" runat="server" Checked='<%# Eval("InactiveCustomer")%>'></dx:ASPxCheckBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="lblExcludeFuelLevy" runat="server" Text="Exclude&nbsp;Fuel&nbsp;Levy:" Width="100px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxCheckBox ID="chkExcludeFuelLevy" runat="server" Checked='<%# Eval("chkCustomerExcludeFuelLevy")%>'></dx:ASPxCheckBox>
                            </div>
                        </div>
                    </div>
                    <div style="text-align: right; padding: 2px; padding-top:60px">
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
        HeaderText="Site List" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="100%" >        
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="Panel2" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">             
                            <dx:ASPxGridView ID="SiteGridView"  ClientInstanceName="SiteGridView" DataSourceID="odsSitesView" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="SiteID" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Cid" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="SiteName" VisibleIndex="2" Width="50%"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Customer" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="AddressLine1" VisibleIndex="4"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Suburb" VisibleIndex="8"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="State" VisibleIndex="9"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="PostCode" VisibleIndex="10"></dx:GridViewDataTextColumn>
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
    <div style="display:none">
        <dx:ASPxTextBox ID="hdnCID" ClientInstanceName="hdnCID" runat="server" Text="0"></dx:ASPxTextBox>
    </div>
    <asp:ObjectDataSource ID="odsSitesView" runat="server" SelectMethod="GetAllByCustomer" TypeName="FMS.Business.DataObjects.tblSites">
        <SelectParameters>
            <asp:ControlParameter ControlID="hdnCID" PropertyName="Text" Name="cust" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>    
    <asp:ObjectDataSource ID="odsRateIncreaseReference" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRateIncreaseReference"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCustomerAgents" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblCustomerAgent"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsZones" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tbZone" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsStates" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblStates"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCustomerRating" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblCustomerRating"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCustomer" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblCustomers" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllWithZoneSortOrder" TypeName="FMS.Business.DataObjects.tblCustomers" UpdateMethod="Update"></asp:ObjectDataSource>
</form>
    
</body>
</html>
