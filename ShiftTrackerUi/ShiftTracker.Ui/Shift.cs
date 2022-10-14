
using Newtonsoft.Json;

namespace ShiftTracker.Ui
{
    [JsonObject]
    public class Shift
    {
        public int ShiftId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal Pay { get; set; }
        public decimal Minutes { get; set; }
        public string Location { get; set; }
    }
    }
