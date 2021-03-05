using System.Web.Http;

namespace Dashboard.Models
{
    public class JsonStatusItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string ItemStatus { get; set; }
        public bool Display { get; set; }
        public bool InProgress { get; set; } = false;
        public int StatusItemId { get; set; }
        public string InProgressUserid { get; set; }
    }
}
