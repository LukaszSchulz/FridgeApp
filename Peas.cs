using System;

namespace FridgeWPF.ConcreteIngredients
{
    public class Peas : AbstractIngredient
    {
        public Peas()
        {
        }

        public Peas(double amount) : base(amount)
        {
        }

        public Peas(double amount, DateTime expiryDate) : base(amount, expiryDate)
        {
        }

        public override string Unit => "kg";
    }
}
