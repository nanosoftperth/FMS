using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net.Mail;
using System.Web;
using System.Net.Mime;
using System.IO; 
using FMS.Business.DataObjects;
using FMS.Business;
using FMS.ReportLogic;
 
 namespace FMS.ReportService
{
    public class EmailService : IService
    {
        readonly Timer _timer;
        public EmailService()
        {    

        } 
        public void Start()
        {     
            try
            {
                GetSchedule();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            } 
            //_timer.Start();
            //Console.WriteLine("Starting Service ...");
        }
        public void Stop()
        {
            //_timer.Stop();
            //Console.WriteLine("Stopping the service ...");
        }

        public void GetSchedule() 
        {
            try
            {
                List<FMS.Business.DataObjects.ReportSchedule> objScheduleList = new List<FMS.Business.DataObjects.ReportSchedule>();

                Guid appID = new Guid("176225F3-3AC6-404C-B191-0B4F69CC651A");

                objScheduleList = ReportSchedule.GetAllForApplication(appID);

          

                string te = ThisSession.ApplicationID.ToString ();

                if (objScheduleList != null)
                {
                    foreach (var Items in objScheduleList)
                    {
                        if (Convert.ToString(Items.ReportName) ==  ReportNameList.VehicleReport) 
                        {
                            CachedVehicleReport rept = new CachedVehicleReport();



                            rept = ReportDataHandler.GetVehicleReportValues(Convert.ToDateTime("3/1/2016"), Convert.ToDateTime("3/1/2016"), "Uniqco02");
                        }
                        else if (Convert.ToString(Items.ReportName) == ReportNameList.DriverOperatingHoursReport) 
                        {

                        }
                        else if (Convert.ToString(Items.ReportName) == ReportNameList.ReportGeoFence_byDriver)
                        {

                        }
                        // Check the email ID 
                        sendEmail(Convert.ToString(Items.RecipientEmail));
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { }         
        }
        public bool sendEmail(string ReceiverEmailID)
        {
            try
            {
                //Command line argument must the the SMTP host.
                 var _with1 = new SmtpClient();

                _with1.Port = 587;
                _with1.Host = "smtp.zoho.com";
                _with1.EnableSsl = true;
                _with1.Timeout = 10000;
                _with1.DeliveryMethod = SmtpDeliveryMethod.Network;
                _with1.UseDefaultCredentials = false;
                _with1.Credentials = new System.Net.NetworkCredential("no-reply@nanosoft.com.au", "notastrongpassword");

                MailMessage mm = new MailMessage();

                mm.From = new MailAddress("no-reply@nanosoft.com.au");
                mm.Subject = "Test Email"; //subject.Replace(Constants.vbNewLine, ". ");
                mm.Body = "Body Content"; 

                mm.To.Add(new System.Net.Mail.MailAddress(ReceiverEmailID));
                //Attachment data = new Attachment(
                //         HttpContext.Current.Server.MapPath("~/Report_scheduler_requirements.docx"),
                //         MediaTypeNames.Application.Octet);
                //mm.Attachments.Add(data); 

                //string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);

                //  string _FileName = HttpContext.Current.Server.MapPath("~/Report_scheduler_requirements.docx");

                //string filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "email/Report_scheduler_requirements.docx");


                //mm.Attachments.Add(new Attachment(filePath, "Test"));

                //foreach (string s in recipient.Split(";").ToList)
                //{
                //    mm.To.Add(new Net.Mail.MailAddress(s));
                //}

                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure; 
                _with1.Send(mm); 
            }
            catch (Exception ex) { return false; }
            finally { } 
            return true; 
        } 
    }


}
