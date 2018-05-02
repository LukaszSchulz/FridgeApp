using System;

namespace FridgeWPF.ConcreteIngredients
{
    public class Cheese : AbstractIngredient
    {
        public Cheese(double amount) : base(amount)
        {
        }

        public Cheese(double amount, DateTime expiryDate) : base(amount, expiryDate)
        {
        }

        public override string Unit => "kg";
    }
}
