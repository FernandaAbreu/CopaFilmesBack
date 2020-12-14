using System;
namespace CopaFilmesBackEnd.Helpers
{
    public class CustomHttpException :  Exception
    {
        public int StatusCode { get; set; }
        public object ErrorMessage { get; set; }

        public CustomHttpException(int statusCode, string message)
        {
            this.StatusCode = statusCode;
            this.ErrorMessage = new { error = message };
        }
    }
}
