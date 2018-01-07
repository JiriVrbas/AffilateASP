using AffilateWeb.Utils;
using DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace AffilateWeb.Servicies
{
    public class ConfirmationService
    {
        private const string EMAIL_TEMPLATE = "<html><head><title>Aktivační email</title></head>"
                        + "<body><p>Děkujeme za Vaši registraci, kliknutím na níže uvedený"
                        +" odkaz aktivujete svůj účet</p><a href='{0}'>Aktivovat nyní!</a></body></html>";
        internal void Confirm(string clink)
        {
            using(var db = new Database())
            {
                var user = db.Users.FirstOrDefault(x => x.ConfirmationLink.EndsWith(clink));
                if(user != null)
                {
                    user.IsConfirmed = true;
                    db.SaveChanges();
                }
            }
        }
        public string GetUniqeConfirmationLink(string serverPath)
        {
            return Generators.GetRandomConfirmationLink(serverPath);
        }

        public void SendConfirmationEmail(string reciever, string confirmationLink)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(new MailAddress(reciever));
            mailMessage.Subject = "Potvrzení registrace";
            mailMessage.Body = string.Format(EMAIL_TEMPLATE,confirmationLink);

            smtpClient.Send(mailMessage);
        }
    }
}