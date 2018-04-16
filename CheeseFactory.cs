using FridgeWPF.ConcreteIngredients;
using System;

namespace FridgeWPF
{
    public class CheeseFactory : AbstractIngredientFactory
    {
        public override string Name { get; protected set; } = "Cheese";

        public override AbstractIngredient Create(double amount, DateTime expiryDate)
        {
            return new Cheese(amount, expiryDate);
        }

        public override AbstractIngredient Create(double amount)
        {
            return new Cheese(amount);
        }
    }
}

