using ExcelParsingApp.Database;
using ExcelParsingApp.Database.Models;
using System.Threading.Tasks;

namespace ExcelParsingApp.Repositories
{
    public class TagValuesRepository
    {
        private readonly DbContextPostgres _db;

        public TagValuesRepository()
        {
            _db = new DbContextPostgres();
        }

        public async Task Add(TagValues tag)
        {
            await _db.TagValues.AddAsync(tag);
            await _db.SaveChangesAsync();
        }

        public async Task<TagValues> Find(int id)
        {
            return await _db.TagValues.FindAsync(id);
        }
    }
}
