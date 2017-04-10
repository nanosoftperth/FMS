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
using System.Diagnostics;
//using DevExpress.XtraReports.UI;
//using DevExpress.XtraReports.Parameters;
using NLog;
using NLog.Fluent;
using System.Globalization;
using System.Configuration;
//using System.Threading;
 

namespace FMS.ReportService
{
    public class EmailService : IService
    {
        private System.Timers.Timer _timer;
        private static Logger logger = LogManager.GetCurrentClassLogger();
         
        public EmailService()
        {
            int miliSecond = 1000 * Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationInterval"]);
            _timer = new Timer(miliSecond) { AutoReset = true };
            _timer.Elapsed += timer_Elapsed; 
        } 
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            GetSchedule();
        } 
        public void Start()
        {   
            _timer.Enabled = true;
            _timer.Start(); 
        } 
        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Enabled = false;
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }
       
        public void GetSchedule()
        {  
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            string VehicleName = string.Empty;
            bool IsEmail = false;

            try
            { 
                List<FMS.Business.DataObjects.ReportSchedule> objScheduleList = new List<FMS.Business.DataObjects.ReportSchedule>();  

                objScheduleList = ReportSchedule.GetScheduleForApplication();
                dynamic GenericObj = null; 

                if (objScheduleList != null)
                {
                    foreach (var Item in objScheduleList)
                    {
                        logger.Info("Getting Report for " + Item.RecipientName + " ");
                        #region Get Report Parameter
                        //  to get the start date  
                        if (Convert.ToString(Item.StartDate) == "Now")
                        {
                            startDate = DateTime.Now;
                        }
                        else if (Convert.ToString(Item.StartDate) == "Beginning of Day")
                        {
                            startDate = ClsExtention.BeginingOftheDay(DateTime.Now);
                        }
                        else if (Convert.ToString(Item.StartDate) == "Beginning of Week")
                        {
                            startDate = ClsExtention.BeginingOfWeek(DateTime.Now, DateTime.Now.DayOfWeek);
                        }
                        else if (Convert.ToString(Item.StartDate) == "Beginning of Year")
                        {
                            startDate = ClsExtention.BeginingOfYear(DateTime.Now);
                        }
                        else if (Convert.ToString(Item.StartDate) == "Specific")
                        {
                            startDate = Convert.ToDateTime(Item.StartDateSpecific);
                        }

                        //  to get the end date
                        if (Convert.ToString(Item.EndDate) == "Now")
                        {
                            endDate = DateTime.Now;
                        }
                        else if (Convert.ToString(Item.EndDate) == "Beginning of Day")
                        {
                            endDate = ClsExtention.BeginingOftheDay(DateTime.Now);
                        }
                        else if (Convert.ToString(Item.EndDate) == "Beginning of Week")
                        {
                            endDate = ClsExtention.BeginingOfWeek(DateTime.Now, DateTime.Now.DayOfWeek);
                        }
                        else if (Convert.ToString(Item.EndDate) == "Beginning of Year")
                        {
                            endDate = ClsExtention.BeginingOfYear(DateTime.Now);
                        }
                        else if (Convert.ToString(Item.EndDate) == "Specific")
                        {
                            endDate = Convert.ToDateTime(Item.EndDateSpecific);
                        }
                        #endregion


                        #region to create the instance of report
                        string ParmType = string.Empty;
                        switch (Convert.ToString(Item.ReportName))
                        {
                            case ReportNameList.VehicleReport:
                                GenericObj = new VehicleReport();
                                ParmType = Convert.ToString(Item.Vehicle);
                                break;

                            case ReportNameList.DriverOperatingHoursReport:
                                GenericObj = new DriverOperatingHoursReport();
                                ParmType = Convert.ToString(Item.Vehicle);
                                break;

                            case ReportNameList.ReportGeoFence_byDriver:
                                GenericObj = new ReportGeoFence_byDriver();
                                ParmType = Convert.ToString(Item.Driver);
                                break;
                        }
                        #endregion
                        MemoryStream mem = new MemoryStream();


                        #region Region to check the Schedule
                        if (Convert.ToString(Item.ReportType) == Utility.OneOff)
                        {
                            if (IsOneOffSendEmail(Convert.ToDateTime(Item.ScheduleDate)))
                            {
                                IsEmail = true;
                            }
                            else { IsEmail = false; }
                        }
                        else if (Convert.ToString(Item.ReportType) == Utility.Daily)
                        {
                            if (IsScheduleTimeEmail(Convert.ToDateTime(Item.ScheduleTime)))
                            {
                                IsEmail = true;
                            }
                            else
                            {
                                IsEmail = false;
                            }
                        }
                        else if (Convert.ToString(Item.ReportType) == Utility.Weekly)
                        {
                            if (Convert.ToString(DateTime.Now.DayOfWeek) == Convert.ToString(Item.DayofWeek))
                            {
                                if (IsScheduleTimeEmail(Convert.ToDateTime(Item.ScheduleTime)))
                                {
                                    IsEmail = true;
                                }
                                else
                                {
                                    IsEmail = false;
                                }
                            }
                        }
                        else if (Convert.ToString(Item.ReportType) == Utility.Monthly)
                        {
                            if (System.DateTime.Now.ToString("dd") == Convert.ToString(Item.DayofMonth))
                            {
                                if (IsScheduleTimeEmail(Convert.ToDateTime(Item.ScheduleTime)))
                                {
                                    IsEmail = true;
                                }
                                else
                                {
                                    IsEmail = false;
                                }
                            }
                        }
                        #endregion

                        if (IsEmail)
                        {
                            logger.Info("email method called for sending " + Convert.ToString(Item.ApplicationId) + " ");

                            GenericObj.Parameters[0].Value = startDate;
                            GenericObj.Parameters[1].Value = endDate;
                            GenericObj.Parameters[2].Value = ParmType;
                            GenericObj.Parameters[3].Value = Convert.ToString(Item.ApplicationId);
                            GenericObj.ExportToPdf(mem);

                            sendEmail(Convert.ToString(Item.RecipientEmail), Convert.ToString(Item.RecipientName), Convert.ToString(Item.ReportName), mem);                              
                        }
                    }
                }
                else
                { 
                    logger.Info("No record found from database");
                }
            }
            catch (Exception ex)
            {  
                logger.Error("Error with query {0} {1} {2}", "Error in generating report", ex.Message, ex.StackTrace);
            }
            finally {   }
        }
       
        public static bool sendEmail(string ReceiverEmail, string ReceiverName, string ReportName, MemoryStream attachment)
        {
            try
            {  
                string pdfFileName = string.Empty; 

                pdfFileName = ReportName + DateTime.Now.ToString("yyyyMMdd");
                // Create a new attachment and put the PDF report into it.
                attachment.Seek(0, System.IO.SeekOrigin.Begin);
                Attachment att = new Attachment(attachment, pdfFileName, "application/pdf");

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
                strContentBody.Append("<table style ='width:100%; cellspacing=10'>");
                strContentBody.Append("<tr>");
                if (ReceiverName.Contains(":"))
                {
                    string[] strName = ReceiverName.Split(':');
                    if (strName != null)
                    {
                        ReceiverName = strName[1];
                    }
                }
                strContentBody.Append("<td>Dear,   " + ReceiverName + "</td><td></td>");
                strContentBody.Append("</tr>");
                strContentBody.Append("<tr>");
                strContentBody.Append("<td>Please find attached the " + ReportName + " report generated on  " + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss") + " </td><td></td>");
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
                mm.To.Add(new System.Net.Mail.MailAddress(ReceiverEmail));


                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                _with1.Send(mm);
                logger.Info("Email has been send successfully to " + ReceiverEmail + " ");
            }
            catch (Exception ex) 
            {
                logger.Error("Error with query {0} {1} {2}", "Error in sending Email Receipient", ex.Message, ex.StackTrace);
                return false;
            }
            finally { }
            return true;
        } 
        public static bool IsOneOffSendEmail(DateTime ScheduleDate)
        {
            if (Convert.ToDateTime(ScheduleDate) >= DateTime.Now && Convert.ToDateTime(ScheduleDate) <= DateTime.Now.AddSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationInterval"])))
            {
                return true;
            }
            else { return false; }
        }
        public static bool IsScheduleTimeEmail(DateTime scheduleTime)
        {
            DateTime ScheduleTime = Convert.ToDateTime(scheduleTime);
            if (ScheduleTime.TimeOfDay >= DateTime.Now.TimeOfDay && ScheduleTime.TimeOfDay <= DateTime.Now.AddSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationInterval"])).TimeOfDay)
            { return true; }
            else { return false; }
        }
          
    } 

}
