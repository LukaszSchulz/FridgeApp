using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeWPF
{
    public abstract class AbstractIngredientFactory //każdą, nowo dodaną fabrykę trzeba koniecznie dodać do FactoryPickera
    {
        public abstract string Name { get; protected set; }

        public abstract AbstractIngredient Create(double amount, DateTime expiryDate);
        public abstract AbstractIngredient Create(double amount);
    }
}
