namespace HastaneClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //2.yöntem
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
            //3.Yöntem
            builder.Services.AddCors(options=>options.AddPolicy("myclient",opt=>opt.WithOrigins("https://localhost:7166","").SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyHeader().AllowAnyMethod()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //1.Yöntem
            //app.UseCors(options =>
            //{
            //    options.AllowAnyHeader().AllowAnyHeader().AllowAnyMethod();
            //});
            //app.UseCors();

            app.UseCors(options=>options.AllowAnyOrigin());

            app.UseCors("myClient");
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}