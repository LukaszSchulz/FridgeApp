using FridgeWPF.ConcreteIngredients;
using System;

namespace FridgeWPF
{
    public class CarrotFactory : AbstractIngredientFactory
    {
        public override string Name { get; protected set; } = "Carrot";

        public override AbstractIngredient Create(double amount, DateTime expiryDate)
        {
            return new Carrot(amount, expiryDate);
        }

        public override AbstractIngredient Create(double amount)
        {
            return new Carrot(amount);
        }
    }
}

