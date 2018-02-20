<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ServiceRunManagement.aspx.vb" Inherits="FMS.WEB.ServiceRunManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Service Run Management</title>
    <script src="../Content/javascript/jquery-1.10.2.min.js"></script>
    <link href="../Content/grid/bootstrap.css" rel="stylesheet" />
    <link href="../Content/grid/grid.css" rel="stylesheet" />
    <style>
        .ServiceRunHeader td {
            border: none !important;
            width: 50%;
            height: 100px;
            text-align: center;
            vertical-align: middle;
            -webkit-transform: rotate(320deg);
            -moz-transform: rotate(320deg);
            -o-transform: rotate(320deg);
            writing-mode: tb-rl;
        }
    </style>

    <script type="text/javascript">
        function ShowPopup(fieldname) {

            //var FocusedCell;
            //FocusedCell = document.getElementById(htmlId);
            //FocusedCell.style.color = 'Red';
            //FocusedCell.style.border = '1px solid Red';

            var intDateDiff = GetDateDiff(clientdteStart.GetDate(), clientdteEnd.GetDate());
            
            setCookie('SRStartDate', clientdteStart.GetDate(), 1)
            setCookie('SREndDate', clientdteEnd.GetDate(), 1)
            setCookie('SRNumDays', intDateDiff, 1)

            var DriverID = 0

            var Tech = fieldname.indexOf('Tech_');
            if (Tech > -1) {
                var ndxS = fieldname.indexOf('_');
                var ndxE = fieldname.indexOf('-');
                if (ndxS > -1) {
                    DriverID = fieldname.substring(ndxS + 1, ndxE);
                    setCookie('TechID', DriverID, 1)
                    setCookie('DriverID', DriverID, 1)
                    setCookie('DriverType', "Tech", 1)
                }
            }


            var Driver = fieldname.indexOf('Driver_');
            if (Driver > -1) {
                var ndxS = fieldname.indexOf('_');
                var ndxE = fieldname.indexOf('-');
                if (ndxS > -1) {
                    DriverID = fieldname.substring(ndxS + 1, ndxE);
                    setCookie('DriverID', DriverID, 1)
                    setCookie('DriverType', "DriverOnly", 1)
                }
            }



        }

        //Create Cookie
        function setCookie(cname, cvalue, exdays) {
            var d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            var expires = "expires=" + d.toGMTString();
            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        }

        //Get Cookie value
        function getCookie(cname) {
            var name = cname + "=";
            var decodedCookie = decodeURIComponent(document.cookie);
            var ca = decodedCookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }

        //Check cookie if exists (return true or false)
        function checkCookie(cookiename) {
            var valCookie = getCookie(cookiename);
            if (valCookie != "") {
                return true;
            }
            else {
                return false;
            }
        }

        //Get datediff in days
        function GetDateDiff(d1, d2) {
            var t2 = d2.getTime();
            var t1 = d1.getTime();

            return parseInt((t2 - t1) / (24 * 3600 * 1000));
        };

        function ContextMenuServiceRun(s, e) {
            var cellInfo = GetCellInfo(s, ASPxClientUtils.GetEventSource(e.htmlEvent));
            if (!cellInfo) return;
            var args = cellInfo.split("_");
            var visibleIndex = parseInt(args[0]);
            var fieldName = s.cpDataColumnMap[parseInt(args[1])];
            var rundate = args[2];

            var TechExist = fieldName.indexOf("Tech");
            var DriverExist = fieldName.indexOf("Driver");
            var TechID = ""
            var DriverID = ""

            var sdateexist = checkCookie('SRStartDate');

            if (TechExist > -1) {
                var ndxS = fieldName.indexOf("_") + 1;
                var ndxE = fieldName.indexOf("-");
                TechID = fieldName.substring(ndxS, ndxE);
                DriverID = TechID
                setCookie('DriverType', "Tech", 1)
            }

            if (DriverExist > -1) {
                var ndx = fieldName.indexOf("_") + 1;
                DriverID = fieldName.substring(ndx, fieldName.length);
                setCookie('DriverType', "DriverOnly", 1)
            }

            var src = ASPxClientUtils.GetEventSource(e.htmlEvent);
            var srcVal = src.innerText;

            var ndx = srcVal.indexOf(" ") + 1;
            var RunNumber = srcVal.substring(ndx, srcVal.length);
            setCookie('RunNumber', RunNumber, 1)
            setCookie('DriverID', DriverID, 1)

            //Get selected date
            var sdate = clientdteStart.GetDate();
            var edate = clientdteEnd.GetDate();
            var intDateDiff = GetDateDiff(clientdteStart.GetDate(), clientdteEnd.GetDate());
            var numdays = intDateDiff;
            var TranDate = '';
            var strEndDate = '';

            for (i = 0; i < numdays - 1; i++) {
                var d = new Date(sdate);
                d.setDate(d.getDate() + i);
                tmpdate = formatDate(d);    //

                if (tmpdate == rundate) {
                    TranDate = formatDateTommddyyyy(d);
                    break;
                }
            }

            if (TranDate.length > 0) {
                var d = new Date(TranDate);
                tmpdate = formatDateTddmmyyyy(d);
                setCookie('RepDate', tmpdate, 1)

            }

            //PageMethods.IsFleetRunCompleted(TranDate, DriverID, RunNumber, OnSuccessCompleted)
            PageMethods.IsFleetRunCompleted(TranDate, fieldName, RunNumber, OnSuccessCompleted)

            clientpuCompleteRun.Show();

            //Show pop up only on drivers
            //var driverType = getCookie('DriverType');

            //if (driverType == "DriverOnly") {
            //    clientpuCompleteRun.Show();
            //}

        }
        //Populate UnAssigned Runs ComboBox
        function OnSuccessCompleted(response) {
            if (response.length > 0) {
                clientcbxCompleteRun.SetChecked(true);
                for (var i in response) {
                    var rundate = response[i].RunDate;
                    var notes = response[i].Notes;
                    var driverid = response[i].DriverID;

                    clientdteCompleted.SetDate(rundate);
                    clientcboDriverCompleted.SetValue(driverid);
                    clienttxtCompletedNotes.SetText(notes);
                }

            }
            else {

                clientcbxCompleteRun.SetChecked(false);

                clientdteCompleted.SetDate(null);
                clientcboDriverCompleted.SetValue(null);
                clienttxtCompletedNotes.SetText(null);
            }

        }

        function GetCellInfo(grid, element) {
            var gridMainElement = grid.GetMainElement();
            while (element && element !== gridMainElement && element.tagName !== "BODY") {
                var cellInfo = element.getAttribute("data-CI");
                if (cellInfo)
                    return cellInfo;
                element = element.parentNode;
            }
        }

        // function is called on changing focused row 
        function OnGridFocusedRowChanged() {
            clientgvServiceRun.GetRowValues(clientgvServiceRun.GetFocusedRowIndex(), 'RunDate', OnGetRowValues);
        }

        // Value contains the "EmployeeID" field value returned from the server, not the list of values 
        function OnGetRowValues(Value) {

            if (Value != null) {
                var TranDate = '';
                var strEndDate = '';

                var sdateexist = checkCookie('SRStartDate');

                if (sdateexist == true) {
                    var sdate = getCookie('SRStartDate');
                    var edate = getCookie('SREndDate');
                    var numdays = getCookie('SRNumDays');

                    for (i = 0; i < numdays - 1; i++) {
                        var d = new Date(sdate);
                        d.setDate(d.getDate() + i);
                        tmpdate = formatDate(d);    //

                        if (tmpdate == Value) {
                            TranDate = formatDateTommddyyyy(d);
                            break;
                        }
                    }

                    if (TranDate.length > 0) {
                        var d = new Date(TranDate);
                        tmpdate = formatDateTddmmyyyy(d);
                        setCookie('RepDate', tmpdate, 1)

                        //PageMethods.GetUnAssignedRuns(TranDate, OnSuccess);
                        showLoadingProcess('Loading Run Definitions. Please wait...')
                        PageMethods.GetUnAssignedRuns(TranDate, OnSuccess);

                    }

                }

                clientpuUnassignedRun.Show();

                //Show pop up only on drivers
                //var driverType = getCookie('DriverType');

                //if (driverType == "DriverOnly") {
                //    clientpuUnassignedRun.Show();
                //}

            }
        }

        //Populate UnAssigned Runs ComboBox
        function OnSuccess(response) {
            clientcboRun.ClearItems();
            clientcboRun.AddItem("--None--", 0);
            for (var i in response) {
                clientcboRun.AddItem(response[i].RunDescription, response[i].Rid);
            }

            HideLoadingProcess();
        }

        function UnAssignedRun_OnSelectedIndexChanged(s, e) {
            var id = s.GetSelectedItem().value;
        }

        //Get date in mmddyyyy format
        function formatDateTommddyyyy(date) {
            var strDay = '';
            var strMonth = '';
            var day = date.getDate();
            var month = date.getMonth() + 1;
            var year = date.getFullYear();

            if (day.toString().length < 2) {
                strDay = '0' + day.toString();
            }
            else {
                strDay = day.toString();
            }

            if (month.toString().length < 2) {
                strMonth = '0' + month.toString();
            }
            else {
                strMonth = month.toString();
            }

            return strMonth + '/' + strDay + '/' + year;
        }

        //Get date in ddmmyyyy format
        function formatDateTddmmyyyy(date) {
            var strDay = '';
            var strMonth = '';
            var day = date.getDate();
            var month = date.getMonth() + 1;
            var year = date.getFullYear();

            if (day.toString().length < 2) {
                strDay = '0' + day.toString();
            }
            else {
                strDay = day.toString();
            }

            if (month.toString().length < 2) {
                strMonth = '0' + month.toString();
            }
            else {
                strMonth = month.toString();
            }

            return strDay + '/' + strMonth + '/' + year;
        }

        //Get date in dd mmm format
        function formatDate(date) {
            var monthNames = [
                "Jan", "Feb", "Mar",
                "Apr", "May", "Jun", "Jul",
                "Aug", "Sep", "Oct",
                "Nov", "Dec"
            ];

            var strDay = '';
            var day = date.getDate();
            var monthIndex = date.getMonth();
            var year = date.getFullYear();

            if (day.toString().length < 2) {
                strDay = '0' + day.toString();
            }
            else {
                strDay = day.toString();
            }

            return strDay + ' ' + monthNames[monthIndex];
        }

        //Call Load Grid
        function CallLoadGrid(e) {
            alert('test');
            $.ajax({
                type: "POST",
                url: "ServiceRunManagement.aspx/LoadGrid",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",

            });
        }


        function showLoadingProcess(msg) {
            LoadingPanel.SetText(msg);
            LoadingPanel.Show();
            //lpProcess.ShowInElementByID(cltbtnProcess.name);
        }

        function HideLoadingProcess() {
            LoadingPanel.Hide();
        }

    </script>


</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <dx:ASPxPageControl ID="carTabPage" Width="100%" runat="server"
                        CssClass="dxtcFixed" ActiveTabIndex="0" EnableHierarchyRecreation="True">
                        <TabPages>
                            <dx:TabPage Text="Service Run">
                                <ContentCollection>
                                    <dx:ContentControl ID="ccServRun" runat="server">
                                        <table>
                                            <tr>
                                                <td style="width: 160px">
                                                    <dx:ASPxDateEdit ID="dteStart" runat="server" NullText="Start Date"
                                                        Width="150px"
                                                        ClientInstanceName="clientdteStart" OnInit="dteStart_Init"
                                                        AutoPostBack="false">
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties>
                                                                <ClearButton Visibility="Auto"></ClearButton>
                                                            </TimeEditProperties>
                                                        </TimeSectionProperties>
                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                    </dx:ASPxDateEdit>
                                                </td>
                                                <td style="width: 160px">
                                                    <dx:ASPxDateEdit ID="dteEnd" runat="server" NullText="End Date"
                                                        Width="150px"
                                                        ClientInstanceName="clientdteEnd" OnInit="dteEnd_Init"
                                                        AutoPostBack="false">
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties>
                                                                <ClearButton Visibility="Auto"></ClearButton>
                                                            </TimeEditProperties>
                                                        </TimeSectionProperties>
                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                    </dx:ASPxDateEdit>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <dx:ASPxGridView ID="gvServiceRun" runat="server" ClientInstanceName="clientgvServiceRun"
                                            OnHtmlDataCellPrepared="gvServiceRun_HtmlDataCellPrepared"
                                            OnCustomJSProperties="gvServiceRun_CustomJSProperties"
                                            cell
                                            EnableTheming="True" Theme="SoftOrange"
                                            KeyFieldName="ID">
                                            <SettingsBehavior AllowFocusedRow="true"></SettingsBehavior>
                                            <Styles>
                                                <Header CssClass="ServiceRunHeader" />
                                                <FocusedRow BackColor="Transparent" ForeColor="Black"></FocusedRow>
                                            </Styles>
                                            <ClientSideEvents ContextMenu=" function(s, e) { 
                                        if (e.objectType == 'row') {
                                            ContextMenuServiceRun(s,e);
                                        }
                                    } " />

                                            <ClientSideEvents FocusedRowChanged="OnGridFocusedRowChanged" />

                                        </dx:ASPxGridView>

                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>

                            <%-- Start Client Assignment --%>
                            <dx:TabPage Name="Run" Text="Run">
                                <ContentCollection>
                                    <dx:ContentControl runat="server">
                                        <dx:ASPxGridView ID="RunGridView" runat="server" DataSourceID="odsTblRuns" KeyFieldName="Rid" Width="550px" Theme="SoftOrange" AutoGenerateColumns="False"
                                            OnRowValidating="RunGridView_RowValidating">
                                            <SettingsDetail ShowDetailRow="true" />
                                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="3" />
                                            <SettingsPopup>
                                                <EditForm Modal="true"
                                                    VerticalAlign="WindowCenter"
                                                    HorizontalAlign="WindowCenter" Width="700px" Height="150px" />
                                            </SettingsPopup>
                                            <Templates>
                                                <DetailRow>
                                                    <dx:ASPxGridView ID="RunDocGridView" runat="server" ClientInstanceName="RunDocGridView" DataSourceID="odsRunMultiDocs" KeyFieldName="DocumentID" Width="550px" Theme="SoftOrange"
                                                        AutoGenerateColumns="False" EditFormLayoutProperties-SettingsItems-VerticalAlign="Top" OnBeforePerformDataSelect="RunGridView_BeforePerformDataSelect">
                                                        <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                        <SettingsPopup>
                                                            <EditForm Modal="true"
                                                                VerticalAlign="WindowCenter"
                                                                HorizontalAlign="WindowCenter" Width="400px" Height="250px" />
                                                        </SettingsPopup>
                                                        <Columns>
                                                            <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn FieldName="DocumentID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="RunID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2" Visible="true"></dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataBinaryImageColumn FieldName="PhotoBinary">
                                                                <PropertiesBinaryImage ImageHeight="170px" ImageWidth="160px">
                                                                    <EditingSettings Enabled="true" UploadSettings-UploadValidationSettings-MaxFileSize="4194304" />
                                                                </PropertiesBinaryImage>
                                                            </dx:GridViewDataBinaryImageColumn>
                                                        </Columns>
                                                        <Settings ShowPreview="true" />
                                                        <SettingsPager PageSize="10" />
                                                    </dx:ASPxGridView>
                                                </DetailRow>
                                            </Templates>
                                            <Columns>
                                                <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn FieldName="RunID" VisibleIndex="0" Visible="false"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ApplicationID" VisibleIndex="1" PropertiesTextEdit-ClientInstanceName="RunNumber" Visible="false" PropertiesTextEdit-MaxLength="10"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Rid" VisibleIndex="2" PropertiesTextEdit-ClientInstanceName="RunName" Visible="false" PropertiesTextEdit-MaxLength="10"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="RunNUmber" VisibleIndex="3"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="RunDescription" VisibleIndex="4" SortOrder="Ascending"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataComboBoxColumn FieldName="RunDriver" Caption="Run Driver" VisibleIndex="2" SortIndex="0" SortOrder="Ascending">
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
                                            </Columns>
                                            <Settings ShowPreview="true" />
                                            <SettingsPager PageSize="10" />
                                        </dx:ASPxGridView>
                                        <asp:ObjectDataSource ID="odsTblRuns" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRuns" DataObjectTypeName="FMS.Business.DataObjects.tblRuns" DeleteMethod="DeleteRun" InsertMethod="Create" UpdateMethod="Update"></asp:ObjectDataSource>
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
                                        <dx:ASPxGridView ID="ClientGridView" runat="server" DataSourceID="odsSites" KeyFieldName="Cid" Width="650px" Theme="SoftOrange" AutoGenerateColumns="False">
                                            <SettingsDetail ShowDetailRow="true" />
                                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                            <Templates>
                                                <DetailRow>
                                                    <dx:ASPxGridView ID="DocGridView" runat="server" ClientInstanceName="DocGridView" DataSourceID="odsMultiDocs" KeyFieldName="DocumentID" Width="550px" Theme="SoftOrange"
                                                        AutoGenerateColumns="False" EditFormLayoutProperties-SettingsItems-VerticalAlign="Top" OnBeforePerformDataSelect="ClientGridView_BeforePerformDataSelect">
                                                        <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                                        <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                        <SettingsPopup>
                                                            <EditForm Modal="true"
                                                                VerticalAlign="WindowCenter"
                                                                HorizontalAlign="WindowCenter" Width="400px" Height="250px" />
                                                        </SettingsPopup>
                                                        <Columns>
                                                            <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn FieldName="DocumentID" VisibleIndex="1" Visible="false"></dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="ClientID" VisibleIndex="2" Visible="false"></dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2" Visible="true"></dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataBinaryImageColumn FieldName="PhotoBinary">
                                                                <PropertiesBinaryImage ImageHeight="170px" ImageWidth="160px">
                                                                    <EditingSettings Enabled="true" UploadSettings-UploadValidationSettings-MaxFileSize="4194304" />
                                                                </PropertiesBinaryImage>
                                                            </dx:GridViewDataBinaryImageColumn>
                                                        </Columns>
                                                        <Settings ShowPreview="true" />
                                                        <SettingsPager PageSize="10" />
                                                    </dx:ASPxGridView>
                                                </DetailRow>
                                            </Templates>
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="ApplicationId" VisibleIndex="0" PropertiesTextEdit-ClientInstanceName="CustomerContactName" Visible="false" PropertiesTextEdit-MaxLength="50"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SiteID" VisibleIndex="1" PropertiesTextEdit-ClientInstanceName="CustomerPhone" Visible="false" PropertiesTextEdit-MaxLength="22"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Cid" VisibleIndex="2" PropertiesTextEdit-ClientInstanceName="CustomerMobile" Visible="true" PropertiesTextEdit-MaxLength="22"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SiteName" VisibleIndex="3" PropertiesTextEdit-ClientInstanceName="CustomerFax" Visible="true" PropertiesTextEdit-MaxLength="22"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Customer" VisibleIndex="4" PropertiesTextEdit-ClientInstanceName="CustomerComments" Visible="true"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CustomerSortOrder" VisibleIndex="5"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AddressLine1" VisibleIndex="6"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AddressLine2" VisibleIndex="7"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AddressLine3" VisibleIndex="8"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="AddressLine4" VisibleIndex="9"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Suburb" VisibleIndex="10"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="State" VisibleIndex="11"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="StateSortOrder" VisibleIndex="12"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PostCode" VisibleIndex="13"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PhoneNo" VisibleIndex="14"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="FaxNo" VisibleIndex="15"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SiteContactName" VisibleIndex="16"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SiteContactPhone" VisibleIndex="17"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SiteContactFax" VisibleIndex="18"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SiteContactMobile" VisibleIndex="19"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SiteContactEmail" VisibleIndex="20"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PostalAddressLine1" VisibleIndex="21"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PostalAddressLine2" VisibleIndex="22"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PostalSuburb" VisibleIndex="23"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PostalState" VisibleIndex="24"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PostalPostCode" VisibleIndex="25"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataDateColumn FieldName="SiteStartDate" VisibleIndex="26">
                                                    <PropertiesDateEdit>
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties>
                                                                <ClearButton Visibility="Auto"></ClearButton>
                                                            </TimeEditProperties>
                                                        </TimeSectionProperties>
                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                    </PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataTextColumn FieldName="SitePeriod" VisibleIndex="27"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="InitialContractPeriodSortOrder" VisibleIndex="28"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataDateColumn FieldName="SiteContractExpiry" VisibleIndex="29">
                                                    <PropertiesDateEdit>
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties>
                                                                <ClearButton Visibility="Auto"></ClearButton>
                                                            </TimeEditProperties>
                                                        </TimeSectionProperties>

                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                    </PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataDateColumn FieldName="SiteCeaseDate" VisibleIndex="30">
                                                    <PropertiesDateEdit>
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties>
                                                                <ClearButton Visibility="Auto"></ClearButton>
                                                            </TimeEditProperties>
                                                        </TimeSectionProperties>

                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                    </PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataTextColumn FieldName="SiteCeaseReason" VisibleIndex="31"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ContractCeaseReasonsSortOrder" VisibleIndex="32"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="InvoiceFrequency" VisibleIndex="33"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="InvoicingFrequencySortOrder" VisibleIndex="34"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataDateColumn FieldName="InvoiceCommencing" VisibleIndex="35">
                                                    <PropertiesDateEdit>
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties>
                                                                <ClearButton Visibility="Auto"></ClearButton>
                                                            </TimeEditProperties>
                                                        </TimeSectionProperties>

                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                    </PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataTextColumn FieldName="InvoiceCommencingString" VisibleIndex="36"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="IndustryGroup" VisibleIndex="37"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="IndustrySortOrder" VisibleIndex="38"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PreviousSupplier" VisibleIndex="39"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PreviousSupplierSortOrder" VisibleIndex="40"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="LostBusinessTo" VisibleIndex="41"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="LostBusinessToSortOrder" VisibleIndex="42"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SalesPerson" VisibleIndex="43"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SalesPersonSortOrder" VisibleIndex="44"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="InitialServiceAgreementNo" VisibleIndex="45"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="InvoiceMonth1" VisibleIndex="46"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="InvoiceMonth2" VisibleIndex="47"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="InvoiceMonth3" VisibleIndex="48"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="InvoiceMonth4" VisibleIndex="49"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="GeneralSiteServiceComments" VisibleIndex="50"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TotalUnits" VisibleIndex="51"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="TotalAmount" VisibleIndex="52"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Zone" VisibleIndex="53"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ZoneSortOrder" VisibleIndex="54"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataCheckColumn FieldName="SeparateInvoice" VisibleIndex="55"></dx:GridViewDataCheckColumn>
                                                <dx:GridViewDataTextColumn FieldName="PurchaseOrderNumber" VisibleIndex="56"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataCheckColumn FieldName="chkSitesExcludeFuelLevy" VisibleIndex="57"></dx:GridViewDataCheckColumn>
                                                <dx:GridViewDataTextColumn FieldName="cmbRateIncrease" VisibleIndex="58"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="cmbRateIncreaseSortOrder" VisibleIndex="59"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CustomerName" VisibleIndex="60"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CustomerRating" VisibleIndex="61"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="CustomerRatingDesc" VisibleIndex="62"></dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings ShowPreview="true" />
                                            <SettingsPager PageSize="10" />
                                        </dx:ASPxGridView>
                                        <asp:ObjectDataSource ID="odsSites" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblSites"></asp:ObjectDataSource>
                                        <asp:ObjectDataSource ID="odsClient" runat="server" DataObjectTypeName="FMS.Business.DataObjects.FleetClient" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.FleetClient" UpdateMethod="Update"></asp:ObjectDataSource>
                                        <asp:ObjectDataSource ID="odsMultiDocs" runat="server" SelectMethod="GetAllByClientCID" TypeName="FMS.Business.DataObjects.fleetDocument" DataObjectTypeName="FMS.Business.DataObjects.FleetDocument" UpdateMethod="Update" DeleteMethod="Delete" InsertMethod="Create">
                                            <SelectParameters>
                                                <asp:SessionParameter SessionField="ClientID" Name="CID" Type="Int32"></asp:SessionParameter>
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <asp:ObjectDataSource ID="odsCustomerList" runat="server" SelectMethod="GetAllCustomer" TypeName="FMS.Business.DataObjects.FleetClient"></asp:ObjectDataSource>
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
                                                <dx:GridViewDataDateColumn FieldName="DateOfRun" VisibleIndex="5" Visible="true"></dx:GridViewDataDateColumn>
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
                                            Theme="SoftOrange" AutoGenerateColumns="False" OnRowValidating="RunSiteGridView_RowValidating">
                                            <Settings ShowGroupPanel="True" ShowFilterRow="True"></Settings>
                                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                                            <SettingsEditing Mode="PopupEditForm" />
                                            <SettingsPopup>
                                                <EditForm Modal="true"
                                                    VerticalAlign="WindowCenter"
                                                    HorizontalAlign="WindowCenter" Width="400px" Height="100px" />
                                            </SettingsPopup>
                                            <Columns>
                                                <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True" ShowNewButtonInHeader="True" ShowDeleteButton="True">
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
                            <%-- End Client Assignment --%>

                        </TabPages>
                    </dx:ASPxPageControl>
                    <dx:ASPxPopupControl ID="puUnassignedRun" runat="server" ClientInstanceName="clientpuUnassignedRun"
                        Height="83px" Modal="True" CloseAction="CloseButton" Width="300px"
                        AllowDragging="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" ShowHeader="False">
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="lblSelectRun" runat="server" Text="Select Run "></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox ID="cboRun" runat="server" ValueType="System.String"
                                                ClientInstanceName="clientcboRun">
                                                <ClientSideEvents SelectedIndexChanged="UnAssignedRun_OnSelectedIndexChanged" />
                                            </dx:ASPxComboBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <dx:ASPxButton ID="btnSelectServiceRun" runat="server" Text="Select Run"
                                    OnClick="btnSelectServiceRun_Click">
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="Cancel"
                                    OnClick="btnCancel_Click" AutoPostBack="false">
                                </dx:ASPxButton>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                    <dx:ASPxPopupControl ID="puCompleteRun" runat="server" ClientInstanceName="clientpuCompleteRun"
                        Height="83px" Modal="True" CloseAction="CloseButton" Width="300px"
                        AllowDragging="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" ShowHeader="False">
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxCheckBox ID="cbxCompleteRun" runat="server" Text="Run Completed (Uncheck to delete completed run)"
                                                ClientInstanceName="clientcbxCompleteRun">
                                            </dx:ASPxCheckBox>
                                            <%--<dx:ASPxLabel ID="lblCompleteRun" runat="server" Text="Complte Run"></dx:ASPxLabel>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%--<dx:ASPxLabel ID="lblDriverID" ClientInstanceName="clientlblDriverID" runat="server" Text="">
                                    </dx:ASPxLabel>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxDateEdit ID="dteCompleted" runat="server" NullText="Date Completed"
                                                ClientInstanceName="clientdteCompleted" AutoPostBack="false">
                                                <TimeSectionProperties>
                                                    <TimeEditProperties>
                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                    </TimeEditProperties>
                                                </TimeSectionProperties>
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxComboBox runat="server" ID="cboDriverCompleted" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
                                                TextField="DriverName" ValueField="DriverID" Width="100%" DataSourceID="odsDriver"
                                                EnableSynchronization="False" ClientInstanceName="clientcboDriverCompleted"
                                                NullText="Select Driver">
                                            </dx:ASPxComboBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox runat="server" ID="txtCompletedNotes" ClientInstanceName="clienttxtCompletedNotes"
                                                NullText="Enter Notes">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <dx:ASPxButton ID="btnCompleteRun" runat="server" Text="Set"
                                    OnClick="btnCompleteRun_Click">
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="btnCancelComplete" runat="server" Text="Cancel"
                                    OnClick="btnCancelComplete_Click">
                                </dx:ASPxButton>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                </div>
                <dx:ASPxPopupControl ID="puDialog" runat="server" ClientInstanceName="clientpuDialog"
                    Height="83px" Modal="True" CloseAction="CloseButton" Width="300px"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" ShowHeader="False">
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server">
                            <dx:ASPxLabel ID="lblDialog" runat="server" Text="Your Text Here">
                            </dx:ASPxLabel>
                            <br />
                            <dx:ASPxButton ID="btnDialogOK" runat="server" Text="OK"
                                OnClick="btnDialogOK_Click">
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="btnDialogCancel" runat="server" Text="Cancel"
                                OnClick="btnDialogCancel_Click">
                            </dx:ASPxButton>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                    Modal="True">
                </dx:ASPxLoadingPanel>
                <asp:ObjectDataSource ID="odsDriver" runat="server" SelectMethod="GetAllPerApplicationMinusInActive" TypeName="FMS.Business.DataObjects.tblDrivers"></asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
