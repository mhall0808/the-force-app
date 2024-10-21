using Microsoft.EntityFrameworkCore;

public class PersonServiceTests
{
    private TheForceDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<TheForceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new TheForceDbContext(options);
    }

    [Fact]
    public async Task CreatePerson_ShouldAddPerson()
    {
        var dbContext = GetInMemoryDbContext();
        var personService = new PersonService(dbContext);
        var person = new Person
        {
            Name = "Luke Skywalker",
            BirthYear = "19BBY",
            Gender = "Male"
        };

        var result = await personService.CreatePerson(person);

        Assert.NotNull(result);
        Assert.Equal("Luke Skywalker", result.Name);
        Assert.Equal(1, await dbContext.People.CountAsync());
    }

    [Fact]
    public async Task GetAllPeople_ShouldReturnAllPeople()
    {
        var dbContext = GetInMemoryDbContext();
        var personService = new PersonService(dbContext);
        dbContext.People.Add(new Person { Name = "Person 1", BirthYear = "20BBY" });
        dbContext.People.Add(new Person { Name = "Person 2", BirthYear = "30BBY" });
        await dbContext.SaveChangesAsync();

        var people = await personService.GetAllPeople();

        Assert.Equal(2, people.Count());
    }

    [Fact]
    public async Task GetPersonById_ShouldReturnPerson()
    {
        var dbContext = GetInMemoryDbContext();
        var personService = new PersonService(dbContext);
        var person = new Person { Name = "Leia Organa", BirthYear = "19BBY" };
        dbContext.People.Add(person);
        await dbContext.SaveChangesAsync();

        var result = await personService.GetPersonById(person.Id);

        Assert.NotNull(result);
        Assert.Equal("Leia Organa", result.Name);
    }

    [Fact]
    public async Task UpdatePerson_ShouldModifyPerson()
    {
        var dbContext = GetInMemoryDbContext();
        var personService = new PersonService(dbContext);
        var person = new Person { Name = "Old Name", BirthYear = "19BBY" };
        dbContext.People.Add(person);
        await dbContext.SaveChangesAsync();

        var updatedPerson = new Person { Name = "Luke Skywalker", BirthYear = "19BBY" };

        var result = await personService.UpdatePerson(person.Id, updatedPerson);

        Assert.True(result);
        var personFromDb = await dbContext.People.FindAsync(person.Id);
        Assert.Equal("Luke Skywalker", personFromDb.Name);
    }

    [Fact]
    public async Task UpdatePerson_ShouldReturnFalseForNonExistentId()
    {
        var dbContext = GetInMemoryDbContext();
        var personService = new PersonService(dbContext);

        var updatedPerson = new Person { Name = "Leia Organa", BirthYear = "19BBY" };

        var result = await personService.UpdatePerson(9999, updatedPerson);

        Assert.False(result);
    }

    [Fact]
    public async Task DeletePerson_ShouldRemovePerson()
    {
        var dbContext = GetInMemoryDbContext();
        var personService = new PersonService(dbContext);
        var person = new Person { Name = "Anakin Skywalker", BirthYear = "41.9BBY" };
        dbContext.People.Add(person);
        await dbContext.SaveChangesAsync();

        var result = await personService.DeletePerson(person.Id);

        Assert.True(result);
        Assert.Equal(0, await dbContext.People.CountAsync());
    }

    [Fact]
    public async Task DeletePerson_ShouldReturnFalseForNonExistentId()
    {
        var dbContext = GetInMemoryDbContext();
        var personService = new PersonService(dbContext);

        var result = await personService.DeletePerson(9999);

        Assert.False(result);
    }
}
