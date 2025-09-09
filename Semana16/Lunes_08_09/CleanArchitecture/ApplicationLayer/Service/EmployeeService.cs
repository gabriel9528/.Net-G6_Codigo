using ApplicationLayer.Dtos;
using DomainLayer.Entities;
using System.Net.Http.Json;

namespace ApplicationLayer.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var data = await _httpClient.DeleteAsync($"api/Employees/{id}");
            var response = await data.Content.ReadFromJsonAsync<ServiceResponse>();
            return response;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<List<Employee>>($"api/Employees");
            return data;
        }

        public async Task<Employee> GetByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<Employee>($"api/Employees/{id}");

        public async Task<ServiceResponse> PostAsync(Employee employee)
        {
            try
            {
                var data = await _httpClient.PostAsJsonAsync("api/Employees", employee);
                if (data.IsSuccessStatusCode)
                {
                    var response = await data.Content.ReadFromJsonAsync<ServiceResponse>();
                    return response;
                }
                else
                {
                    return new ServiceResponse(false, "Error in response from server");
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, $"Error: {ex.Message}");
            }
        }

        public async Task<ServiceResponse> PutAsync(Employee employee)
        {
            var data = await _httpClient.PutAsJsonAsync("api/Employees", employee);
            var response = await data.Content.ReadFromJsonAsync<ServiceResponse>();
            return response;
        }
    }
}
