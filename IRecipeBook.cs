using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeWPF
{
    public interface IRecipeBook // interfejs wymuszający posiadanie listy przepisów o nazwie Recipebook
    {
        List<AbstractRecipe> Recipebook { get; }
    }
}
