using FridgeWPF.ConcreteIngredients;
using System;

namespace FridgeWPF
{
    public class LeekFactory : AbstractIngredientFactory
    {
        public override string Name { get; protected set; } = "Leek";

        public override AbstractIngredient Create(double amount, DateTime expiryDate)
        {
            return new Leek(amount, expiryDate);
        }

        public override AbstractIngredient Create(double amount)
        {
            return new Leek(amount);
        }
    }
}

