using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RssApi.Utils.Exceptions;

namespace RssApi.Filters.Exceptions
{
    public class NotFoundExceptionAttribute :  Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled && context.Exception is NotFoundException notFoundEx)
            {
                context.Result = new NotFoundObjectResult(notFoundEx.Message);
                context.ExceptionHandled = true;
            }
        }
    }
}