using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommodityScience
{
    public class Product
    {
        private string _productName;
        private string _manufacturer;
        private double _price;
        private int _shelfLife; 
        private DateTime _productionDate;

        public Product(string productName, string manufacturer, double price,
                      DateTime productionDate, int shelfLife)
        {
            Name = productName;
            Manufacturer = manufacturer;
            Price = price;
            ProductionDate = productionDate;
            ShelfLife = shelfLife;
        }

        public string Name
        {
            get => _productName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Наименование товара не может быть пустым");
                _productName = value;
            }
        }

        public string Manufacturer
        {
            get => _manufacturer;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Производитель не может быть пустым");
                _manufacturer = value;
            }
        }

        public double Price
        {
            get => _price;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Цена не может быть отрицательной");
                _price = value;
            }
        }

        public DateTime ProductionDate
        {
            get => _productionDate;
            private set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Дата производства не может быть в будущем");
                _productionDate = value;
            }
        }

        public int ShelfLife
        {
            get => _shelfLife;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Срок годности не может быть отрицательным");
                _shelfLife = value;
            }
        }

        public DateTime ExpirationDate => ProductionDate.AddDays(ShelfLife);

        public bool IsExpired()
        {
            return DateTime.Now > ExpirationDate;
        }

        public override string ToString()
        {
            string status = IsExpired() ? "Товар просрочен" : "Товар годен";
            return $"Наименование: {Name}\n" +
                   $"Производитель: {Manufacturer}\n" +
                   $"Цена: {Price} руб.\n" +
                   $"Статус: {status}";
        }
    }

    public class DiscountedProduct : Product
    {
        private double _discount;

        public DiscountedProduct(string productName, string manufacturer, double price,
                                DateTime productionDate, int shelfLife, double discount)
            : base(productName, manufacturer, price, productionDate, shelfLife)
        {
            Discount = discount;
        }

        public double Discount
        {
            get => _discount;
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Размер скидки должен быть от 0 до 100%");
                _discount = value;
            }
        }

        public double DiscountedPrice => Math.Round(Price * (1 - Discount / 100.0), 2);

        public override string ToString()
        {
            string status = IsExpired() ? "Товар просрочен" : "Товар годен";
            return $"Наименование: {Name}\n" +
                   $"Производитель: {Manufacturer}\n" +
                   $"Цена: {Price} руб.\n" +
                   $"Скидка: {Discount}%\n" +
                   $"Акционная цена: {DiscountedPrice} руб.\n" +
                   $"Статус: {status}";
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            int productCount = GetProductCount();

            Product[] products = new Product[productCount];

            for (int i = 0; i < products.Length; i++)
            {
                Console.WriteLine($"\n    Товар {i + 1}");
                products[i] = GetInfoAboutProduct(i + 1);
            }

            Console.WriteLine("\n     Добавленные товары:");

            foreach (var product in products)
            {
                Console.WriteLine(product + "\n");
            }

            DiscountedProduct[] discountedProducts = CreateDiscountedProducts();

            Console.WriteLine("\n     Акционные товары:");

            foreach (var product in discountedProducts)
            {
                Console.WriteLine(product + "\n");
            }
        }

        static int GetProductCount()
        {
            while (true)
            {
                Console.Write("Введите количество товаров, которые хотите добавить: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int count) && count > 0)
                {
                    return count;
                }
                else
                {
                    Console.WriteLine("Ошибка: введите положительное целое число.");
                }
            }
        }

        static Product GetInfoAboutProduct(int number)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine($"Введите данные о товаре");

                    Console.Write("Наименование: ");
                    string productName = Console.ReadLine();

                    Console.Write("Производитель: ");
                    string manufacturer = Console.ReadLine();

                    Console.Write("Цена: ");
                    double price = double.Parse(Console.ReadLine());

                    Console.Write("Дата производства (ДД.ММ.ГГГГ): ");
                    DateTime productionDate = DateTime.ParseExact(
                        Console.ReadLine(),
                        "dd.MM.yyyy",
                        System.Globalization.CultureInfo.InvariantCulture
                    );

                    Console.Write("Срок годности (в днях): ");
                    int shelfLife = int.Parse(Console.ReadLine());

                    Product product = new Product(productName, manufacturer, price,
                                                productionDate, shelfLife);

                    return product;
                }

                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: неверный формат данных. Проверьте ввод.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                Console.WriteLine("Повторите ввод информации о товаре\n");
            }
        }


        static DiscountedProduct[] CreateDiscountedProducts()
        {
            DiscountedProduct[] discountedProducts = new DiscountedProduct[2];

            discountedProducts[0] = new DiscountedProduct(
                "Молоко",
                "Молочный комбинат",
                89.90,
                new DateTime(2026, 7, 15),
                7,
                15
            );

            discountedProducts[1] = new DiscountedProduct(
                "Сыр",
                "Молочный комбинат",
                250.00,
                new DateTime(2026, 7, 8),
                30,
                25
            );

            return discountedProducts;
        }
    }
}
