using System;

namespace FinishTask7
{
    // Использование абстрактных классов
    #region Abstract class
    public abstract class Product
    {
        //Использовантие свойств
        protected string Name;          //Наименование продукта
        protected string Code;          //Товарный код
        protected double Cost;          //Стоимость

        public Product(string name, string code, double cost)
        {
            Name = name;
            Code = code;
            Cost = cost;
        }   //Конструктор
        public static Product[] operator +(Product[] array, Product A)
        {
            if (array == null) return array = new Product[1] { A };
            Product[] NewArray = new Product[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                NewArray[i] = array[i];
            }
            NewArray[array.Length] = A;
            return array = NewArray;
        }   //Переопределение оператора
        public abstract void GetInformation();                        //Абстрактный метод
        public override string ToString()
        {
            string txt = "Наименование продукта: " + Name + "\n";
            txt += "Код продукта:" + Code + "\n";
            txt += "Стоимость: " + Cost;
            return txt;
        }                          //Переопределенный метод
        public double GetCost ()
        {
            return this.Cost;
        }
    }
    abstract class Delivery<T>             //Использование обобщенного класса
    {
        protected string AdressForDelivery { get; set; }
        public void ShowAdress()
        {
            Console.WriteLine(AdressForDelivery);
        }
        public abstract void ChoseAdress();
        public T NumberOfOrder { get; set; }
        public Delivery(T number)
        {
            NumberOfOrder = number;
        }
        

    }
    
    #endregion          
    //Использование статических классов
    #region static class
    public static class function
    {
        public static Product[] AddItem(Product[] Array, Product A)
        {
            return Array + A;
        }
        public static Product[] RemItem(Product[] Array)
        {
            Console.WriteLine("Укажите индекс удаляемого элемента.");
            byte number = (byte)(byte.Parse(Console.ReadLine())-1);
            if (number >= Array.Length||number<0) return Array;
            Array[number] = null;
            Product[] NewArray = new Product[Array.Length - 1];
            for(int i = 0,k=0; i < NewArray.Length; i++,k++)
            {
                if (Array[k] == null) k++;
                NewArray[i] = Array[k];
            }
            return NewArray;



        }
        public static void Show(object [] list)
        {
            for(int i = 0; i < list.Length; i++)
            {
                Console.WriteLine($"{i+1}."+list[i]);
            }
        }
    }
    public static class Information
    {
        public static string[] TypeOfDelivery = new string[] { "На дом.", "На точку.", "В магазин." };
        public static string[] StringCode = new string[] { "12AB-73", "90FE-00", "MI987-03", "PF111-42" };
        public static int[] IntCode = new int[] { 302 , 653, 821 , 942 ,010  };
        public static string[] AdressShp = new string[] {"Ленина 91","К.Маркса 15","Пер.Рабочий 207","Пр-т, Мира1" };
        public static string[] AdressPoint = new string[] { "Киевская 61", "Пер. Цветочный 16", "ул. Сибирская 120/2" };
        public static Product[] products = new Product[] {new Chair("Стул №1","12А-77",159.99,"Ткань типа Б"),
            new Table("Стол №1","89-СС3",250,"Береза"),new Chair("Стул №2","845-А",120.5,"Ткань типа А"),
        new Table("Стол №2","0213-П",399.9,"Дуб")};
        public static string[] OrderFunction = new string[]
        {
            "Посмотреть каталог",
            "Добавить в корзину",
            "Удалить из корзины",
            "Посмотреть корзину",
            "Сделать заказ",
            "Выход"
        };
    }
    #endregion

    //Использование наследования 
    #region Furniture:Product                  
    class Chair : Product
    {
        protected string Sheating;
        public Chair(string name, string code, double cost, string sheating) : base(name, code, cost)
        {
            Sheating = sheating;
        }           //Инкапсуляция
        public override void GetInformation()
        {
            Console.WriteLine(this);
            Console.WriteLine("Тип обшивки: " + Sheating);
        }
    }
    class Table : Product
    {
        protected string Material;
        public Table(string name, string code, double cost, string material) : base(name, code, cost)
        {
            Material = material;
        }
        public override void GetInformation()
        {
            Console.WriteLine(this);
            Console.WriteLine("Материал: " + Material);
        }

    }

    #endregion                                      

    #region OptionsOfDelivery
    //Наследование от обобщенного класса
    class HomeDelivery<T> : Delivery<T>
    {
        public override void ChoseAdress()
        {
            Console.WriteLine("Укажите Ваш адресс.");
            base.AdressForDelivery = Console.ReadLine();

        }
        public HomeDelivery(T number) : base(number)
        {
            Console.WriteLine("Выбрана доставка до дома.");
            ChoseAdress();
            base.ShowAdress();
        }
    }
    class PickPointDelivery<T> : Delivery<T>
    {
        public override void ChoseAdress()
        {
            Console.WriteLine("Выберете адресс доставки на точку.");
            Console.WriteLine("Укажите цифрой");
            function.Show(Information.AdressPoint);
            base.AdressForDelivery = Information.AdressPoint[(byte)(Byte.Parse(Console.ReadLine())-1)];

        }
        public PickPointDelivery(T number) : base(number)
        {
            Console.WriteLine("Выбрана доставка на точку.");
            ChoseAdress();
            base.ShowAdress();
        }
    }
    class ShopDelivery<T> : Delivery<T>
    {
        public override void ChoseAdress()
        {
            Console.WriteLine("Выберете магазин.");
            Console.WriteLine("Укажите цифрой");
            function.Show(Information.AdressShp);
            base.AdressForDelivery = Information.AdressShp[(byte)(Byte.Parse(Console.ReadLine())-1)];
            
        }
        public ShopDelivery(T number) : base(number)
        {
            Console.WriteLine("Выбрана доставка в магазин");
            ChoseAdress();
            base.ShowAdress();
        }
    }
    #endregion

    class Order
    {
        Product[] BasketList=null;
        Random rnd = new Random();
        
        public Order()
        {
            Control();
        }
        public Order(Product A)       //Использование агрегации  
        {
            AddItemInBasket(A);
        }
        public Order(Product[]List)
        {
            BasketList = List;
        }
        private Product this[byte number]
        {
            get { return BasketList[number]; }
        }           //Использование индексаторов
        private void AddItemInBasket(Product product)
        {
            BasketList = function.AddItem(BasketList, product);
        }
        private void Finish(Delivery<object> A)
        {
            Console.WriteLine("В вашей корзине:");
            function.Show(BasketList);
            double FinishCostOrder = 0;
            for(int i = 0; i < BasketList.Length; i++)
            {
                FinishCostOrder += BasketList[i].GetCost();
            }
            Console.WriteLine($"Сумма заказа:{FinishCostOrder}");
            Console.WriteLine($"Номер заказа:{A.NumberOfOrder}");
            A.ShowAdress();
           
        }
        private void DelItemInBasket()
        {
            BasketList = function.RemItem(BasketList);
        }
        Delivery<Object> delivery;                              //Использование композиции
        private void MakeOrder()
        {
            Console.WriteLine("Выберете тип доставки.\nУказать цифру.");
            function.Show(Information.TypeOfDelivery);
            string Answer = Console.ReadLine();
            switch (Answer)
            {
                case "1":
                    delivery = new HomeDelivery<object>(Information.StringCode[rnd.Next(0,Information.StringCode.Length+1)]);
                    break;
                case "2":
                    delivery = new PickPointDelivery<object>(Information.IntCode[rnd.Next(0, Information.StringCode.Length + 1)]);
                    break;
                case "3":
                    delivery = new ShopDelivery<object>("A30B"+rnd.Next(3213,9217));
                    break;
            }
            Finish(delivery);
            BasketList = null;

        }
        
        public void Control()
        {
            string answer="";
            Console.WriteLine("Доброго времени суток! Перечень функций доступных пользователю:");
     
          
            while (answer != "6")
            {
                function.Show(Information.OrderFunction);
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Укажите операцию(Цифра).");
                
                Console.WriteLine("--------------------------------------");
                Console.Write("Команда №");
                answer = Console.ReadLine();
                switch (answer)
                {
                    case "6":
                        Console.WriteLine("ВЫХОД");
                        break;
                    case "1":
                        Console.WriteLine("КАТАЛОГ");
                        function.Show(Information.products);
                        Console.WriteLine("--------------------------------------");
                        break;
                    case "2":
                        Console.WriteLine("ДОБАВЛЕНИЕ В КОРЗИНУ");
                        function.Show(Information.products);
                        Console.WriteLine("Укажите номер , интересующего Вас продукта");
                        byte number = (byte)(Byte.Parse(Console.ReadLine())-1);
                        AddItemInBasket(Information.products[number]);
                        Console.WriteLine("--------------------------------------");
                        break;
                    case "3":
                        Console.WriteLine("УДАЛЕНИЕ ИЗ КОРЗИНЫ");
                        function.Show(BasketList);
                     
                        DelItemInBasket();
                        function.Show(BasketList);
                        Console.WriteLine("--------------------------------------");
                        break;
                   case "4":
                        Console.WriteLine("КОРЗИНА");
                        if (BasketList != null) function.Show(BasketList);
                        else
                        {
                            Console.WriteLine("Корзина пустая");
                            Console.WriteLine("--------------------------------------");
                        }
                        break;
                    case "5":
                        Console.WriteLine("ОФОРМЛЕНИЕ ЗАКАЗА");
                        MakeOrder();
                        Console.WriteLine("--------------------------------------");
                        break;
                    default:
                        Console.WriteLine("Неопознанная команда");
                        break;
                        Console.WriteLine("--------------------------------------");
                }

            }
        }
    }
   class programm
    {
        static void Main()
        {
            Product [] list = Information.products;
            Order order = new Order();
            
           
        }
    }
}


