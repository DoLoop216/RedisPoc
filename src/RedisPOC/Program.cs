using Bogus;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisPOC;
using RedisPOC.Entities;

var dbContext = new RedisPOCDbContext();

SeedUsers();

var usersKey = "users";
for (int i = 0; i < 10; i++)
{
    List<UserEntity> users = [];
    // var usersString = Constants.MemoryCache.GetString(usersKey);
    var usersString = Constants.MemoryCache.GetString(usersKey);
    if (usersString == null)
    {
        users = dbContext.Users.ToList();
        usersString = JsonConvert.SerializeObject(users);
        Constants.MemoryCache.SetString(usersKey, usersString);
    }
    else
    {
        users = JsonConvert.DeserializeObject<List<UserEntity>>(usersString);
    }
    var start = DateTime.UtcNow;
    
    users.Where(x => x.Username.StartsWith("A") || x.Email.Contains("Z") || x.Email.Contains("F"))
    .ToList();
    var stop = DateTime.UtcNow;
    var elapsed = DateTime.UtcNow - start;
    Console.WriteLine($"Total elapsed: {elapsed.TotalMilliseconds.ToString("#,##0.00")} ms");
}


void SeedUsers()
{
    if (dbContext.Users.Any())
        return;

    var userId = 1;
    var fakeUsers = new Faker<UserEntity>()
        .RuleFor(x => x.Id, f => userId++)
        .RuleFor(x => x.Username, f => f.Person.UserName)
        .RuleFor(x => x.Email, f => f.Person.Email)
        .RuleFor(x => x.Password, f => f.Random.AlphaNumeric(10));

    int count = 1_000;
    Console.WriteLine($"Generating {count} users and insert into database... This will take some time...");
    var users = fakeUsers.GenerateBetween(count, count);
    dbContext.Users.AddRange(users);
    dbContext.SaveChanges();
}