using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeWPF
{
    public class MilkFactory : AbstractIngredientFactory
    {
        public override string Name { get; protected set; } = "Milk";
        public override AbstractIngredient Create(double amount, DateTime expiryDate)
        {
            return new Milk(amount, expiryDate);
        }

        public override AbstractIngredient Create(double amount)
        {
            return new Milk(amount);
        }
    }
}
