using E_CommerceWebsite.BusinessLogicLayer.Abstract;
using E_CommerceWebsite.EntitiesLayer.Model;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace E_CommerceWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IValidator<ProductDTO> _validator;
        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment, IValidator<ProductDTO> validator)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _validator = validator;
        }

        [HttpGet]
        [Route("api/getAll")]
        public IActionResult GetAll()
        {
            try
            {

                return Ok(_productService.GetAll());


            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    errors = ex.Message
                });
            }

        }

        [HttpGet]
        [Route("api/get")]

        public IActionResult Get(int id)
        {
            try
            {

                var product = _productService.Get(i => i.Id == id);

                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return BadRequest(new
                    {
                        errorMessage = "Ürün bulunamadı."
                    });
                }

            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    errorMessage = ex.Message
                });
            }

            




            

        }



        [HttpPost]
        [Route("api/addProduct")]
        public IActionResult AddProduct([FromForm] ProductDTO productDTO)
        {
            var check = _validator.Validate(productDTO);

            if (check.IsValid)
            {

                Product product = new Product()
                {
                    ProductDescription = productDTO.ProductDescription,
                    ProductName = productDTO.ProductName,
                    ProductPrice = productDTO.ProductPrice,
                    ProductImage = UploadFile(productDTO.ProductImage),
                    RowGuid = Guid.NewGuid()
                };

                _productService.Add(product);
                _productService.Save();

                return Ok(new
                {
                    success = "Ürün başarıyla oluşturuldu."
                });


            }
            else
            {

                return BadRequest(new
                {
                    errors = check.Errors.Select(e => e.ErrorMessage)
                   
                });
            }
            
           


            
        }


       
        private string UploadFile(IFormFile file)
        {
            string? fileName = null;

            if (file != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.ContentRootPath, "Resources", "Images");
                fileName = Guid.NewGuid().ToString() + '-' + file.FileName;
                string filePath = Path.Combine(uploadDir, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return fileName;
        }









    }
}
