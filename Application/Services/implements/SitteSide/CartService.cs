using Application.DTOs.UserSide.Account;
using Application.Services.Interfaces.UserSide;
using Application.ViewModel.UserSide;
using Domain.entities.Cart;
using Domain.entities.GamePart.Game;
using Domain.IRepository.GamePart;
using Domain.IRepository.UserPart;
using Microsoft.AspNetCore.Components.Forms;




namespace Application.Services.implements.SitteSide;

public class CartService : ICartService
{
    #region Ctor
    private readonly ICartRepository _cartRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IPlatformRepository _platformRepository;
    public CartService(ICartRepository cartRepository,
        IAccountRepository accountRepository,
        IGameRepository gameRepository,
        IPlatformRepository platformRepository)
    {
        _cartRepository = cartRepository;
        _accountRepository = accountRepository;
        _gameRepository = gameRepository;
        _platformRepository = platformRepository;
    }
    #endregion

    #region General


    public async Task<List<CartDto>> ShowProductIncart(int userid)
    {
        var cart = await _cartRepository.ShowCartDetails(userid);



        if (cart != null && cart.Any())
        {
            List<CartDto> model = new List<CartDto>();

            foreach (var item in cart)
            {
                CartDto childmodel = new CartDto()
                {
                    Id = item.CartDetailsId,
                    GameId = item.GameId,
                    GameName = item.Game.Name,
                    Platform = item.Platform,
                    Quantity = item.Quantity,
                    Price = item.Game.Price,
                    Screenshot = item.Game.Screenshots.First().AvararUrl,
                    UserId = userid
                };
                model.Add(childmodel);
            }
            return model;
        }
        return null;
    }


    public async Task AddToCart(ProductViewModel model, int userid)
    {
        if (model != null)
        {
            var usercarts = await _cartRepository.GetCArtByUserId(userid);




            if (usercarts == null)
            {
                Carts newcart = new Carts()
                {
                    UserId = userid,
                    Price = 0,
                    IsFinally = false,

                };

                await _cartRepository.AddUserCartToCarts(newcart);

            }

            var user = await _accountRepository.GetUserByIdAsync(userid);
            var cartid = user.cart.Where(x => x.IsFinally == false).First().CartId;
            var game = await _gameRepository.GetGameById(model.Game.Id);
            var platform = await _platformRepository.GetSelectedPlatform(model.Platformid);
            if (platform != null)
            {
                var exist = _cartRepository.IsGameExistInCart(cartid, game.Id, platform.Name);

                if (exist != null)
                {
                    exist.Quantity += model.Quantity;
                    await _cartRepository.AddOneMoreToCart(exist);
                }
                else
                {
                    CartDeatails cartdetails = new CartDeatails()
                    {
                        CartId = cartid,
                        GameId = model.Game.Id,
                        Price = game.Price,
                        Quantity = model.Quantity,
                        Platform = platform.Name,




                    };
                    await _cartRepository.AddToCart(cartdetails);
                }
            }


        }
    }

    public async Task<bool> DeleteCartProduct(int id)
    {
        var cart = await _cartRepository.FindCartDetailsById(id);
        if (cart == null)
        {

            return false;
        }
        _cartRepository.DeleteCart(cart);
        await _cartRepository.SaveChanges();
        return true;

    }



    public async Task<CheckOutViewModel> ShowCheckOut(int user)
    {
        var cart = await _cartRepository.ShowCartDetails(user);



        if (cart != null && cart.Any())
        {
            List<OrderDetailsViewModel> model = new List<OrderDetailsViewModel>();
            foreach (var item in cart)
            {
                OrderDetailsViewModel childmodel = new OrderDetailsViewModel()
                {
                    CartDetailsId = item.CartDetailsId,
                    CartId = item.CartId,
                    GameId = item.GameId,
                    GameName = item.Game.Name,
                    Platform = item.Platform,
                    Price = item.Price,
                    Quantity = item.Quantity,
                };
                model.Add(childmodel);
            }
            CheckOutViewModel checkOutViewModel = new CheckOutViewModel()
            {
                OrderDetails = model,
            };
            return checkOutViewModel;
        }
        return null;
    }



    public async Task<CheckOutViewModel> GetOrderDetails(int orderid)
    {
        var OrderItems = await _cartRepository.OrderDeatails(orderid);
        var location = await _cartRepository.LocationAsync(orderid);

        List<OrderDetailsViewModel> detailsmodel = new List<OrderDetailsViewModel>();

        foreach (var item in OrderItems)
        {
            OrderDetailsViewModel childmodel = new OrderDetailsViewModel()
            {
                CartDetailsId = item.CartDetailsId,
                GameId = item.GameId,
                GameName = item.Game.Name,
                ItemImage = item.Game.Screenshots.First().AvararUrl,
                Platform = item.Platform,
                Price = item.Price,
                Quantity = item.Quantity,


            };
            detailsmodel.Add(childmodel);
        }



        BillingViewModel locationmodel = new BillingViewModel()
        {

            FirstName = location.FirstName,
            LastName = location.LastName,
            Address = location.Address,
            City = location.City,
            Email = location.Email,
            OrderNote = location.OrderNote,
            PhoneNumber = location.PhoneNumber,
            PosstCode = location.PostCode,

        };

        var Tcode = OrderItems.Select(x => x.Cart.TrackingPostCode).ToString();

        OrderDetailsViewModel cartid = new OrderDetailsViewModel()
        {
            CartId = orderid,
            TrackingPostCode = Tcode

        };


        CheckOutViewModel model = new CheckOutViewModel()
        {
            Billing = locationmodel,
            OrderDetails = detailsmodel,
            oreder = cartid

        };


        return model;
    }

    // public async Task MinusQuantityOfGames(List<Game> games)
    //{

    //}

    public async Task<bool> SubmitOrder(CheckOutViewModel model, int userid)
    {
        var cart = await _cartRepository.GetCArtByUserId(userid);

        if(cart.CartDeatails != null && cart.CartDeatails.Any())  
        {
            var billing = model.Billing;

            #region AddLocation to order
            Location location = new Location()
            {
                FirstName = billing.FirstName,
                LastName = billing.LastName,
                Address = billing.Address,
                City = billing.City,
                Email = billing.Email,
                OrderNote = billing.OrderNote,
                PhoneNumber = billing.PhoneNumber,
                PostCode = billing.PosstCode,
                CartId = cart.CartId

            };

            await _cartRepository.AddLocation(location);

            #endregion

            decimal total = 0;
            foreach (var item in cart.CartDeatails)
            {
                total += item.Price * item.Quantity;
            }

            cart.RegestredDate = DateTime.Now;
            cart.Price = total;
            cart.IsFinally = true;
            cart.Status = OrderStatus.Registred;
            cart.LocationId = location.Id;


            #region Quantit of games
            foreach (var item in cart.CartDeatails)
            {

                var game = await _gameRepository.GetGameById(item.GameId);
                if (game != null)
                {

                    game.Quantitiy -= item.Quantity;
                }
                _gameRepository.UpdateGame(game);
                #endregion

            }


            await _cartRepository.FinalOrder(cart);
            await _cartRepository.SaveChanges();
            return true;
        }

        return false;
       
        #endregion

    }
}
