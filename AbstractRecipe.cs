using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeWPF
{
    public abstract class AbstractRecipe//Szablon dla przepisów, czyli obiektów zawierających nazwę, opis i listę składników
                                        //konkretne egzemplarze tworzone są przy pomocy odpowiednich fabryk
    {
        public string Name { get; } //nazwa jest ustalana przy tworzeniu przepisu
        public List<AbstractIngredient> ListOfIngredients { get; protected set; }//lista zawierająca składniki z nazwą i ilością
        public string Description { get; protected set; }//opis jako dodatkowe informacje o przepisie
        
        public AbstractRecipe(string name, List<AbstractIngredient> listOfIngredients, string description)
        {
            Name = name;
            ListOfIngredients = listOfIngredients;
            Description = description;
        }
    }
}
