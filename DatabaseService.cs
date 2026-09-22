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

        public Task<List<Conversion>> GetAllConversionsAsync()
        {
            return _database.Table<Conversion>()
                             .OrderByDescending(c => c.ConvertedAt)
                             .ToListAsync();
        }

        public Task<Conversion> GetConversionByIdAsync(int id)
        {
            return _database.Table<Conversion>()
                             .Where(c => c.Id == id)
                             .FirstOrDefaultAsync();
        }

        public Task<int> SaveConversionAsync(Conversion conversion)
        {
            return _database.InsertAsync(conversion);
        }

        public Task<int> UpdateConversionAsync(Conversion conversion)
        {
            return _database.UpdateAsync(conversion);
        }

        public Task<int> DeleteConversionAsync(Conversion conversion)
        {
            return _database.DeleteAsync(conversion);
        }
    }
}