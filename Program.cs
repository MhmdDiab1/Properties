using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Program
    {

        static void Main()
        {

            var Notification = new NotificationCenter();

            List<Properties> P = new List<Properties>();

            P.Add(new Appartment("First Appartment", "First Address", 2));
            P.Add(new Appartment("Second Appartment", "Second Address", 3));
            P.Add(new Appartment("Third Appartment", "Third Address", 4));
            P.Add(new Appartment("Fourth Appartment", "Fourth Address", 5));
            P.Add(new Appartment("Fifth Appartment", "Fifth Address", 6));
            P.Add(new Land("First Land", "First Address", 50, true));
            P.Add(new Land("Second Land", "Second Address", 100, false));
            P.Add(new Shop("First Shop", "First Address", 50, "Food"));
            P.Add(new Shop("Second Shop", "Second Address", 100, "Repair"));
            P.Add(new Shop("Third Shop", "Third Address", 150, "Retail"));
            Buyer B1 = new Buyer();
            B1.Create("First Buyer", 60000);
            Buyer B2 = new Buyer();
            B2.Create("Second Buyer", 10000);
            Buyer B3 = new Buyer();
            B3.Create("Third Buyer", 400000);

            Console.WriteLine("\n List of Properties:\n");
            foreach (Properties p in P)
            {
                p.write();
            }

            Console.WriteLine("\n List of Lands:\n");
            foreach (Properties p in P)
            {
                if (p.PropertyType == "Land")
                    p.write();
            }

            Console.WriteLine("\n Properties whose price between 45000 and 100000:\n");
            foreach (Properties p in P)
            {
                if (p.Price >= 45000 && p.Price <= 100000)
                    p.write();
            }
            Console.WriteLine("\n \n");
            B1.PurchaseDone += Notification.OnBuying;
            B2.PurchaseDone += Notification.OnBuying;
            B3.PurchaseDone += Notification.OnBuying;
            B1.Buy(P[2]);
            B2.Buy(P[1]);
            B3.Buy(P[5]);
            B3.Buy(P[6]);
            B3.Buy(P[7]);
            B3.Buy(P[2]);

            Console.WriteLine("\n Buyer List:\n");
            B1.write();
            B2.write();
            B3.write();
            Console.WriteLine("\n");
            Properties P1 = P.Find(p => p.Id == 2);
            P1.UpdateTitle("New Appartment");
            Console.WriteLine("\n \n");

            Random rand = new Random();
            while (true)
            {
                int i = rand.Next(0, P.Count);

                if (P[i].Sold == false)
                {
                    Console.WriteLine("Property with ID " + P[i].Id + " has been removed !");
                    P.RemoveAt(i);

                }
                else
                {
                    if (P[i].Sold == true)
                        Console.WriteLine("Property with ID " + P[i].Id + " is an owned property, it cant be removed !");
                }
                if (P.Count == 8)
                    break;
            }

            Console.WriteLine("\n List of Properties:\n");
            foreach (Properties p in P)
            {
                p.write();
            }
            Console.Read();

        }

        public class NotificationCenter
        {
            internal void OnBuying(object source, Properties P, string Name)
            {
                Console.WriteLine(P.PropertyType + " with ID " + P.Id + " was purshased by " + Name + " for " + P.Price);
            }
        }
    }
    
}