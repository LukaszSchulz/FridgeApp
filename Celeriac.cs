using System;

namespace FridgeWPF.ConcreteIngredients
{
    public class Celeriac : AbstractIngredient
    {
        public Celeriac(double amount) : base(amount)
        {
        }

        public Celeriac(double amount, DateTime expiryDate) : base(amount, expiryDate)
        {
        }

        public override string Unit => "kg";
    }
}
