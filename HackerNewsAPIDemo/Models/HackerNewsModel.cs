using Newtonsoft.Json;

namespace HackerNewsAPIDemo.Models
{/// <summary>
/// Model for Hacker News
/// </summary>
    public class HackerNewsDetailModel
    {
        public int id { get; set; }
        public bool? deleted { get; set; }
        public string? type { get; set; }
        public string? authorby { get; set; }

        public string? url { get; set; }
        public string? title { get; set; }
    }

}
