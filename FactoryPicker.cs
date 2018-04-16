using System.Collections.Generic;

namespace FridgeWPF
{
     sealed class FactoryPicker //SINGLETON przechowuje listę dostępnych, zaprogramowanych fabryk 
                                //trzeba tu dodać każdą nową fabrykę
        {
            public List<AbstractIngredientFactory> listOfFactories = new List<AbstractIngredientFactory>();
            private static FactoryPicker instance;

            private FactoryPicker()
            {
            listOfFactories = new List<AbstractIngredientFactory>()
            {
                new BeansFactory(),
                new CabbageFactory(),
                new CarrotFactory(),
                new CeleriacFactory(),
                new CheeseFactory(),
                new CucumberFactory(),
                new EggFactory(),
                new FlourFactory(),
                new LeekFactory(),
                new MilkFactory(),
                new ParsleyFactory(),
                new PeasFactory(),
                new TomatoesFactory()
            };
            }

            public static FactoryPicker Instance
            {
                get
                {
                    if(instance == null)
                    {
                        instance = new FactoryPicker();
                    }
                    return instance;
                }
            }

            public AbstractIngredientFactory Pick(string ingredientName) //dobiera fabrykę na podstawie jej nazwy zakodowanej w bazie danych
            {
                AbstractIngredientFactory pickedFactory=null;
                foreach(AbstractIngredientFactory IF in listOfFactories)
                {
                    if (ingredientName == IF.Name)
                    {
                        pickedFactory = IF;
                        return pickedFactory;
                    }
                }
                return pickedFactory;
            }
    }
}
