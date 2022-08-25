﻿namespace Mango.Services.ProductAPI.Models.Dto
{
    public class ResponsDto
    {
        public bool IsSuccess { get; set; } = true;
        public object  Result { get; set; }
        public string  DisplayMessage { get; set; } = String.Empty;
        public List<string> ErrorMessages { get; set; }
    }
}
