using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.Models
{
    public class Common
    {
        public static String EmailHeader()
        {
            return @"<html><head><style type=""text/css"">body{font-family: Verdana,'sans-serif';font-size:12px;}p{text-align:justify;margin:10px 0 !important;}
                a{color:#1a4e94;text-decoration:none;font-weight:bold;}a:hover{color:#3c92fe;}table td{font-family: Verdana,'sans-serif' !important;font-size:12px;padding:3px;border-bottom:1px solid #dddddd;}
                </style></head><body>
                <div style=""width:100%; margin:5px auto;font-family: Verdana,'sans-serif';font-size:12px;line-height:20px; background-color:#f2f2f2;"">
                <div style=""padding:10px;"">";
        }

        public static String EmailSignature()
        {
            return @"<p>Please do let us know if you have any questions. Email us on <a href=""mailto:support@sunrisediamonds.com.hk"">support@sunrisediamonds.com.hk</a></p>
                <p>Thanks and Regards,<br />Sunrise Diamond Team,<br />Room 1,14/F, Peninsula Square<br/>East Wing, 18 Sung On Street<br/>Hunghom, Kowloon<br/>Hong Kong<br/>
                <a href=""https://sunrisediamonds.com.hk"">www.sunrisediamonds.com.hk</a></p>
                </div></div></body></html>";
        }
        public static bool EmailForgotPassword(string fsToAdd, string fsName, string fsUsername, string fsPassword)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear " + fsName + ",</p>");
                loSb.Append(@"<p>Thank you for requesting your account detail.</p>");
                loSb.Append(@"<p>Please store below information for further communication.<br />");
                loSb.Append(@"<b>Username: </b>" + fsUsername + "<br />");
                loSb.Append("<b>Password: </b>" + fsPassword + "<br /></p>");

                //loSb.Append(EmailSignature());

                SendMail(fsToAdd, "Sunrise Diamonds – Forgot Password – " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"),
                    Convert.ToString(loSb));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void SendMail(string fsToAdd, string fsSubject, string fsMsgBody)
        {
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                loMail.To.Add(fsToAdd);
                loMail.Bcc.Add("hardik@brainwaves.co.in");

                loMail.Subject = fsSubject;
                loMail.IsBodyHtml = true;

                AlternateView av = AlternateView.CreateAlternateViewFromString(fsMsgBody, null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);


                Thread email = new Thread(delegate ()
                {
                    loSmtp.Send(loMail);
                });

                email.IsBackground = true;
                email.Start();
                if (!email.IsAlive)
                {

                    email.Abort();
                }
                ///////////////////////
            }


            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
