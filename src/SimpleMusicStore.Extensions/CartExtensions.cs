//using Microsoft.AspNetCore.Http;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//
//namespace SimpleMusicStore.Extensions
//{
//    public static class CartExtensions
//    {
//        private const string CART = "cart";
//
//        public static void SaveCart(this ISession session, string content)
//        {
//            session.SetString(CART, content);
//        }
//
//        public static string Serialized(this IDictionary<int, int> collection)
//        {
//            return JsonConvert.SerializeObject(collection);
//        }
//    }
//}
