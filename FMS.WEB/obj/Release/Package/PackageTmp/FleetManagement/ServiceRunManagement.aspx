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
                var ndxS = fieldName.indexOf("_") + 1;
                var ndxE = fieldName.indexOf("-");
                DriverID = fieldName.substring(ndxS, ndxE);
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
                        //showLoadingProcess('Loading Run Definitions. Please wait...')
                        //PageMethods.GetUnAssignedRuns(TranDate, OnSuccess);

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
                                            EnableTheming="True" Theme="SoftOrange"
                                            KeyFieldName="ID">
                                            <SettingsPager PageSize="100">
                                                <PageSizeItemSettings Items="10, 20, 50, 100" Visible="true">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
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
                                            <dx:ASPxComboBox ID="cboRun" runat="server" DropDownStyle="DropDownList" DataSourceID="odsRuns"
                                                ValueField="Rid" ValueType="System.Int32" TextFormatString="{0}" Width="100%"
                                                NullText="Select Run">
                                                <Columns>
                                                    <dx:ListBoxColumn FieldName="RunDescription" Width="300px" />
                                                </Columns>
                                                <ClientSideEvents SelectedIndexChanged="UnAssignedRun_OnSelectedIndexChanged" />
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </dx:ASPxComboBox>
                                            <%--<dx:ASPxComboBox ID="cboRun" runat="server" ValueType="System.String"
                                                ClientInstanceName="clientcboRun" DataSourceID="odsRuns">
                                                <ClientSideEvents SelectedIndexChanged="UnAssignedRun_OnSelectedIndexChanged" />
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </dx:ASPxComboBox>--%>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <dx:ASPxButton ID="btnSelectServiceRun" runat="server" Text="Select Run"
                                    OnClick="btnSelectServiceRun_Click">
                                    <ClientSideEvents Click="function(s, e){clientpuUnassignedRun.HideWindow(clientpuUnassignedRun.GetWindow(0)); }" />
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="Cancel"
                                    OnClick="btnCancel_Click" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e){clientpuUnassignedRun.HideWindow(clientpuUnassignedRun.GetWindow(0)); }" />
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
                                    <ClientSideEvents Click="function(s, e){clientpuCompleteRun.HideWindow(clientpuCompleteRun.GetWindow(0)); }" />
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="btnCancelComplete" runat="server" Text="Cancel"
                                    OnClick="btnCancelComplete_Click">
                                    <ClientSideEvents Click="function(s, e){clientpuCompleteRun.HideWindow(clientpuCompleteRun.GetWindow(0)); }" />
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
                                <ClientSideEvents Click="function(s, e){clientpuDialog.HideWindow(clientpuDialog.GetWindow(0)); }" />
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="btnDialogCancel" runat="server" Text="Cancel"
                                OnClick="btnDialogCancel_Click">
                                <ClientSideEvents Click="function(s, e){clientpuDialog.HideWindow(clientpuDialog.GetWindow(0)); }" />
                            </dx:ASPxButton>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
                <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                    Modal="True">
                </dx:ASPxLoadingPanel>
                <asp:ObjectDataSource ID="odsDriver" runat="server" SelectMethod="GetAllPerApplicationMinusInActive" TypeName="FMS.Business.DataObjects.tblDrivers"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="odsRuns" runat="server" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.tblRuns"></asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
