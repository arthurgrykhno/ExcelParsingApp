using ExcelParsingApp.Database;
using ExcelParsingApp.Database.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelParsingApp.Repositories
{
    public class TagRepository
    {
        private readonly DbContextPostgres _db;

        public TagRepository()
        {
            _db = new DbContextPostgres();
        }

        public async Task Add(Tag tag)
        {
            await _db.Tags.AddAsync(tag);
            await _db.SaveChangesAsync();
        }

        public List<string> GetNames()
        {
            return _db.Tags.Select(tag => tag.Name).ToList();
        }

        public int GetTagsCount()
        {
            return _db.Tags.Count();
        }

        public async Task<Tag> Find(int id)
        {
            return await _db.Tags.FindAsync(id);
        }

        public int? FindByName(string name)
        {
            return _db.Tags.FirstOrDefault(tag => tag.Name == name)?.Id;
        }

        public void AddMany(List<Tag> tags)
        {
            _db.Tags.AddRange(tags);
            _db.SaveChanges();
        }
    }
}
