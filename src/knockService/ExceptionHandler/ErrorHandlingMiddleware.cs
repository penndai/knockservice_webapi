using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace knockService.ExceptionHandler
{
    public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate next;
		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			this.next = next;
		}
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await next(context);
				if(context.Response.StatusCode == 404) //handle 404 not found
				{
					await HandleExceptionAsync(context, new Exception());
				}
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}
		private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			if (exception == null) return;

			// if it's not one of the expected exception, set it to 500
			var code = HttpStatusCode.InternalServerError;

			if (exception is ArgumentOutOfRangeException) code = HttpStatusCode.BadRequest; //400
			else if (exception is TriangleException) code = HttpStatusCode.OK; //return 200,but display error message
			else
			{
				code = HttpStatusCode.NotFound;     //404	not found					
			}

			await WriteExceptionAsync(context, exception, code).ConfigureAwait(false);
		}
		private static async Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
		{
			var response = context.Response;
			response.ContentType = "text/plain";
			response.StatusCode = (int)code;

			if (code == HttpStatusCode.NotFound)
			{
				await response.WriteAsync(
					string.Format(
						"No HTTP resource was found that matches the request URI '{0}'", 
						context.Request.Path))
						.ConfigureAwait(false);			
			}
			else
			{
				await response.WriteAsync(exception.Message).ConfigureAwait(false);				
			}
		}
	}
}
