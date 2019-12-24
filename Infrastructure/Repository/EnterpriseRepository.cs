using Domain.Contracts.IRepository;
using Domain.Entities;
using Infrasctruture.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class EnterpriseRepository : Repository<Enterprise>, IEnterpriseRepository
    {
        public EnterpriseRepository(APIContext db) : base(db)
        {
        }

        public async Task<Enterprise> GetEnterpriseNameByIndex(Guid id, string name)
        {
            return await DbContext.Enterprise.AsNoTracking().Where(x => x.Name.Contains(name) && x.Id == id).FirstOrDefaultAsync();
        }

    }
}
