using FridgeWPF.ConcreteIngredients;
using System;

namespace FridgeWPF
{
    public class CeleriacFactory : AbstractIngredientFactory
    {
        public override string Name { get; protected set; } = "Celeriac";

        public override AbstractIngredient Create(double amount, DateTime expiryDate)
        {
            return new Celeriac(amount, expiryDate);
        }

        public override AbstractIngredient Create(double amount)
        {
            return new Celeriac(amount);
        }
    }
}

