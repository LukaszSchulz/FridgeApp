using FridgeWPF.ConcreteIngredients;
using System;

namespace FridgeWPF
{
    public class PeasFactory : AbstractIngredientFactory
    {
        public override string Name { get; protected set; } = "Peas";

        public override AbstractIngredient Create(double amount, DateTime expiryDate)
        {
            return new Peas(amount, expiryDate);
        }

        public override AbstractIngredient Create(double amount)
        {
            return new Peas(amount);
        }
    }
}

