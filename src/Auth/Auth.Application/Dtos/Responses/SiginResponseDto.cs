using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Dtos.Responses
{
    public class SiginResponseDto
    {
        public string? Token { get; set; }
        public DateTime ExpierTime { get; set; }
        public bool isSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }

}
