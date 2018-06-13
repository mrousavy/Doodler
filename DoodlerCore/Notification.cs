using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace DoodlerCore
{
    /// 
    /// @author Fatih Aydin
    /// Sends a E-Mail to the user
    ///
    class Notification
    {
        String titleString;
        String messageString;
        System.Net.Mail.MailMessage message;

        public Notification()
        {
            titleString = "Test title";
            messageString = "This is the default notification";
            message = new System.Net.Mail.MailMessage();

        }

        public void sendMail()  //Boolean
        {
            message.To.Add("faydin@student.tgm.ac.at");
            message.Subject = titleString;
            message.From = new MailAddress("");
            message.Body = messageString;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 584);   //gmail.com ?
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential("faydin@student.tgm.ac.at", "password");
            //smtp.Send(message);

            try
            {
                smtp.Send("faydin@student.tgm.ac.at", "fatih65644@gmail.com", "test", "testtesttest");
                Console.WriteLine("email sent successfully!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("email not sent; reasons:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
