using System;

namespace FridgeWPF
{
    public class Egg : AbstractIngredient
    {

        public Egg(double amount) : base(amount)
        {
        }

        public Egg(double amount, DateTime expiryDate)
            : base(amount, expiryDate) { }

        public override string Unit => "pcs.";
    }
}
