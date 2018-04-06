using System;

namespace FridgeWPF
{
    public abstract class AbstractIngredient //każdą, nowo dodaną fabrykę trzeba koniecznie dodać do FridgePickera
    {
        public string Name { get { return this.ToString(); } }
        public double Amount { get; }
        public DateTime ExpiryDate { get; }

        public AbstractIngredient(double amount, DateTime expiryDate)
        {
            Amount = amount;
            ExpiryDate = expiryDate;
        }
    }
}