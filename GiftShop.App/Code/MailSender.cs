using GiftShop.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HospitalScheduler.WebApp.Code
{
    public class MailSender
    {
        private SmtpClient client;
        public MailSender()
        {
            client = new SmtpClient("smtp.gmail.com");

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential("giftshopappmailservice@gmail.com", "giftshop@ess");
            client.EnableSsl = true;
            client.Credentials = credential;

        }
        public void SendWelcomeMail(string emailAddress)
        {
            MailMessage message = new MailMessage("giftshopappmailservice@gmail.com", emailAddress);

            message.Subject = "Welcome";
            message.Body = "<h1> Welcome to our site! <h1>";
            message.IsBodyHtml = true;
            client.Send(message);
        }

        public void SendOrderConfirmation(string emailAddress, Order order)
        {

            MailMessage message = new MailMessage("giftshopappmailservice@gmail.com", emailAddress);

            message.Subject = "Order Confirmation";
            message.Body = "<p>Thank you for choosing us. Order number "+ order.Id +" has been registered </p>";
            message.IsBodyHtml = true;
            client.Send(message);
        }
    }
}