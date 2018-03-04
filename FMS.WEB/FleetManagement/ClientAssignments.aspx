<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClientAssignments.aspx.vb" Inherits="FMS.WEB.ClientAssignments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
    <script src="../Content/javascript/jquery-1.10.2.min.js"></script>
    <style>
        .container {
            width: 910px;
        }

        .dxeMemoEditAreaSys {
            border-width: 1px !Important;
        }
    </style>
    <script>
        function OnFileUploadComplete(s, e) {
            if (e.callbackData !== "") {
                lblFileName.SetText(e.callbackData);
            }
        }

        function AdjustPopupHeight() {
            if ($('.dxgvPopupEditForm_SoftOrange').height() > 400) {
                var windowHeight = $(window).height() - $(".headerTop").height();
                var popupHeight = 480;
                var popupEdit = $('.dxgvPopupEditForm_SoftOrange');
                if (windowHeight < 500) {
                    var computed = 530 - windowHeight;
                    popupEdit.css({ "height": popupHeight - computed });
                } else {
                    popupEdit.css({ "height": popupHeight });
                }
            }
        }
        $(window).resize(function () {
            AdjustPopupHeight();
        })
        $(window).mousedown(function () {
            AdjustPopupHeight();
        })
        function ShowCustomerWindow() {
            viewPopup.SetHeaderText("Customer Details");
            viewPopup.Show();
        }
        function ExecuteLinkCustomer(cid) {
            var httpOrigin = window.location.origin;
            if (cbCustomer.GetValue() != null) {
                cid = cbCustomer.GetValue();
            }
            $("#ifrPopup").width(860);
            $("#ifrPopup").height(640);
            $("#ifrPopup").attr("src", httpOrigin + "/FleetManagement/CustomerDetailsMainPopup.aspx?cid=" + cid);
            ShowCustomerWindow();
        }
        function ShowIndustryGroupWindow() {
            viewPopup.SetHeaderText("Industry Group");
            viewPopup.Show();
        }
        function ExecuteLinkIndustryGroup(aid) {
            var httpOrigin = window.location.origin;
            if (cbIndustryGroup.GetValue() != null) {
                aid = cbIndustryGroup.GetValue();
            }
            $("#ifrPopup").width(460);
            $("#ifrPopup").height(450);
            $("#ifrPopup").attr("src", httpOrigin + "/FleetManagement/IndustryGroupPopup.aspx?aid=" + aid);
            ShowIndustryGroupWindow();
        }
        function ShowPreviousSupplierWindow() {
            viewPopup.SetHeaderText("Previous Supplier");
            viewPopup.Show();
        }
        function ExecuteLinkPreviousSupplier(cid) {
            var httpOrigin = window.location.origin;
            if (cbPreviousSupplier.GetValue() != null) {
                cid = cbPreviousSupplier.GetValue();
            }
            $("#ifrPopup").width(460);
            $("#ifrPopup").height(450);
            $("#ifrPopup").attr("src", httpOrigin + "/FleetManagement/PreviousSupplierPopup.aspx?cid=" + cid);
            ShowPreviousSupplierWindow();
        }
        function ShowCeaseReasonsWindow() {
            viewPopup.SetHeaderText("Contract Cease Reason");
            viewPopup.Show();
        }
        function ExecuteLinkCeaseReasons(aid) {
            var httpOrigin = window.location.origin;

            if (cbSiteCeaseReason.GetValue() != null) {
                aid = cbSiteCeaseReason.GetValue();
            }

            $("#ifrPopup").width(600);
            $("#ifrPopup").height(460);
            $("#ifrPopup").attr("src", httpOrigin + "/FleetManagement/ContractCeaseReasonsPopup.aspx?aid=" + aid);
            ShowCeaseReasonsWindow();
        }
        function ShowLostBusinessToWindow() {
            viewPopup.SetHeaderText("Contract Cease Reason");
            viewPopup.Show();
        }
        function ExecuteLinkLostBusinessTo(aid) {
            var httpOrigin = window.location.origin;
            if (cbLostBusinessTo.GetValue() != null) {
                aid = cbLostBusinessTo.GetValue();
            }
            $("#ifrPopup").width(600);
            $("#ifrPopup").height(460);
            $("#ifrPopup").attr("src", httpOrigin + "/FleetManagement/PreviousSupplierPopup.aspx?cid=" + aid);
            ShowLostBusinessToWindow();
        }
        function getSiteID(_cid) {
            hdnStoreCid.SetText(_cid);
        }
        function getSiteInvoicingDetails(_cid) {
            if (_cid != null || _cid != "") {
                $.ajax({
                    type: "POST",
                    url: 'SiteDetailsMain.aspx/GetSiteInvoicingDetails',
                    dataType: "json",
                    data: JSON.stringify({ Cid: _cid }),
                    contentType: "application/json",
                    crossDomain: true,
                    success: function (data) {
                        if (data.d != null) {
                            cbInvoiceMonth1.SetSelectedIndex(data.d.InvoiceMonth1 - 1);
                            cbInvoiceMonth2.SetSelectedIndex(data.d.InvoiceMonth2 - 1);
                            cbInvoiceMonth3.SetSelectedIndex(data.d.InvoiceMonth3 - 1);
                            cbInvoiceMonth4.SetSelectedIndex(data.d.InvoiceMonth4 - 1);
                            var dateInv = new Date(data.d.InvoiceCommencingString);
                            dtInvoiceCommencing.SetDate(dateInv);
                            cbInvoiceFrequency.SetSelectedIndex(data.d.InvoicingFrequencySortOrder - 1);
                            txtPostalAddressLine1.SetText(data.d.PostalAddressLine1);
                            txtPostalAddressLine2.SetText(data.d.PostalAddressLine2);
                            cbPostalState.SetSelectedIndex(data.d.PostalState - 1);
                            txtPostalPostCode.SetText(data.d.PostalPostCode);
                            txtPostalSuburb.SetText(data.d.PostalSuburb);
                        }
                    }
                });
            }
        }
        function ReCalculateSiteServices(siteID) {
            $.ajax({
                type: "POST",
                url: 'SiteDetailsMain.aspx/GetRecalculatedServices',
                dataType: "json",
                data: JSON.stringify({ siteId: siteID }),
                contentType: "application/json",
                crossDomain: true,
                success: function (data) {
                    if (data.d != null) {
                        txtTotalUnits.SetText(data.d.ServiceUnits);
                        txtTotalAmount.SetText(data.d.PerAnnumCharge);
                    }
                }
            });
        }
        function ViewSiteList(siteID) {
            alert("to follow...");
        }
        function SetServiceEnabledDisabled(e) {
            if (hdnSiteCid.GetText() != "") {
                SiteDetailsPageControl.tabs[3].SetEnabled(true);
            } else {
                SiteDetailsPageControl.tabs[3].SetEnabled(false);
            }
        }
        function SetServiceInitialize(e) {
            if (hdnSiteCid.GetText() != "") {
                SiteDetailsPageControl.tabs[3].SetEnabled(true);
            } else {
                SiteDetailsPageControl.tabs[3].SetEnabled(false);
            }
        }

        //Cesar: Added functions
        function CreateRun(id, visibleindex) {
            PageMethods.GetUnAssignedRuns(OnSuccessCreateRun);

            var reponse = OnSuccessCreateRun;

        }


        //Cesar: Use for Delete Dialog Box (SiteDetail)
        var visibleIndex;
        function OnCustomButtonClick(s, e, item) {
            visibleIndex = e.visibleIndex;

            if (item == 'RunTab') {
                popupDelete_Run.SetHeaderText("Delete Item");
                popupDelete_Run.Show();
            }
            if (item == 'RunDoc') {
                popupDelete_RunDoc.SetHeaderText("Delete Item");
                popupDelete_RunDoc.Show();
            }
            if (item == 'RunSite') {
                popupDelete_RunSite.SetHeaderText("Delete Item");
                popupDelete_RunSite.Show();
            }
            //if (item == 'Comments') {
            //    popupDelete_Comments.SetHeaderText("Delete Item");
            //    popupDelete_Comments.Show();
            //}
        }
        function OnClickYes(s, e, item) {
            if (item == 'RunTab') {
                clientRunGridView.DeleteRow(visibleIndex);
                popupDelete_Run.Hide();
            }
            if (item == 'RunDoc') {
                RunDocGridView.DeleteRow(visibleIndex);
                popupDelete_RunDoc.Hide();
            }
            if (item == 'RunSite') {
                cltRunSiteGridView.DeleteRow(visibleIndex);
                popupDelete_RunSite.Hide();
            }
            //if (item == 'Comments') {
            //    cltSiteCommentsGridView.DeleteRow(visibleIndex);
            //    popupDelete_Comments.Hide();
            //}
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxPageControl ID="FleetManagementPageControl" runat="server" ActiveTabIndex="3">
                <TabPages>
                    <dx:TabPage Name="Run" Text="Run">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <div>
                                                <dx:ASPxComboBox runat="server" ID="cbFields" ValueType="System.Int32" SelectedIndex="0" Theme="SoftOrange" Caption="Group by">
                                                    <Items>
                                                        <dx:ListEditItem Text="None" Value="0" />
                                                        <dx:ListEditItem Text="Run Number" Value="1" />
                                                        <dx:ListEditItem Text="Run Description" Value="2" />
                                                        <dx:ListEditItem Text="Monday Run" Value="3" />
                                                        <dx:ListEditItem Text="Tuesday Run" Value="4" />
                                                        <dx:ListEditItem Text="Wednesday Run" Value="5" />
                                                        <dx:ListEditItem Text="Thursday Run" Value="6" />
                                                        <dx:ListEditItem Text="Friday Run" Value="7" />
                                                        <dx:ListEditItem Text="Saturday Run" Value="8" />
                                                        <dx:ListEditItem Text="Sunday Run" Value="9" />
                                                        <dx:ListEditItem Text="Notes" Value="10" />
                                                    </Items>
                                                    <ClientSideEvents SelectedIndexChanged="function(s) { clientRunGridView.PerformCallback(s.GetValue()) }" />

<ClearButton Visibility="Auto"></ClearButton>
                                                </dx:ASPxComboBox>
                                            </div>
                                        </td>
                                        <td style="width: 20px">

                                        </td>
                                        <td>
                                            <div>
                                                <dx:ASPxButton runat="server" ID="btnCollapse" Text="Collapse All Rows" UseSubmitBehavior="false" Theme="SoftOrange" Width="100px"
                                                    AutoPostBack="false">
                                                    <ClientSideEvents Click="function() { clientRunGridView.CollapseAll() }" />
                                                </dx:ASPxButton>
                                            </div>
                                        </td>
                                        <td style="width: 5px">

                                        </td>
                                        <td>
                                            <div>
                                                <dx:ASPxButton runat="server" ID="btnExpand" Text="Expand All Rows" UseSubmitBehavior="false" Theme="SoftOrange" Width="100px"
                                                    AutoPostBack="false">
                                                    <ClientSideEvents Click="function() { clientRunGridView.ExpandAll() }" />
                                                </dx:ASPxButton>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <dx:ASPxGridView ID="RunGridView" runat="server" DataSourceID="odsTblRuns" KeyFieldName="Rid" Width="550px" Theme="SoftOrange" AutoGenerateColumns="False"
                                    OnRowValidating="RunGridView_RowValidating"
                                    SettingsDetail-AllowOnlyOneMasterRowExpanded="true"
                                    ClientInstanceName="clientRunGridView"
                                    OnCustomCallback="RunGridView_CustomCallback">
                                    <SettingsDetail ShowDetailRow="true" />
                                    <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                    <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="2" />
                                    <SettingsPopup>
                                        <EditForm Modal="true"
                                            VerticalAlign="WindowCenter"
                                            HorizontalAlign="WindowCenter" Width="700px" Height="300px" />
                                    </SettingsPopup>
                                    <ClientSideEvents CustomButtonClick="function(s, e)
                                        {
                                            OnCustomButtonClick(s, e, 'RunTab');
                                        }" />
                                    <Templates>
                                        <%--<EditForm>
                                            <dx:ASPxButton ID="btnUpdate" runat="server" Text="Updt" AutoPostBack="false">
                                                <ClientSideEvents Click="function(s, e) { clientRunGridView.UpdateEdit(); }" />
                                            </dx:ASPxButton>
                                        </EditForm>--%>
                                        <DetailRow>
                                            <dx:ASPxGridView ID="RunDocGridView" runat="server" ClientInstanceName="RunDocGridView" DataSourceID="odsRunMultiDocs" KeyFieldName="DocumentID" Width="550px" Theme="SoftOrange"
                                                AutoGenerateColumns="False" EditFormLayoutProperties-SettingsItems-VerticalAlign="Top" OnBeforePerformDataSelect="RunGridView_BeforePerformDataSelect">
                                                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                <SettingsPopup>
                                                    <EditForm Modal="true"
                                                        VerticalAlign="WindowCenter"
                                                        HorizontalAlign="WindowCenter" Width="400px" Height="400px" />
                                                </SettingsPopup>
                                                <ClientSideEvents CustomButtonClick="function(s, e)
                                                    {
                                                        OnCustomButtonClick(s, e, 'RunDoc');
                                                    }" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="true">
                                                        <CustomButtons>
                                                            <dx:GridViewCommandColumnCustomButton ID="btnDelete_RunDoc" Text="Delete" />
                                                        </CustomButtons>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn FieldName="DocumentID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Rid" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2" Visible="true"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataBinaryImageColumn FieldName="PhotoBinary" Caption="Image File" VisibleIndex="4">
                                                        <PropertiesBinaryImage ImageHeight="170px" ImageWidth="160px">
                                                            <EditingSettings Enabled="false" UploadSettings-UploadValidationSettings-MaxFileSize="4194304">
                                                            </EditingSettings>
                                                        </PropertiesBinaryImage>
                                                    </dx:GridViewDataBinaryImageColumn>
                                                    <dx:GridViewDataTextColumn FieldName="PhotoLocation" Caption="Url" UnboundType="Object" VisibleIndex="3">
                                                        <DataItemTemplate>
                                                            <dx:ASPxHyperLink ID="RunHyperLink" OnLoad="RunHyperLink_Load" runat="server" Target="_blank" Text="No data uploaded">
                                                            </dx:ASPxHyperLink>
                                                        </DataItemTemplate>
                                                        <EditItemTemplate>
                                                            <dx:ASPxLabel ID="lblAllowebMimeType" runat="server" Text="Allowed image types: jpeg, jpg, png, pdf" Font-Size="8pt" />
                                                            <br />
                                                            <dx:ASPxLabel ID="lblMaxFileSize" runat="server" Text="Maximum file size: 4Mb" Font-Size="8pt" />
                                                            <br />
                                                            <dx:ASPxUploadControl ID="RunFileUpload" ShowProgressPanel="true" UploadMode="Auto" AutoStartUpload="true" FileUploadMode="OnPageLoad"
                                                                OnFileUploadComplete="RunFileUpload_FileUploadComplete" runat="server">
                                                                <ValidationSettings MaxFileSize="4194304" MaxFileSizeErrorText="Size of the uploaded file exceeds maximum file size" AllowedFileExtensions=".jpg,.jpeg,.png,.pdf">
                                                                </ValidationSettings>
                                                                <ClientSideEvents FileUploadComplete="OnFileUploadComplete" />
                                                            </dx:ASPxUploadControl>
                                                            <br />
                                                            <dx:ASPxLabel ID="lblFileName" runat="server" ClientInstanceName="lblFileName" Font-Size="20pt" Width="50px" Style="width: 11em; word-wrap: break-word;" />
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Settings ShowPreview="true" />
                                                <SettingsPager PageSize="10" />
                                            </dx:ASPxGridView>
                                        </DetailRow>
                                    </Templates>
                                    <Columns>
                                        <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="true" >
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnDelete_Run" Text="Delete" />
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="RunID" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="1" PropertiesTextEdit-ClientInstanceName="RunNumber" Visible="false" PropertiesTextEdit-MaxLength="10">
<PropertiesTextEdit MaxLength="10" ClientInstanceName="RunNumber"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Rid" VisibleIndex="2" PropertiesTextEdit-ClientInstanceName="RunName" Visible="false" PropertiesTextEdit-MaxLength="10">
<PropertiesTextEdit MaxLength="10" ClientInstanceName="RunName"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="RunNUmber" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="RunDescription" VisibleIndex="4" SortOrder="Ascending"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn FieldName="RunDriver" Caption="Run Driver" VisibleIndex="2" SortIndex="0" SortOrder="Ascending" Visible="false">
                                            <PropertiesComboBox DataSourceID="odsDriverList" TextField="DriverName" ValueField="Did">
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataCheckColumn FieldName="MondayRun" VisibleIndex="6"></dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataCheckColumn FieldName="TuesdayRun" VisibleIndex="7"></dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataCheckColumn FieldName="WednesdayRun" VisibleIndex="8"></dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataCheckColumn FieldName="ThursdayRun" VisibleIndex="9"></dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataCheckColumn FieldName="FridayRun" VisibleIndex="10"></dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataCheckColumn FieldName="SaturdayRun" VisibleIndex="11"></dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataCheckColumn FieldName="SundayRun" VisibleIndex="12"></dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataCheckColumn FieldName="InactiveRun" VisibleIndex="13"></dx:GridViewDataCheckColumn>
                                        <dx:GridViewDataTextColumn FieldName="Notes" VisibleIndex="14"></dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowPreview="true" />
                                    <SettingsPager PageSize="10" />
                                    <Settings ShowGroupPanel="true" />
                                </dx:ASPxGridView>
                                <asp:ObjectDataSource ID="odsTblRuns" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRuns" DataObjectTypeName="FMS.Business.DataObjects.tblRuns" DeleteMethod="DeleteRunByRid" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsRun" runat="server" DataObjectTypeName="FMS.Business.DataObjects.FleetRun" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.FleetRun" UpdateMethod="Update"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsRunMultiDocs" runat="server" SelectMethod="GetAllByRunRID" TypeName="FMS.Business.DataObjects.FleetDocument" DataObjectTypeName="FMS.Business.DataObjects.FleetDocument" UpdateMethod="Update" DeleteMethod="Delete" InsertMethod="Create">
                                    <SelectParameters>
                                        <asp:SessionParameter SessionField="RunID" Name="RID" Type="Int32"></asp:SessionParameter>
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsRunsList" runat="server" SelectMethod="GetRunList" TypeName="FMS.Business.DataObjects.FleetRun"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsDriverList" runat="server" SelectMethod="GetAllPerApplication" TypeName="FMS.Business.DataObjects.tblDrivers"></asp:ObjectDataSource>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="Site" Text="Site">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <div style="display: none">
                                    <dx:ASPxTextBox ID="hdnStoreCid" ClientInstanceName="hdnStoreCid" AutoPostBack="true" runat="server" Text=""></dx:ASPxTextBox>
                                </div>
                                <dx:ASPxGridView ID="SiteDetailsGridView" runat="server" DataSourceID="odsSiteDetails" AutoGenerateColumns="False"
                                    KeyFieldName="Cid" Theme="SoftOrange" OnRowUpdating="SiteDetailsGridView_RowUpdating"
                                    OnRowInserting="SiteDetailsGridView_RowInserting" SettingsDetail-AllowOnlyOneMasterRowExpanded="true">
                                    <Settings ShowGroupPanel="True" ShowFilterRow="True" ShowTitlePanel="true"></Settings>
                                    <SettingsDetail ShowDetailRow="true" />
                                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                    <Templates>
                                        <TitlePanel>Sites</TitlePanel>
                                        <DetailRow>
                                            <dx:ASPxGridView ID="DocGridView" runat="server" ClientInstanceName="DocGridView" DataSourceID="odsMultiDocs" KeyFieldName="DocumentID" Width="550px" Theme="SoftOrange"
                                                AutoGenerateColumns="False" EditFormLayoutProperties-SettingsItems-VerticalAlign="Top" OnBeforePerformDataSelect="ClientGridView_BeforePerformDataSelect">
                                                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                <SettingsPopup>
                                                    <EditForm Modal="true"
                                                        VerticalAlign="WindowCenter"
                                                        HorizontalAlign="WindowCenter" Width="400px" Height="400px" />
                                                </SettingsPopup>
                                                <Columns>
                                                    <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn FieldName="DocumentID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Cid" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2" Visible="true"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataBinaryImageColumn FieldName="PhotoBinary">
                                                        <PropertiesBinaryImage ImageHeight="170px" ImageWidth="160px">
                                                            <EditingSettings Enabled="false" UploadSettings-UploadValidationSettings-MaxFileSize="4194304" />
                                                        </PropertiesBinaryImage>
                                                    </dx:GridViewDataBinaryImageColumn>
                                                    <dx:GridViewDataTextColumn FieldName="PhotoLocation" Caption="Url" UnboundType="Object" VisibleIndex="3">
                                                        <DataItemTemplate>
                                                            <dx:ASPxHyperLink ID="RunHyperLink" OnLoad="RunHyperLink_Load" runat="server" Target="_blank" Text="No data uploaded">
                                                            </dx:ASPxHyperLink>
                                                        </DataItemTemplate>
                                                        <EditItemTemplate>
                                                            <dx:ASPxLabel ID="lblAllowebMimeType" runat="server" Text="Allowed image types: jpeg, jpg, png, pdf" Font-Size="8pt" />
                                                            <br />
                                                            <dx:ASPxLabel ID="lblMaxFileSize" runat="server" Text="Maximum file size: 4Mb" Font-Size="8pt" />
                                                            <br />
                                                            <dx:ASPxUploadControl ID="RunFileUpload" ShowProgressPanel="true" UploadMode="Auto" AutoStartUpload="true" FileUploadMode="OnPageLoad"
                                                                OnFileUploadComplete="RunFileUpload_FileUploadComplete" runat="server">
                                                                <ValidationSettings MaxFileSize="4194304" MaxFileSizeErrorText="Size of the uploaded file exceeds maximum file size" AllowedFileExtensions=".jpg,.jpeg,.png,.pdf">
                                                                </ValidationSettings>
                                                                <ClientSideEvents FileUploadComplete="OnFileUploadComplete" />
                                                            </dx:ASPxUploadControl>
                                                            <br />
                                                            <dx:ASPxLabel ID="lblFileName" runat="server" ClientInstanceName="lblFileName" Font-Size="20pt" Width="50px" Style="width: 11em; word-wrap: break-word;" />
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Settings ShowPreview="true" />
                                                <SettingsPager PageSize="10" />
                                            </dx:ASPxGridView>
                                        </DetailRow>
<EditForm>
                                            <div class="container">
                                                <div style="display: none">
                                                    <dx:ASPxTextBox ID="txtSiteID" ClientInstanceName="siteID" runat="server" Text='<%# Eval("SiteID") %>'></dx:ASPxTextBox>
                                                    <dx:ASPxTextBox ID="hdnSiteCid" ClientInstanceName="hdnSiteCid" runat="server" Text='<%# Eval("CID") %>'></dx:ASPxTextBox>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1"><b>Customer&nbsp;Rating:</b></div>
                                                    <div style="width: 15px;"></div>
                                                    <dx:ASPxTextBox ID="txtCustomerRating" ClientInstanceName="txtCustomerRating" Width="30px" runat="server" Text='<%# Eval("CustomerRating") %>' ReadOnly="true"></dx:ASPxTextBox>
                                                    <dx:ASPxTextBox ID="txtCustomerRatingDesc" ClientInstanceName="txtCustomerRatingDesc" Width="100px" runat="server" Text='<%# Eval("CustomerRatingDesc") %>' ReadOnly="true"></dx:ASPxTextBox>
                                                    <div class="col-md-1" style="text-align: right"><b>Customer</b></div>
                                                    <dx:ASPxTextBox ID="txtCustomerName" ClientInstanceName="txtCustomerName" runat="server" Text='<%# Eval("CustomerName") %>' ReadOnly="true"></dx:ASPxTextBox>
                                                    <div style="width: 10px;"></div>
                                                    <b>Site</b>
                                                    <div style="width: 5px;"></div>
                                                    <dx:ASPxTextBox ID="txtSiteNameMain" ClientInstanceName="txtSiteNameMain" runat="server" Text='<%# Eval("SiteName") %>' ReadOnly="true"></dx:ASPxTextBox>
                                                    <div style="width: 5px;"></div>
                                                    <b>Site ID:</b>
                                                    <div style="width: 5px;"></div>
                                                    <dx:ASPxTextBox ID="txtSiteIDMain" ClientInstanceName="txtSiteIDMain" Width="40px" runat="server" Text='<%# Eval("CID") %>' ReadOnly="true"></dx:ASPxTextBox>
                                                </div>
                                                <div class="row"></div>
                                                <dx:ASPxPageControl ID="SiteDetailsPageControl" runat="server" ClientInstanceName="SiteDetailsPageControl">
                                                    <ClientSideEvents TabClick="function(s,e){
                                                        SetServiceEnabledDisabled(e);
                                                    }"
                                                        Init="function(s,e){SetServiceInitialize(e);}" />
                                                    <TabPages>
                                                        <dx:TabPage Name="SiteDetails" Text="Site Details">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
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
                                                                            <dx:ASPxLabel ID="lblSiteName" runat="server" Text="Site&nbsp;Name:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxTextBox ID="txtSiteName" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("SiteName") %>'></dx:ASPxTextBox>
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
                                                                            <dx:ASPxLabel ID="lblAddressLine2" runat="server" Text="Address&nbsp;Line&nbsp;2:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxTextBox ID="txtAddressLine2" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("AddressLine2")%>'></dx:ASPxTextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblCustomer" runat="server" Text="Customer:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxComboBox ID="cbCustomer" ClientInstanceName="cbCustomer" DataSourceID="odsCustomers" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="RightSides" runat="server" Width="260px" Height="20px"
                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("CustomerSortOrder") - 1%>' TextField="CustomerName" ValueField="Cid">
                                                                                <Columns>
                                                                                    <dx:ListBoxColumn FieldName="CustomerName" Width="130px" />
                                                                                    <dx:ListBoxColumn FieldName="Suburb" Width="100%" />
                                                                                </Columns>
                                                                            </dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblAddressLine4" runat="server" Text="Address&nbsp;Line&nbsp;4:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxTextBox ID="txtAddressLine4" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("AddressLine4")%>'></dx:ASPxTextBox>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxButton ID="btnCustomerDetails" ClientInstanceName="btnCustomerDetails" AutoPostBack="false" runat="server" Text="Customer Details">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                        ExecuteLinkCustomer(hdnCustomerID.GetText());
                                                                                    }" />
                                                                            </dx:ASPxButton>
                                                                            <div style="display: none">
                                                                                <dx:ASPxTextBox ID="hdnCustomerID" ClientInstanceName="hdnCustomerID" runat="server" Text='<%# Eval("Customer")%>'></dx:ASPxTextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row ">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblSuburb" runat="server" Text="Suburb:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-7">
                                                                            <div class="container">
                                                                                <div class="row row-md-margin-top">
                                                                                    <dx:ASPxTextBox ID="txtSuburb" runat="server" Width="111px" MaxLength="50" Text='<%# Eval("Suburb") %>'></dx:ASPxTextBox>
                                                                                    &nbsp;
                                                                                    <dx:ASPxLabel ID="lblState" runat="server" Text="State:"></dx:ASPxLabel>
                                                                                    &nbsp;
                                                                                    <dx:ASPxComboBox ID="cbState" DataSourceID="odsStates" PopupVerticalAlign="Below" PopupHorizontalAlign="RightSides" runat="server" Width="112px" SelectedIndex='<%# Eval("StateSortOrder") - 1 %>' Height="20px" TextField="StateDesc" ValueField="StateCode"></dx:ASPxComboBox>
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                    <dx:ASPxLabel ID="lblPCode" runat="server" Text="P/Code:"></dx:ASPxLabel>
                                                                                    &nbsp;
                                                                                    <dx:ASPxTextBox ID="txtPCode" runat="server" Width="50px" MaxLength="22" Text='<%# Eval("PostCode") %>'></dx:ASPxTextBox>
                                                                                    &nbsp;
                                                                                    <dx:ASPxLabel ID="lblZone" runat="server" Text="Zone:"></dx:ASPxLabel>
                                                                                    &nbsp;
                                                                                    <dx:ASPxComboBox ID="cbZone" DataSourceID="odsZones" PopupVerticalAlign="Below" PopupHorizontalAlign="RightSides" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("ZoneSortOrder") - 1%>' TextField="AreaDescription" ValueField="Aid"></dx:ASPxComboBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblIndustryGroup" runat="server" Text="Industry Group:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxTextBox ID="txtContactName" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("SiteContactName")%>'></dx:ASPxTextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblSiteContactName" runat="server" Text="Site&nbsp;Contact&nbsp;Name:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxComboBox ID="cbIndustryGroup" ClientInstanceName="cbIndustryGroup" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="RightSides" DataSourceID="odsIndustryGroups" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("IndustrySortOrder") - 1%>' TextField="IndustryDescription" ValueField="Aid"></dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxButton ID="btnIndustryGroup" ClientInstanceName="btnIndustryGroup" AutoPostBack="false" runat="server" Text="Industry Group">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                        ExecuteLinkIndustryGroup(hdnIndustryGroupID.GetText());
                                                                                    }" />
                                                                            </dx:ASPxButton>
                                                                            <div style="display: none">
                                                                                <dx:ASPxTextBox ID="hdnIndustryGroupID" ClientInstanceName="hdnIndustryGroupID" runat="server" Text='<%# Eval("IndustryGroup")%>'></dx:ASPxTextBox>
                                                                            </div>
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
                                                                            <dx:ASPxLabel ID="lblSitePhone" runat="server" Text="Site&nbsp;Phone:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxTextBox ID="txtSitePhone" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("SiteContactPhone")%>'></dx:ASPxTextBox>
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
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblSiteMobile" runat="server" Text="Site&nbsp;Mobile:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxTextBox ID="txtSiteContactMobile" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("SiteContactMobile")%>'></dx:ASPxTextBox>
                                                                        </div>
                                                                    </div>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Name="SiteContractDetails" Text="Site Contract Details">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblSiteContractExpiry" runat="server" Text="Site&nbsp;Contract&nbsp;Expiry:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxComboBox ID="cbPreviousSupplier" ClientInstanceName="cbPreviousSupplier" DataSourceID="odsPreviousSuppliers" PopupHorizontalAlign="RightSides" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("PreviousSupplierSortOrder") - 1%>' TextField="PreviousSupplier" ValueField="Aid"></dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblPreviousSupplier" runat="server" Text="Previous Supplier:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxDateEdit ID="dtContractExpiryDate" ClientInstanceName="dtContractExpiryDate" PopupHorizontalAlign="RightSides" runat="server" Date='<%# Eval("SiteContractExpiry")%>'></dx:ASPxDateEdit>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblInitialContract" runat="server" Text="Initial&nbsp;Contract&nbsp;Start&nbsp;Date:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxDateEdit ID="dtContractStartDate" ClientInstanceName="dtContractStartDate" PopupHorizontalAlign="RightSides" runat="server" Date='<%# Eval("SiteStartDate") %>'></dx:ASPxDateEdit>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxButton ID="btnPreviousSupplier" ClientInstanceName="btnPreviousSupplier" AutoPostBack="false" runat="server" Text="View Previous Suppliers">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                        ExecuteLinkPreviousSupplier(hdnPreviousSupplierID.GetText());
                                                                                    }" />
                                                                            </dx:ASPxButton>
                                                                            <div style="display: none">
                                                                                <dx:ASPxTextBox ID="hdnPreviousSupplierID" ClientInstanceName="hdnPreviousSupplierID" runat="server" Text='<%# Eval("PreviousSupplier")%>'></dx:ASPxTextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblInitialContractPeriod" runat="server" Text="Initial&nbsp;Contract&nbsp;Period:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxComboBox ID="cbInitialContractPeriod" DataSourceID="odsInitialContractPeriod" PopupHorizontalAlign="RightSides" runat="server" Width="170px" Height="20px"
                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("InitialContractPeriodSortOrder") - 1%>' TextField="ContractPeriodDesc" ValueField="Aid">
                                                                                <Columns>
                                                                                    <dx:ListBoxColumn FieldName="ContractPeriodDesc" Width="130px" />
                                                                                    <dx:ListBoxColumn FieldName="ContractPeriodMonths" Width="100%" />
                                                                                </Columns>
                                                                            </dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblInitialServiceAgreementNo" runat="server" Text="Initial&nbsp;Service&nbsp;Agreement&nbsp;No:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxComboBox ID="cbSalesPerson" DataSourceID="odsSalesPerson" runat="server" PopupHorizontalAlign="RightSides" Width="170px" Height="20px" SelectedIndex='<%# Eval("SalesPersonSortOrder") - 1%>' TextField="SalesPerson" ValueField="Aid"></dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblSalesPerson" runat="server" Text="Sales Person:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxTextBox ID="txtInitialServiceAgreementNo" runat="server" Width="260px" MaxLength="22" Text='<%# Eval("InitialServiceAgreementNo") %>'></dx:ASPxTextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblContractCeaseReason" runat="server" Text="Contract Cease Reason:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxComboBox ID="cbSiteCeaseReason" ClientInstanceName="cbSiteCeaseReason" PopupHorizontalAlign="RightSides" DataSourceID="odsSiteCeaseReason" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("ContractCeaseReasonsSortOrder") - 1%>' TextField="CeaseReasonDescription" ValueField="Aid"></dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblContractCeaseDate" runat="server" Text="Contract Cease Date:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxDateEdit ID="dtContractCeaseDate" ClientInstanceName="dtContractCeaseDate" PopupHorizontalAlign="Center" runat="server" Date='<%# Eval("SiteCeaseDate") %>'></dx:ASPxDateEdit>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxButton ID="btnViewCeasedReasons" ClientInstanceName="btnViewCeasedReasons" AutoPostBack="false" runat="server" Text="View Ceased Reasons">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                        ExecuteLinkCeaseReasons(hdnSiteCeaseReason.GetText());
                                                                                    }" />
                                                                            </dx:ASPxButton>
                                                                            <div style="display: none">
                                                                                <dx:ASPxTextBox ID="hdnSiteCeaseReason" ClientInstanceName="hdnSiteCeaseReason" runat="server" Text='<%# Eval("SiteCeaseReason")%>'></dx:ASPxTextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-8">
                                                                            <div style="display: none">
                                                                            </div>
                                                                            <dx:ASPxGridView ID="ResignHistoryGridView" KeyFieldName="ResignHistoryID" DataSourceID="odsSiteResignDetails" runat="server" Theme="SoftOrange"
                                                                                AutoGenerateColumns="False" OnRowUpdating="ResignHistoryGridView_RowUpdating" OnRowInserting="ResignHistoryGridView_RowInserting">
                                                                                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                                                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                                                <Settings ShowPreview="true" />
                                                                                <SettingsPager PageSize="10" />
                                                                                <Columns>
                                                                                    <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="ResignHistoryID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="Cid" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="SiteCId" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataDateColumn FieldName="ReSignDate" VisibleIndex="4" Visible="true"></dx:GridViewDataDateColumn>
                                                                                    <dx:GridViewDataComboBoxColumn FieldName="ReSignPeriod" Caption="Re-Sign Period" VisibleIndex="5">
                                                                                        <PropertiesComboBox DataSourceID="odsInitialContractPeriod" TextField="ContractPeriodDesc" ValueField="Aid" Width="150px">
                                                                                            <Columns>
                                                                                                <dx:ListBoxColumn FieldName="ContractPeriodDesc" Width="80px" />
                                                                                                <dx:ListBoxColumn FieldName="ContractPeriodMonths" Width="80px" />
                                                                                            </Columns>
                                                                                            <ClearButton Visibility="Auto"></ClearButton>
                                                                                        </PropertiesComboBox>
                                                                                    </dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="ServiceAgreementNo" VisibleIndex="6" Visible="true" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataComboBoxColumn FieldName="SalesPerson" Caption="Sales Person" VisibleIndex="7">
                                                                                        <PropertiesComboBox DataSourceID="odsSalesPerson" TextField="SalesPerson" ValueField="Aid" Width="150px" PopupVerticalAlign="Above">
                                                                                            <ClearButton Visibility="Auto"></ClearButton>
                                                                                        </PropertiesComboBox>
                                                                                    </dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataDateColumn FieldName="ContractExpiryDate" Caption="Expiry Date" VisibleIndex="8" Visible="true"></dx:GridViewDataDateColumn>
                                                                                </Columns>
                                                                            </dx:ASPxGridView>
                                                                            <asp:ObjectDataSource ID="odsSiteResignDetails" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblSiteReSignDetails" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllSiteID" TypeName="FMS.Business.DataObjects.tblSiteReSignDetails" UpdateMethod="Update">
                                                                                <SelectParameters>
                                                                                    <asp:ControlParameter ControlID="hdnSiteCid" PropertyName="Text" Name="siteID" Type="Int32"></asp:ControlParameter>
                                                                                </SelectParameters>
                                                                            </asp:ObjectDataSource>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblLostBusinessTo" runat="server" Text="Lost Business To:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxComboBox ID="cbLostBusinessTo" ClientInstanceName="cbLostBusinessTo" PopupHorizontalAlign="RightSides" DataSourceID="odsPreviousSuppliers" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("LostBusinessToSortOrder") - 1%>' TextField="PreviousSupplier" ValueField="Aid"></dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxButton ID="btnLostBusinessTo" ClientInstanceName="btnLostBusinessTo" AutoPostBack="false" runat="server" Text="View Lost Business To">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                        ExecuteLinkLostBusinessTo(hdnLostBusinessTo.GetText());
                                                                                    }" />
                                                                            </dx:ASPxButton>
                                                                            <div style="display: none">
                                                                                <dx:ASPxTextBox ID="hdnLostBusinessTo" ClientInstanceName="hdnLostBusinessTo" runat="server" Text='<%# Eval("LostBusinessTo")%>'></dx:ASPxTextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Name="SiteInvoicingDetails" Text="Site Invoicing Details">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <div style="border: 1px solid gray; width: 600px; padding: 5px">
                                                                        <div class="row">
                                                                            <div class="col-md-2">
                                                                                <dx:ASPxLabel ID="lblAddress1" runat="server" Text="Address&nbsp;Line&nbsp;1:" Width="100px"></dx:ASPxLabel>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <p style="font-weight: bold; color: black">Postal Address</p>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <dx:ASPxTextBox ID="txtPostalAddressLine1" ClientInstanceName="txtPostalAddressLine1" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("PostalAddressLine1") %>'></dx:ASPxTextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-2">
                                                                                <dx:ASPxLabel ID="lblAddress2" runat="server" Text="Address&nbsp;Line&nbsp;2:" Width="100px"></dx:ASPxLabel>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <dx:ASPxTextBox ID="txtPostalAddressLine2" ClientInstanceName="txtPostalAddressLine2" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("PostalAddressLine2") %>'></dx:ASPxTextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row ">
                                                                            <div class="col-md-2">
                                                                                <dx:ASPxLabel ID="lblSiteInvoicingSuburb" runat="server" Text="Suburb:" Width="100px"></dx:ASPxLabel>
                                                                            </div>
                                                                            <div class="col-md-7">
                                                                                <div class="container">
                                                                                    <div class="row row-md-margin-top">
                                                                                        <dx:ASPxTextBox ID="txtPostalSuburb" ClientInstanceName="txtPostalSuburb" runat="server" Width="111px" MaxLength="22" Text='<%# Eval("PostalSuburb")%>'></dx:ASPxTextBox>
                                                                                        &nbsp;
                                                                                        <dx:ASPxLabel ID="lblSiteInvoicingState" runat="server" Text="State:"></dx:ASPxLabel>
                                                                                        &nbsp;
                                                                                        <dx:ASPxComboBox ID="cbPostalState" ClientInstanceName="cbPostalState" DataSourceID="odsStates" runat="server" Width="112px" SelectedIndex='<%# Eval("StateSortOrder") - 1%>' Height="20px" TextField="StateDesc" ValueField="Sid"></dx:ASPxComboBox>
                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                        <dx:ASPxLabel ID="lblSiteInvoicingPostalState" runat="server" Text="P/Code:"></dx:ASPxLabel>
                                                                                        &nbsp;
                                                                                        <dx:ASPxTextBox ID="txtPostalPostCode" ClientInstanceName="txtPostalPostCode" runat="server" Width="50px" MaxLength="22" Text='<%# Eval("PostalPostCode")%>'></dx:ASPxTextBox>
                                                                                        &nbsp;
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblInvoiceFrequency" runat="server" Text="Invoice&nbsp;Frequency:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxComboBox ID="cbInvoiceFrequency" ClientInstanceName="cbInvoiceFrequency" DataSourceID="odsInvoiceFrequency" runat="server" Width="170px" Height="20px"
                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("InvoicingFrequencySortOrder") - 1%>' TextField="Frequency" ValueField="iid">
                                                                            </dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblInvoiceCommencing" runat="server" Text="Invoice&nbsp;Commencing:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxDateEdit ID="dtInvoiceCommencing" ClientInstanceName="dtInvoiceCommencing" runat="server" Date='<%# Eval("InvoiceCommencing")%>'></dx:ASPxDateEdit>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row"></div>
                                                                    <div class="row"></div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblSeparateInvoice" runat="server" Text="Separate&nbsp;Invoice:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxCheckBox ID="chkSeparateInvoice" runat="server" Checked='<%# Eval("SeparateInvoice")%>'></dx:ASPxCheckBox>
                                                                            <dx:ASPxLabel ID="lblPurchaseOrderNumber" runat="server" Text="Purchase&nbsp;Order&nbsp;Number:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxTextBox ID="txtPurchaseOrderNumber" runat="server" Width="200px" MaxLength="50" Text='<%# Eval("PurchaseOrderNumber")%>'></dx:ASPxTextBox>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblExcludeFuelLevy" runat="server" Text="Exclude&nbsp;Fuel&nbsp;Levy:" Width="100px"></dx:ASPxLabel>
                                                                            <dx:ASPxCheckBox ID="chkExcludeFuelLevy" runat="server" Checked='<%# Eval("chkSitesExcludeFuelLevy")%>'></dx:ASPxCheckBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <p style="font-weight: bold; color: black">Invoicing Months</p>
                                                                        </div>
                                                                    </div>
                                                                    <div style="width: 680px">
                                                                        <div style="float: right">
                                                                            <div class="row">
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-2">
                                                                                    <dx:ASPxButton ID="btnCopyPreviousDetails" ClientInstanceName="btnCopyPreviousDetails" AutoPostBack="false" runat="server" Text="Copy Previous Details">
                                                                                        <ClientSideEvents Click="function(s,e) {
                                                                                                getSiteID(hdnSiteCid.GetText());
                                                                                            }" />
                                                                                    </dx:ASPxButton>

                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <dx:ASPxComboBox ID="cbRateIncrease" DataSourceID="odsRateIncrease" runat="server" Width="170px" Height="20px"
                                                                                        CallbackPageSize="30" SelectedIndex='<%# Eval("cmbRateIncreaseSortOrder") - 1%>' TextField="RateIncreaseDescription" ValueField="Aid">
                                                                                    </dx:ASPxComboBox>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <dx:ASPxLabel ID="lblRateIncrease" runat="server" Text="Rate&nbsp;Increase&nbsp;:" Width="100px"></dx:ASPxLabel>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-2">
                                                                                    <dx:ASPxButton ID="btnPastePreviousDetails" ClientInstanceName="btnPastePreviousDetails" AutoPostBack="false" runat="server" Text="Paste Previous Details">
                                                                                        <ClientSideEvents Click="function(s,e) {
                                                                                                getSiteInvoicingDetails(hdnStoreCid.GetText());
                                                                                            }" />
                                                                                    </dx:ASPxButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div style="border: 1px solid gray; width: 350px; padding: 5px">
                                                                            <div class="row">
                                                                                <div class="col-md-2">
                                                                                    <dx:ASPxLabel ID="lblInvoiceMonth2" runat="server" Text="Invoice&nbsp;Month&nbsp;2:" Width="100px"></dx:ASPxLabel>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <dx:ASPxComboBox ID="cbInvoiceMonth2" ClientInstanceName="cbInvoiceMonth2" DataSourceID="odsInvoiceMonth" runat="server" Width="170px" Height="20px"
                                                                                        CallbackPageSize="30" SelectedIndex='<%# Eval("InvoiceMonth2") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                    </dx:ASPxComboBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-2">
                                                                                    <dx:ASPxLabel ID="lblInvoiceMonth1" runat="server" Text="Invoice&nbsp;Month&nbsp;1:" Width="100px"></dx:ASPxLabel>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <dx:ASPxComboBox ID="cbInvoiceMonth1" ClientInstanceName="cbInvoiceMonth1" DataSourceID="odsInvoiceMonth" runat="server" Width="170px" Height="20px"
                                                                                        CallbackPageSize="30" SelectedIndex='<%# Eval("InvoiceMonth1") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                    </dx:ASPxComboBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-2">
                                                                                    <dx:ASPxLabel ID="lblInvoiceMonth4" runat="server" Text="Invoice&nbsp;Month&nbsp;4:" Width="100px"></dx:ASPxLabel>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <dx:ASPxComboBox ID="cbInvoiceMonth4" ClientInstanceName="cbInvoiceMonth4" DataSourceID="odsInvoiceMonth" runat="server" Width="170px" Height="20px"
                                                                                        CallbackPageSize="30" SelectedIndex='<%# Eval("InvoiceMonth4") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                    </dx:ASPxComboBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-2">
                                                                                    <dx:ASPxLabel ID="lblInvoiceMonth3" runat="server" Text="Invoice&nbsp;Month&nbsp;3:" Width="100px"></dx:ASPxLabel>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <dx:ASPxComboBox ID="cbInvoiceMonth3" ClientInstanceName="cbInvoiceMonth3" DataSourceID="odsInvoiceMonth" runat="server" Width="170px" Height="20px"
                                                                                        CallbackPageSize="30" SelectedIndex='<%# Eval("InvoiceMonth3") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                    </dx:ASPxComboBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div style="padding: 2px; padding-top: 140px"></div>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Name="SiteServices" Text="Site Services">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxGridView ID="CustomerServiceGridView" KeyFieldName="CustomerServiceID" Theme="SoftOrange"
                                                                                DataSourceID="odsCustomerService" runat="server" AutoGenerateColumns="False" Width="900px"
                                                                                OnCancelRowEditing="CustomerServiceGridView_CancelRowEditing"
                                                                                OnRowUpdating="CustomerServiceGridView_RowUpdating" OnRowInserting="CustomerServiceGridView_RowInserting">
                                                                                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                                                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                                                <Settings ShowPreview="true" />
                                                                                <SettingsPager PageSize="5" />
                                                                                <Columns>
                                                                                    <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="CustomerServiceID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="ID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="CId" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataComboBoxColumn PropertiesComboBox-DataSourceID="odsServices" FieldName="CSid" PropertiesComboBox-TextField="ServiceDescription" PropertiesComboBox-ValueField="sid" Caption="Services" VisibleIndex="4"></dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataComboBoxColumn PropertiesComboBox-DataSourceID="odsFrequency" FieldName="ServiceFrequencyCode" PropertiesComboBox-TextField="FrequencyDescription" PropertiesComboBox-ValueField="Fid" Caption="Frequency" VisibleIndex="5"></dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="ServiceUnits" VisibleIndex="6" Caption="Service Units"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="ServicePrice" VisibleIndex="7" Caption="Unit Price PA"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="PerAnnumCharge" VisibleIndex="8" Caption="Amount Per Annum"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataComboBoxColumn PropertiesComboBox-DataSourceID="odsServiceRun" FieldName="ServiceRun" PropertiesComboBox-TextField="RunDescription" PropertiesComboBox-ValueField="Rid" Caption="Frequency" VisibleIndex="9"></dx:GridViewDataComboBoxColumn>
                                                                                </Columns>
                                                                                <Templates>
                                                                                    <EditForm>
                                                                                        <div style="width: 850px; height: 200px">
                                                                                            <div class="row">
                                                                                                <div class="col-md-10">
                                                                                                    <div class="row">
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-1">
                                                                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Sort&nbsp;Code" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                        </div>
                                                                                                        <div style="width: 50px;"></div>
                                                                                                        <div class="col-md-1">
                                                                                                            <dx:ASPxComboBox ID="cbFrequency" ClientInstanceName="cbFrequency" DataSourceID="odsFrequency" runat="server" Width="100px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequencySortOrder") - 1%>' TextField="FrequencyDescription" ValueField="Fid">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="width: 10px;"></div>
                                                                                                        <div class="col-md-1">
                                                                                                            <dx:ASPxSpinEdit ID="txtServiceUnits" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("ServiceUnits")%>'></dx:ASPxSpinEdit>
                                                                                                        </div>
                                                                                                        <div style="width: 10px;"></div>
                                                                                                        <div class="col-md-1">
                                                                                                            <dx:ASPxSpinEdit ID="txtServicePrice" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("ServicePrice")%>'></dx:ASPxSpinEdit>
                                                                                                        </div>
                                                                                                        <div style="width: 15px;"></div>
                                                                                                        <div class="col-md-1">
                                                                                                            <dx:ASPxSpinEdit ID="txtPerAnnumCharge" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("PerAnnumCharge")%>'></dx:ASPxSpinEdit>
                                                                                                        </div>
                                                                                                        <div style="width: 15px;"></div>
                                                                                                        <div class="col-md-2">
                                                                                                            <dx:ASPxComboBox ID="cbServiceRun" ClientInstanceName="cbServiceRun" DataSourceID="odsServiceRun" runat="server" Width="100px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceRunSortOrder") - 1%>' TextField="RunDescription" ValueField="Rid">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div class="col-md-4">
                                                                                                            <dx:ASPxLabel ID="lblMonthsForPeriodicRuns" runat="server" Text="Months&nbsp;for&nbsp;periodic&nbsp;runs" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                        </div>
                                                                                                        <div class="col-md-4">
                                                                                                            <dx:ASPxCheckBox ID="chkUnitsHaveMoreThanOneRun" runat="server" Checked='<%# Eval("UnitsHaveMoreThanOneRun")%>'></dx:ASPxCheckBox>
                                                                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Units&nbsp;Have&nbsp;More&nbsp;Than&nbsp;One&nbsp;Run" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                        </div>
                                                                                                        <div class="col-md-1">
                                                                                                            <dx:ASPxComboBox ID="cbServices" ClientInstanceName="cbServices" DataSourceID="odsServices" runat="server" Width="140px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServicesSortOrder") - 1%>' TextField="ServiceDescription" ValueField="sid" IncrementalFilteringMode="StartsWith">
                                                                                                                <Columns>
                                                                                                                    <dx:ListBoxColumn FieldName="ServiceDescription" Width="200px" />
                                                                                                                    <dx:ListBoxColumn FieldName="ServiceCode" Width="80px" />
                                                                                                                </Columns>
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div style="padding-left: 12px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency1" ClientInstanceName="cbServiceFrequency1" DataSourceID="odsInvoiceMonth" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency1SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency3" ClientInstanceName="cbServiceFrequency3" DataSourceID="odsServiceFrequency" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency3SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency2" ClientInstanceName="cbServiceFrequency2" DataSourceID="odsServiceFrequency" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency2SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency5" ClientInstanceName="cbServiceFrequency5" DataSourceID="odsServiceFrequency" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency5SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency4" ClientInstanceName="cbServiceFrequency4" DataSourceID="odsServiceFrequency" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency4SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency7" ClientInstanceName="cbServiceFrequency7" DataSourceID="odsServiceFrequency" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency7SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency6" ClientInstanceName="cbServiceFrequency6" DataSourceID="odsServiceFrequency" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency6SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxTextBox ID="txtSortCode" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("ServiceSortOrderCode")%>'></dx:ASPxTextBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency8" ClientInstanceName="cbServiceFrequency8" DataSourceID="odsServiceFrequency" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency8SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div style="position: absolute; z-index: 1; left: 700px;">
                                                                                                    <dx:ASPxMemo ID="txtServiceComments" runat="server" class="dxeMemoEditAreaSys" Width="180px" Height="110px" Text='<%# Eval("ServiceComments")%>'></dx:ASPxMemo>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row">
                                                                                                <div class="col-md-1">
                                                                                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Service" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                </div>
                                                                                                <div style="width: 50px;"></div>
                                                                                                <div class="col-md-1">
                                                                                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Frequency" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                </div>
                                                                                                <div class="col-md-1">
                                                                                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Service Units" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                </div>
                                                                                                <div style="width: 5px;"></div>
                                                                                                <div class="col-md-1">
                                                                                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Unit Price PA" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                </div>
                                                                                                <div style="width: 5px;"></div>
                                                                                                <div class="col-md-1">
                                                                                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Amount Per Annum" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                </div>
                                                                                                <div style="width: 5px;"></div>
                                                                                                <div class="col-md-2">
                                                                                                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Run / Driver" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                </div>
                                                                                                <div class="col-md-1">
                                                                                                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Comments" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div style="text-align: right; padding-right: 10px">
                                                                                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                                                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                                                        </div>
                                                                                    </EditForm>
                                                                                </Templates>
                                                                            </dx:ASPxGridView>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="General Site Service Comments:" Font-Bold="true" Width="100%"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxMemo ID="txtGeneralSiteServiceComments" runat="server" class="dxeMemoEditAreaSys" Width="630px" Height="90px" Text='<%# Eval("GeneralSiteServiceComments")%>'></dx:ASPxMemo>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2"></div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Total Units:" Font-Bold="true" Width="100%"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxTextBox ID="txtTotalUnits" ClientInstanceName="txtTotalUnits" Width="70px" runat="server" Text='<%# Eval("TotalUnits")%>'></dx:ASPxTextBox>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Total&nbsp;Amount:" Font-Bold="true" Width="100%"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxTextBox ID="txtTotalAmount" ClientInstanceName="txtTotalAmount" Width="70px" runat="server" Text='<%# Eval("TotalAmount")%>'></dx:ASPxTextBox>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxButton ID="btnReCalculate" ClientInstanceName="btnReCalculate" AutoPostBack="false" runat="server" Text="Re-Calculate">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                        ReCalculateSiteServices(hdnSiteCid.GetText());
                                                                                    }" />
                                                                            </dx:ASPxButton>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxButton ID="btnSiteList" ClientInstanceName="btnSiteList" AutoPostBack="false" runat="server" Text="Site List">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                        ViewSiteList(hdnSiteCid.GetText());
                                                                                    }" />
                                                                            </dx:ASPxButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row"></div>
                                                                    <asp:ObjectDataSource ID="odsCustomerService" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblCustomerServices" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllByCidWithSortOrders" TypeName="FMS.Business.DataObjects.tblCustomerServices" UpdateMethod="Update">
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="hdnSiteCid" PropertyName="Text" Name="cid" Type="Int32"></asp:ControlParameter>
                                                                        </SelectParameters>
                                                                    </asp:ObjectDataSource>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Name="CIRHistory" Text="CIR History">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxGridView ID="CIRHistoryGridView" KeyFieldName="HistoryID" Theme="SoftOrange"
                                                                                DataSourceID="odsCIRHistory" runat="server" AutoGenerateColumns="False" Width="900px"
                                                                                OnRowUpdating="CIRHistoryGridView_RowUpdating" OnRowInserting="CIRHistoryGridView_RowInserting">
                                                                                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                                                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                                                <Settings ShowPreview="true" />
                                                                                <SettingsPager PageSize="10" />
                                                                                <Columns>
                                                                                    <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="HistoryID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="NCId" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="Cid" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataDateColumn FieldName="NCRDate" Caption="CIR Date" VisibleIndex="4" Visible="true"></dx:GridViewDataDateColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="NCRNumber" Caption="Number" VisibleIndex="5" Visible="true"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataComboBoxColumn PropertiesComboBox-DataSourceID="odsReason" FieldName="NCRReason" PropertiesComboBox-TextField="CIRReason" PropertiesComboBox-ValueField="CId" Caption="Reason" VisibleIndex="6"></dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataComboBoxColumn PropertiesComboBox-DataSourceID="odsDrivers" FieldName="Driver" PropertiesComboBox-TextField="DriverName" PropertiesComboBox-ValueField="Did" Caption="Driver" VisibleIndex="7"></dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="NCRRecordedBY" Caption="Recorded By" VisibleIndex="8" Visible="true"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="NCRClosedBy" Caption="Recorded By" VisibleIndex="9" Visible="true"></dx:GridViewDataTextColumn>
                                                                                </Columns>
                                                                                <Templates>
                                                                                    <EditForm>
                                                                                        <div class="row">
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxSpinEdit ID="txtNCRNumber" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("NCRNumber")%>'></dx:ASPxSpinEdit>
                                                                                            </div>
                                                                                            <div style="width: 75px;"></div>
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxTextBox ID="txtNCRRecordedBY" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("NCRRecordedBY")%>'></dx:ASPxTextBox>
                                                                                            </div>
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxTextBox ID="txtNCRClosedBy" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("NCRClosedBy")%>'></dx:ASPxTextBox>
                                                                                            </div>
                                                                                            <div style="width: 80px;"></div>
                                                                                            <div class="col-md-2">
                                                                                                <dx:ASPxDateEdit ID="dtNCRDate" PopupVerticalAlign="Below" ClientInstanceName="dtNCRDate" runat="server" Date='<%# Eval("NCRDate") %>'></dx:ASPxDateEdit>
                                                                                            </div>
                                                                                            <div style="width: 10px;"></div>
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxLabel ID="lblRecordedBy" runat="server" Text="Recorded By" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                            </div>
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxLabel ID="lblClosedBy" runat="server" Text="Closed By" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <dx:ASPxComboBox ID="cbReason" ClientInstanceName="cbReason" DataSourceID="odsReason" runat="server" Width="180px" Height="20px"
                                                                                                    CallbackPageSize="30" SelectedIndex='<%# Eval("NCRReasonSortOrder") - 1%>' TextField="CIRReason" ValueField="CId">
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxLabel ID="lblCirDate" runat="server" Text="CIR Date" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <dx:ASPxComboBox ID="cbDrivers" ClientInstanceName="cbDrivers" DataSourceID="odsDrivers" runat="server" Width="180px" Height="20px"
                                                                                                    CallbackPageSize="30" SelectedIndex='<%# Eval("DriverSortOrder") - 1%>' TextField="DriverName" ValueField="Did">
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                            <div style="width: 5px;"></div>
                                                                                            <div class="col-md-2">
                                                                                                <dx:ASPxLabel ID="lblDriver" runat="server" Text="Driver" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                            </div>
                                                                                            <div style="width: 5px;"></div>
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxLabel ID="lblNumber" runat="server" Text="Number" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                            </div>
                                                                                            <div style="width: 5px;"></div>
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxLabel ID="lblReason" runat="server" Text="Reason" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-12">
                                                                                                <dx:ASPxMemo ID="txtNCRDescription" runat="server" class="dxeMemoEditAreaSys" Width="870px" Height="90px" Text='<%# Eval("NCRDescription")%>'></dx:ASPxMemo>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div style="padding: 2px; padding-top: 140px"></div>
                                                                                        <div style="text-align: right; padding-right: 10px">
                                                                                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                                                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                                                        </div>
                                                                                    </EditForm>
                                                                                </Templates>
                                                                            </dx:ASPxGridView>
                                                                        </div>
                                                                    </div>
                                                                    <asp:ObjectDataSource ID="odsCIRHistory" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblCIRHistory" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllByCIDWithSortOrder" TypeName="FMS.Business.DataObjects.tblCIRHistory" UpdateMethod="Update">
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="hdnSiteCid" PropertyName="Text" Name="cid" Type="Int16"></asp:ControlParameter>
                                                                        </SelectParameters>
                                                                    </asp:ObjectDataSource>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Name="Comments" Text="Comments">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxGridView ID="SiteCommentsGridView" KeyFieldName="CommentsID"
                                                                                DataSourceID="odsSiteComments" runat="server" Theme="SoftOrange" AutoGenerateColumns="False" Width="900px"
                                                                                OnRowUpdating="SiteCommentsGridView_RowUpdating" OnRowInserting="SiteCommentsGridView_RowInserting">
                                                                                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                                                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                                                <Settings ShowPreview="true" />
                                                                                <SettingsPager PageSize="10" />
                                                                                <Columns>
                                                                                    <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="CommentsID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="Aid" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="Cid" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataDateColumn FieldName="CommentDate" VisibleIndex="5"></dx:GridViewDataDateColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="Comments" VisibleIndex="6"></dx:GridViewDataTextColumn>
                                                                                </Columns>
                                                                                <Templates>
                                                                                    <EditForm>
                                                                                        <div class="row">
                                                                                            <div class="col-md-2">Date</div>
                                                                                            <div class="col-md-3">
                                                                                                <dx:ASPxMemo ID="txtSiteComments" runat="server" class="dxeMemoEditAreaSys" Width="670px" Height="90px" Text='<%# Eval("Comments")%>'></dx:ASPxMemo>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-2">
                                                                                                <dx:ASPxDateEdit ID="dtCommentDate" ClientInstanceName="dtNCRDate" runat="server" Date='<%# Eval("CommentDate")%>'></dx:ASPxDateEdit>
                                                                                            </div>
                                                                                            <div class="col-md-1">Comments</div>
                                                                                        </div>
                                                                                        <div style="text-align: right; padding-right: 10px; padding-top: 10px">
                                                                                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                                                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                                                        </div>
                                                                                    </EditForm>
                                                                                </Templates>
                                                                            </dx:ASPxGridView>
                                                                            <asp:ObjectDataSource ID="odsSiteComments" runat="server" SelectMethod="GetAllByCID" TypeName="FMS.Business.DataObjects.tblSiteComments" DataObjectTypeName="FMS.Business.DataObjects.tblSiteComments" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update">
                                                                                <SelectParameters>
                                                                                    <asp:ControlParameter ControlID="hdnSiteCid" PropertyName="Text" Name="cid" Type="Int16"></asp:ControlParameter>
                                                                                </SelectParameters>
                                                                            </asp:ObjectDataSource>
                                                                        </div>
                                                                    </div>
                                                                    <div style="padding: 2px; padding-top: 140px"></div>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                    </TabPages>
                                                </dx:ASPxPageControl>
                                            </div>
                                            <div style="text-align: right; padding: 2px">
                                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                                            </div>
                                        
</EditForm>
                                    </Templates>
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="SiteID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Cid" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SiteName" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CustomerName" VisibleIndex="4"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="AddressLine1" VisibleIndex="5"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Suburb" VisibleIndex="6" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="State" VisibleIndex="7" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PostCode" VisibleIndex="8" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PhoneNo" VisibleIndex="9" Visible="false"></dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowPreview="true" />
                                    <SettingsPager PageSize="10" />
                                    <SettingsEditing Mode="PopupEditForm" />
                                    <SettingsPopup>
                                        <EditForm Modal="true"
                                            VerticalAlign="WindowCenter"
                                            HorizontalAlign="WindowCenter" Height="480px" />
                                    </SettingsPopup>
                                    <Templates>
                                        <EditForm>
                                            <div class="container">
                                                <div style="display: none">
                                                    <dx:ASPxTextBox ID="txtSiteID" ClientInstanceName="siteID" runat="server" Text='<%# Eval("SiteID") %>'></dx:ASPxTextBox>
                                                    <dx:ASPxTextBox ID="hdnSiteCid" ClientInstanceName="hdnSiteCid" runat="server" Text='<%# Eval("CID") %>'></dx:ASPxTextBox>
                                                </div>
                                                <div class="row"></div>
                                                <div class="row">
                                                    <div class="col-md-1"><b>Customer&nbsp;Rating:</b></div>
                                                    <div style="width: 15px;"></div>
                                                    <dx:ASPxTextBox ID="txtCustomerRating" ClientInstanceName="txtCustomerRating" Width="30px" runat="server" Text='<%# Eval("CustomerRating") %>' ReadOnly="true"></dx:ASPxTextBox>
                                                    <dx:ASPxTextBox ID="txtCustomerRatingDesc" ClientInstanceName="txtCustomerRatingDesc" Width="100px" runat="server" Text='<%# Eval("CustomerRatingDesc") %>' ReadOnly="true"></dx:ASPxTextBox>
                                                    <div class="col-md-1" style="text-align: right"><b>Customer</b></div>
                                                    <dx:ASPxTextBox ID="txtCustomerName" ClientInstanceName="txtCustomerName" runat="server" Text='<%# Eval("CustomerName") %>' ReadOnly="true"></dx:ASPxTextBox>
                                                    <div style="width: 10px;"></div>
                                                    <b>Site</b>
                                                    <div style="width: 5px;"></div>
                                                    <dx:ASPxTextBox ID="txtSiteNameMain" ClientInstanceName="txtSiteNameMain" runat="server" Text='<%# Eval("SiteName") %>' ReadOnly="true"></dx:ASPxTextBox>
                                                    <div style="width: 5px;"></div>
                                                    <b>Site ID:</b>
                                                    <div style="width: 5px;"></div>
                                                    <dx:ASPxTextBox ID="txtSiteIDMain" ClientInstanceName="txtSiteIDMain" Width="40px" runat="server" Text='<%# Eval("CID") %>' ReadOnly="true"></dx:ASPxTextBox>
                                                </div>
                                                <dx:ASPxPageControl ID="SiteDetailsPageControl" runat="server" ClientInstanceName="SiteDetailsPageControl">
                                                    <ClientSideEvents TabClick="function(s,e){
                                                        SetServiceEnabledDisabled(e);
                                                    }"
                                                        Init="function(s,e){SetServiceInitialize(e);}" />
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
                                                                                    <dx:ASPxTextBox ID="txtSuburb" runat="server" Width="111px" MaxLength="50" Text='<%# Eval("Suburb") %>'></dx:ASPxTextBox>
                                                                                    &nbsp;
                                                                                    <dx:ASPxLabel ID="lblState" runat="server" Text="State:"></dx:ASPxLabel>
                                                                                    &nbsp;
                                                                                    <dx:ASPxComboBox ID="cbState" DataSourceID="odsStates" PopupVerticalAlign="Below" PopupHorizontalAlign="RightSides" runat="server" Width="112px" SelectedIndex='<%# Eval("StateSortOrder") - 1 %>' Height="20px" TextField="StateDesc" ValueField="StateCode"></dx:ASPxComboBox>
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                    <dx:ASPxLabel ID="lblPCode" runat="server" Text="P/Code:"></dx:ASPxLabel>
                                                                                    &nbsp;
                                                                                    <dx:ASPxTextBox ID="txtPCode" runat="server" Width="50px" MaxLength="22" Text='<%# Eval("PostCode") %>'></dx:ASPxTextBox>
                                                                                    &nbsp;
                                                                                    <dx:ASPxLabel ID="lblZone" runat="server" Text="Zone:"></dx:ASPxLabel>
                                                                                    &nbsp;
                                                                                    <dx:ASPxComboBox ID="cbZone" DataSourceID="odsZones" PopupVerticalAlign="Below" PopupHorizontalAlign="RightSides" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("ZoneSortOrder") - 1%>' TextField="AreaDescription" ValueField="Aid"></dx:ASPxComboBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblCustomer" runat="server" Text="Customer:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxComboBox ID="cbCustomer" ClientInstanceName="cbCustomer" DataSourceID="odsCustomers" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="RightSides" runat="server" Width="260px" Height="20px"
                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("CustomerSortOrder") - 1%>' TextField="CustomerName" ValueField="Cid">
                                                                                <Columns>
                                                                                    <dx:ListBoxColumn FieldName="CustomerName" Width="130px" />
                                                                                    <dx:ListBoxColumn FieldName="Suburb" Width="100%" />
                                                                                </Columns>
                                                                            </dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxButton ID="btnCustomerDetails" ClientInstanceName="btnCustomerDetails" AutoPostBack="false" runat="server" Text="Customer Details">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                        ExecuteLinkCustomer(hdnCustomerID.GetText());
                                                                                    }" />
                                                                            </dx:ASPxButton>
                                                                            <div style="display: none">
                                                                                <dx:ASPxTextBox ID="hdnCustomerID" ClientInstanceName="hdnCustomerID" runat="server" Text='<%# Eval("Customer")%>'></dx:ASPxTextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblIndustryGroup" runat="server" Text="Industry Group:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxComboBox ID="cbIndustryGroup" ClientInstanceName="cbIndustryGroup" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="RightSides" DataSourceID="odsIndustryGroups" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("IndustrySortOrder") - 1%>' TextField="IndustryDescription" ValueField="Aid"></dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxButton ID="btnIndustryGroup" ClientInstanceName="btnIndustryGroup" AutoPostBack="false" runat="server" Text="Industry Group">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                        ExecuteLinkIndustryGroup(hdnIndustryGroupID.GetText());
                                                                                    }" />
                                                                            </dx:ASPxButton>
                                                                            <div style="display: none">
                                                                                <dx:ASPxTextBox ID="hdnIndustryGroupID" ClientInstanceName="hdnIndustryGroupID" runat="server" Text='<%# Eval("IndustryGroup")%>'></dx:ASPxTextBox>
                                                                            </div>
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
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblPreviousSupplier" runat="server" Text="Previous Supplier:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxComboBox ID="cbPreviousSupplier" ClientInstanceName="cbPreviousSupplier" DataSourceID="odsPreviousSuppliers" PopupHorizontalAlign="RightSides" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("PreviousSupplierSortOrder") - 1%>' TextField="PreviousSupplier" ValueField="Aid"></dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxButton ID="btnPreviousSupplier" ClientInstanceName="btnPreviousSupplier" AutoPostBack="false" runat="server" Text="View Previous Suppliers">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                        ExecuteLinkPreviousSupplier(hdnPreviousSupplierID.GetText());
                                                                                    }" />
                                                                            </dx:ASPxButton>
                                                                            <div style="display: none">
                                                                                <dx:ASPxTextBox ID="hdnPreviousSupplierID" ClientInstanceName="hdnPreviousSupplierID" runat="server" Text='<%# Eval("PreviousSupplier")%>'></dx:ASPxTextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblInitialContract" runat="server" Text="Initial&nbsp;Contract&nbsp;Start&nbsp;Date:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxDateEdit ID="dtContractStartDate" ClientInstanceName="dtContractStartDate" PopupHorizontalAlign="RightSides" runat="server" Date='<%# Eval("SiteStartDate") %>'></dx:ASPxDateEdit>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblSiteContractExpiry" runat="server" Text="Site&nbsp;Contract&nbsp;Expiry:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxDateEdit ID="dtContractExpiryDate" ClientInstanceName="dtContractExpiryDate" PopupHorizontalAlign="RightSides" runat="server" Date='<%# Eval("SiteContractExpiry")%>'></dx:ASPxDateEdit>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblSalesPerson" runat="server" Text="Sales Person:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxComboBox ID="cbSalesPerson" DataSourceID="odsSalesPerson" runat="server" PopupHorizontalAlign="RightSides" Width="170px" Height="20px" SelectedIndex='<%# Eval("SalesPersonSortOrder") - 1%>' TextField="SalesPerson" ValueField="Aid"></dx:ASPxComboBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblInitialContractPeriod" runat="server" Text="Initial&nbsp;Contract&nbsp;Period:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxComboBox ID="cbInitialContractPeriod" DataSourceID="odsInitialContractPeriod" PopupHorizontalAlign="RightSides" runat="server" Width="170px" Height="20px"
                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("InitialContractPeriodSortOrder") - 1%>' TextField="ContractPeriodDesc" ValueField="Aid">
                                                                                <Columns>
                                                                                    <dx:ListBoxColumn FieldName="ContractPeriodDesc" Width="130px" />
                                                                                    <dx:ListBoxColumn FieldName="ContractPeriodMonths" Width="100%" />
                                                                                </Columns>
                                                                            </dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblInitialServiceAgreementNo" runat="server" Text="Initial&nbsp;Service&nbsp;Agreement&nbsp;No:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxTextBox ID="txtInitialServiceAgreementNo" runat="server" Width="260px" MaxLength="22" Text='<%# Eval("InitialServiceAgreementNo") %>'></dx:ASPxTextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblContractCeaseDate" runat="server" Text="Contract Cease Date:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxDateEdit ID="dtContractCeaseDate" ClientInstanceName="dtContractCeaseDate" PopupHorizontalAlign="Center" runat="server" Date='<%# Eval("SiteCeaseDate") %>'></dx:ASPxDateEdit>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblContractCeaseReason" runat="server" Text="Contract Cease Reason:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxComboBox ID="cbSiteCeaseReason" ClientInstanceName="cbSiteCeaseReason" PopupHorizontalAlign="RightSides" DataSourceID="odsSiteCeaseReason" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("ContractCeaseReasonsSortOrder") - 1%>' TextField="CeaseReasonDescription" ValueField="Aid"></dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxButton ID="btnViewCeasedReasons" ClientInstanceName="btnViewCeasedReasons" AutoPostBack="false" runat="server" Text="View Ceased Reasons">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                        ExecuteLinkCeaseReasons(hdnSiteCeaseReason.GetText());
                                                                                    }" />
                                                                            </dx:ASPxButton>
                                                                            <div style="display: none">
                                                                                <dx:ASPxTextBox ID="hdnSiteCeaseReason" ClientInstanceName="hdnSiteCeaseReason" runat="server" Text='<%# Eval("SiteCeaseReason")%>'></dx:ASPxTextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblLostBusinessTo" runat="server" Text="Lost Business To:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxComboBox ID="cbLostBusinessTo" ClientInstanceName="cbLostBusinessTo" PopupHorizontalAlign="RightSides" DataSourceID="odsPreviousSuppliers" runat="server" Width="170px" Height="20px" SelectedIndex='<%# Eval("LostBusinessToSortOrder") - 1%>' TextField="PreviousSupplier" ValueField="Aid"></dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxButton ID="btnLostBusinessTo" ClientInstanceName="btnLostBusinessTo" AutoPostBack="false" runat="server" Text="View Lost Business To">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                        ExecuteLinkLostBusinessTo(hdnLostBusinessTo.GetText());
                                                                                    }" />
                                                                            </dx:ASPxButton>
                                                                            <div style="display: none">
                                                                                <dx:ASPxTextBox ID="hdnLostBusinessTo" ClientInstanceName="hdnLostBusinessTo" runat="server" Text='<%# Eval("LostBusinessTo")%>'></dx:ASPxTextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-8">
                                                                            <div style="display: none">
                                                                            </div>
                                                                            <dx:ASPxGridView ID="ResignHistoryGridView" KeyFieldName="ResignHistoryID" DataSourceID="odsSiteResignDetails" runat="server" Theme="SoftOrange"
                                                                                AutoGenerateColumns="False" OnRowUpdating="ResignHistoryGridView_RowUpdating" OnRowInserting="ResignHistoryGridView_RowInserting">
                                                                                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                                                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                                                <Settings ShowPreview="true" />
                                                                                <SettingsPager PageSize="10" />
                                                                                <Columns>
                                                                                    <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="ResignHistoryID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="Cid" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="SiteCId" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataDateColumn FieldName="ReSignDate" VisibleIndex="4" Visible="true"></dx:GridViewDataDateColumn>
                                                                                    <dx:GridViewDataComboBoxColumn FieldName="ReSignPeriod" Caption="Re-Sign Period" VisibleIndex="5">
                                                                                        <PropertiesComboBox DataSourceID="odsInitialContractPeriod" TextField="ContractPeriodDesc" ValueField="Aid" Width="150px">
                                                                                            <Columns>
                                                                                                <dx:ListBoxColumn FieldName="ContractPeriodDesc" Width="80px" />
                                                                                                <dx:ListBoxColumn FieldName="ContractPeriodMonths" Width="80px" />
                                                                                            </Columns>
                                                                                            <ClearButton Visibility="Auto"></ClearButton>
                                                                                        </PropertiesComboBox>
                                                                                    </dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="ServiceAgreementNo" VisibleIndex="6" Visible="true" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataComboBoxColumn FieldName="SalesPerson" Caption="Sales Person" VisibleIndex="7">
                                                                                        <PropertiesComboBox DataSourceID="odsSalesPerson" TextField="SalesPerson" ValueField="Aid" Width="150px" PopupVerticalAlign="Above">
                                                                                            <ClearButton Visibility="Auto"></ClearButton>
                                                                                        </PropertiesComboBox>
                                                                                    </dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataDateColumn FieldName="ContractExpiryDate" Caption="Expiry Date" VisibleIndex="8" Visible="true"></dx:GridViewDataDateColumn>
                                                                                </Columns>
                                                                            </dx:ASPxGridView>
                                                                            <asp:ObjectDataSource ID="odsSiteResignDetails" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblSiteReSignDetails" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllSiteID" TypeName="FMS.Business.DataObjects.tblSiteReSignDetails" UpdateMethod="Update">
                                                                                <SelectParameters>
                                                                                    <asp:ControlParameter ControlID="hdnSiteCid" PropertyName="Text" Name="siteID" Type="Int32"></asp:ControlParameter>
                                                                                </SelectParameters>
                                                                            </asp:ObjectDataSource>
                                                                        </div>
                                                                    </div>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Name="SiteInvoicingDetails" Text="Site Invoicing Details">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <div style="border: 1px solid gray; width: 600px; padding: 5px">
                                                                        <div class="row">
                                                                            <div class="col-md-2">
                                                                                <p style="font-weight: bold; color: black">Postal Address</p>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-2">
                                                                                <dx:ASPxLabel ID="lblAddress1" runat="server" Text="Address&nbsp;Line&nbsp;1:" Width="100px"></dx:ASPxLabel>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <dx:ASPxTextBox ID="txtPostalAddressLine1" ClientInstanceName="txtPostalAddressLine1" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("PostalAddressLine1") %>'></dx:ASPxTextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-2">
                                                                                <dx:ASPxLabel ID="lblAddress2" runat="server" Text="Address&nbsp;Line&nbsp;2:" Width="100px"></dx:ASPxLabel>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <dx:ASPxTextBox ID="txtPostalAddressLine2" ClientInstanceName="txtPostalAddressLine2" runat="server" Width="260px" MaxLength="50" Text='<%# Eval("PostalAddressLine2") %>'></dx:ASPxTextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row ">
                                                                            <div class="col-md-2">
                                                                                <dx:ASPxLabel ID="lblSiteInvoicingSuburb" runat="server" Text="Suburb:" Width="100px"></dx:ASPxLabel>
                                                                            </div>
                                                                            <div class="col-md-7">
                                                                                <div class="container">
                                                                                    <div class="row row-md-margin-top">
                                                                                        <dx:ASPxTextBox ID="txtPostalSuburb" ClientInstanceName="txtPostalSuburb" runat="server" Width="111px" MaxLength="22" Text='<%# Eval("PostalSuburb")%>'></dx:ASPxTextBox>
                                                                                        &nbsp;
                                                                                        <dx:ASPxLabel ID="lblSiteInvoicingState" runat="server" Text="State:"></dx:ASPxLabel>
                                                                                        &nbsp;
                                                                                        <dx:ASPxComboBox ID="cbPostalState" ClientInstanceName="cbPostalState" DataSourceID="odsStates" runat="server" Width="112px" SelectedIndex='<%# Eval("StateSortOrder") - 1%>' Height="20px" TextField="StateDesc" ValueField="Sid"></dx:ASPxComboBox>
                                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                        <dx:ASPxLabel ID="lblSiteInvoicingPostalState" runat="server" Text="P/Code:"></dx:ASPxLabel>
                                                                                        &nbsp;
                                                                                        <dx:ASPxTextBox ID="txtPostalPostCode" ClientInstanceName="txtPostalPostCode" runat="server" Width="50px" MaxLength="22" Text='<%# Eval("PostalPostCode")%>'></dx:ASPxTextBox>
                                                                                        &nbsp;
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row"></div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblInvoiceFrequency" runat="server" Text="Invoice&nbsp;Frequency:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxComboBox ID="cbInvoiceFrequency" ClientInstanceName="cbInvoiceFrequency" DataSourceID="odsInvoiceFrequency" runat="server" Width="170px" Height="20px"
                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("InvoicingFrequencySortOrder") - 1%>' TextField="Frequency" ValueField="iid">
                                                                            </dx:ASPxComboBox>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblInvoiceCommencing" runat="server" Text="Invoice&nbsp;Commencing:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxDateEdit ID="dtInvoiceCommencing" ClientInstanceName="dtInvoiceCommencing" runat="server" Date='<%# Eval("InvoiceCommencing")%>'></dx:ASPxDateEdit>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblSeparateInvoice" runat="server" Text="Separate&nbsp;Invoice:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxCheckBox ID="chkSeparateInvoice" runat="server" Checked='<%# Eval("SeparateInvoice")%>'></dx:ASPxCheckBox>
                                                                            <dx:ASPxLabel ID="lblPurchaseOrderNumber" runat="server" Text="Purchase&nbsp;Order&nbsp;Number:" Width="100px"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxTextBox ID="txtPurchaseOrderNumber" runat="server" Width="200px" MaxLength="50" Text='<%# Eval("PurchaseOrderNumber")%>'></dx:ASPxTextBox>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxLabel ID="lblExcludeFuelLevy" runat="server" Text="Exclude&nbsp;Fuel&nbsp;Levy:" Width="100px"></dx:ASPxLabel>
                                                                            <dx:ASPxCheckBox ID="chkExcludeFuelLevy" runat="server" Checked='<%# Eval("chkSitesExcludeFuelLevy")%>'></dx:ASPxCheckBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row"></div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <p style="font-weight: bold; color: black">Invoicing Months</p>
                                                                        </div>
                                                                    </div>
                                                                    <div style="width: 680px">
                                                                        <div style="float: right">
                                                                            <div class="row">
                                                                                <div class="col-md-2">
                                                                                    <dx:ASPxLabel ID="lblRateIncrease" runat="server" Text="Rate&nbsp;Increase&nbsp;:" Width="100px"></dx:ASPxLabel>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <dx:ASPxComboBox ID="cbRateIncrease" DataSourceID="odsRateIncrease" runat="server" Width="170px" Height="20px"
                                                                                        CallbackPageSize="30" SelectedIndex='<%# Eval("cmbRateIncreaseSortOrder") - 1%>' TextField="RateIncreaseDescription" ValueField="Aid">
                                                                                    </dx:ASPxComboBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-2">
                                                                                    <dx:ASPxButton ID="btnCopyPreviousDetails" ClientInstanceName="btnCopyPreviousDetails" AutoPostBack="false" runat="server" Text="Copy Previous Details">
                                                                                        <ClientSideEvents Click="function(s,e) {
                                                                                                getSiteID(hdnSiteCid.GetText());
                                                                                            }" />
                                                                                    </dx:ASPxButton>

                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-2">
                                                                                    <dx:ASPxButton ID="btnPastePreviousDetails" ClientInstanceName="btnPastePreviousDetails" AutoPostBack="false" runat="server" Text="Paste Previous Details">
                                                                                        <ClientSideEvents Click="function(s,e) {
                                                                                                getSiteInvoicingDetails(hdnStoreCid.GetText());
                                                                                            }" />
                                                                                    </dx:ASPxButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div style="border: 1px solid gray; width: 350px; padding: 5px">
                                                                            <div class="row">
                                                                                <div class="col-md-2">
                                                                                    <dx:ASPxLabel ID="lblInvoiceMonth1" runat="server" Text="Invoice&nbsp;Month&nbsp;1:" Width="100px"></dx:ASPxLabel>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <dx:ASPxComboBox ID="cbInvoiceMonth1" ClientInstanceName="cbInvoiceMonth1" DataSourceID="odsInvoiceMonth" runat="server" Width="170px" Height="20px"
                                                                                        CallbackPageSize="30" SelectedIndex='<%# Eval("InvoiceMonth1") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                    </dx:ASPxComboBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-2">
                                                                                    <dx:ASPxLabel ID="lblInvoiceMonth2" runat="server" Text="Invoice&nbsp;Month&nbsp;2:" Width="100px"></dx:ASPxLabel>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <dx:ASPxComboBox ID="cbInvoiceMonth2" ClientInstanceName="cbInvoiceMonth2" DataSourceID="odsInvoiceMonth" runat="server" Width="170px" Height="20px"
                                                                                        CallbackPageSize="30" SelectedIndex='<%# Eval("InvoiceMonth2") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                    </dx:ASPxComboBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-2">
                                                                                    <dx:ASPxLabel ID="lblInvoiceMonth3" runat="server" Text="Invoice&nbsp;Month&nbsp;3:" Width="100px"></dx:ASPxLabel>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <dx:ASPxComboBox ID="cbInvoiceMonth3" ClientInstanceName="cbInvoiceMonth3" DataSourceID="odsInvoiceMonth" runat="server" Width="170px" Height="20px"
                                                                                        CallbackPageSize="30" SelectedIndex='<%# Eval("InvoiceMonth3") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                    </dx:ASPxComboBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-2">
                                                                                    <dx:ASPxLabel ID="lblInvoiceMonth4" runat="server" Text="Invoice&nbsp;Month&nbsp;4:" Width="100px"></dx:ASPxLabel>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <dx:ASPxComboBox ID="cbInvoiceMonth4" ClientInstanceName="cbInvoiceMonth4" DataSourceID="odsInvoiceMonth" runat="server" Width="170px" Height="20px"
                                                                                        CallbackPageSize="30" SelectedIndex='<%# Eval("InvoiceMonth4") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                    </dx:ASPxComboBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div style="padding: 2px; padding-top: 140px"></div>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Name="SiteServices" Text="Site Services">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <div class="row">
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="General Site Service Comments:" Font-Bold="true" Width="100%"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <dx:ASPxMemo ID="txtGeneralSiteServiceComments" runat="server" class="dxeMemoEditAreaSys" Width="630px" Height="90px" Text='<%# Eval("GeneralSiteServiceComments")%>'></dx:ASPxMemo>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxGridView ID="CustomerServiceGridView" KeyFieldName="CustomerServiceID" Theme="SoftOrange"
                                                                                DataSourceID="odsCustomerService" runat="server" AutoGenerateColumns="False" Width="900px"
                                                                                OnCancelRowEditing="CustomerServiceGridView_CancelRowEditing"
                                                                                OnRowUpdating="CustomerServiceGridView_RowUpdating" OnRowInserting="CustomerServiceGridView_RowInserting">
                                                                                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                                                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                                                <Settings ShowPreview="true" />
                                                                                <SettingsPager PageSize="5" />
                                                                                <Columns>
                                                                                    <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="CustomerServiceID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="ID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="CId" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataComboBoxColumn PropertiesComboBox-DataSourceID="odsServices" FieldName="CSid" PropertiesComboBox-TextField="ServiceDescription" PropertiesComboBox-ValueField="sid" Caption="Services" VisibleIndex="4"></dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataComboBoxColumn PropertiesComboBox-DataSourceID="odsFrequency" FieldName="ServiceFrequencyCode" PropertiesComboBox-TextField="FrequencyDescription" PropertiesComboBox-ValueField="Fid" Caption="Frequency" VisibleIndex="5"></dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="ServiceUnits" VisibleIndex="6" Caption="Service Units"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="ServicePrice" VisibleIndex="7" Caption="Unit Price PA"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="PerAnnumCharge" VisibleIndex="8" Caption="Amount Per Annum"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataComboBoxColumn PropertiesComboBox-DataSourceID="odsServiceRun" FieldName="ServiceRun" PropertiesComboBox-TextField="RunDescription" PropertiesComboBox-ValueField="Rid" Caption="Frequency" VisibleIndex="9"></dx:GridViewDataComboBoxColumn>
                                                                                </Columns>
                                                                                <Templates>
                                                                                    <EditForm>
                                                                                        <div style="width: 850px; height: 200px">
                                                                                            <div class="row">
                                                                                                <div class="col-md-1">
                                                                                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Service" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                </div>
                                                                                                <div style="width: 50px;"></div>
                                                                                                <div class="col-md-1">
                                                                                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Frequency" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                </div>
                                                                                                <div class="col-md-1">
                                                                                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Service Units" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                </div>
                                                                                                <div style="width: 5px;"></div>
                                                                                                <div class="col-md-1">
                                                                                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Unit Price PA" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                </div>
                                                                                                <div style="width: 5px;"></div>
                                                                                                <div class="col-md-1">
                                                                                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Amount Per Annum" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                </div>
                                                                                                <div style="width: 5px;"></div>
                                                                                                <div class="col-md-2">
                                                                                                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Run / Driver" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                </div>
                                                                                                <div class="col-md-1">
                                                                                                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Comments" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="row">
                                                                                                <div class="col-md-10">
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-1">
                                                                                                            <dx:ASPxComboBox ID="cbServices" ClientInstanceName="cbServices" DataSourceID="odsServices" runat="server" Width="140px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServicesSortOrder") - 1%>' TextField="ServiceDescription" ValueField="sid" IncrementalFilteringMode="StartsWith">
                                                                                                                <Columns>
                                                                                                                    <dx:ListBoxColumn FieldName="ServiceDescription" Width="200px" />
                                                                                                                    <dx:ListBoxColumn FieldName="ServiceCode" Width="80px" />
                                                                                                                </Columns>
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="width: 50px;"></div>
                                                                                                        <div class="col-md-1">
                                                                                                            <dx:ASPxComboBox ID="cbFrequency" ClientInstanceName="cbFrequency" DataSourceID="odsFrequency" runat="server" Width="100px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequencySortOrder") - 1%>' TextField="FrequencyDescription" ValueField="Fid">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="width: 10px;"></div>
                                                                                                        <div class="col-md-1">
                                                                                                            <dx:ASPxSpinEdit ID="txtServiceUnits" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("ServiceUnits")%>'></dx:ASPxSpinEdit>
                                                                                                        </div>
                                                                                                        <div style="width: 10px;"></div>
                                                                                                        <div class="col-md-1">
                                                                                                            <dx:ASPxSpinEdit ID="txtServicePrice" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("ServicePrice")%>'></dx:ASPxSpinEdit>
                                                                                                        </div>
                                                                                                        <div style="width: 15px;"></div>
                                                                                                        <div class="col-md-1">
                                                                                                            <dx:ASPxSpinEdit ID="txtPerAnnumCharge" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("PerAnnumCharge")%>'></dx:ASPxSpinEdit>
                                                                                                        </div>
                                                                                                        <div style="width: 15px;"></div>
                                                                                                        <div class="col-md-2">
                                                                                                            <dx:ASPxComboBox ID="cbServiceRun" ClientInstanceName="cbServiceRun" DataSourceID="odsServiceRun" runat="server" Width="100px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceRunSortOrder") - 1%>' TextField="RunDescription" ValueField="Rid">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-4">
                                                                                                            <dx:ASPxLabel ID="lblMonthsForPeriodicRuns" runat="server" Text="Months&nbsp;for&nbsp;periodic&nbsp;runs" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                        </div>
                                                                                                        <div class="col-md-4">
                                                                                                            <dx:ASPxCheckBox ID="chkUnitsHaveMoreThanOneRun" runat="server" Checked='<%# Eval("UnitsHaveMoreThanOneRun")%>'></dx:ASPxCheckBox>
                                                                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Units&nbsp;Have&nbsp;More&nbsp;Than&nbsp;One&nbsp;Run" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                        </div>
                                                                                                        <div class="col-md-1">
                                                                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Sort&nbsp;Code" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="row">
                                                                                                        <div style="padding-left: 12px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency1" ClientInstanceName="cbServiceFrequency1" DataSourceID="odsInvoiceMonth" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency1SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency2" ClientInstanceName="cbServiceFrequency2" DataSourceID="odsServiceFrequency" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency2SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency3" ClientInstanceName="cbServiceFrequency3" DataSourceID="odsServiceFrequency" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency3SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency4" ClientInstanceName="cbServiceFrequency4" DataSourceID="odsServiceFrequency" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency4SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency5" ClientInstanceName="cbServiceFrequency5" DataSourceID="odsServiceFrequency" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency5SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency6" ClientInstanceName="cbServiceFrequency6" DataSourceID="odsServiceFrequency" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency6SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency7" ClientInstanceName="cbServiceFrequency7" DataSourceID="odsServiceFrequency" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency7SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxComboBox ID="cbServiceFrequency8" ClientInstanceName="cbServiceFrequency8" DataSourceID="odsServiceFrequency" runat="server" Width="70px" Height="20px"
                                                                                                                CallbackPageSize="30" SelectedIndex='<%# Eval("ServiceFrequency8SortOrder") - 1%>' TextField="MonthDescription" ValueField="MonthNo">
                                                                                                            </dx:ASPxComboBox>
                                                                                                        </div>
                                                                                                        <div style="padding-left: 1px">
                                                                                                            <dx:ASPxTextBox ID="txtSortCode" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("ServiceSortOrderCode")%>'></dx:ASPxTextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div style="position: absolute; z-index: 1; left: 700px;">
                                                                                                    <dx:ASPxMemo ID="txtServiceComments" runat="server" class="dxeMemoEditAreaSys" Width="180px" Height="110px" Text='<%# Eval("ServiceComments")%>'></dx:ASPxMemo>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div style="text-align: right; padding-right: 10px">
                                                                                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                                                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                                                        </div>
                                                                                    </EditForm>
                                                                                </Templates>
                                                                            </dx:ASPxGridView>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row"></div>
                                                                    <div class="row">
                                                                        <div class="col-md-2"></div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Total Units:" Font-Bold="true" Width="100%"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxTextBox ID="txtTotalUnits" ClientInstanceName="txtTotalUnits" Width="70px" runat="server" Text='<%# Eval("TotalUnits")%>'></dx:ASPxTextBox>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Total&nbsp;Amount:" Font-Bold="true" Width="100%"></dx:ASPxLabel>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxTextBox ID="txtTotalAmount" ClientInstanceName="txtTotalAmount" Width="70px" runat="server" Text='<%# Eval("TotalAmount")%>'></dx:ASPxTextBox>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxButton ID="btnReCalculate" ClientInstanceName="btnReCalculate" AutoPostBack="false" runat="server" Text="Re-Calculate">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                        ReCalculateSiteServices(hdnSiteCid.GetText());
                                                                                    }" />
                                                                            </dx:ASPxButton>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <dx:ASPxButton ID="btnSiteList" ClientInstanceName="btnSiteList" AutoPostBack="false" runat="server" Text="Site List">
                                                                                <ClientSideEvents Click="function(s,e) {
                                                                                        ViewSiteList(hdnSiteCid.GetText());
                                                                                    }" />
                                                                            </dx:ASPxButton>
                                                                        </div>
                                                                    </div>
                                                                    <asp:ObjectDataSource ID="odsCustomerService" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblCustomerServices" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllByCidWithSortOrders" TypeName="FMS.Business.DataObjects.tblCustomerServices" UpdateMethod="Update">
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="hdnSiteCid" PropertyName="Text" Name="cid" Type="Int32"></asp:ControlParameter>
                                                                        </SelectParameters>
                                                                    </asp:ObjectDataSource>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Name="CIRHistory" Text="CIR History">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxGridView ID="CIRHistoryGridView" KeyFieldName="HistoryID" Theme="SoftOrange"
                                                                                DataSourceID="odsCIRHistory" runat="server" AutoGenerateColumns="False" Width="900px"
                                                                                OnRowUpdating="CIRHistoryGridView_RowUpdating" OnRowInserting="CIRHistoryGridView_RowInserting">
                                                                                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                                                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                                                <Settings ShowPreview="true" />
                                                                                <SettingsPager PageSize="10" />
                                                                                <Columns>
                                                                                    <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="HistoryID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="NCId" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="Cid" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataDateColumn FieldName="NCRDate" Caption="CIR Date" VisibleIndex="4" Visible="true"></dx:GridViewDataDateColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="NCRNumber" Caption="Number" VisibleIndex="5" Visible="true"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataComboBoxColumn PropertiesComboBox-DataSourceID="odsReason" FieldName="NCRReason" PropertiesComboBox-TextField="CIRReason" PropertiesComboBox-ValueField="CId" Caption="Reason" VisibleIndex="6"></dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataComboBoxColumn PropertiesComboBox-DataSourceID="odsDrivers" FieldName="Driver" PropertiesComboBox-TextField="DriverName" PropertiesComboBox-ValueField="Did" Caption="Driver" VisibleIndex="7"></dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="NCRRecordedBY" Caption="Recorded By" VisibleIndex="8" Visible="true"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="NCRClosedBy" Caption="Recorded By" VisibleIndex="9" Visible="true"></dx:GridViewDataTextColumn>
                                                                                </Columns>
                                                                                <Templates>
                                                                                    <EditForm>
                                                                                        <div class="row">
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxLabel ID="lblCirDate" runat="server" Text="CIR Date" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                            </div>
                                                                                            <div style="width: 75px;"></div>
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxLabel ID="lblNumber" runat="server" Text="Number" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                            </div>
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxLabel ID="lblReason" runat="server" Text="Reason" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                            </div>
                                                                                            <div style="width: 80px;"></div>
                                                                                            <div class="col-md-2">
                                                                                                <dx:ASPxLabel ID="lblDriver" runat="server" Text="Driver" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                            </div>
                                                                                            <div style="width: 10px;"></div>
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxLabel ID="lblRecordedBy" runat="server" Text="Recorded By" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                            </div>
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxLabel ID="lblClosedBy" runat="server" Text="Closed By" Font-Bold="true" Width="100px"></dx:ASPxLabel>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-2">
                                                                                                <dx:ASPxDateEdit ID="dtNCRDate" PopupVerticalAlign="Below" ClientInstanceName="dtNCRDate" runat="server" Date='<%# Eval("NCRDate") %>'></dx:ASPxDateEdit>
                                                                                            </div>
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxSpinEdit ID="txtNCRNumber" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("NCRNumber")%>'></dx:ASPxSpinEdit>
                                                                                            </div>
                                                                                            <div class="col-md-2">
                                                                                                <dx:ASPxComboBox ID="cbReason" ClientInstanceName="cbReason" DataSourceID="odsReason" runat="server" Width="180px" Height="20px"
                                                                                                    CallbackPageSize="30" SelectedIndex='<%# Eval("NCRReasonSortOrder") - 1%>' TextField="CIRReason" ValueField="CId">
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                            <div style="width: 5px;"></div>
                                                                                            <div class="col-md-2">
                                                                                                <dx:ASPxComboBox ID="cbDrivers" ClientInstanceName="cbDrivers" DataSourceID="odsDrivers" runat="server" Width="180px" Height="20px"
                                                                                                    CallbackPageSize="30" SelectedIndex='<%# Eval("DriverSortOrder") - 1%>' TextField="DriverName" ValueField="Did">
                                                                                                </dx:ASPxComboBox>
                                                                                            </div>
                                                                                            <div style="width: 5px;"></div>
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxTextBox ID="txtNCRRecordedBY" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("NCRRecordedBY")%>'></dx:ASPxTextBox>
                                                                                            </div>
                                                                                            <div style="width: 5px;"></div>
                                                                                            <div class="col-md-1">
                                                                                                <dx:ASPxTextBox ID="txtNCRClosedBy" runat="server" Width="100px" MaxLength="50" Text='<%# Eval("NCRClosedBy")%>'></dx:ASPxTextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-12">
                                                                                                <dx:ASPxMemo ID="txtNCRDescription" runat="server" class="dxeMemoEditAreaSys" Width="870px" Height="90px" Text='<%# Eval("NCRDescription")%>'></dx:ASPxMemo>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div style="padding: 2px; padding-top: 140px"></div>
                                                                                        <div style="text-align: right; padding-right: 10px">
                                                                                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                                                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                                                        </div>
                                                                                    </EditForm>
                                                                                </Templates>
                                                                            </dx:ASPxGridView>
                                                                        </div>
                                                                    </div>
                                                                    <asp:ObjectDataSource ID="odsCIRHistory" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblCIRHistory" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAllByCIDWithSortOrder" TypeName="FMS.Business.DataObjects.tblCIRHistory" UpdateMethod="Update">
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="hdnSiteCid" PropertyName="Text" Name="cid" Type="Int16"></asp:ControlParameter>
                                                                        </SelectParameters>
                                                                    </asp:ObjectDataSource>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Name="Comments" Text="Comments">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <div class="row">
                                                                        <div class="col-md-2">
                                                                            <dx:ASPxGridView ID="SiteCommentsGridView" KeyFieldName="CommentsID"
                                                                                DataSourceID="odsSiteComments" runat="server" Theme="SoftOrange" AutoGenerateColumns="False" Width="900px"
                                                                                OnRowUpdating="SiteCommentsGridView_RowUpdating" OnRowInserting="SiteCommentsGridView_RowInserting">
                                                                                <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                                                <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                                                <Settings ShowPreview="true" />
                                                                                <SettingsPager PageSize="10" />
                                                                                <Columns>
                                                                                    <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="CommentsID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="Aid" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="Cid" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataDateColumn FieldName="CommentDate" VisibleIndex="5"></dx:GridViewDataDateColumn>
                                                                                    <dx:GridViewDataTextColumn FieldName="Comments" VisibleIndex="6"></dx:GridViewDataTextColumn>
                                                                                </Columns>
                                                                                <Templates>
                                                                                    <EditForm>
                                                                                        <div class="row">
                                                                                            <div class="col-md-2">Date</div>
                                                                                            <div class="col-md-1">Comments</div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-md-2">
                                                                                                <dx:ASPxDateEdit ID="dtCommentDate" ClientInstanceName="dtNCRDate" runat="server" Date='<%# Eval("CommentDate")%>'></dx:ASPxDateEdit>
                                                                                            </div>
                                                                                            <div class="col-md-3">
                                                                                                <dx:ASPxMemo ID="txtSiteComments" runat="server" class="dxeMemoEditAreaSys" Width="670px" Height="90px" Text='<%# Eval("Comments")%>'></dx:ASPxMemo>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div style="text-align: right; padding-right: 10px; padding-top: 10px">
                                                                                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                                                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                                                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                                                        </div>
                                                                                    </EditForm>
                                                                                </Templates>
                                                                            </dx:ASPxGridView>
                                                                            <asp:ObjectDataSource ID="odsSiteComments" runat="server" SelectMethod="GetAllByCID" TypeName="FMS.Business.DataObjects.tblSiteComments" DataObjectTypeName="FMS.Business.DataObjects.tblSiteComments" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update">
                                                                                <SelectParameters>
                                                                                    <asp:ControlParameter ControlID="hdnSiteCid" PropertyName="Text" Name="cid" Type="Int16"></asp:ControlParameter>
                                                                                </SelectParameters>
                                                                            </asp:ObjectDataSource>
                                                                        </div>
                                                                    </div>
                                                                    <div style="padding: 2px; padding-top: 140px"></div>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                    </TabPages>
                                                </dx:ASPxPageControl>
                                            </div>
                                            <div style="text-align: right; padding: 2px">
                                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                                            </div>
                                        </EditForm>
                                    </Templates>
                                </dx:ASPxGridView>
                                <dx:ASPxPopupControl ID="viewPopup" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="viewPopup"
                                    AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="300px" Height="300px">
                                    <ContentCollection>
                                        <dx:PopupControlContentControl runat="server">
                                            <dx:ASPxPanel ID="Panel2" runat="server" DefaultButton="btOK">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <iframe id="ifrPopup" name="ifrPopup" src="#"></iframe>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                        </dx:PopupControlContentControl>
                                    </ContentCollection>
                                    <ContentStyle>
                                        <Paddings PaddingBottom="5px" />
                                    </ContentStyle>
                                </dx:ASPxPopupControl>
                                <asp:ObjectDataSource ID="odsDrivers" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblDrivers"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsReason" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblCIRReason"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsServiceFrequency" runat="server" SelectMethod="GetAllByMonthDescription" TypeName="FMS.Business.DataObjects.tblMonths"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsServiceRun" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRuns"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsFrequency" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblServiceFrequency"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsServices" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblServices"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsRateIncrease" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRateIncreaseReference"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsInvoiceMonth" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblMonths"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsInvoiceFrequency" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblInvoicingFrequency"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsSiteCeaseReason" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblContractCeaseReasons"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsInitialContractPeriod" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblContractPeriods"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsSalesPerson" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblSalesPersons"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsPreviousSuppliers" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblPreviousSuppliers"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsIndustryGroups" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblIndustryGroups"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsCustomers" runat="server" SelectMethod="GetAllWithZoneSortOrder" TypeName="FMS.Business.DataObjects.tblCustomers"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsZones" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tbZone"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsStates" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblStates"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsSiteDetails" runat="server" SelectMethod="GetAllWithZoneSortOrder" TypeName="FMS.Business.DataObjects.tblSites" DataObjectTypeName="FMS.Business.DataObjects.tblSites" DeleteMethod="Delete" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsSites" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblSites"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsMultiDocs" runat="server" SelectMethod="GetAllByClientCID" TypeName="FMS.Business.DataObjects.fleetDocument" DataObjectTypeName="FMS.Business.DataObjects.FleetDocument" UpdateMethod="Update" DeleteMethod="Delete" InsertMethod="Create">
                                    <SelectParameters>
                                        <asp:SessionParameter SessionField="ClientID" Name="CID" Type="Int32"></asp:SessionParameter>
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="RunCompletion" Text="Run Assignments">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <dx:ASPxGridView ID="GetRunsForAssignmentGridView" runat="server" KeyFieldName="UniqueID" DataSourceID="odsGetRunsForAssignment"
                                    Width="750px" Theme="SoftOrange" AutoGenerateColumns="False" OnRowValidating="GetRunsForAssignmentGridView_RowValidating">
                                    <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                    <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                    <SettingsPopup>
                                        <EditForm Modal="true"
                                            VerticalAlign="WindowCenter"
                                            HorizontalAlign="WindowCenter" Width="400px" Height="120px" />
                                    </SettingsPopup>
                                    <Columns>
                                        <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="UniqueID" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Rid" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="RunNUmber" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="RunDescription" VisibleIndex="3" ReadOnly="true" SortOrder="Ascending"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn FieldName="DriverId" Caption="Driver Name" VisibleIndex="4" SortIndex="0" SortOrder="Ascending">
                                            <PropertiesComboBox DataSourceID="odsDriverList" TextField="DriverName" ValueField="Did">
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataDateColumn FieldName="DateOfRun" VisibleIndex="5" Visible="true">
<PropertiesDateEdit>
<TimeSectionProperties>
<TimeEditProperties>
<ClearButton Visibility="Auto"></ClearButton>
</TimeEditProperties>
</TimeSectionProperties>

<ClearButton Visibility="Auto"></ClearButton>
</PropertiesDateEdit>
                                        </dx:GridViewDataDateColumn>
                                    </Columns>
                                    <Settings ShowPreview="true" />
                                    <SettingsPager PageSize="10" />
                                </dx:ASPxGridView>
                                <asp:ObjectDataSource ID="odsGetRunsForAssignment" runat="server" DataObjectTypeName="FMS.Business.DataObjects.usp_GetRunsForAssignment" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.usp_GetRunsForAssignment" UpdateMethod="Update"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsTblDrivers" runat="server" SelectMethod="GetAllDrivers" TypeName="FMS.Business.DataObjects.usp_GetAllDrivers"></asp:ObjectDataSource>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="RunClient" Text="Run Site">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <dx:ASPxGridView ID="RunSiteGridView" runat="server" DataSourceID="odsRunSite" KeyFieldName="RunSiteID" Width="550px"
                                    Theme="SoftOrange" AutoGenerateColumns="False" OnRowValidating="RunSiteGridView_RowValidating"
                                    ClientInstanceName="cltRunSiteGridView">
                                    <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                    <SettingsEditing Mode="PopupEditForm" />
                                    <SettingsPopup>
                                        <EditForm Modal="true"
                                            VerticalAlign="WindowCenter"
                                            HorizontalAlign="WindowCenter" Width="400px" Height="100px" />
                                    </SettingsPopup>
                                    <ClientSideEvents CustomButtonClick="function(s, e)
                                    {
                                        OnCustomButtonClick(s, e, 'RunSite');
                                    }" />
                                    <Columns>
                                        <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnDelete_RunSite" Text="Delete" />
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="RunSiteID" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn FieldName="Rid" VisibleIndex="1" Caption="Run">
                                            <PropertiesComboBox DataSourceID="odsTblRuns" TextField="RunDescription" ValueField="Rid">
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataComboBoxColumn FieldName="Cid" VisibleIndex="2" Caption="Site">
                                            <PropertiesComboBox DataSourceID="odsSites" TextField="SiteName" ValueField="Cid">
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowPreview="true" />
                                    <SettingsPager PageSize="10" />
                                </dx:ASPxGridView>
                                <asp:ObjectDataSource ID="odsRunSite" runat="server" DataObjectTypeName="FMS.Business.DataObjects.tblRunSite" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRunSite" UpdateMethod="Update"></asp:ObjectDataSource>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="RunClient" Text="Run Completion">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <dx:ASPxGridView ID="gvRunCompletion" runat="server" DataSourceID="odsRunCompletion" KeyFieldName="RunCompletionID" Width="550px"
                                    Theme="SoftOrange" AutoGenerateColumns="False">
                                    <Settings ShowFilterRow="True"></Settings>
                                    <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                    <SettingsEditing Mode="PopupEditForm" />
                                    <SettingsPopup>
                                        <EditForm Modal="true"
                                            VerticalAlign="WindowCenter"
                                            HorizontalAlign="WindowCenter" Width="400px" Height="100px" />
                                    </SettingsPopup>
                                    <Columns>
                                        <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="false" ShowNewButtonInHeader="false" ShowDeleteButton="false"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="RunCompletionID" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="RunID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="DriverID" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn FieldName="RunDate" Caption="Completion Date" VisibleIndex="4" Visible="true">
<PropertiesDateEdit>
<TimeSectionProperties>
<TimeEditProperties>
<ClearButton Visibility="Auto"></ClearButton>
</TimeEditProperties>
</TimeSectionProperties>

<ClearButton Visibility="Auto"></ClearButton>
</PropertiesDateEdit>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn FieldName="Notes" VisibleIndex="5" Visible="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="RunNumber" VisibleIndex="6" Visible="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="RunDescription" VisibleIndex="7" Visible="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="DID" VisibleIndex="8" Visible="true"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="DriverName" VisibleIndex="9" Visible="true"></dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                                <asp:ObjectDataSource ID="odsRunCompletion" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.usp_GetRunCompletionForLogReport"></asp:ObjectDataSource>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </div>
        <dx:ASPxPopupControl ID="DeleteDialog_SiteDetail" runat="server" Text="Are you sure you want to delete this?"
            ClientInstanceName="popupDelete_Run" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <dx:ASPxButton ID="yesButton_SiteDetail" runat="server" Text="Yes" AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e)
                            {
                                OnClickYes(s, e, 'RunTab');
                            }" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="noButton_SiteDetail" runat="server" Text="No" AutoPostBack="false">
                        <ClientSideEvents Click="function(){ popupDelete_Run.Hide(); }" />
                    </dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="DeleteDialog_RunDoc" runat="server" Text="Are you sure you want to delete this?"
            ClientInstanceName="popupDelete_RunDoc" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <dx:ASPxButton ID="yesButton_RunDoc" runat="server" Text="Yes" AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e)
                            {
                                OnClickYes(s, e, 'RunDoc');
                            }" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="noButton_RunDoc" runat="server" Text="No" AutoPostBack="false">
                        <ClientSideEvents Click="function(){ popupDelete_RunDoc.Hide(); }" />
                    </dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="DeleteDialog_RunSite" runat="server" Text="Are you sure you want to delete this?"
            ClientInstanceName="popupDelete_RunSite" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <br />
                    <dx:ASPxButton ID="yesButton_RunSite" runat="server" Text="Yes" AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e)
                            {
                                OnClickYes(s, e, 'RunSite');
                            }" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="noButton_RunSite" runat="server" Text="No" AutoPostBack="false">
                        <ClientSideEvents Click="function(){ popupDelete_RunSite.Hide(); }" />
                    </dx:ASPxButton>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </form>
</body>
</html>
