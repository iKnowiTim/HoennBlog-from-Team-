using System;
using System.Collections.Generic;
using System.Text;
using BlogMVVM.Dto;

namespace BlogMVVM.Exceptions
{
    public class HoennApiException : Exception
    {
        public HoennApiException(string message, ErrorDto errorDto) : base(message)
        {
            ErrorDto = errorDto;
        }

        public ErrorDto ErrorDto { get; }
    }
}
