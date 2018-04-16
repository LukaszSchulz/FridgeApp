using System;

namespace FridgeWPF
{
    public class Flour : AbstractIngredient
    {
        public Flour(double amount) : base(amount)
        {
        }

        public Flour(double amount, DateTime expiryDate)
            : base(amount, expiryDate) { }

        public override string Unit => "kg";
    }
}
