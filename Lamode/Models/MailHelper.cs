using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Lamode.Models
{
    public class MailHelper
    {
        public const string SUCCESS
        = "Success! Your email has been sent.  Please allow up to 48 hrs for a reply.";
        const string TO = "f_lamee2002@yahoo.com"; // Specify where you want this email sent.
                                 // This value may/may not be constant.
                                 // To get started use one of your email 
                                 // addresses.
        public string EmailFromArvixe(Message message)
        {

            // Use credentials of the Mail account that you created with the steps above.
            const string FROM = "noreply@farideh-lamee.com";
            const string FROM_PWD = "FHp3003976";
            const bool USE_HTML = true;

            // Get the mail server obtained in the steps described above.
            const string SMTP_SERVER = "173.201.192.229";
            try
            {
                MailMessage mailMsg = new MailMessage(FROM, TO);
                mailMsg.Subject = message.Subject;
                mailMsg.Body = message.Body + "<br/>sent by: " + message.Sender;
                mailMsg.IsBodyHtml = USE_HTML;

                SmtpClient smtp = new SmtpClient();
                smtp.Port = 25;
                smtp.Host = SMTP_SERVER;
                smtp.Credentials = new System.Net.NetworkCredential(FROM, FROM_PWD);
                smtp.Send(mailMsg);
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
            return SUCCESS;
        }
    }

}
