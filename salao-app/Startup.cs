using Dapper;
using Microsoft.OpenApi.Models;
using salao_app.Business.Interfaces;
using salao_app.Business.Services;
using salao_app.Repository.Handlers;
using salao_app.Repository.Intefaces;
using salao_app.Repository.Interfaces;
using salao_app.Repository.Services;
using salao_app.Services;
using salao_app.Services.Interfaces;
using System.Text.Json.Serialization;

namespace salao_app
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            SqlMapper.AddTypeHandler(new TimeSpanHandler());
            SqlMapper.AddTypeHandler(new DateTimeHandler());

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            // Adicionar e configurar o serviço do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });
            });

            services.AddControllersWithViews();

            // Configurar CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // Services
            services.AddScoped<IClientesService, ClientesService>();
            services.AddScoped<IHorarioService, HorarioService>();
            services.AddScoped<IBarbeiroService, BarbeiroService>();

            // Repository
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IHorarioRepository, HorarioRepository>();
            services.AddScoped<IBarbeiroRepository, BarbeiroRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Adicionar middleware do Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Habilitar CORS
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
