namespace Schd.API.Models
{
    public class SchedulerTemplateViewModel
    {
        public long Id { get; set; }
        public bool Checked { get; set; }
        public string Name { get; set; } = "";
        public int RegularityDaysEnum { get; set; }
        public string[] RegularityDays { get; set; } = new string[0] { };
        public int RegularityTimesEnum { get; set; }
        public string[] RegularityTimes { get; set; } = new string[0] { };
    }
}
