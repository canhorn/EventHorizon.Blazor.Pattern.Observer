namespace EventHorizon.Blazor.Pattern.Observer
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using EventHorizon.Blazor.Pattern.Observer.Count;
    using EventHorizon.Observer.State;
    using MediatR;
    using EventHorizon.Observer.Admin.State;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<GenericObserverState>()
                .AddSingleton<ObserverState>(services => services.GetService<GenericObserverState>())
                .AddSingleton<AdminObserverState>(services => services.GetService<GenericObserverState>());

            services.AddSingleton<StandardCountState>()
                .AddScoped<CountState>(services => services.GetService<StandardCountState>())
                .AddScoped<CountStateActions>(services => services.GetService<StandardCountState>());

            services.AddMediatR(
                typeof(Startup).Assembly,
                typeof(ObserverState).Assembly
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
