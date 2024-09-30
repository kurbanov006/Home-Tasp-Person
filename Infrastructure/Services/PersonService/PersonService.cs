using Infrastructure.Models;
using Npgsql;
using Dapper;

namespace Infrastructure.Services.PersonService;

public class PersonService : IPersonService
{
    public async Task<bool> Create(Person person)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.CreatePerson, person) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Update(Person person)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.UpdatePerson, person) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.ExecuteAsync(SqlCommand.DeletePerson, new { Id = id }) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Person?> GetById(int id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<Person?>(SqlCommand.GetPersonById, new { Id = id });
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Person?>> GetAll()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<Person?>(SqlCommand.GetAllPersons);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}

file class SqlCommand
{
    public const string ConnectionString = "Server=localhost; Port=5432; Database=person_db; User Id=postgres; Password=12345";
    public const string CreatePerson = @"insert into persons(id, name, email, age) values
                                              (@Id, @Name, @Email, @Age);";

    public const string UpdatePerson = @"update persons set name=@name, email=@email, age=@age where id = @Id";
    public const string DeletePerson = @"delete from persons where id = @Id";
    public const string GetPersonById = @"select * from persons where id = @Id";
    public const string GetAllPersons = @"select * from persons";
}