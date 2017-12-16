using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T4TCase.Model;
using T4TCase.Data;
using T4TCase.ViewModel;

using MailKit.Net.Smtp;
using MimeKit;

namespace T4TCase.Method
{
    public class Functions
    {
        public static void CompareCustomer(DatabaseContext context, Customer a, Customer b)
        {
            
            if (a.LastName != b.LastName
                    || a.FirstName != b.FirstName
                    || a.Email != b.Email
                    || a.PhoneNumer != b.PhoneNumer
                    || a.Age != b.Age
                    || a.Address != b.Address
                    || a.City != b.City)
            {
                a.LastName = b.LastName;
                a.FirstName = b.FirstName;
                a.Email = b.Email;
                a.PhoneNumer = b.PhoneNumer;
                a.Age = b.Age;
                a.Address = b.Address;
                a.City = b.City;
                context.SaveChanges();
            }
        }
        public static void CompareOrder(DatabaseContext context,Order a, OrderViewModel b)
        {
            decimal totalPrice = 0;
            foreach (var item in b.Itemvms)
            {
                if (item.Aantal > 0)
                {
                    decimal price = item.Price;
                    price = price * item.Aantal;
                    totalPrice += price;

                }
            }
            if (a.Description != b.Description || a.TotalPrice!=b.TotalPrice)
            {
                a.Description = b.Description;
                a.TotalPrice = totalPrice;
                context.SaveChanges();
            }

            foreach (var item in b.Itemvms)
            {
                if (item.Aantal > 0)
                {
                   // var test = a.OrderID;
                   // var i = context.OrderItem.Any(x=>x.OrderID == a.OrderID && x.ItemID == item.ItemID);
                    if (context.OrderItem.Any(x => x.OrderID == a.OrderID && x.ItemID == item.ItemID))
                    {
                        var i = context.OrderItem.First(x => x.OrderID == a.OrderID && x.ItemID == item.ItemID);
                        if (i.Amount != item.Aantal)
                        {
                            i.Amount = item.Aantal;
                        }

                        //context.SaveChanges();
                    }
                    else
                    {
                        context.OrderItem.Add(new OrderItem { OrderID = a.OrderID, ItemID = item.ItemID, Amount = item.Aantal });
                    }
                }
                
            }

            context.SaveChanges();

        }
        public static void SendMail(string Email,string Subject, string Message )
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("t4tcase@gmail.com"));
            message.To.Add(new MailboxAddress(Email));
            message.Subject = Subject;
            message.Body = new TextPart("html")
            {
                Text = Message
            };
            using (var client = new SmtpClient())
            {

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("t4tcase@gmail.com", "t4tcase123");
                client.Send(message);

                client.Disconnect(true);

            }



         /*   try
            {
                using (SmtpClient smtpClient = new SmtpClient())
                {

               

                    smtpClient.Connect("smtp.gmail.com", 587, false);
                    smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    smtpClient.Authenticate("t4tcase@gmail.com", "t4tcase123");
                    smtpClient.Send(message);
                    smtpClient.Disconnect(true);
                }
            }

            catch { }*/
        }
    }
    
}
