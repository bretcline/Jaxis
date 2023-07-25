using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Jaxis.MessageLibrary;

namespace CloudAlerts
{
    public class CloudNotification : ICloudNotification
    {
        public void PushAlert(AlertMessage _alert)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Credentials = new NetworkCredential("Alert@beveragemonitor.com", "rfid123!!!");
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;

                using ( var message = new MailMessage(new MailAddress("Alert@beveragemonitor.com"), new MailAddress(_alert.Addresses[0])))
                {
                    for (int i = 1; i < _alert.Addresses.Count; ++i)
                    {
                        message.To.Add(new MailAddress(_alert.Addresses[i]));
                    }

                    message.BodyEncoding = Encoding.UTF8;
                    message.SubjectEncoding = Encoding.UTF8;
                    message.Subject = _alert.Subject;
                    message.Body = _alert.Message;

                    smtpClient.Send(message);
                }
            }
        }
    }
}
