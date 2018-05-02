using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeWPF.ConcreteIngredients
{
    public class Carrot : AbstractIngredient
    {
        public Carrot(double amount) : base(amount)
        {
        }

        public Carrot(double amount, DateTime expiryDate) : base(amount, expiryDate)
        {
        }

        public override string Unit => "kg";
    }
}
