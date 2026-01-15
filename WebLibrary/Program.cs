namespace WebLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(options =>
            {
                // Optional: Configure session options, e.g., timeout
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Default is 20 minutes
                options.Cookie.HttpOnly = true; // Make the session cookie inaccessible to client-side scripts
                options.Cookie.IsEssential = true; // Essential cookies are exempt from GDPR requirements
            });

            // ... other services like AddControllersWithViews()

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Guest}/{action=HomePage}/{id?}");

            app.Run();
        }
    }
}
