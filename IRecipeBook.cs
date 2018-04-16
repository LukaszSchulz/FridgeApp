using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeWPF
{
    public interface IRecipeBook
    {
        List<AbstractRecipe> Recipebook { get; }
    }
}
