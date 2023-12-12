using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemiProjectAPI.IRepositories;

namespace UdemiProjectAPI.Configuration
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        Task CompleteAsync();
    }
}
