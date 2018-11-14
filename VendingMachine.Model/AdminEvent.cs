using System;
using System.ComponentModel.DataAnnotations;

namespace VendingMachine.Model
{
    public sealed class AdminEvent
    {
        [Key]
        public int Id { get; set; }
        public DateTime EventTime { get; set; }
        public AdminEventType EventType { get; set; }
    }
}
