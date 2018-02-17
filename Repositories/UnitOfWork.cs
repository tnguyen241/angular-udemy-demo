using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using angular_udemy_demo.Models;

namespace angular_udemy_demo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDBContext context;

        public UnitOfWork(VegaDBContext context)
        {
            this.context = context;
        }
        public async Task Commit()
        {
           await context.SaveChangesAsync();
        }
    }
}
