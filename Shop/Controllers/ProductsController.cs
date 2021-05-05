using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shop.Dtos;
using Shop.Models;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsServices _productsServices;
        private readonly IMapper _mapper;
        public ProductsController(IProductsServices productsServices, IMapper mapper)
        {
            _productsServices = productsServices;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult <IEnumerable<ProductReadDto>> GetProducts()
        {
            var products = _productsServices.GetProducts();
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
        }

        [HttpGet("{id}", Name ="GetProduct")]
        public ActionResult GetProduct(int id)
        {
            var product = _productsServices.GetProduct(id);
            if(product != null)
            {
                return Ok(_mapper.Map<ProductReadDto>(product));
            }
            return NotFound($"Product with id: {id} was not found");
        }

        [HttpPost]
        public ActionResult AddProduct(ProductCreateDto product)
        {
            var _product = _mapper.Map<Product>(product);
            _productsServices.AddProduct(_product);
            _productsServices.SaveChanges();
            var productReadDto = _mapper.Map<ProductReadDto>(_product);
            return CreatedAtRoute(nameof(GetProduct), new { id = productReadDto.Id }, productReadDto);

        }

        [HttpPatch("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] JsonPatchDocument<ProductUpdateDto> patch)
        {
            var _product = _productsServices.GetProduct(id);
            if(_product == null)
            {
                return NotFound();
            }
            var product = _mapper.Map<ProductUpdateDto>(_product);
            patch.ApplyTo(product, ModelState);
            if (!TryValidateModel(product))
            {
                return ValidationProblem();
            }
            _mapper.Map(product, _product);
            _productsServices.UppdateProduct(_product);
            _productsServices.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var _product = _productsServices.GetProduct(id);
            if (_product == null)
            {
                return NotFound($"Product with id: {id} was not found");
            }
            _productsServices.DeleteProduct(_product);
            _productsServices.SaveChanges();
            return NoContent();
        }
    }
}
