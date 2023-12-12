using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemiProjectAPI.Configuration;
using UdemiProjectAPI.IRepositories;
using UdemiProjectAPI.Repositories;

namespace UdemiProjectAPI.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IProductRepository Products { get; private set; }

        private readonly ILogger _logger;
        private readonly StoreContext _context;

        
        public UnitOfWork(ILoggerFactory logger, StoreContext context)
        {
            _context = context;
            _logger = logger.CreateLogger("logs");
            Products = new ProductRepository(_context, _logger);

        }
        public async Task CompleteAsync()
        {
             await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        //public async Task Dispose()
        //{
        //    await _context.DisposeAsync();
        //}

    }
}
