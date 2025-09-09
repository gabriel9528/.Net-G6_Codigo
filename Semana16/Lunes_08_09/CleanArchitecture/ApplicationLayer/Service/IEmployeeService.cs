using ApplicationLayer.Dtos;
using DomainLayer.Entities;

namespace ApplicationLayer.Service
{
    public interface IEmployeeService
    {
        Task<ServiceResponse> PostAsync(Employee employee);
        Task<ServiceResponse> PutAsync(Employee employee);
        Task<ServiceResponse> DeleteAsync(int id);
        Task<Employee> GetByIdAsync(int id);
        Task<List<Employee>> GetAllAsync();
    }
}
