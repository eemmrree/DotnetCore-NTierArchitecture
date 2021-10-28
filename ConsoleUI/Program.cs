using System;
using System.Diagnostics.Tracing;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;

namespace ConsoleUI
{
    class Program
    {
        static void Main()
        {
            ProductTest();

            CategoryTest();
        }

        private static void CategoryTest()
        {
            Console.WriteLine("--------Category---------------");

            var categoryManager = new CategoryManager(new EfCategoryDal());

            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryId + "->" + category.CategoryName);
            }

            Console.WriteLine("---------CategoryId = 7 -------------");

            Console.WriteLine(categoryManager.GetById(7).CategoryName);
        }

        private static void ProductTest()
        {
            var productManager = new ProductManager(new EfProductDal());

            foreach (var item in productManager.GetAll().Data)
            {
                Console.WriteLine(item.ProductName + " -> " + item.UnitPrice);
            }

            Console.WriteLine("------------------Category----------------");

            foreach (var item in productManager.GetAllByCategoryId(7).Data)
            {
                Console.WriteLine(item.CategoryId + " - " + item.ProductName + " -> " + item.UnitPrice);
            }

            Console.WriteLine("----------------Unit Price-----------------");

            foreach (var item in productManager.GetByUnitPrice(10, 2000).Data)
            {
                Console.WriteLine(item.ProductName + " -> " + item.UnitPrice);
            }



            Console.WriteLine("----------------ProductDetails-----------------");
            var results = productManager.GetProductDetails();
            if (results.Success==true)
            {
                foreach (var item in results.Data)
                {
                    Console.WriteLine(item.ProductName + " / " + item.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(results.Message);
            }

            
           
        }
    }
}
