using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace Eyedentify.App_Code
{
    public class EmailHelper
    {

        public EmailHelper()
        {
        }

        internal void Send_Registration_Email(string firstName, string lastName, string userName, string emailAddress)
        {
            string registrationEmailPath = Utility.ReadConfigSetting("EmailTemplateFilePath") + "RegistrationEmail.txt";
            MailMessage mm = GenerateEmail(registrationEmailPath, emailAddress);
            string emailSubject = mm.Subject;
            emailSubject = emailSubject.Replace("{UserName}", userName);
            mm.Subject = emailSubject;

            string emailBody = mm.Body;
            emailBody = emailBody.Replace("{FirstName}", firstName);
            emailBody = emailBody.Replace("{LastName}", lastName);
            emailBody = emailBody.Replace("{UserName}", userName);
            mm.Body = emailBody;
            SendEmail(mm);
        }

        private MailMessage GenerateEmail(string emailFilePath, string emailAddr)
        {
            MailMessage email = new MailMessage();
            string emailText = string.Empty;
            if (!emailFilePath.Equals(string.Empty))
            {
                emailText = File.ReadAllText(emailFilePath);
                bool windowsLineBreak = emailText.IndexOf("\r\n") != -1;
                string lineBreakString = windowsLineBreak ? "\r\n" : "\n";
                int indexOfFirstLineBreak = emailText.IndexOf(lineBreakString);
                email.Subject = emailText.Substring(0, indexOfFirstLineBreak);
                email.Body = ReplaceValuesForEmail(emailText.Substring(indexOfFirstLineBreak + lineBreakString.Length));
            }

            email.IsBodyHtml = true;
            email.From = new MailAddress("do_not_reply@eyedentify.co.nz");
            //email.Bcc.Add(new MailAddress("email_backup@eyedentify.co.nz"));
            email.To.Add(new MailAddress(emailAddr));
            return email;
        }

        private void SendEmail(MailMessage email)
        {
            SmtpClient client = new SmtpClient(Utility.ReadConfigSetting("SMTPServer"));
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("support@eyedentify.co.nz", "eyedentify12");
            client.EnableSsl = true; // in prod
            // client.EnableSsl = false;
            try
            {
                client.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string ReplaceValuesForEmail(string textToReplace)
        {
            textToReplace = textToReplace.Replace("{DomainName}", Utility.ReadConfigSetting("DomainName"));
            return textToReplace;
        }
    }
}
