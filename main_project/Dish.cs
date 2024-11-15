using System;

namespace main_project
{
    internal class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Composition { get; set; }
        public string Weight { get; set; }
        public double Price { get; set; }
        public Category DishCategory { get; set; }
        public int CookingTime { get; set; }
        public string[] Type { get; set; }

        public Dish(int id, string name, string composition, string weight, double price, Category category, int cookingTime, string[] type)
        {
            this.Id = id;
            this.Name = name;
            this.Composition = composition;
            this.Weight = weight;
            this.Price = price;
            this.DishCategory = category;
            this.CookingTime = cookingTime;
            this.Type = type;
        }
        public void UpdateDishInfo(string name, string composition, string weight, double price, Category category, int cookingTime, params string[] type)
        {
            this.Name = name;
            this.Composition = composition;
            this.Weight = weight;
            this.Price = price;
            this.DishCategory = category;
            this.CookingTime = cookingTime;
            this.Type = type;
        }
        public void PrintInfo()
        {
            Console.Write($"\nНазвание блюда: {this.Name}\nСостав: {this.Composition}\nВес: {this.Weight}\nЦена: {this.Price}\nКатегория: {this.DishCategory}\nВремя готовки: {this.CookingTime} мин.\nТип: ");
            foreach ( var item in this.Type )
            {
                Console.Write( item + "; ");
            }
            Console.WriteLine();
        }
        public void Cancel()
        {
            Console.WriteLine($"Блюдо {this.Id} удалено.");
        }
    }
}
