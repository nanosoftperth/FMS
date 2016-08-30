<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Test.aspx.vb" Inherits="FMS.WEB.Test1" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.1, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v15.1.Core, Version=15.1.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link href="Content/Site.css" rel="stylesheet" />
    


    <title></title>
    <style type="text/css">
        #TextArea1 {
            width: 289px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        

        <%--<dx:ASPxTrackBar ID="ASPxTrackBar1" runat="server" Height="26px">
        </dx:ASPxTrackBar>--%>
        
        <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" AppointmentDataSourceID="appointmentDataSource" ClientIDMode="AutoID"
            OnAppointmentRowInserted="ASPxScheduler1_AppointmentRowInserted" GroupType="Resource" ResourceDataSourceID="odsResources" Start="2016-03-11" Theme="SoftOrange">
            <Storage>
                <Appointments>
                    <Mappings AppointmentId="Id" Start="StartTime" End="EndTime" Subject="Subject" AllDay="AllDay"
                        Description="Description" Label="Label" Location="Location" RecurrenceInfo="RecurrenceInfo"
                        ReminderInfo="ReminderInfo" Status="Status" Type="EventType" />
                </Appointments>
                <Resources>
                    <Mappings ResourceId="ResourceID" Caption="Name" />
                </Resources>

            </Storage>

            <OptionsForms AppointmentFormTemplateUrl="~/Controls/DriverAppointmentForm.ascx" 
                            AppointmentInplaceEditorFormTemplateUrl="~/DevExpress/ASPxSchedulerForms/InplaceEditor.ascx" 
                            GotoDateFormTemplateUrl="~/DevExpress/ASPxSchedulerForms/GotoDateForm.ascx" 
                            RecurrentAppointmentDeleteFormTemplateUrl="~/DevExpress/ASPxSchedulerForms/RecurrentAppointmentDeleteForm.ascx" 
                            RecurrentAppointmentEditFormTemplateUrl="~/DevExpress/ASPxSchedulerForms/RecurrentAppointmentEditForm.ascx" 
                            RemindersFormTemplateUrl="~/DevExpress/ASPxSchedulerForms/ReminderForm.ascx" />

            <Views>
                <DayView>
                    <TimeRulers>
                        <cc1:TimeRuler></cc1:TimeRuler>
                    </TimeRulers>
                </DayView>

                <WorkWeekView>
                    <TimeRulers>
                        <cc1:TimeRuler></cc1:TimeRuler>
                    </TimeRulers>
                </WorkWeekView>

<FullWeekView><TimeRulers>
<cc1:TimeRuler></cc1:TimeRuler>
</TimeRulers>
</FullWeekView>
            </Views>
            <OptionsToolTips AppointmentDragToolTipUrl="~/DevExpress/ASPxSchedulerForms/AppointmentDragToolTip.ascx" AppointmentToolTipUrl="~/DevExpress/ASPxSchedulerForms/AppointmentToolTip.ascx" SelectionToolTipUrl="~/DevExpress/ASPxSchedulerForms/SelectionToolTip.ascx" />
        </dxwschs:ASPxScheduler>


        <asp:ObjectDataSource ID="appointmentDataSource" runat="server" DataObjectTypeName="FMS.Business.DataObjects.CustomEvent" DeleteMethod="DeleteMethodHandler" InsertMethod="InsertMethodHandler" SelectMethod="SelectMethodHandler" TypeName="FMS.Business.DataObjects.CustomEventDataSource" UpdateMethod="UpdateMethodHandler"></asp:ObjectDataSource>


        <asp:ObjectDataSource ID="odsResources" runat="server" SelectMethod="SelectMethodHandler" TypeName="FMS.Business.DataObjects.CustomResourceDataSource"></asp:ObjectDataSource>


    </form>
</body>
</html>
