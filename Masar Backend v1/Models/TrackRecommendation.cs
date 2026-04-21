using System.Text.Json.Serialization;

namespace Masar_Backend_v1.Models
{
    public class ConfidenceInfo
    {
        public double Score { get; set; }
        public string? Label { get; set; }
        [JsonPropertyName("top_track")]
        public string? TopTrack { get; set; }
        [JsonPropertyName("second_track")]
        public string? SecondTrack { get; set; }
    }

    public class TrackRecommendation
    {
        [JsonPropertyName("track")]
        public string Track { get; set; }

        [JsonPropertyName("confidence")]
        public ConfidenceInfo Confidence { get; set; }

        [JsonPropertyName("traits")]
        public Dictionary<string, double> Traits { get; set; }

        [JsonPropertyName("scores")]
        public Dictionary<string, double> Scores { get; set; }

        [JsonPropertyName("explanation")]
        public List<string> Explanation { get; set; }
    }
}
