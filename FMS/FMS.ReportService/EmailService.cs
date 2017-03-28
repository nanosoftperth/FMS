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
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;

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
                        //  to get the start date  
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

                        //  to get the end date
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
                            endDate = Convert.ToDateTime(Items.EndDateSpecific);
                        } 
                        // End Block

                         
                        if (Convert.ToString(Items.ReportName) ==  ReportNameList.VehicleReport) 
                        {
                            CachedVehicleReport rept = new CachedVehicleReport();
                           // rept = ReportDataHandler.GetVehicleReportValues(Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), Convert.ToString(Items.Vehicle), new Guid(Convert.ToString(Items.ApplicationId)));
                             


                        }
                        else if (Convert.ToString(Items.ReportName) == ReportNameList.DriverOperatingHoursReport) 
                        {     

                        }
                        else if (Convert.ToString(Items.ReportName) == ReportNameList.ReportGeoFence_byDriver)
                        {
                            ClientSide_GeoFenceReport_ByDriver rept = new ClientSide_GeoFenceReport_ByDriver();
                            rept = ReportDataHandler.GetGeoCacheReportByDrivers(Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), Convert.ToString(Items.Vehicle), new Guid(Convert.ToString(Items.ApplicationId)));
                        }
                        // Check the email ID 
                        sendEmail(Convert.ToString(Items.RecipientEmail), Convert.ToString(Items.RecipientName ), Convert.ToString (Items .ReportName), Convert .ToString (Items .ApplicationId ));
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { }         
        }
        public bool sendEmail(string receiverEmailID, string receiverName, string  reportName, string appID)
        {
            try
            {
                ////Command line argument must the the SMTP host.
                VehicleReport report = new VehicleReport();
                report.Parameters[0].Value = "03/01/2016";
                report.Parameters[1].Value = "03/30/2017";
                report.Parameters[2].Value = "Uniqco02";
                //report.Parameters[3].Value = new Guid(appID);

                MemoryStream mem = new MemoryStream();
                report.ExportToPdf(mem);

                // Create a new attachment and put the PDF report into it.
                mem.Seek(0, System.IO.SeekOrigin.Begin);
                Attachment att = new Attachment(mem, "TestReport.pdf", "application/pdf");

                 var _with1 = new SmtpClient();

                _with1.Port = 587;
                _with1.Host = "smtp.zoho.com";
                _with1.EnableSsl = true;
                _with1.Timeout = 10000;
                _with1.DeliveryMethod = SmtpDeliveryMethod.Network;
                _with1.UseDefaultCredentials = false;
                _with1.Credentials = new System.Net.NetworkCredential("no-reply@nanosoft.com.au", "notastrongpassword");

                
                MailMessage mm = new MailMessage();
                mm.Attachments.Add(att);
                mm.From = new MailAddress("no-reply@nanosoft.com.au");
                mm.Subject = "Schedule Report"; //subject.Replace(Constants.vbNewLine, ". ");
                mm.IsBodyHtml = true;

                StringBuilder strContentBody = new StringBuilder();
                strContentBody.Append("<table style ='width:100%'>");
                strContentBody.Append("<tr>");
                if (receiverName.Contains(":"))
                {
                    string[] strName = receiverName.Split(':');
                    if (strName != null)
                    {
                        receiverName = strName[1];
                    }
                }
                strContentBody.Append("<td>Dear,   " + receiverName + "</td><td></td>");
                strContentBody.Append("</tr>");
                strContentBody.Append("<tr>");
                strContentBody.Append("<td>Please find attached the " + reportName  + " report generated on  "+  DateTime.Now.ToString ("dd/MMM/yyyy HH:mm:ss") +" </td><td></td>");
                strContentBody.Append("</tr>");
                strContentBody.Append("<tr>");
                strContentBody.Append("<td>This was generated by the Nanosoft GPS report generator, if you would like to view more then please <a href ='http://demo.nanosoft.com.au/Home.aspx'>click here: </a></td><td></td>");
                strContentBody.Append("</tr>");
                strContentBody.Append("<tr>");
                strContentBody.Append("<td></td><td></td>");
                strContentBody.Append("</tr>");
                strContentBody.Append("<tr>");
                strContentBody.Append("<td>Thank you</td><td></td>");
                strContentBody.Append("</tr>"); 
                strContentBody.Append("</tr>");
                strContentBody.Append("</table>"); 
                mm.Body = Convert.ToString(strContentBody);
                  
                mm.To.Add(new System.Net.Mail.MailAddress(receiverEmailID));
                
                 
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
