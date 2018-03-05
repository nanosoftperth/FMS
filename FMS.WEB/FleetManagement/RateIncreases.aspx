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
        function ViewCuaRatesClick() {
            ShowCuaRatesWindow();
        }
        function ShowCuaRatesWindow() {
            viewCuaRates.Show();
        }
        function ViewCuaProcessRatesClick(ID) {
            cuaTxtServiceCode.SetText(ID);
            cuaTxtService.SetText(hdnServiceDescription.GetText());
            ShowCuaProcessRatesWindow();
        }
        function ShowCuaProcessRatesWindow() {
            viewProcessCuaRates.Show();
        }
        function ViewUpdateRatesClick() {
            ShowUpdateRatesWindow();
        }
        function ShowUpdateRatesWindow() {
            viewUpdateRates.Show();
        }
        function ProcessScheduleOfRatesClick(obj) {
            alert(obj);
        }
        function ProcessRatesClick(obj) {
            if (cuaDtEffectiveDate.GetText() == '') {
                document.getElementById("pMessage").innerHTML = "Please enter an effective date";
                ShowMyAlertWindow();
            } else {
                document.getElementById("pMessageWithYesNoButton").innerHTML = "You are about to process CUA rate increases <br> For "
                    + cuaTxtService.GetText() +
                    " <br> ARE YOU SURE YOU WANT TO PROCEED?";
                ShowMyAlertWindowWithYesNoButton();
            }
        }
        function ShowMyAlertWindowWithYesNoButton() {
            myAlertWithYesNoButton.Show();
        }
        function ShowMyAlertWindow() {
            myAlert.Show();
        }
        function btnOK_Click() {
            alert('ok');
        }
        function CancelledCuaProcess() {
            myAlertWithYesNoButton.Hide();
            document.getElementById("pMessage").innerHTML = "Process cancelled as requested - Thank You";
            ShowMyAlertWindow();
        }
        function ProceedCuaProcess() {
            ProcessCuaRateIncrease();
        }
        function ProcessUpdateRatesClick() {
            if (txtPercentageIncrease.GetText() == '') {
                document.getElementById("pMessage").innerHTML = "Please enter a percentage increase.";
                ShowMyAlertWindow();
            } else {
                LoadingPanel.SetText("");
                LoadingPanel.Show();
                ProcessUpdateRates(txtPercentageIncrease.GetText(), ChkReportOnlyNoRateIncrease.GetChecked());
            }
        }
        function ProcessUpdateRates(percentageIncrease, reportOnly) {
            $.ajax({
                type: "POST",
                url: 'RateIncreases.aspx/ProcessRateIncrease',
                dataType: "json",
                data: JSON.stringify({ 'percentageIncrease': percentageIncrease,'reportOnly':reportOnly }),
                contentType: "application/json",
                crossDomain: true,
                success: function (data) {
                    document.getElementById("pMessage").innerHTML = "Processing of rate increases complete - Thank you and have a nice day";
                    myAlertWithYesNoButton.Hide();
                    ShowMyAlertWindow();
                    LoadingPanel.Hide();
                }
            });
        }
        function ProcessCuaRateIncrease() {
            var param = {
                ReportOnly: cuaChkReportOnlyNoRateIncrease.GetChecked(),
                Sid: cuaTxtServiceCode.GetValue(),
                IndustryGroupCode: cuaTxtIndustryGroupCode.GetValue(),
                EffectiveDate: cuaDtEffectiveDate.GetValue()
            }
            $.ajax({
                type: "POST",
                url: 'RateIncreases.aspx/ProcessCuaRateIncrease',
                dataType: "json",
                data: JSON.stringify({'objCuaRateIncrease':param}),
                contentType: "application/json",
                crossDomain: true,
                success: function (data) {
                    document.getElementById("pMessage").innerHTML = "Processing of rate increases complete - Thank you and have a nice day";
                    myAlertWithYesNoButton.Hide();
                    ShowMyAlertWindow();
                }
            });
        }

        //Cesar: Use for Delete Dialog Box
        var visibleIndex;
        function OnCustomButtonClick(s, e) {
            visibleIndex = e.visibleIndex;
            popupDelete.SetHeaderText("Delete Item");
            popupDelete.Show();
        }
        function OnClickYes(s, e) {
            cltRateIncreasesGridView.DeleteRow(visibleIndex);
            popupDelete.Hide();
        }
        function OnClickNo(s, e) {
            popupDelete.Hide();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        
        <dx:ASPxGridView ID="RateIncreasesGridView" KeyFieldName="RateIncreaseID" DataSourceID="odsRateIncreases" 
            Theme="SoftOrange" runat="server" AutoGenerateColumns="False" ClientInstanceName="cltRateIncreasesGridView"
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
            <ClientSideEvents CustomButtonClick="OnCustomButtonClick" />
            <Columns>
                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="deleteButton" Text="Delete" />
                    </CustomButtons>
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
                                            ViewCuaRatesClick();
                                        }" />
                                </dx:ASPxButton>
                        <dx:ASPxButton ID="aspxButton" runat="server" AutoPostBack="false" Text="Update Rates">
                            <ClientSideEvents Click="function(s,e) {
                                            ViewUpdateRatesClick();
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
                                            <dx:ASPxSpinEdit ID="txtPercentageIncrease" ClientInstanceName="txtPercentageIncrease" runat="server" Width="100px" Text='<%# Eval("RateIncreaseDescription") %>'></dx:ASPxSpinEdit>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Report&nbsp;Only&nbsp;-&nbsp;No&nbsp;rate&nbsp;increase:" Width="150px"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-md-3">
                                            <dx:ASPxCheckBox ID="chkReportOnlyNoRateIncrease" ClientInstanceName="ChkReportOnlyNoRateIncrease" runat="server" Checked="true"></dx:ASPxCheckBox>
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
                                    <dx:ASPxButton ID="aspxButton1" Width="80px" runat="server" AutoPostBack="false" Text="Process"><ClientSideEvents Click="function(s,e) {
                                            ProcessUpdateRatesClick();
                                        }" />
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
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" ClientInstanceName="viewCuaRates" 
            HeaderText="Concept Foot Pedal Unit Monthly Service" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="300" >        
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                        <PanelCollection>
                            <dx:PanelContent runat="server">   
                                <div class="container">
                                    <div class="row">
                                        <div style="align-items:center">
                                            <dx:ASPxGridView ID="ServicesGridView" KeyFieldName="ServicesID" ClientInstanceName="ServicesGridView" DataSourceID="odsServicesView" runat="server" 
                                                AutoGenerateColumns="False" Theme="SoftOrange" Width="620px" >
                                                <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="true"></Settings>
                                                <SettingsDetail ShowDetailRow="true" />
                                                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                <Templates>
                                                    <TitlePanel>Services</TitlePanel>
                                                    <DetailRow>
                                                        <div style="display:none">
                                                            <dx:ASPxTextBox ID="hdnServiceID" ClientInstanceName="hdnServiceID" runat="server" Text='<%# Eval("Sid")%>'></dx:ASPxTextBox>
                                                            <dx:ASPxTextBox ID="hdnServiceDescription" ClientInstanceName="hdnServiceDescription" runat="server" Text='<%# Eval("ServiceDescription")%>'></dx:ASPxTextBox>
                                                        </div>
                                                        <dx:ASPxGridView ID="ScheduleOfRatesGridView" runat="server" ClientInstanceName="ScheduleOfRatesGridView" DataSourceID="odsCUAScheduleOfRates" KeyFieldName="RatesAutoId" Width="550px" Theme="SoftOrange" 
                                                            AutoGenerateColumns="False" EditFormLayoutProperties-SettingsItems-VerticalAlign="Top" OnBeforePerformDataSelect="ScheduleOfRatesGridView_BeforePerformDataSelect">
                                                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn FieldName="RatesAutoId" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Service" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataSpinEditColumn FieldName="FromUnits" VisibleIndex="2" Visible="true"></dx:GridViewDataSpinEditColumn>
                                                                <dx:GridViewDataSpinEditColumn FieldName="ToUnits" VisibleIndex="3" Visible="true"></dx:GridViewDataSpinEditColumn>
                                                                <dx:GridViewDataSpinEditColumn FieldName="UnitPrice" VisibleIndex="4" Width="50%"></dx:GridViewDataSpinEditColumn>
                                                            </Columns>
                                                            <Settings ShowPreview="true" />
                                                            <SettingsPager PageSize="5" />
                                                            <SettingsEditing Mode="PopupEditForm"/>
                                                            <SettingsPopup>
                                                                <EditForm  Modal="true" 
                                                                    VerticalAlign="WindowCenter" 
                                                                    HorizontalAlign="WindowCenter" Height="100px"/>                
                                                            </SettingsPopup>
                                                        </dx:ASPxGridView>
                                                        <dx:ASPxButton ID="aspxButton4" Width="80px" runat="server" BackColor="Orange" AutoPostBack="false" Text="Process Rate Increases"><ClientSideEvents Click="function(s,e) {
                                                                ViewCuaProcessRatesClick(hdnServiceID.GetText());
                                                            }" />
                                                        </dx:ASPxButton>
                                                         <%--ProcessScheduleOfRatesClick(hdnServiceID.GetText());--%>
                                                    </DetailRow>
                                                </Templates>
                                                <Columns>
                                                    <%--<dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>--%>
                                                    <dx:GridViewDataTextColumn FieldName="ServicesID" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Sid" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="CostOfService" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ServiceDescription" VisibleIndex="4" Width="50%"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ServiceCode" VisibleIndex="5" Visible="true"></dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Settings ShowPreview="true" />
                                                <SettingsPager PageSize="5" />
                                            </dx:ASPxGridView>
                                        </div>
                                    </div>
                                </div>
                                <asp:ObjectDataSource ID="odsServicesView" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblServices" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblServices" UpdateMethod="Update"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsCUAScheduleOfRates" runat="server" SelectMethod="GetCUAScheduleOfRatesByService" TypeName="FMS.Business.DataObjects.tblCUAScheduleOfRates" DataObjectTypeName="FMS.Business.DataObjects.tblCUAScheduleOfRates" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update">
                                    <SelectParameters>
                                        <asp:SessionParameter SessionField="ServiceID" Name="service" Type="Int32"></asp:SessionParameter>
                                    </SelectParameters>
                                </asp:ObjectDataSource>
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
                                    <div class="row">
                                        <div class="col-md-4">
                                            <dx:ASPxLabel ID="ASPxLabel7" Font-Bold="true" Font-Size="Larger" runat="server" Text="Industry&nbsp;Group:" Width="150px"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-md-3">
                                            <dx:ASPxTextBox ID="cuaTxtIndustryGroup" Font-Bold="true" Font-Size="Larger" ClientInstanceName="cuaTxtIndustryGroup" Text="CUA" runat="server" Width="300px" ReadOnly="true"></dx:ASPxTextBox>&nbsp;
                                            <dx:ASPxTextBox ID="cuaTxtIndustryGroupCode" Font-Bold="true" Font-Size="Larger" ClientInstanceName="cuaTxtIndustryGroupCode" Text="29" runat="server" Width="50px" ReadOnly="true"></dx:ASPxTextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <dx:ASPxLabel ID="ASPxLabel8" Font-Bold="true" Font-Size="Larger" runat="server" Text="Service:" Width="150px"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-md-3">
                                            <dx:ASPxTextBox ID="cuaTxtService" Font-Bold="true" Font-Size="Larger" ClientInstanceName="cuaTxtService" runat="server" Width="300px" ReadOnly="true"></dx:ASPxTextBox>&nbsp;
                                            <dx:ASPxTextBox ID="cuaTxtServiceCode" Font-Bold="true" Font-Size="Larger" ClientInstanceName="cuaTxtServiceCode" runat="server" Width="50px" ReadOnly="true"></dx:ASPxTextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Report&nbsp;Only&nbsp;-&nbsp;No&nbsp;rate&nbsp;increase:" Width="150px"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-md-3">
                                            <dx:ASPxCheckBox ID="cuaChkReportOnlyNoRateIncrease" ClientInstanceName="cuaChkReportOnlyNoRateIncrease" runat="server"></dx:ASPxCheckBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Effective&nbsp;date:" Width="150px"></dx:ASPxLabel>
                                        </div>
                                        <div class="col-md-3">
                                            <dx:ASPxDateEdit ID="cuaDtEffectiveDate" ClientInstanceName="cuaDtEffectiveDate" runat="server">
                                                </dx:ASPxDateEdit>
                                        </div>
                                    </div>
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
                                    <dx:ASPxButton ID="aspxButton4" Width="80px" runat="server" AutoPostBack="false" Text="Process">
                                        <ClientSideEvents Click="function(s,e) {
                                            ProcessRatesClick('xxx');
                                        }" />
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
        <dx:ASPxPopupControl ID="myAlert" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="myAlert"
                HeaderText="Information" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="370px" >        
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                            <PanelCollection>
                                <dx:PanelContent runat="server">             
                                    <p style="font-size:larger; font-weight:bold"  id="pMessage"></p>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>  
                        <table style="border: none; width:100%">
                            <tr style="text-align:right">
                                <td>
                                    <dx:ASPxButton ID="ASPxButton7" runat="server" AutoPostBack="False" ClientInstanceName="btnOk"
                                        Text="Ok" Width="80px">
                                        <ClientSideEvents Click="function(s, e) { myAlert.Hide(); }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="myAlertWithYesNoButton" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="myAlertWithYesNoButton"
                HeaderText="Information" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="370px" >        
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btOK">
                            <PanelCollection>
                                <dx:PanelContent runat="server">             
                                    <p style="font-size:larger; font-weight:bold"  id="pMessageWithYesNoButton"></p>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>         
                        <table style="border: none; width:100%">
                            <tr style="text-align:right">
                                <td>
                                    <dx:ASPxButton ID="btnCuaYes" ClientInstanceName="btnCuaYes"  runat="server" AutoPostBack="False" Text="Yes" Width="80px">
                                        <ClientSideEvents Click="function(s,e) { ProceedCuaProcess(); }"/>
                                    </dx:ASPxButton>
                                    <dx:ASPxButton ID="btnCuaNo" runat="server" AutoPostBack="False" ClientInstanceName="btnCuaNo"
                                        Text="Cancel" Width="80px">
                                        <ClientSideEvents Click="function(s, e) { CancelledCuaProcess(); }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="DeleteDialog" runat="server" Text="Are you sure you want to delete this?" 
            ClientInstanceName="popupDelete" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <dx:ASPxButton ID="yesButton" runat="server" Text="Yes" AutoPostBack="false">
                        <ClientSideEvents Click="OnClickYes" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="noButton" runat="server" Text="No" AutoPostBack="false">
                        <ClientSideEvents Click="OnClickNo" />
                    </dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <asp:ObjectDataSource ID="odsRateIncreases" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRateIncreaseReference" DataObjectTypeName="FMS.Business.DataObjects.tblRateIncreaseReference" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update">
        </asp:ObjectDataSource>
        <div>
            <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" 
                Modal="True">
                <Image URL="../Content/Images/Gear Set.gif"/>
            </dx:ASPxLoadingPanel>
        </div>
    </form>
</body>
</html>
