using Domain.Contracts.IRepository.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.IRepository
{
    public interface IEnterpriseRepository:IBaseRepository<Enterprise>
    {
        Task<Enterprise> GetEnterpriseNameByIndex(Guid id, string name);
    }
}
