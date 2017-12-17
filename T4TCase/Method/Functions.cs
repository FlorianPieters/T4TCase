using MailKit.Net.Smtp;
using MimeKit;
using System.Linq;
using T4TCase.Data;
using T4TCase.Model;
using T4TCase.ViewModel;

namespace T4TCase.Method
{
    public class Functions
    {
        public static void CompareCustomer(DatabaseContext context, Customer a, Customer b)
        {
            if (a.LastName != b.LastName
                    || a.FirstName != b.FirstName
                    || a.Email != b.Email
                    || a.PhoneNumber != b.PhoneNumber
                    || a.Age != b.Age
                    || a.Address != b.Address
                    || a.City != b.City)
            {
                a.LastName = b.LastName;
                a.FirstName = b.FirstName;
                a.Email = b.Email;
                a.PhoneNumber = b.PhoneNumber;
                a.Age = b.Age;
                a.Address = b.Address;
                a.City = b.City;
                context.SaveChanges();
            }
        }


        public static void CompareOrder(DatabaseContext context, Order a, OrderViewModel b)
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

            if (a.Description != b.Description || a.TotalPrice != b.TotalPrice)
            {
                a.Description = b.Description;
                a.TotalPrice = totalPrice;
                context.SaveChanges();
            }

            foreach (var item in b.Itemvms)
            {
                if (context.OrderItem.Any(x => x.OrderID == a.OrderID && x.ItemID == item.ItemID))
                {
                    var i = context.OrderItem.First(x => x.OrderID == a.OrderID && x.ItemID == item.ItemID);
                    if (i.Amount != item.Aantal)
                    {
                        if (item.Aantal == 0) context.OrderItem.Remove(i);
                        else i.Amount = item.Aantal;
                    }
                }
                else
                {
                    if (item.Aantal >= 0) context.OrderItem.Add(new OrderItem { OrderID = a.OrderID, ItemID = item.ItemID, Amount = item.Aantal });
                }
            }
            context.SaveChanges();
        }


        public static void SendMail(string email, string subject, string msg)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("t4tcase@gmail.com"));
            message.To.Add(new MailboxAddress(email));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = msg
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("t4tcase@gmail.com", "t4tcase123");
                client.Send(message);

                client.Disconnect(true);
            }
        }
    }
}
