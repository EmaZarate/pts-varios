using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Hoshin.Core.Domain.Sector;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository
{
    public class SectorRepository : ISectorRepository
    {
        private readonly SQLHoshinCoreContext _ctx;
        private readonly IMapper _mapper;
        public SectorRepository(SQLHoshinCoreContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<List<Sector>> GetAll()
        {
            var list = await _ctx.Sectors
                        .Include(x => x.SectorsPlants)
                            .ThenInclude(y => y.Sector)
                            .ToListAsync();
            return _mapper.Map<List<Sectors>, List<Sector>>(list);
        }

        public Sector GetOne(int id)
        {
            return _mapper.Map<Sectors, Sector>(_ctx.Sectors.Find(id));
        }

        public Sector Add(Sector sector)
        {
            Sectors sectorDb = _mapper.Map<Sector, Sectors>(sector);
            _ctx.Sectors.Add(sectorDb);
            _ctx.SaveChanges();
            return _mapper.Map<Sectors, Sector>(sectorDb);
        }

        public Sector Update(Sector sector)
        {
            Sectors sectorDb = _mapper.Map<Sector, Sectors>(sector);
            _ctx.Sectors.Update(sectorDb);
            _ctx.SaveChanges();
            return _mapper.Map<Sectors, Sector>(sectorDb);
        }

        public Sector CheckDuplicated(Sector sector)
        {
            return _mapper.Map<Sectors, Sector>(_ctx.Sectors.Where(x => (x.Name == sector.Name || x.Code == sector.Code) && x.SectorID != sector.Id).FirstOrDefault());
        }
    }
}
