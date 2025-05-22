using E_CommerceWebsite.BusinessLogicLayer.Abstract;
using E_CommerceWebsite.BusinessLogicLayer.Concrete;
using E_CommerceWebsite.EntitiesLayer.Model;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Linq;

namespace E_CommerceWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _context;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IDeliveryAdressesService _deliveryAdressesService;
        private readonly IMasterOrderService _masterOrderService;

       

        public OrderController(IOrderService orderService, 
            IProductService productService, 
            IHttpContextAccessor context, 
            ITokenService tokenService, 
            IUserService userService, 
            IDeliveryAdressesService deliveryAdressesService,
            IMasterOrderService masterOrderService)
        {
            _orderService = orderService;
            _productService = productService;
            _context = context;
            _tokenService = tokenService;
            _userService = userService;
            _deliveryAdressesService = deliveryAdressesService;
            _masterOrderService = masterOrderService;
        }

        [HttpGet]
        [Route("api/getNumber")]

        public IActionResult GetNumber() {

            var principal = _tokenService.GetTokenPrincipal();

            var userId = _userService.Get(i => i.UserName == principal.Identity.Name).Id;

            var itemNumber = _orderService.GetFilteredList(i => i.userId == userId && i.OrderStatus == OrderStatus.InCart).Select(i => i.ProductNumber).Sum();

            

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
        public IActionResult GetCartItems() {

            

            var principal = _tokenService.GetTokenPrincipal();

            var userId = _userService.Get(i => i.UserName == principal.Identity.Name).Id;


            try
            {
                List<CartDTO> cartDTO = new List<CartDTO>();

                var cartItems = _orderService.GetFilteredList(i => i.OrderStatus == OrderStatus.InCart && i.userId == userId);

                foreach(var cartItem in cartItems)
                {
                    cartDTO.Add(new CartDTO
                    {
                        Id = cartItem.Id,
                        ProductImage = cartItem.ProductImage,
                        ProductName = cartItem.ProductName,
                        ProductNumber = cartItem.ProductNumber,
                        ProductPrice = cartItem.ProductPrice,
                        Size = cartItem.Size
                    });

                }

                if (cartItems.Count != 0)
                {

                    return Ok(cartDTO);
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


            

            var principal = _tokenService.GetTokenPrincipal();

            var userId = _userService.Get(i => i.UserName == principal.Identity.Name).Id;


            var existCartItems = _orderService.GetFilteredList(i => i.userId == userId && i.OrderStatus == OrderStatus.InCart);

            if (existCartItems.Count != 0 && existCartItems.Any(i => i.ProductImage == product.ProductImage && i.Size == orderDTO.Size))
            {
                var existCartItem = existCartItems.First(i => i.ProductImage == product.ProductImage && i.Size == orderDTO.Size);


                var perPrice = Convert.ToDecimal(existCartItem.ProductPrice) / existCartItem.ProductNumber;

                existCartItem.ProductNumber += 1;

                var totalPrice = perPrice * existCartItem.ProductNumber;

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
                    userId = userId,
                    Size = orderDTO.Size
                   


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


        [HttpPut]
        [Route("api/reduceNumberOutOfCart")]

        public IActionResult ReduceNumberOutOfCart(ReduceNumberDTO reduceNumberDTO)
        {
            var principal = _tokenService.GetTokenPrincipal();

            var userId = _userService.Get(i => i.UserName == principal.Identity.Name).Id;

            var cartItems = _orderService.GetFilteredList(i => i.OrderStatus == OrderStatus.InCart && i.userId == userId);

            var product = _productService.Get(i => i.Id == reduceNumberDTO.ProductId);

            var cartItem = cartItems.Where(i => i.ProductImage == product.ProductImage && i.Size == reduceNumberDTO.Size ).FirstOrDefault();

            if (cartItem != null)
            {
                if(cartItem.ProductNumber == 1)
                {
                    try
                    {
                        _orderService.Remove(cartItem);
                        _orderService.Save();
                        return Ok();
                    }
                    catch
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    cartItem.ProductNumber -= 1;

                    try
                    {
                        _orderService.Update(cartItem);
                        _orderService.Save();
                        return Ok();
                    }
                    catch
                    {
                        return BadRequest();
                    }
                    
                    
                }
            }
            return BadRequest();

        
        }

        [HttpPost]
        [Route("api/completeOrder")]


        public IActionResult CompleteOrder(AdressDTO adress)
        {
            var principal = _tokenService.GetTokenPrincipal();

            var user = _userService.Get(i => i.UserName == principal.Identity.Name);

            var cartItems = _orderService.GetFilteredList(i => i.OrderStatus == OrderStatus.InCart && i.userId == user.Id);

            foreach (var cartItem in cartItems)
            {
                
                var product = _productService.GetProductSizes(i => i.ProductImage == cartItem.ProductImage);
                if (product != null)
                {
                    var productSize = product.ProductSizes.FirstOrDefault(ps => ps.Size == cartItem.Size);
                    if (productSize != null)
                    {
                        
                        int currentStock = int.Parse(productSize.Stock);
                        int newStock = currentStock - cartItem.ProductNumber;
                        if (newStock >= 0)
                        {
                            productSize.Stock = newStock.ToString();
                            _productService.Update(product);
                        }
                        else
                        {
                            return BadRequest(new { message = "Yetersiz stok!" });
                        }
                    }
                }

                
              

               
                    
                    cartItem.OrderStatus = OrderStatus.Ordered;
                    cartItem.CreatedDate = DateTime.Now;

                try
                {
                    _orderService.Update(cartItem);
                    _orderService.Save();
                    
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
                    
               
            }

            MasterOrder masterOrder = new MasterOrder()
            {
                CreatedDate = DateTime.Now,
                Orders = cartItems,
                Guid = Guid.NewGuid(),
                DeliveryAdress = new DeliveryAdress()
                {
                    Adress = adress.Adress

                },
                userId = user.Id

            };

            try
            {
                _masterOrderService.Add(masterOrder);
                _masterOrderService.Save();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("api/getPastOrders")]

        public IActionResult GetPastOrders()
        {

            var principal = _tokenService.GetTokenPrincipal();

            var user = _userService.Get(i => i.UserName == principal.Identity.Name);
         
            try
            {
                var pastOrders = _masterOrderService.MOIncludeRelationTables(i => i.userId == user.Id);
                List<MasterOrderDTO> masterOrderDTOs = new List<MasterOrderDTO>();

                foreach (var pastOrder in pastOrders)
                {
                    masterOrderDTOs.Add(new MasterOrderDTO()
                    {
                        Guid = pastOrder.Guid.ToString(),
                        CreatedDate = pastOrder.CreatedDate,
                        DeliveryAdress = pastOrder.DeliveryAdress.Adress,
                        Orders = pastOrder.Orders
                    });
                }
                return Ok(masterOrderDTOs);
            }
            catch
            {
                return BadRequest();
            }

            
        }

        [HttpGet]
        [Route("api/getOrder")]

        public IActionResult GetOrder(string guid) {

           


            try
            {
                var order = _masterOrderService.MOIncludeRelationTables(i => i.Guid == Guid.Parse(guid)).FirstOrDefault();

                MasterOrderDTO masterOrderDTO = new MasterOrderDTO()
                {
                    CreatedDate = order.CreatedDate,
                    DeliveryAdress = order.DeliveryAdress.Adress,
                    Orders = order.Orders,
                    Guid = order.Guid.ToString()
                };
                return Ok(masterOrderDTO);
            }
            catch
            {
                return BadRequest();
            }





            
        }


    }
}
