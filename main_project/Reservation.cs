using System;

namespace main_project
{
    internal class Reservation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int ReservationStartTime { get; set; }
        public int ReservationEndTime { get; set; }
        public string Comment { get; set; }
        public int TableId { get; set; }

        public Reservation(int id, string name, string phoneNumber, int reservationStartTime, int reservationEndTime, string comment, int tableId)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            ReservationStartTime = reservationStartTime;
            ReservationEndTime = reservationEndTime;
            Comment = comment;
            TableId = tableId;
        }
        public void UpdateReservation(string name, string phoneNumber, int reservationStartTime, int reservationEndTime, string comment)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            ReservationStartTime = reservationStartTime;
            ReservationEndTime = reservationEndTime;
            Comment = comment;
        }
        public void Cancel()
        {
            Console.WriteLine("Бронь отменена.");
        }
    }
}
