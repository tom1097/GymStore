using System;

namespace GymStore1
{
    public class User
    {
        public int Id { get; set; } // Add Id for DB operations
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        public decimal MoneyPerMonth { get; set; }
    }
}
