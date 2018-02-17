using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angular_udemy_demo.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
