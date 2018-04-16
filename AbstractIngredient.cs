using System;

namespace FridgeWPF
{
    public abstract class AbstractIngredient 
    {
        public string Name { get { return ShortName(this.ToString()); } }
        public double Amount { get; private set; }
        public DateTime ExpiryDate { get; }
        public abstract string Unit { get; }

        public AbstractIngredient()
        {

        }

        public AbstractIngredient(double amount)
        {
            Amount = amount;
        }

        public AbstractIngredient(double amount, DateTime expiryDate)
        {
            Amount = amount;
            ExpiryDate = expiryDate;
        }
        string ShortName(string longName)
        {
            string shortName;

            string[] dividedName = longName.Split('.');

            shortName = dividedName[(dividedName.Length - 1)];

            return shortName;
        }

        public double AddAmount(double amount)
        {
            return Amount += amount;
        }

        public double TakeAmount(double amount)
        {
            return Amount -= amount;
        }
    }
}