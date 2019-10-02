using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Store.Infrastructure;
using System;

namespace Store.Models
{
    public class SessionCart : Cart
    {
        [JsonIgnore]
        public ISession Session { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            var session = services.GetRequiredService<IHttpContextAccessor>()?
                    .HttpContext.Session;
            var cart = session?.GetJson<SessionCart>("Cart")
                    ?? new SessionCart();
            cart.Session = session;

            return cart;
        }

        public override void AddItem(Toy toy, int quantity)
        {
            base.AddItem(toy, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(int toyId)
        {
            base.RemoveLine(toyId);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}