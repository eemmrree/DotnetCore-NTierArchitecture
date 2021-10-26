using System;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;

namespace ConsoleUI
{
    class Program
    {
        static void Main()
        {
            var productManager = new ProductManager(new EfProductDal());

            foreach (var item in productManager.GetAll())
            {
                Console.WriteLine(item.CategoryId+ " - " +item.ProductName + " -> " + item.UnitPrice);
            }

            Console.WriteLine("------------------Category----------------");

            foreach (var item in productManager.GetAllByCategoryId(3))
            {
                Console.WriteLine(item.ProductName + " -> " + item.UnitPrice);
            }

            Console.WriteLine("----------------Unit Price-----------------");

            foreach (var item in productManager.GetByUnitPrice(300,2000))
            {
                Console.WriteLine(item.ProductName + " -> " + item.UnitPrice);
            }

        }
    }
}
