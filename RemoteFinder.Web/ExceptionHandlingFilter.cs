using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using RemoteFinder.BLL.Exceptions;

namespace RemoteFinder.Web
{
    public class ExceptionHandlingFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is UnauthenticatedException)
            {
                context.Result = new JsonResult(new {errorMessage = exception.Message})
                {
                    StatusCode = (int) HttpStatusCode.Unauthorized
                };
                context.ExceptionHandled = true;
                
                return;
            }
            
            if (exception is NotFoundException)
            {
                context.Result = new JsonResult(new {errorMessage = exception.Message})
                {
                    StatusCode = (int) HttpStatusCode.NotFound
                };
                context.ExceptionHandled = true;
                
                return;
            }
            
            if (exception is AccessDeniedException)
            {
                context.Result = new JsonResult(new {errorMessage = exception.Message})
                {
                    StatusCode = (int) HttpStatusCode.Forbidden
                };
                context.ExceptionHandled = true;
                
                return;
            }

            if (exception is ValidationException)
            {
                context.Result = new JsonResult(new {errorMessage = exception.Message})
                {
                    StatusCode = (int) HttpStatusCode.BadRequest
                };
                context.ExceptionHandled = true;
                
                return;
            }

            if (exception is ValidationFormException validationFormException)
            {
                context.Result = new JsonResult(new {errors = validationFormException.Errors})
                {
                    StatusCode = (int) HttpStatusCode.BadRequest
                };
                context.ExceptionHandled = true;
                
                return;
            }

            if (exception is DbUpdateException updateException)
            {
                context.Result = new JsonResult(new {errorMessage = $"{updateException.Message} {updateException.InnerException}"})
                {
                    StatusCode = (int) HttpStatusCode.InternalServerError
                };
                context.ExceptionHandled = true;
                
                return;
            }

            if (exception != null)
            {
                context.Result = new JsonResult(new {errorMessage = $"{exception.Message} {exception.StackTrace} {exception.InnerException}"})
                {
                    StatusCode = (int) HttpStatusCode.BadRequest
                };
                context.ExceptionHandled = true; 
            }
        }
    }
}