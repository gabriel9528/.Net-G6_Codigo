using Blazor.Server.Models;

namespace Blazor.Server.Services
{
    public class DepartmentService
    {
        private readonly HttpClient _httpClient;
        public DepartmentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("LocalAPI");
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            var getListDepartments = await _httpClient.GetFromJsonAsync<List<Department>>("api/departments");
            return getListDepartments;
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var getDepartment = await _httpClient.GetFromJsonAsync<Department>($"api/departments/{id}");
            return getDepartment;
        }

        public async Task CreateDepartmentAsync(Department department)
        {
            await _httpClient.PostAsJsonAsync("api/departments", department);
        }
    }
}
