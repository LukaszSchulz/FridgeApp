using FridgeWPF.ConcreteIngredients;
using System;

namespace FridgeWPF
{
    public class CucumberFactory : AbstractIngredientFactory
    {
        public override string Name { get; protected set; } = "Cucumber";

        public override AbstractIngredient Create(double amount, DateTime expiryDate)
        {
            return new Cucumber(amount, expiryDate);
        }

        public override AbstractIngredient Create(double amount)
        {
            return new Cucumber(amount);
        }
    }
}

