using DapperMVC.Data.DataAccess;
using DapperMVC.Data.Models.Domain;

namespace DapperMVC.Data.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ISqlDataAccess _db;
        public PersonRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<bool> AddPersonAsync(Person person)
        {
            try
            {
                await _db.SaveData("sp_create_person", new
                {
                    person.Name,
                    person.Email,
                    person.Address
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdatePersonAsync(Person person)
        {
            try
            {
                await _db.SaveData("sp_update_person", new
                {
                    person.Id,
                    person.Name,
                    person.Email,
                    person.Address,
                    person.IsActive
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            try
            {
                await _db.SaveData("sp_delete_person", new { Id = id });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Person>> GetAllPersonAsync()
        {
            try
            {
                var result = await _db.GetData<Person, dynamic>("sp_get_list_person", new { });
                return result;
            }
            catch (Exception)
            {
                return Enumerable.Empty<Person>();
            }
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            try
            {
                IEnumerable<Person> result = await _db.GetData<Person, dynamic>("sp_getPersonById", new { Id = id });
                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }
}
