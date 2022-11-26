using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal Pay { get; set; }
        public decimal Minutes { get; set; }
        public string? Location { get; set; }
    }
}
