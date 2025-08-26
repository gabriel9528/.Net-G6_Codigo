using DapperMVC.Data.Models.Domain;

namespace DapperMVC.Data.Repository
{
    public interface IPersonRepository
    {
        Task<bool> AddPersonAsync(Person person);
        Task<bool> UpdatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(int id);
        Task<Person> GetPersonByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllPersonAsync();
    }
}
