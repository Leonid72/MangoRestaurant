﻿namespace Mango.Web
{
    public static class SD
    {
        public static string ProuctAPIBase { get; set; }
        public static string ShoppingCartAPIBase { get; set; }

        public enum ApiType {
            GET,
            POST,
            PUT,
            DELETE
        }

    }
}
