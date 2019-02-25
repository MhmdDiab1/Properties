using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class Properties
    {
        private static int CurrentId = 1;
        public int Id;
        internal string Title;
        public int Price;
        internal string Address;
        public string PropertyType;
        public bool Sold;

        public void Creat(string T, string Ad, string Pt)
        {
            Id = CurrentId;
            CurrentId += 1;
            Title = T;
            Address = Ad;
            PropertyType = Pt;
            Sold = false;
        }
        public void UpdateTitle(string T)
        {
            Title = T;
            Console.WriteLine("Title Updated:");
            this.write();
        }
        public void write()
        {
            Console.WriteLine(Id + "\t" + PropertyType + "\t" + Title + "\t" + Address + "\t" + Price);

        }
        public bool Sell()
        {
            try
            {
                if (Sold == true)
                {
                    throw new ArgumentException("Unable to complete purchase, property with ID " + Id + " has already been sold \n");
                }
                else
                {
                    Sold = true;
                    return true;
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Unable to complete purchase, property with ID " + Id + " has already been sold \n");
                return false;
            }

        }
    }


    public class Appartment : Properties
    {
        private int NbOfRooms;


        public Appartment(string T, string Ad, int Nbr)
        {
            Creat(T, Ad, "Appartment");
            NbOfRooms = Nbr;
            Price = Nbr * 15000;
        }

    }
    class Land : Properties
    {
        private int Area;
        private bool CanBeFarmed;

        public Land(string T, string Ad, int A, bool Cbf)
        {
            Creat(T, Ad, "Land");
            Area = A;
            CanBeFarmed = Cbf;
            Price = Area * 3000;
        }
    }


    class Shop : Properties
    {
        private int Area;
        private string Business;

        public Shop(string T, string Ad, int A, string B)
        {
            Creat(T, Ad, "Shop");
            Area = A;
            if (B != "Food" && B != "Repair" && B != "Retail")
            {
                throw new ArgumentException("Invalid Business Name plz reastart and enter a valid one");
            }
            else
            {
                Business = B;
            }
            if (Area > 50)
            {
                Price = 120000;
            }
            else
            {
                Price = 80000;
            }

        }
    }


    class Buyer
    {
        private string Name;
        private int Credit;
        private List<Properties> Property = new List<Properties>();

        public void Create(string N, int C)
        {
            Name = N;
            Credit = C;
        }

        public delegate void PurchaseEventHandler(object source, Properties P, string N);
        public event PurchaseEventHandler PurchaseDone;

        public void Buy(Properties P)
        {
            try
            {
                if (P.Price > Credit)
                {
                    throw new ArgumentException(Name + " has insufficient fund for this purshase \n");

                }
                else
                {
                    if (P.Sell() != false)
                    {
                        Property.Add(P);
                        Credit -= P.Price;
                        OnPurchaseDone(P);
                    }
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine(Name + " has insufficient fund for this purshase");
            }

        }
        public void write()
        {
            Console.WriteLine("Buyer:" + Name);
            Console.WriteLine("Nb of Owned Properties:" + Property.Count);
            Console.WriteLine("Remaining Credit:" + Credit + "\n");
        }
        protected virtual void OnPurchaseDone(Properties P)
        {
            if (PurchaseDone != null)
                PurchaseDone(this, P, Name);
        }

    }

}