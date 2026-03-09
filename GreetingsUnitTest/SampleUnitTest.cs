using GreetingsCore.Adapters.Db;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Microsoft.Data.Sqlite;

namespace GreetingsUnitTest
{
    public class Tests
    {
        private GreetingContext _context;
        private SqliteConnection _connection;

        [SetUp]
        public void Setup()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<GreetingContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new GreetingContext(options);
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
            _connection?.Dispose();
        }

        [Test]
        public void CanConnnectToDataBase_ReturnTrue()
        {
            var isConnected = _context.Database.CanConnect();
            Assert.IsTrue(isConnected, "It should connect with in-memory SQLite database.");
        }

    }
}