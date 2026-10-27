namespace Assignment_13
{
    internal class Program
    {

        public static List<Product> SearchProducts(List<Product> products, Func<Product, bool> filter)
        {
            List<Product> result = new List<Product>();
            foreach (Product product in products)
            {
                if (filter(product))
                {
                    result.Add(product);
                }
            }
            return result;
        }

        public static List<TResult> TransformProducts<TResult>(List<Product> products, Func<Product, TResult> transform)
        {
            List<TResult> results = new List<TResult>();
            foreach (Product product in products)
            {
                results.Add(transform(product));
            }
            return results;
        }

        public static List<Product> FilterProducts(List<Product> products , Predicate<Product> predicate)
        {
            List<Product> newResult = new List<Product>();
            foreach(Product product in products)
            {
                if (predicate(product))
                {
                    newResult.Add(product);
                }
            }
            return newResult;
        }
         
        // ------------  Main   --------------//
        static void Main(string[] args)
        {

            List<Product> catalog = new()
            {
                new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 1200, Stock = 10 },
                new Product { Id = 2, Name = "Phone", Category = "Electronics", Price = 800, Stock = 25 },
                new Product { Id = 3, Name = "T-Shirt", Category = "Clothing", Price = 30, Stock = 100 },
                new Product { Id = 4, Name = "Jeans", Category = "Clothing", Price = 60, Stock = 50 },
                new Product { Id = 5, Name = "Chocolate", Category = "Food", Price = 5, Stock = 200 },
                new Product { Id = 6, Name = "Coffee Beans", Category = "Food", Price = 15, Stock = 80 },
                new Product { Id = 7, Name = "C# Book", Category = "Books", Price = 45, Stock = 30 },
                new Product { Id = 8, Name = "Novel", Category = "Books", Price = 20, Stock = 60 },
                new Product { Id = 9, Name = "Headphones", Category = "Electronics", Price = 150, Stock = 40 }

            };

            #region Task 01 : Smart Product Search


            List<Product> electronics = SearchProducts(catalog, p => p.Category == "Electronics");

            List<Product> under50 = SearchProducts(catalog, p => p.Price < 50);

            List<Product> inStock = SearchProducts(catalog, p => p.Stock > 0);

            List<Product> clothingUnder100 = SearchProducts(catalog, p => p.Category == "Clothing" && p.Price < 100);


            static void PrintProducts(string title, List<Product> products)
            {
                Console.WriteLine($"\n--- {title} ---");

                foreach (Product product in products)
                {
                    Console.WriteLine(
                        $"{product.Name} - ${product.Price} (Stock: {product.Stock})"
                    );
                }
            }

            PrintProducts("Electronics", electronics);
            PrintProducts("Under $50", under50);
            PrintProducts("In Stock", inStock);
            PrintProducts("Clothing Under $100", clothingUnder100);

            #endregion


            #region Task 3.1 : Custom Report Generator

            static void PrintReport(List<Product> products, Action<Product> printAction)
            {
                foreach (Product product in products)
                {
                    printAction(product);
                }
            }

            Console.WriteLine("\n--- Short Report ---");
            PrintReport(catalog, p => Console.WriteLine($"{p.Name} - ${p.Price}"));

            Console.WriteLine("\n--- Detailed Report ---");
            PrintReport(catalog, p => Console.WriteLine($"[{p.Category}] {p.Name} | Price: ${p.Price} | Stock: {p.Stock}"));

            #endregion


            #region Task 3.2 : Transform Products

            Console.WriteLine("\n--- Summary List ---");

            List<string> productSummaries = TransformProducts(catalog, p => $"{p.Name} (${p.Price})");
            foreach (string summary in productSummaries)
            {
                Console.WriteLine(summary);
            }

            Console.WriteLine("\n--- Price Labels ---");

            List<string> priceLabels = TransformProducts(catalog, p => p.Price >100 ? "Expensive!" : "Affordable");

                for(int i=0; i<catalog.Count; i++)
                {
                    Console.WriteLine($"{catalog[i].Name}: {priceLabels[i]}");
                }

            #endregion


            #region Task 3.3: Filter Products

            Console.WriteLine("--- Low-Stock Alert ---");

            List<Product> lowStockProducts = FilterProducts(catalog , p => p.Stock < 20);
            foreach(Product product in lowStockProducts)
            {
                Console.WriteLine($"[LOW STOCK] {product.Name} : only {product.Stock} left! ");
            }

            #endregion
        }

    }
}
