using FridgeWPF.ConcreteIngredients;
using System;

namespace FridgeWPF
{
    public class ParsleyFactory : AbstractIngredientFactory
    {
        public override string Name { get; protected set; } = "Parsley";

        public override AbstractIngredient Create(double amount, DateTime expiryDate)
        {
            return new Parsley(amount, expiryDate);
        }

        public override AbstractIngredient Create(double amount)
        {
            return new Parsley(amount);
        }
    }
}

