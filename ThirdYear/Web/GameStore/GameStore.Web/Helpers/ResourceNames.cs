using System.Collections.Generic;

namespace GameStore.Web.Helpers
{
    public struct ResourceNames
    {
        public struct PaymentTypes
        {
            public static string Box { get; } = "Box";

            public static string Bank { get; } = "Bank";

            public static string Visa { get; } = "Visa";
        }

        public struct NameViews
        {
            public static string PaymentSuccessful { get; } = "PaymentSuccessful";
        }

        public struct Urls
        {
            public const string Games = "/games";

            public const string Basket = "/busket";

            public static IEnumerable<string> ForBoxPayment { get; set; }
        }

        public struct Classes
        {
            public static string Error { get; } = "Error";
        }

        public struct Methods
        {
            public static string NotFound { get; } = "SetStatusCodeNotFound";

            public static string BadRequest { get; } = "SetStatusCodeBadRequest";
        }

        public struct PathResourceJson
        {
            public const string UrlsForBoxPaymant = "ResourceVariables:UrlsForBoxPaymant";

            public const string DefaultConnection = "DefaultConnection";
        }
    }
}
