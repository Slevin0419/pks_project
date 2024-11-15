using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace main_project
{
    public enum Category
    {
        Напитки,
        Салаты,
        ХолодныеЗакуски,
        ГорячиеЗакуски,
        Супы,
        ВторыеБлюда,
        Десерты
    }
    class Program
    {
        static List<Table> tables = new List<Table>();
        static List<Reservation> reservations = new List<Reservation>();
        static List<Dish> dishes = new List<Dish>();
        static List<Order> orders = new List<Order>();
        public static void CreateReservation()
        {
            Console.Write("Имя для брони: ");
            string? name = Console.ReadLine();
            Console.Write("Номер телефона: ");
            string? phoneNumber = Console.ReadLine();
            Console.Write("Начало брони(время в формате: целое число часов): ");
            int startTime = Convert.ToInt32(Console.ReadLine());
            Console.Write("Конец брони(время в формате: целое число часов): ");
            int endTime = Convert.ToInt32(Console.ReadLine());
            Console.Write("Комментарий: ");
            string? comment = Console.ReadLine();
            Console.Write("Назначенный столик: ");
            int tableId = Convert.ToInt32(Console.ReadLine());
            Reservation reservation = new Reservation(reservations.Count + 1, name, phoneNumber, startTime, endTime, comment, tableId);
            foreach(var table in tables)
            {
                if (table.Id == tableId)
                {
                    if (table.Reserve(reservation))
                    {
                        reservations.Add(reservation);
                        Console.WriteLine("Столик успешно забронирован.");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("На данное время невозможно забронировать данный столик.");
                        return;
                    }
                }
            }
            Console.WriteLine("Назначенный столик не найден.");
            return;
        }
        public static void CreateTable()
        {
            Console.Write("Расположение стола: ");
            string? placement = Console.ReadLine();
            Console.Write("Кол-во мест: ");
            int numberOfSeats = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Создан стол {tables.Count + 1}");
            tables.Add(new Table(tables.Count+1, placement, numberOfSeats));
        }
        public static void CreateSetOfTables()
        {
            Console.Write("Введите кол-во столов, которое вы хотите создать: ");
            int numberOfTables = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= numberOfTables; i++)
            {
                Console.WriteLine($"\nСоздание стола {i}.");
                CreateTable();
            }
        }

        public static void CreateSetOfReservations()
        {
            Console.Write("\nВведите кол-во бронирований, которое вы хотите создать: ");
            int numberOfReservations = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= numberOfReservations; i++)
            {
                Console.WriteLine($"\nБронь {i}.");
                CreateReservation();
            }
        }
        static void PrintTableInfo(int tableId)
        {
            var table = tables.FirstOrDefault(t => t.Id == tableId);
            table?.PrintInfo();
        }
        static void ChangeTableInfo(int tableId, string placement, int numberOfSeats)
        {

            var table = tables.FirstOrDefault(t => t.Id == tableId);
            if (table.IsReserved(9, 18))
            {
                Console.WriteLine("Стол находится в активном бронировании.");
            }
            table?.ChangeInfo(placement, numberOfSeats);
        }
        static void PrintAllReservations()
        {
            Console.WriteLine("Все бронирования:");
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"ID: {reservation.Id}, Имя: {reservation.Name}, Телефон: {reservation.PhoneNumber}");
            }
        }
        static void SearchReservations(string lastFourDigits, string name)
        {
            Console.WriteLine("Информация о бронировании: ");
            foreach (var reservation in reservations)
            {
                if ((reservation.Name == name) && (reservation.PhoneNumber.Substring(reservation.PhoneNumber.Length - 4) == lastFourDigits))
                {
                    Console.WriteLine($"{reservation.Id}, {reservation.Name}, {reservation.PhoneNumber}. Время брони: с {reservation.ReservationStartTime} до {reservation.ReservationEndTime}");
                    break;
                }
            }
        }
        static void CancelReservation(int id)
        {
            var reservationToCancel = reservations.Find(r => r.Id == id);
            if (reservationToCancel != null)
            {
                var table = tables.FirstOrDefault(t => t.Id == reservationToCancel.TableId);
                table?.DeleteReservation(reservationToCancel);
                reservationToCancel.Cancel();
                reservations.Remove(reservationToCancel);
            }
            else
            {
                Console.WriteLine($"Бронь {id} не найдена.");
            }
        }
        static void PrintTablesWithFilterPlacement(string placement)
        {
            Console.WriteLine($"\nСтолы с расположением {placement}:");
            foreach (var table in tables)
            {
                if (table.Placement == placement) Console.WriteLine($"ID {table.Id}, {table.Placement}, мест: {table.NumberOfSeats}");
            }
        }
        static void PrintTablesWithFilterNumberOfSeats(int numberOfSeats)
        {
            Console.WriteLine($"\nСтолы с кол-вом мест: {numberOfSeats}:");
            foreach (var table in tables)
            {
                if (table.NumberOfSeats == numberOfSeats) Console.WriteLine($"ID {table.Id}, {table.Placement}, мест: {table.NumberOfSeats}");
            }
        }
        public static void CreateDish()
        {
            Console.Write("Название: ");
            string name = Console.ReadLine();
            Console.Write("Состав: ");
            string composition = Console.ReadLine();
            Console.Write("Вес(формат: 100/20/50): ");
            string weight = Console.ReadLine();
            Console.Write("Цена: ");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.Write("Выберите к какой категории относится блюдо: \n1 - Напитки\n2 - Салаты\n3 - Холодные закуски\n4 - Горячие закуски\n5 - Супы\n6 - Вторые блюда\n7 - Десерты\nВведите номер категории: ");
            int numberOfCategory = Convert.ToInt32(Console.ReadLine());
            Console.Write("Время приготовления(целое число минут): ");
            int cookingTime = Convert.ToInt32(Console.ReadLine());
            Console.Write("Тип (слова через пробел): ");
            string input = Console.ReadLine();
            string[] type = input.Split(' ');
            switch (numberOfCategory)
            {
                case 1:
                    dishes.Add(new Dish(dishes.Count + 1, name, composition, weight, price, Category.Напитки, cookingTime, type));
                    break;
                case 2:
                    dishes.Add(new Dish(dishes.Count + 1, name, composition, weight, price, Category.Салаты, cookingTime, type));
                    break;
                case 3:
                    dishes.Add(new Dish(dishes.Count + 1, name, composition, weight, price, Category.ХолодныеЗакуски, cookingTime, type));
                    break;
                case 4:
                    dishes.Add(new Dish(dishes.Count + 1, name, composition, weight, price, Category.ГорячиеЗакуски, cookingTime, type));
                    break;
                case 5:
                    dishes.Add(new Dish(dishes.Count + 1, name, composition, weight, price, Category.Супы, cookingTime, type));
                    break;
                case 6:
                    dishes.Add(new Dish(dishes.Count + 1, name, composition, weight, price, Category.ВторыеБлюда, cookingTime, type));
                    break;
                case 7:
                    dishes.Add(new Dish(dishes.Count + 1, name, composition, weight, price, Category.Десерты, cookingTime, type));
                    break;
            }
            Console.WriteLine($"Создано блюдо {dishes.Count}");
        }
        public static void CreateSetOfDishes()
        {
            Console.Write("Введите кол-во блюд, которое вы хотите создать: ");
            int numberOfDishes = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= numberOfDishes; i++)
            {
                Console.WriteLine($"\nСоздание блюда {i}.");
                CreateDish();
            }
        }
        static void PrintDishInfo(int dishId)
        {
            var dish = dishes.FirstOrDefault(t => t.Id == dishId);
            dish?.PrintInfo();
        }

        static void CancelDish(int dishId)
        {
            var dish = dishes.FirstOrDefault(t => t.Id == dishId);
            if (dish != null)
            {
                dishes.Remove(dish);
                dish.Cancel();
            }
            else
            {
                Console.WriteLine($"Блюдо {dishId} не найдено.");
            }
        }

        public static void CreateOrder()
        {
            Console.Write("\nВведите ID стола: ");
            int tableId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Меню (для официанта):");
            foreach (var dish in dishes)
            {
                Console.WriteLine($"ID {dish.Id}, {dish.Name}");
            }
            Console.Write("Через пробел введите Id блюд, которые вы хотите добавить в заказ: ");
            string strOfId = Console.ReadLine();
            int[] numbers = strOfId.Split(' ').Select(int.Parse).ToArray();
            List<Dish> dishesOfOrder  = new List<Dish>();
            foreach (var id in numbers)
            {
                foreach (var dish in dishes)
                {
                    if (id == dish.Id)
                    {
                        dishesOfOrder.Add(dish);
                    }
                }
            }
            Console.Write("Комментарий: ");
            string comment = Console.ReadLine();
            Console.Write("Время принятия заказа: ");
            string startTime = Console.ReadLine();
            Console.Write("ID официанта: ");
            int waiterId = Convert.ToInt32(Console.ReadLine());
            orders.Add(new Order(orders.Count + 1, tableId, dishesOfOrder, comment, startTime, waiterId));
            Console.WriteLine("Заказ создан.");
        }

        static void PrintOrderInfo(int orderId)
        {
            var order = orders.FirstOrDefault(t => t.OrderId == orderId);
            order?.PrintInfo();
        }

        static void UpdateOrderInfo(int orderId)
        {
            Console.WriteLine($"\nИзменение заказа {orderId}.");
            var order = orders.FirstOrDefault(t => t.OrderId == orderId);
            if (order.ClosingTime != null)
            {
                Console.WriteLine("Заказ уже закрыт.");
                return;
            }
            Console.Write("ID стола: ");
            int tableId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Меню (для официанта):");
            foreach (var dish in dishes)
            {
                Console.WriteLine($"ID {dish.Id}, {dish.Name}");
            }
            Console.Write("Через пробел введите Id блюд, которые вы хотите добавить в заказ: ");
            string strOfId = Console.ReadLine();
            int[] numbers = strOfId.Split(' ').Select(int.Parse).ToArray();
            List<Dish> dishesOfOrder = new List<Dish>();
            foreach (var id in numbers)
            {
                foreach (var dish in dishes)
                {
                    if (id == dish.Id)
                    {
                        dishesOfOrder.Add(dish);
                    }
                }
            }
            Console.Write("Комментарий: ");
            string comment = Console.ReadLine();
            Console.Write("ID официанта: ");
            int waiterId = Convert.ToInt32(Console.ReadLine());
            order.UpdateOrder(tableId, dishesOfOrder, comment, waiterId);
            Console.WriteLine($"Заказ {orderId} изменен.");
        }

        public static void CreateSetOfOrders()
        {
            Console.Write("Введите кол-во заказов, которое вы создать: ");
            int numberOfOrders = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= numberOfOrders; i++)
            {
                Console.WriteLine($"\nСоздание заказа {i}.");
                CreateOrder();
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine("----------МЕНЮ----------");
            var groupedDishes = dishes.GroupBy(dish => dish.DishCategory);
            foreach (var group in groupedDishes)
            {
                Console.WriteLine($"\n{group.Key}");
                foreach (var dish in group)
                {
                    Console.WriteLine($"{dish.Name}________{dish.Price} руб.__({dish.Weight})\nСостав: {dish.Composition}\n");
                }
            }
        }

        static void CloseOrder(int orderId)
        {
            Console.WriteLine($"\nЗакрытие заказа {orderId}");
            Console.Write("Введите время закрытия заказа: ");
            string closingTime = Console.ReadLine();
            var order = orders.FirstOrDefault(t => t.OrderId == orderId);
            order?.CloseOrder(closingTime);
        }

        static void SumOfClosedOrders()
        {
            double sumOfTotalPrice = 0;
            foreach (var order in orders)
            {
                if (order.ClosingTime != null) sumOfTotalPrice += order.TotalPrice;
            }
            Console.WriteLine($"\nСтоимость всех закрытых заказов на данный момент: {sumOfTotalPrice} руб.");
        }

        static void NumberClosedOrdersOfWaiter(int waiterId)
        {
            double countOrders = 0;
            foreach (var order in orders)
            {
                if (order.WaiterId == waiterId)
                {
                    if (order.ClosingTime != null) countOrders++;
                }
            }
            Console.WriteLine($"\nКол-во обслуженных официантом {waiterId} заказов: {countOrders}");
        }

        static void SatisticsForEachDish()
        {
            Console.WriteLine("\n---------Статистика---------");
            foreach (var dish in dishes)
            {
                int countDishes = 0;
                foreach (var order in orders)
                {
                    if (order.ClosingTime != null)
                    {
                        foreach (var dishh in order.Dishes)
                        {
                            if (dish.Id == dishh.Id) countDishes++;
                        }
                    }
                }
                Console.WriteLine($"ID {dish.Id}, {dish.Name} - Количество заказов: {countDishes}");
            }
        }
        static void PrintReceipt(int orderId)
        {
            string output = "";
            Console.WriteLine($"\nЧек заказа {orderId}");
            var order = orders.FirstOrDefault(t => t.OrderId == orderId);
            order.PrintReceipt(out Dictionary<Dish, int> dishCount, ref output);
            if (dishCount.Count < 1)
            {
                Console.WriteLine(output);
                return;
            }
            var groupedDishes = dishCount.GroupBy(dish => dish.Key.DishCategory);
            foreach (var group in groupedDishes)
            {
                double sumGroup = 0;
                Console.WriteLine($"\n{group.Key}");
                foreach (var dish in group)
                {
                    Console.WriteLine($"{dish.Key.Name}________{dish.Key.Price}*{dish.Value} = {dish.Key.Price*dish.Value}руб");
                    sumGroup += dish.Key.Price * dish.Value;
                }
                Console.WriteLine($"Подитог: {sumGroup}руб");
            }
            Console.WriteLine($"Итого к оплате: {order.TotalPrice}руб.");
        }
        static void Main(string[] args)
        {
            CreateSetOfDishes();
            PrintMenu();
            CreateOrder();
            PrintReceipt(1);
            NumberClosedOrdersOfWaiter(1);
            SumOfClosedOrders();
            SatisticsForEachDish();
            CloseOrder(1);
            PrintReceipt(1);
            UpdateOrderInfo(1);
            NumberClosedOrdersOfWaiter(1);
            SumOfClosedOrders();
            SatisticsForEachDish();
        }
    }
}
