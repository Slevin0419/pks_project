using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main_project
{
    internal class Order
    {
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public List<Dish> Dishes { get; set; } = new List<Dish>();
        public string Comment { get; set; }
        public string OrderTime { get; set; }
        public int WaiterId { get; set; }
        public string ClosingTime { get; set; }
        public double TotalPrice { get; set; }
        public Order(int orderId, int tableId, List<Dish> dishes, string comment, string orderTime, int waiterId)
        {
            OrderId = orderId;
            TableId = tableId;
            Dishes = dishes;
            Comment = comment;
            OrderTime = orderTime;
            WaiterId = waiterId;
            foreach (var dish in dishes)
            {
                this.TotalPrice += dish.Price;
            }
        }
        public void UpdateOrder(int tableId, List<Dish> dishes, string comment, int waiterId)
        {
            TableId = tableId;
            Dishes = dishes;
            Comment = comment;
            WaiterId = waiterId;
            TotalPrice = 0;
            foreach (var dish in dishes)
            {
                this.TotalPrice += dish.Price;
            }
        }
        public void PrintInfo()
        {
            Console.WriteLine($"ID стола: {this.TableId}\nЗаказанные блюда: ");
            foreach (var dish in Dishes)
            {
                Console.WriteLine(dish.Name);
            }
            Console.WriteLine($"Комментарий: {this.Comment}\nВремя принятия заказа: {this.OrderTime}\nID официанта: {this.WaiterId}");
            if (this.ClosingTime != null)
            {
                Console.WriteLine($"Время закрытия заказа: {this.ClosingTime}");
            }
            Console.WriteLine($"Итоговая стоимость: {this.TotalPrice}");
        }
        public void CloseOrder(in string closingTime)
        {
            this.ClosingTime = closingTime;
            Console.WriteLine($"Заказ {this.OrderId} закрыт.");
        }
        public void PrintReceipt(out Dictionary<Dish, int> dishCount, ref string output)
        {
            dishCount = new Dictionary<Dish, int>();

            if (this.ClosingTime == null)
            {
                output = "Заказ еще не закрыт.";
                return;
            }
            foreach (var dish in Dishes)
            {
                if (dishCount.ContainsKey(dish))
                {
                    dishCount[dish]++;
                }
                else
                {
                    dishCount[dish] = 1;
                }
            }
        }
    }
}
