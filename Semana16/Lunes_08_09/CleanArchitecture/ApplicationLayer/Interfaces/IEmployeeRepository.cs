using ApplicationLayer.Dtos;
using DomainLayer.Entities;

namespace ApplicationLayer.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<ServiceResponse> PostAsync(Employee employee);
        Task<ServiceResponse> EditAsync(Employee employee);
        Task<ServiceResponse> DeleteAsync(int id);
        Task<Employee> GetByIdAsync(int id);
        Task<List<Employee>> GetAllAsync();
        Task<List<Employee>> GetAllDeleteAsync();
    }
}
