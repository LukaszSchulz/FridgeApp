using System;

namespace FridgeWPF.ConcreteIngredients
{
    public class Leek : AbstractIngredient
    {
        public Leek(double amount) : base(amount)
        {
        }

        public Leek(double amount, DateTime expiryDate) : base(amount, expiryDate)
        {
        }

        public override string Unit => "pcs.";
    }
}
