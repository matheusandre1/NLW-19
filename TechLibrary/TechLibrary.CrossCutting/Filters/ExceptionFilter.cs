﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechLibrary.Application.Response;
using TechLibrary.Exception;

namespace TechLibrary.CrossCutting.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is TechLibraryException techLibraryException)
            {
                context.HttpContext.Response.StatusCode = (int)techLibraryException.GetStatusCode();
                context.Result = new ObjectResult(new ResponseErrorMessages
                {
                    Errors = techLibraryException.GetErrorMessages()
                });
            }
            else 
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = new ObjectResult(new ResponseErrorMessages
                {
                    Errors = ["Erro desconhecido"]
                });

            }
        }
    }
}
