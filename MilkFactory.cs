using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeWPF
{
    public class MilkFactory : IngredientFactory
    {
        public override string name { get; } = "Milk";
        public override AbstractIngredient Create(double amount, DateTime expiryDate)
        {
            return new Milk(amount, expiryDate);
        }
    }
}
