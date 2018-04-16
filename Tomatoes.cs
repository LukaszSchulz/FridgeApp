using System;

namespace FridgeWPF.ConcreteIngredients
{
    public class Tomatoes : AbstractIngredient
    {
        public Tomatoes()
        {
        }

        public Tomatoes(double amount) : base(amount)
        {
        }

        public Tomatoes(double amount, DateTime expiryDate) : base(amount, expiryDate)
        {
        }

        public override string Unit => "kg";
    }
}
