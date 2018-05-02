using System;

namespace FridgeWPF
{
    public abstract class AbstractIngredient //Szablon dla składników, które znajdują się na listach w przepisach i w lodówce
    {                                        //konkretne egzemplarze tworzone są przy pomocy odpowiednich fabryk
        public string Name { get { return ShortName(this.ToString()); } } //pole nazwa wypełnia się na podstawie nazwy konkretnego
                                                                          //składnika. Ma to na celu ułatwienie późniejszej 
                                                                          //pracy na instancji tej klasy
        public double Amount { get; private set; }  //oznacza liczbę jednostek danego składnika
        public DateTime ExpiryDate { get; } //oznacza datę przydatności danego składnika, na podstawie której program 
                                            //może selekcjonować starsze i nowsze składniki
        public abstract string Unit { get; } //nazwa jednostki w której podana jest ilość składnika. 
                                             //Jest właściwością wypełnianą przy tworzeniu konkretnej klasy

        public AbstractIngredient(double amount) //konstruktor wykorzystywany przez listę w przepisie - 
        {                                           //nie potrzebuje właściwości Expiry date
            Amount = amount;
        }

        public AbstractIngredient(double amount, DateTime expiryDate)// konstruktor wykorzystywany przez listę w lodówce
        {
            Amount = amount;
            ExpiryDate = expiryDate;
        }

        string ShortName(string longName)//metoda tworząca nazwę instancji konkternej klasy na podstawie nazwy klasy
        {
            string shortName;

            string[] dividedName = longName.Split('.');//rozdziela ścieżkę nazwy klasy

            shortName = dividedName[(dividedName.Length - 1)];//wykorzystuje ostatni moduł nazwy klasy

            return shortName;
        }

        public double AddAmount(double amount) //metoda umożliwiająca zwiększanie ilości danego składnika
        {
            return Amount += amount;
        }

        public double TakeAmount(double amount) //metoda umożliwiająca odejmowanie ilości od danego składnika
        {
            return Amount -= amount;
        }
    }
}