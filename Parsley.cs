using System;

namespace FridgeWPF.ConcreteIngredients
{
    public class Parsley : AbstractIngredient
    {
        public Parsley(double amount) : base(amount)
        {
        }

        public Parsley(double amount, DateTime expiryDate) : base(amount, expiryDate)
        {
        }

        public override string Unit => "kg";
    }
}
