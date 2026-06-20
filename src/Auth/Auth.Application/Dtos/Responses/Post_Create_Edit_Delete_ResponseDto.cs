using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Dtos.Responses
{
    public class Post_Create_Edit_Delete_ResponseDto
    {
        public bool isSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
