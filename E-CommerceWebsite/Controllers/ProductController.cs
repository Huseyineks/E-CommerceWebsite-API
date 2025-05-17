using E_CommerceWebsite.BusinessLogicLayer.Abstract;
using E_CommerceWebsite.EntitiesLayer.Model;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public IActionResult Get(string rowGuid)
        {
            try
            {

                var product = _productService.GetProductSizes(i => i.RowGuid.ToString() == rowGuid);



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

            var productSizesJson = Request.Form["ProductSizes"];
            if (!string.IsNullOrEmpty(productSizesJson))
            {
                productDTO.ProductSizes = JsonConvert.DeserializeObject<List<ProductSizes>>(productSizesJson);
            }

            var check = _validator.Validate(productDTO);

            if (check.IsValid)
            {

                Product product = new Product()
                {
                    ProductDescription = productDTO.ProductDescription,
                    ProductName = productDTO.ProductName,
                    ProductPrice = productDTO.ProductPrice,
                    ProductImage = UploadFile(productDTO.ProductImage),
                    RowGuid = Guid.NewGuid(),
                    ProductSizes = productDTO.ProductSizes
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

        [HttpPut]
        [Route("api/updateProduct")]
        public IActionResult UpdateProduct(ProductDTO editedProduct)
        {

            var productSizesJson = Request.Form["ProductSizes"];
            if (!string.IsNullOrEmpty(productSizesJson))
            {
                editedProduct.ProductSizes = JsonConvert.DeserializeObject<List<ProductSizes>>(productSizesJson);
            }

            var product = _productService.GetProductSizes(i => i.Id == editedProduct.Id);

            product.ProductPrice = editedProduct.ProductPrice != null ? editedProduct.ProductPrice : product.ProductPrice;
            product.ProductName = editedProduct.ProductName != null ? editedProduct.ProductName : product.ProductName;
            product.ProductDescription = editedProduct.ProductDescription != null ? editedProduct.ProductDescription : product.ProductDescription;

            if (editedProduct.ProductSizes != null)
            {
                foreach (var item in editedProduct.ProductSizes)
                {

                    var productSize = product.ProductSizes.FirstOrDefault(i => i.Id == item.Id);

                    if (productSize != null)
                    {

                        productSize.Size = item.Size;

                        if(String.Equals(item.Stock,""))
                        {
                            productSize.Stock = "0";
                        }
                        else
                        {
                            productSize.Stock = item.Stock;

                        }
                       
                    }


                }

            }
            
            

            if(editedProduct.ProductImage != null)
            {
                product.ProductImage = UploadFile(editedProduct.ProductImage);
            }
           


            try
            {
                _productService.Update(product);
                _productService.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }





        }

        [HttpDelete]
        [Route("api/deleteProduct")]


        public IActionResult DeleteProduct(string rowGuid)
        {

            var deletedProduct = _productService.Get(i => i.RowGuid.ToString() == rowGuid);

            try
            {
                _productService.Remove(deletedProduct);
                _productService.Save();

                return Ok();
            }
            catch {


                return BadRequest();
            
            }




        }

        [HttpPost]

        [Route("api/productQuantity")]
        public IActionResult ProductQuantity(OrderDTO orderDTO)
        {
            var product = _productService.GetProductSizes(i => i.Id == orderDTO.ProductId);

            var productQuantity = product.ProductSizes.FirstOrDefault(i => i.Size == orderDTO.Size).Stock;

            return Ok(int.Parse(productQuantity));
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
