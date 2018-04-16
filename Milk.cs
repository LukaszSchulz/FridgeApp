using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeWPF
{
    public class Milk : AbstractIngredient
    {
        public Milk(double amount) 
            : base(amount)
        {
        }

        public Milk(double amount, DateTime expiryDate)
            : base(amount, expiryDate) { }

        public override string Unit => "liter(s)";
    }
}
