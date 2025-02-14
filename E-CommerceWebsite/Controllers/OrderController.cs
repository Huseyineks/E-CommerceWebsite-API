using E_CommerceWebsite.BusinessLogicLayer.Abstract;
using E_CommerceWebsite.EntitiesLayer.Model;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        public OrderController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [HttpPost]
        [Route("api/getCartItems")]
        public IActionResult GetCartItems([FromBody] string userId) {

            try
            {

                var cartItems = _orderService.GetFilteredList(i => i.OrderStatus == OrderStatus.InCart && i.userId.ToString() == userId);

                if (cartItems.Count != 0)
                {

                    return Ok(cartItems);

                }
                else
                {
                    return BadRequest(new
                    {
                        message = "Sepette ürününüz yok."
                    });

                }

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                 message = ex.Message
                });
            }




           

          
        
        }

        [HttpPost]
        [Route("api/addItemToCart")]
        public IActionResult AddItemToCart([FromBody] OrderDTO orderDTO)
        {
            var product = _productService.Get(i => i.Id == orderDTO.ProductId);
            Order newOrder = new Order()
            {
                Guid = Guid.NewGuid(),
                OrderStatus = OrderStatus.InCart,
                ProductDescription = product.ProductDescription,
                ProductImage = product.ProductImage,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ShippingStatus = ShippingStatus.Unknown,
                userId = orderDTO.UserId
                

            };

            try
            {
                _orderService.Add(newOrder);
                _orderService.Save();

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
