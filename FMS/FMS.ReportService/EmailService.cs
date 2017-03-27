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
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            string VehicleName = string.Empty;
            try
            {
                List<FMS.Business.DataObjects.ReportSchedule> objScheduleList = new List<FMS.Business.DataObjects.ReportSchedule>();  

                objScheduleList = ReportSchedule.GetAllForApplication();                   

                if (objScheduleList != null)
                {
                    foreach (var Items in objScheduleList)
                    {
                        // startDate 
                        if (Convert.ToString(Items.StartDate) == "Now")
                        {
                            startDate = DateTime.Now;
                        }
                        else if (Convert.ToString(Items.StartDate) == "Beginning of Day")
                        {
                            startDate = ClsExtention.BeginingOftheDay(DateTime.Now);
                        }
                        else if (Convert.ToString(Items.StartDate) == "Beginning of Week")
                        {
                            startDate = ClsExtention.BeginingOfWeek(DateTime.Now, DayOfWeek.Monday);
                        }
                        else if (Convert.ToString(Items.StartDate) == "Beginning of Year")
                        {
                            startDate = ClsExtention.BeginingOfYear(DateTime.Now);
                        }
                        else if (Convert.ToString(Items.StartDate) == "Specific")
                        {
                            startDate = Convert.ToDateTime(Items.StartDateSpecific);
                        }

                        // endDate
                        if (Convert.ToString(Items.EndDate) == "Now")
                        {
                            endDate  = DateTime.Now;
                        }
                        else if (Convert.ToString(Items.EndDate) == "Beginning of Day")
                        {
                            endDate = ClsExtention.BeginingOftheDay(DateTime.Now);
                        }
                        else if (Convert.ToString(Items.EndDate) == "Beginning of Week")
                        {
                            endDate = ClsExtention.BeginingOfWeek(DateTime.Now, DayOfWeek.Monday);
                        }
                        else if (Convert.ToString(Items.EndDate) == "Beginning of Year")
                        {
                            endDate = ClsExtention.BeginingOfYear(DateTime.Now);
                        }
                        else if (Convert.ToString(Items.EndDate) == "Specific")
                        {
                            endDate = Convert.ToDateTime(Items.StartDateSpecific);
                        } 

                        // End Block

                        if (Convert.ToString(Items.ReportName) ==  ReportNameList.VehicleReport) 
                        {
                            CachedVehicleReport rept = new CachedVehicleReport();
                            rept = ReportDataHandler.GetVehicleReportValues(Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), Convert.ToString(Items.Vehicle), new Guid(Convert.ToString(Items.ApplicationId)));
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

                if (!string.IsNullOrEmpty(ReceiverEmailID))
                {
                    mm.To.Add(new System.Net.Mail.MailAddress(ReceiverEmailID));
                }
                else
                {
                    mm.To.Add(new System.Net.Mail.MailAddress(ReceiverEmailID));
                }

                //string AppLocation = "";
                //AppLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                //AppLocation = AppLocation.Replace("file:\\", "");
                //string file = AppLocation + "\\email\\Report_scheduler_requirements.docx";
      

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(@"C:\\Users\aman\Desktop\Code\FMS\FMS.ReportService\email\Report_scheduler_requirements.docx"); //Attaching File to Mail  
                mm.Attachments.Add(attachment); 
                 
               

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
