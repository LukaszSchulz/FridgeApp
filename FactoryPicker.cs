using System.Collections.Generic;

namespace FridgeWPF
{
     sealed class FactoryPicker //SINGLETON umożliwiający wybieranie odpowiedniej fabryki na podstawie nazwy docelowego produktu
                                //przechowuje listę dostępnych, zaprogramowanych fabryk 
                                //trzeba tu dodać każdą nową fabrykę, która ma być wykorzystywana przez program
        {
            public List<AbstractIngredientFactory> listOfFactories = new List<AbstractIngredientFactory>();
                                                                            //przechowuje listę dostępnych fabryk
            private static FactoryPicker instance;//prywatna instancja singletona

            private FactoryPicker()
            {
            listOfFactories = new List<AbstractIngredientFactory>()//poszczególne fabryki dodawane są w konstruktorze
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

            public static FactoryPicker Instance//właściwość z publicznym getterem, umożliwiająca dostęp do Pickera
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

            public AbstractIngredientFactory Pick(string ingredientName) 
            {                                   //dobiera fabrykę na podstawie jej nazwy zakodowanej w bazie danych
                AbstractIngredientFactory pickedFactory=null;
                foreach(AbstractIngredientFactory IF in listOfFactories)//porównuje nazwę każdej fabryki z listy z nazwą podanego 
                {                                                       //składnika
                    if (ingredientName == IF.Name)
                    {
                        pickedFactory = IF;
                        return pickedFactory;
                    }
                }
                return pickedFactory; //zwraca wybraną fabrykę
            }
    }
}
