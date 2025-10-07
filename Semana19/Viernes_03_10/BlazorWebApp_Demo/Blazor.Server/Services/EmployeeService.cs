using Blazor.Server.Models;

namespace Blazor.Server.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;
        public EmployeeService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("LocalAPI");
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var getListEmployees = await _httpClient.GetFromJsonAsync<List<Employee>>("api/employees");
            return getListEmployees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var getEmployee = await _httpClient.GetFromJsonAsync<Employee>($"api/employees/{id}");
            return getEmployee;
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _httpClient.PostAsJsonAsync("api/employees", employee);
        }
    }
}
