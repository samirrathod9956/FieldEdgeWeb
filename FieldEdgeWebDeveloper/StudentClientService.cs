using FieldEdgeWebDeveloper.Models;

namespace FieldEdgeWebDeveloper
{
    public class StudentClientService
    {
        private readonly HttpClient _httpClient;

        public StudentClientService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<CustomerModel>> GetStudentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CustomerModel>>("Customers");
        }

        public async Task<CustomerModel> GetStudentsByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CustomerModel>($"Customer/{id}");
        }

        public async Task<HttpResponseMessage> CreateStudentsAsync(CustomerModel customerModel)
        {
            var response = await _httpClient.PostAsJsonAsync("Customer", customerModel);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> UpdateStudentsAsync(string? id, CustomerModel? customerModel)
        {
            var response = await _httpClient.PostAsJsonAsync($"Customer/{id}", customerModel);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> DeleteStudentsAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Customer/{id}");
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
