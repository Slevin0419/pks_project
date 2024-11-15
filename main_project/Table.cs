using System;
using System.Collections.Generic;

namespace main_project
{
    internal class Table
    {
        public int Id { get; set; }
        public string Placement { get; set; }
        public int NumberOfSeats { get; set; }
        public Dictionary<int, Reservation?> Schedule { get; set; }

        public Table(int id, string placement, int numberOfSeats)
        {
            Id = id;
            Placement = placement;
            NumberOfSeats = numberOfSeats;
            Schedule = new Dictionary<int, Reservation?>
            {
                { 9, null },
                { 10, null },
                { 11, null },
                { 12, null },
                { 13, null },
                { 14, null },
                { 15, null },
                { 16, null },
                { 17, null }
            };
        }

        public void ChangeInfo(string placement, int numberOfSeats)
        {
            Placement = placement;
            NumberOfSeats = numberOfSeats;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"\nID: {Id}");
            Console.WriteLine($"Расположение: {Placement}");
            Console.WriteLine($"Количество мест: {NumberOfSeats}");
            Console.WriteLine("Расписание:");

            foreach (var slot in Schedule)
            {
                Console.Write($"{slot.Key}:00 - {slot.Key + 1}:00");

                if (slot.Value != null)
                {
                    Console.Write($" -- ID {slot.Value.Id}, {slot.Value.Name}, {slot.Value.PhoneNumber}");
                }
                else
                {
                    Console.Write(" -- Свободно");
                }
                Console.WriteLine();
            }
        }

        public bool Reserve(Reservation reservation)
        {
            if (IsReserved(reservation.ReservationStartTime, reservation.ReservationEndTime))
            {
                return false;
            }
            else
            {
                for (int i = reservation.ReservationStartTime; i < reservation.ReservationEndTime; i++)
                {
                    Schedule[i] = reservation;
                }
                return true;
            }
        }
        public bool IsReserved(int startTime, int endTime)
        {
            for (int i = startTime; i < endTime; i++)
            {
                if (Schedule[i] != null) return true;
            }
            return false;
        }
        public void DeleteReservation (Reservation reservation)
        {
            for (int i = reservation.ReservationStartTime; i < reservation.ReservationEndTime;i++) { Schedule[i] = null; }
        }
    }
}
