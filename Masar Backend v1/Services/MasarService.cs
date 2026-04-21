using Masar_Backend_v1.Models;

namespace Masar_Backend_v1.Services
{

    public interface IMasarService
    {
        Task<TrackRecommendation> GetRecommendationAsync(Dictionary<string, string> answers);
    }

    public class MasarService : IMasarService
    {
        private readonly HttpClient _httpClient;
        private readonly string _masarUrl = "https://masaranalyticalsystem-production.up.railway.app";

        public MasarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TrackRecommendation> GetRecommendationAsync(Dictionary<string, string> answers)
        {
            var body = new AnswersRequest { Answers = answers };
            var response = await _httpClient.PostAsJsonAsync(_masarUrl + "/recommend", body);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TrackRecommendation>();
        }
    }
}
