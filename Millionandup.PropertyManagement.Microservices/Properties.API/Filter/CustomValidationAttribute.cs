using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Properties.API.Filter;

public class CustomValidationAttribute : ActionFilterAttribute
{
    /// <summary>
    /// sacamos errores del model para enviar respuesta personalizada
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            // obtenemos errores del modelo
            List<string>? errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)?
                    .SelectMany(v => v.Errors)?
                    .Select(v => v.ErrorMessage)?
                    .ToList();

            var responseObj = new
            {
                Message = "One or more validation errors occurred.",
                Errors = errors
            };

            context.Result = new JsonResult(responseObj)
            {
                StatusCode = 422
            };
        }
    }
}
