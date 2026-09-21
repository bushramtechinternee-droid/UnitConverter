using SQLite;
using System.IO;
using UnitConverter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitConverter
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "conversions.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Conversion>().Wait();
        }

        public Task<List<Conversion>> GetRecentConversionsAsync()
        {
            return _database.Table<Conversion>()
                             .OrderByDescending(c => c.ConvertedAt)
                             .Take(5)
                             .ToListAsync();
        }

        public Task<int> SaveConversionAsync(Conversion conversion)
        {
            return _database.InsertAsync(conversion);
        }
    }
}