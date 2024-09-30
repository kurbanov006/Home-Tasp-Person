using Infrastructure.Models;

namespace Infrastructure.Services.PersonService;

public interface IPersonService
{
    Task<bool> Create(Person person);
    Task<bool> Update(Person person);
    Task<bool> Delete(int id);
    Task<Person?> GetById(int id);
    Task<IEnumerable<Person?>> GetAll();
}