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

        [HttpGet]
        [Route("api/getNumber")]

        public IActionResult GetNumber() { 
        
            var itemNumber = _orderService.GetAll().Select(i => i.ProductNumber).Sum();

            try
            {



                return Ok(itemNumber);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        }

        [HttpGet]
        [Route("api/getCartItems")]
        public IActionResult GetCartItems(string userId) {

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

            var existCartItems = _orderService.GetFilteredList(i => i.userId == orderDTO.UserId && i.OrderStatus == OrderStatus.InCart);

            if (existCartItems.Count != 0 && existCartItems.Any(i => i.ProductImage == product.ProductImage))
            {
                var existCartItem = existCartItems.First(i => i.ProductImage == product.ProductImage);

                existCartItem.ProductNumber += 1;
                var decimalPrice = Convert.ToDecimal(existCartItem.ProductPrice);
                

                var totalPrice = decimalPrice * existCartItem.ProductNumber;

                existCartItem.ProductPrice = totalPrice.ToString();

                try
                {


                    _orderService.Update(existCartItem);
                    _orderService.Save();
                    return Ok();

                }
                catch(Exception ex) {

                    return BadRequest(new
                    {
                        message = ex.Message
                    });

                }
            }
            else
            {



                Order newOrder = new Order()
                {
                    Guid = Guid.NewGuid(),
                    OrderStatus = OrderStatus.InCart,
                    ProductDescription = product.ProductDescription,
                    ProductImage = product.ProductImage,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductNumber = 1,
                    ShippingStatus = ShippingStatus.Unknown,
                    userId = orderDTO.UserId


                };

                try
                {
                    _orderService.Add(newOrder);
                    _orderService.Save();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        message = ex.Message
                    });
                }
            }
           
        }
        [HttpPut]
        [Route("api/increaseNumber")]

        public IActionResult increaseNumber(int orderId)
        {
            var cartItem = _orderService.Get(i => i.Id == orderId);

            var perPrice = Convert.ToDecimal(cartItem.ProductPrice) / cartItem.ProductNumber;

            cartItem.ProductNumber += 1;
            
            var totalPrice = Convert.ToDecimal(cartItem.ProductPrice) + perPrice;

            cartItem.ProductPrice = totalPrice.ToString();

            try
            {


                _orderService.Update(cartItem);
                _orderService.Save();
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    message = ex.Message
                });

            }
        }

        [HttpPut]
        [Route("api/reduceNumber")]

        public IActionResult reduceNumber(int orderId)
        {
            var cartItem = _orderService.Get(i => i.Id == orderId);

            if(cartItem.ProductNumber <= 1)
            {
                try
                {


                    _orderService.Remove(cartItem);
                    _orderService.Save();
                    return Ok();

                }
                catch (Exception ex)
                {

                    return BadRequest(new
                    {
                        message = ex.Message
                    });

                }

            }

            var perPrice = Convert.ToDecimal(cartItem.ProductPrice) / cartItem.ProductNumber;

            cartItem.ProductNumber -= 1;

            var totalPrice = Convert.ToDecimal(cartItem.ProductPrice) - perPrice;

            cartItem.ProductPrice = totalPrice.ToString();
            try
            {


                _orderService.Update(cartItem);
                _orderService.Save();
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    message = ex.Message
                });

            }
        }


    }
}
