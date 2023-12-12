using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemiProjectAPI.Configuration;
using UdemiProjectAPI.Entities;

namespace UdemiProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(ILogger<ProductsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Products.Add(product);
                await _unitOfWork.CompleteAsync();
                return CreatedAtAction("GetItem", new { product.Id }, product);
            }
            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _unitOfWork.Products.All();
            if(products == null)
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        { 
            var product = await _unitOfWork.Products.GetById(id);
            if (product == null)
                return new JsonResult("Something went wrong") { StatusCode = 500 };
            return Ok(product);
        }
    }
}
