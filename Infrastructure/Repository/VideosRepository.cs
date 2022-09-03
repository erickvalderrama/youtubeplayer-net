
using AFEXChile.Domain.Entity;
using System.Collections.Generic;

namespace AFEXChile.Infrastructure.Repository
{

    public interface VideosRepository
    {
        List<Videos> List();
        Videos Find(int id);
        Videos Add(Videos item);
        void Delete(int id);
    }
}