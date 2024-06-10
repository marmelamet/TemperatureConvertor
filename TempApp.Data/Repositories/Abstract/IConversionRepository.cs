using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempApp.Core.Entities.Concrete;

namespace TempApp.Data.Repositories.Abstract
{
    public interface IConversionRepository<T> : IRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        Task<bool> AddAsync(T model);
        bool Update(T model);
        Task<int> SaveAsync();
    }
}
