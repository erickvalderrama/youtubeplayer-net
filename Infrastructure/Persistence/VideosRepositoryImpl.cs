using AFEXChile.Domain.Context;
using AFEXChile.Domain.Entity;
using AFEXChile.Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;

namespace AFEXChile.Infrastructure.Persistence
{
    public class VideosRepositoryImpl : VideosRepository
    {
        private AppDbContext _context;
        public VideosRepositoryImpl(AppDbContext context)
        {
            _context = context;
        }
        public Videos Add(Videos item)
        {
            Videos n = new Videos();
            n.Id = item.Id;
            n.Image = item.Image;
            n.Description = item.Description;
            n.Title = item.Title;
            n.Url = item.Url;
            n.Duration = item.Duration;
            _context.Videos.Add(n);
            _context.SaveChanges();
            return n;
        }

        public void Delete(int id)
        {
            Videos n = this.Find(id);
            _context.Videos.Remove(n);
            _context.SaveChanges();
        }

        public Videos Find(int id) => _context.Videos.Where(x=>x.Id==id).FirstOrDefault();

        public List<Videos> List() => _context.Videos.ToList();
    }
}