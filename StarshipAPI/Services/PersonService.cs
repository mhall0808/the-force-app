using Microsoft.EntityFrameworkCore;

public class PersonService
{
    private readonly TheForceDbContext _context;

    public PersonService(TheForceDbContext context)
    {
        _context = context;
    }

    // Create a new Person
    public async Task<Person> CreatePerson(Person person)
    {
        _context.People.Add(person);
        await _context.SaveChangesAsync();
        return person;
    }

    // Get all People
    public async Task<IEnumerable<Person>> GetAllPeople()
    {
        return await _context.People.ToListAsync();
    }

    // Get a specific Person by Id
    public async Task<Person> GetPersonById(int id)
    {
        return await _context.People
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Update an existing Person
    public async Task<bool> UpdatePerson(int id, Person updatedPerson)
    {
        var existingPerson = await _context.People.FindAsync(id);
        if (existingPerson == null)
        {
            return false;
        }

        // Update properties
        existingPerson.Name = updatedPerson.Name;
        existingPerson.Height = updatedPerson.Height;
        existingPerson.Mass = updatedPerson.Mass;
        existingPerson.HairColor = updatedPerson.HairColor;
        existingPerson.SkinColor = updatedPerson.SkinColor;
        existingPerson.EyeColor = updatedPerson.EyeColor;
        existingPerson.BirthYear = updatedPerson.BirthYear;
        existingPerson.Gender = updatedPerson.Gender;
        existingPerson.Homeworld = updatedPerson.Homeworld;
        existingPerson.Films = updatedPerson.Films;
        existingPerson.Species = updatedPerson.Species;
        existingPerson.Starships = updatedPerson.Starships;
        existingPerson.Vehicles = updatedPerson.Vehicles;
        existingPerson.Edited = DateTime.Now;

        _context.People.Update(existingPerson);
        await _context.SaveChangesAsync();

        return true;
    }

    // Delete a Person by Id
    public async Task<bool> DeletePerson(int id)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null)
        {
            return false;
        }

        _context.People.Remove(person);
        await _context.SaveChangesAsync();
        return true;
    }
}
