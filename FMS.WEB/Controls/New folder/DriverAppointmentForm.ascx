<%@ Control Language="vb" AutoEventWireup="true" Inherits="DriverAppointmentForm" CodeFile="DriverAppointmentForm.ascx.vb" %>

<%@ Import Namespace="DXWebApplication1" %>


<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.1, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>

<style type="text/css">
    #ASPxScheduler1_formBlock_AptFrmContainer_PW-1 {
        /*HACK!*/
        height: 150px !Important;
        width: 424px !Important;
    }
</style>

<div>
     <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Visible="false" AssociatedControlID="tbSubject" Text="Subject:">
                        </dxe:ASPxLabel>

     <dxe:ASPxTextBox ClientInstanceName="_dx" ID="tbSubject" visible="false" runat="server" Width="100%" 
                                    Text='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).Appointment.Subject%>' />
            

    <table>
        <tr>
            <td>
                <dxe:ASPxLabel ID="lblSubject" runat="server" AssociatedControlID="Driver" Text="Driver:">
                </dxe:ASPxLabel>
            </td>
            <td>
                <dxe:ASPxComboBox ID="comboDrivers"
                    ClientInstanceName="comboDrivers"
                    runat="server" DataSourceID="odsDrivers" EnableTheming="True" Theme="SoftOrange">
                    <Columns>
                        <dxe:ListBoxColumn FieldName="Surname" />
                        <dxe:ListBoxColumn FieldName="FirstName" />
                    </Columns>
                    <ClearButton Visibility="Auto"></ClearButton>
                </dxe:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" AssociatedControlID="" Text="Vehicle:">
                </dxe:ASPxLabel>
            </td>
            <td>
                <dxe:ASPxComboBox ID="ASPxComboBox1"
                    ClientInstanceName="comboDrivers"
                    runat="server" DataSourceID="odsVehicles" ValueField="ApplicationVehileID" TextField="Name" EnableTheming="True" Theme="SoftOrange">
                    
                    <ClearButton Visibility="Auto"></ClearButton>
                </dxe:ASPxComboBox>
                <asp:ObjectDataSource runat="server" ID="odsVehicles" SelectMethod="GetAll" TypeName="FMS.Business.DataObjects.ApplicationVehicle">
                    <SelectParameters>
                        <asp:SessionParameter SessionField="ApplicationID" DbType="Guid" Name="appplicationID"></asp:SessionParameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>

    <dxsc:AppointmentRecurrenceForm ID="AppointmentRecurrenceForm1" runat="server"
        IsRecurring='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).Appointment.IsRecurring%>'
        DayNumber='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).RecurrenceDayNumber%>'
        End='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).RecurrenceEnd%>'
        Month='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).RecurrenceMonth%>'
        OccurrenceCount='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).RecurrenceOccurrenceCount%>'
        Periodicity='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).RecurrencePeriodicity%>'
        RecurrenceRange='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).RecurrenceRange%>'
        Start='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).RecurrenceStart%>'
        WeekDays='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).RecurrenceWeekDays%>'
        WeekOfMonth='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).RecurrenceWeekOfMonth%>'
        RecurrenceType='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).RecurrenceType%>'
        IsFormRecreated='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).IsFormRecreated%>'>
    </dxsc:AppointmentRecurrenceForm>

    <table cellpadding="0" cellspacing="0" style="width: 25%;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <dxe:ASPxButton runat="server" ClientInstanceName="_dx" ID="btnOk"
                                Text="OK" UseSubmitBehavior="false" AutoPostBack="false"
                                EnableViewState="false" Width="91px" />
                        </td>
                        <td>
                            <dxe:ASPxButton runat="server" ClientInstanceName="_dx" ID="btnCancel" Text="Cancel" UseSubmitBehavior="false" AutoPostBack="false" EnableViewState="false"
                                Width="91px" CausesValidation="False" />
                        </td>
                        <td>
                            <dxe:ASPxButton runat="server" ClientInstanceName="_dx" ID="btnDelete" Text="Delete" UseSubmitBehavior="false"
                                AutoPostBack="false" EnableViewState="false" Width="91px"
                                Enabled='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).CanDeleteAppointment%>'
                                CausesValidation="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <dxsc:ASPxSchedulerStatusInfo runat="server" ID="schedulerStatusInfo" Priority="1"
                    MasterControlID='<%#(CType(Container, DriverAppointmentFormTemplateContainer)).ControlId%>' />

                <asp:ObjectDataSource ID="odsDrivers" runat="server" SelectMethod="GetAllDrivers" TypeName="FMS.Business.DataObjects.ApplicationDriver">
                    <SelectParameters>
                        <asp:SessionParameter DbType="Guid" Name="applicatoinid" SessionField="ApplicationID" />
                    </SelectParameters>
                </asp:ObjectDataSource>

            </td>
        </tr>
    </table>

</div>


<script id="dxss_ASPxSchedulerAppoinmentForm" type="text/javascript">
    function OnEdtMultiResourceSelectedIndexChanged(s, e) {
        var resourceNames = new Array();
        var items = s.GetSelectedItems();
        var count = items.length;
        if (count > 0) {
            for (var i = 0; i < count; i++)
                _aspxArrayPush(resourceNames, items[i].text);
        }
        else
            _aspxArrayPush(resourceNames, ddResource.cp_Caption_ResourceNone);
        ddResource.SetValue(resourceNames.join(', '));
    }
    function OnChkReminderCheckedChanged(s, e) {
        var isReminderEnabled = s.GetValue();
        if (isReminderEnabled)
            _dxAppointmentForm_cbReminder.SetSelectedIndex(3);
        else
            _dxAppointmentForm_cbReminder.SetSelectedIndex(-1);

        _dxAppointmentForm_cbReminder.SetEnabled(isReminderEnabled);

    }
</script>
