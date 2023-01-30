using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RssApi.Utils;

namespace RssApi.Filters.Exceptions
{
    public class FeedExceptionAttribute :  Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                context.Result = new BadRequestObjectResult(Messages.WrongFeedUrl);
                context.ExceptionHandled = true;
            }
        }
    }
}