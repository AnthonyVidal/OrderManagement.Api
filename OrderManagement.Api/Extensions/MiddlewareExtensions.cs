using OrderManagement.Api.Middlewares;

namespace OrderManagement.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static WebApplication UseMiddlewareExtensions(
            this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            //app.MapControllers();

            return app;
        }
    }
}
