using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeWPF
{
    public abstract class IngredientFactory
    {
        public abstract string name { get; }

        public abstract AbstractIngredient Create(double amount, DateTime expiryDate);
        
    }
}
