using System;

namespace FridgeWPF.ConcreteIngredients
{
    public class Beans : AbstractIngredient
    {
        public Beans()
        {
        }

        public Beans(double amount) : base(amount)
        {
        }

        public Beans(double amount, DateTime expiryDate) : base(amount, expiryDate)
        {
        }

        public override string Unit => "kg";
    }
}
