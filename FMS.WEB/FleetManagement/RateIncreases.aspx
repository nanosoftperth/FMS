<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RateIncreases.aspx.vb" Inherits="FMS.WEB.RateIncreases" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
    <script src="../Content/javascript/jquery-1.10.2.min.js"></script>
    <style>
        .container {
            width: 600px;
        }
        .dxeMemoEditAreaSys{
            border-width:1px !Important;
        }
    </style>
    <script type="text/javascript">
        function ViewCuaRatesClick(ID) {
            ShowCuaRatesWindow();
        }
        function ShowCuaRatesWindow() {
            viewCuaRates.Show();
        }
        function ViewCuaProcessRatesClick(ID) {
            ShowCuaProcessRatesWindow();
        }
        function ShowCuaProcessRatesWindow() {
            viewProcessCuaRates.Show();
        }
        function ViewUpdateRatesClick(ID) {
            ShowUpdateRatesWindow();
        }
        function ShowUpdateRatesWindow() {
            viewUpdateRates.Show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="RateIncreasesGridView" KeyFieldName="RateIncreaseID" DataSourceID="odsRateIncreases" 
            Theme="SoftOrange" runat="server" AutoGenerateColumns="False"
            OnRowInserting="RateIncreasesGridView_RowInserting"
            OnRowUpdating="RateIncreasesGridView_RowUpdating">
            <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="true"></Settings>
            <Templates>
                <TitlePanel>Rate Increases</TitlePanel>
            </Templates>
            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
            <Settings ShowPreview="true" />
            <SettingsPager PageSize="10" />
            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
            <SettingsPopup>
                <EditForm Modal="true"
                    VerticalAlign="WindowCenter"
                    HorizontalAlign="WindowCenter" Width="300px" />
            </SettingsPopup>
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="RateIncreaseID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RateIncreaseDescription" VisibleIndex="3"></dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn FieldName="AnnualIncreaseApplies" VisibleIndex="3"></dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn FieldName="AlreadyDoneThisYear" VisibleIndex="3"></dx:GridViewDataCheckColumn>
            </Columns>
            <Templates>
                <EditForm>
                    <div class="container">
                        <div class="row">
                            <div class="col-md-1">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Rate Increase Description:" Width="150px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxTextBox ID="txtRateIncreaseDescription" ClientInstanceName="txtRateIncreaseDescription" runat="server" Width="350px" Text='<%# Eval("RateIncreaseDescription") %>'></dx:ASPxTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2"> 
                                <dx:ASPxLabel ID="lblInactiveCustomer" runat="server" Text="Annual Increase Applies:" Width="150px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxCheckBox ID="chkAnnualIncreaseApplies" runat="server" Checked='<%# Eval("AnnualIncreaseApplies")%>'></dx:ASPxCheckBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Already done this year:" Width="150px"></dx:ASPxLabel>
                            </div>
                            <div class="col-md-3">
                                <dx:ASPxCheckBox ID="chkAlreadyDoneThisYear" runat="server" Checked='<%# Eval("AlreadyDoneThisYear")%>'></dx:ASPxCheckBox>
                            </div>
                        </div>
                        <div class="row">
                            
                        </div>
                    </div>
                    <div style="text-align: right; padding: 2px; padding-top:60px">
                        <dx:ASPxButton ID="aspxButton1" runat="server" AutoPostBack="false" Text="CUA Rate Increases">
                            <ClientSideEvents Click="function(s,e) {
                                            ViewCuaRatesClick('xxx');
                                        }" />
                                </dx:ASPxButton>
                        <dx:ASPxButton ID="aspxButton" runat="server" AutoPostBack="false" Text="Update Rates">
                            <ClientSideEvents Click="function(s,e) {
                                            ViewUpdateRatesClick('xxx');
                                        }" />
                        </dx:ASPxButton>
                        &nbsp;&nbsp;&nbsp;
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
        <dx:ASPxPopupControl ID="viewUpdateRates" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="viewUpdateRates" 
            HeaderText="Process Rate Increase" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="500" >        
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxPanel ID="Panel2" runat="server" DefaultButton="btOK">
                        <PanelCollection>
                            <dx:PanelContent runat="server">   
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Percentage&nbsp;Increase:" Width="150px"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-md-3">
                                            <dx:ASPxTextBox ID="txtRateIncreaseDescription" ClientInstanceName="txtRateIncreaseDescription" runat="server" Width="300px" Text='<%# Eval("RateIncreaseDescription") %>'></dx:ASPxTextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Report&nbsp;Only&nbsp;-&nbsp;No&nbsp;rate&nbsp;increase:" Width="150px"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-md-3">
                                            <dx:ASPxCheckBox ID="chkReportOnlyNoRateIncrease" runat="server"></dx:ASPxCheckBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Effective&nbsp;date:" Width="150px"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-md-3">
                                            <dx:ASPxDateEdit ID="dtEffectiveDate" ClientInstanceName="dtEffectiveDate" runat="server">
                                                </dx:ASPxDateEdit>
                                        </div>
                                    </div>
                                    <div class="row"></div>
                                    <div class="row"></div>
                                    <div class="row"></div> 
                                    <div class="row"></div>
                                    <div class="row">
                                        <div class="col-md-12" style="text-align:center">
                                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" ForeColor="red" Font-Bold="true" Font-Size="X-Large" Text="This process is irreversible" Width="350px"></dx:ASPxLabel>
                                        </div>
                                    </div>
                                </div>
                                <div style="text-align: right; padding: 2px; padding-top:60px">
                                    <dx:ASPxButton ID="aspxButton1" Width="80px" runat="server" AutoPostBack="false" Text="Process"><%--<ClientSideEvents Click="function(s,e) {
                                            ViewSitesClick('xxx');
                                        }" />--%>
                                    </dx:ASPxButton>
                                    <dx:ASPxButton ID="aspxButton"  Width="80px" runat="server" AutoPostBack="false" Text="Exit">                                        
                                        <ClientSideEvents Click="function(s, e) { viewUpdateRates.Hide(); }" />
                                    </dx:ASPxButton>
                                </div>
                                    
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="viewCuaRates" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="viewCuaRates" 
            HeaderText="Concept Foot Pedal Unit Monthly Service" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="300" >        
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                        <PanelCollection>
                            <dx:PanelContent runat="server">   
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <dx:ASPxGridView ID="ServicesGridView" KeyFieldName="ServicesID" ClientInstanceName="ServicesGridView" DataSourceID="odsServicesView" runat="server" 
                                                AutoGenerateColumns="False" Theme="SoftOrange" Width="500px" >
                                                <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="true"></Settings>
                                                <SettingsDetail ShowDetailRow="true" />
                                                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                <Templates>
                                                    <TitlePanel>Services</TitlePanel>
                                                    <DetailRow>
                                                        <dx:ASPxGridView ID="RunDocGridView" runat="server" ClientInstanceName="RunDocGridView" DataSourceID="odsServicesView" KeyFieldName="DocumentID" Width="550px" Theme="SoftOrange" 
                                                            AutoGenerateColumns="False" EditFormLayoutProperties-SettingsItems-VerticalAlign="Top" >
                                                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn FieldName="ServicesID" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Sid" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="CostOfService" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="ServiceDescription" VisibleIndex="4" Width="50%"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="ServiceCode" VisibleIndex="5" Visible="true"></dx:GridViewDataTextColumn>
                                                            </Columns>
                                                            <Settings ShowPreview="true" />
                                                            <SettingsPager PageSize="10" />
                                                        </dx:ASPxGridView>
                                                    </DetailRow>
                                                </Templates>
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ServicesID" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Sid" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="CostOfService" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ServiceDescription" VisibleIndex="4" Width="50%"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ServiceCode" VisibleIndex="5" Visible="true"></dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Settings ShowPreview="true" />
                                                <SettingsPager PageSize="10" />
                                            </dx:ASPxGridView>
                                        </div>
                                    </div>
                                    <div class="row"></div>
                                    <div class="row"></div> 
                                    <div class="row"></div>
                                </div>
                                <div style="text-align: right; padding: 2px; padding-top:60px">
                                    <dx:ASPxButton ID="aspxButton2" Width="80px" runat="server" AutoPostBack="false" Text="Process Rate Increases"><ClientSideEvents Click="function(s,e) {
                                            ViewCuaProcessRatesClick('xxx');
                                        }" />
                                    </dx:ASPxButton>
                                    <dx:ASPxButton ID="aspxButton3"  Width="80px" runat="server" AutoPostBack="false" Text="Exit">                                        
                                        <ClientSideEvents Click="function(s, e) { viewCuaRates.Hide(); }" />
                                    </dx:ASPxButton>
                                </div>
                                <asp:ObjectDataSource ID="odsServicesView" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblServices" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblServices" UpdateMethod="Update"></asp:ObjectDataSource>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="viewProcessCuaRates" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="viewProcessCuaRates" 
            HeaderText="CUA Process Rate Increase" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="500" >        
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxPanel ID="ASPxPanel2" runat="server" DefaultButton="btOK">
                        <PanelCollection>
                            <dx:PanelContent runat="server">   
                                <div class="container">
                                    <div class="row"></div>
                                    <div class="row"></div>
                                    <div class="row"></div> 
                                    <div class="row"></div>
                                    <div class="row">
                                        <div class="col-md-12" style="text-align:center">
                                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" ForeColor="red" Font-Bold="true" Font-Size="X-Large" Text="This process is irreversible" Width="350px"></dx:ASPxLabel>
                                        </div>
                                    </div>
                                </div>
                                <div style="text-align: right; padding: 2px; padding-top:60px">
                                    <dx:ASPxButton ID="aspxButton4" Width="80px" runat="server" AutoPostBack="false" Text="Process"><%--<ClientSideEvents Click="function(s,e) {
                                            ViewUpdateRatesClick('xxx');
                                        }" />--%>
                                    </dx:ASPxButton>
                                    <dx:ASPxButton ID="aspxButton5"  Width="80px" runat="server" AutoPostBack="false" Text="Exit">                                        
                                        <ClientSideEvents Click="function(s, e) { viewProcessCuaRates.Hide(); }" />
                                    </dx:ASPxButton>
                                </div>
                                    
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ContentStyle>
                <Paddings PaddingBottom="5px" />
            </ContentStyle>
        </dx:ASPxPopupControl>        
        <asp:ObjectDataSource ID="odsRateIncreases" runat="server" SelectMethod="GetAllByApplicationID" TypeName="FMS.Business.DataObjects.tblRateIncreaseReference" DataObjectTypeName="FMS.Business.DataObjects.tblRateIncreaseReference" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update">
            <SelectParameters>
                <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appID"></asp:SessionParameter>
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
