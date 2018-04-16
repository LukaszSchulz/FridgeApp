using FridgeWPF.ConcreteIngredients;
using System;

namespace FridgeWPF
{
    public class BeansFactory : AbstractIngredientFactory
    {
        public override string Name { get; protected set; } = "Beans";

        public override AbstractIngredient Create(double amount, DateTime expiryDate)
        {
            return new Beans(amount, expiryDate);
        }

        public override AbstractIngredient Create(double amount)
        {
            return new Beans(amount);
        }
    }
}

