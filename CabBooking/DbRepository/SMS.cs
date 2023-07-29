using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace CabBooking.Models
{
    public class SMS
    {
        static public string SendSMS(string Mobile, string Message, string TempId)
        {
            string Status;
            try
            {
                string SMSAPI = ConfigurationManager.ConnectionStrings["SMSAPI"].ToString();
                SMSAPI = SMSAPI.Replace("[AND]", "&");
                SMSAPI = SMSAPI.Replace("[MOBILE]", Mobile);
                SMSAPI = SMSAPI.Replace("[MESSAGE]", Message);
                SMSAPI = SMSAPI.Replace("[TEMPLATEID]", TempId);
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI));
                HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
                Status = httpResponse.StatusCode.ToString();
                return Status;
            }
            catch (Exception ex)
            {
                return Status = ex.ToString();
            }
        }

        static public string OTPMemberMessage2(string MemberName, string OTP, string purpose)
        {

            string Message = ConfigurationManager.AppSettings["Examotp"].ToString();
            Message = Message.Replace("[MEMBER-NAME]", MemberName);
            Message = Message.Replace("[OTP]", OTP);
            Message = Message.Replace("[PURPOSE]", purpose);
            return Message;
        }

        static public string CollegeCredential(string MemberName, string UserId, string Passwprd)
        {

            string Message = ConfigurationManager.AppSettings["CollegeCred"].ToString();
            Message = Message.Replace("[MEMBER-NAME]", MemberName);
            Message = Message.Replace("[UserId]", UserId);
            Message = Message.Replace("[Password]", Passwprd);
            return Message;
        }

        static public string otpMessageForCollegeRegistration(string OTP, string MemberName, string UserId, string Password, string CollegeName)
        {

            string Message = ConfigurationManager.AppSettings["Examinor"].ToString();
            Message = Message.Replace("[MEMBER-NAME]", MemberName);
            Message = Message.Replace("[OTP]", OTP);
            Message = Message.Replace("[UserId]", UserId);
            Message = Message.Replace("[Password]", Password);
            Message = Message.Replace("[CName]", CollegeName);
            return Message;
        }

    }

    public class Mail
    {

        static public string SendEmail(string receiver, string subject, string message)
        {
            string status = null;
            try
            {
                var senderEmail = new MailAddress("quickpedigital@gmail.com", "MGKVP");
                var receiverEmail = new MailAddress(receiver, "Receiver");
                var password = "pdnnyairqqydthbp"; /*pdnnyairqqydthbp*/
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                    status = "Ok";
                }
                return status;
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }
            return status;
        }

        static public void SendEmailMessag1(string receiver, string subject, string message)
        {
            try
            {
                var emailSender = ConfigurationManager.AppSettings["emailSender"].ToString();
                string emailSenderPassword = ConfigurationManager.AppSettings["password"].ToString();
                string emailSenderHost = ConfigurationManager.AppSettings["smtp"].ToString();
                int emailSenderPort = Convert.ToInt16(ConfigurationManager.AppSettings["portnumber"].ToString());
                Boolean emailIsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSSL"]);
                string Subject = subject;
                MailMessage _mailmsg = new MailMessage();
                _mailmsg.IsBodyHtml = true;
                _mailmsg.From = new MailAddress(emailSender, "FastYatra");
                _mailmsg.To.Add(receiver.ToString());
                _mailmsg.Subject = subject;
                _mailmsg.Body = message;
                SmtpClient _smtp = new SmtpClient();
                _smtp.Host = emailSenderHost;
                _smtp.Port = emailSenderPort;
                _smtp.EnableSsl = emailIsSSL;
                NetworkCredential _network = new NetworkCredential(emailSender, emailSenderPassword);
                _smtp.Credentials = _network;
                _smtp.Send(_mailmsg);
            }
            catch (Exception ex)
            {
            }
        }

        static public void SendEmailMessag(string receiver, string subject, string message)
        {
            // Replace with your GoDaddy email and password
            string email = "support@fastyatra.com";
            string password = "Kd800753*";

            // Replace with the recipient email address
            string recipientEmail = receiver;

            MailMessage mailMessage = new MailMessage(email, recipientEmail)
            {
                IsBodyHtml = true,
                From = new MailAddress("support@fastyatra.com", "FastYatra"),
                Subject = subject,
                Body = message
            };

            // Configure SMTP client settings for GoDaddy
            SmtpClient smtpClient = new SmtpClient("smtpout.secureserver.net", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true
            };

            try
            {
                // Send the email
                smtpClient.Send(mailMessage);
                //ViewBag.Message = "Email sent successfully!";
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during sending
                //ViewBag.Error = $"Error sending email: {ex.Message}";
            }
        }

    }
}