using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger.Model;
using Tarefas.Data;
using Tarefas.Interface;
using Tarefas.Repositorio;


namespace Tarefas
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();  //Serviço para habilitar o uso das Controllers
            services.AddDbContext<ClassDbContext>(); //Injeção de dependencia, permitindo uso do Context em toda parte do codigo
            services.AddScoped<ITarefas, TarefasRepositorio>(); //Configurando serviço que indica para qual Repositorio a Interface vai interagir

            //services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Controle de Tarefas - parte I", Version = "v1" }));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Teste - CRUD Tarefas",
                    Description = "Projeto CRUD Basico - Tarefas" 
                }
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //falta configurar swagger
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo Api"));

            //controla mapeamento e indica qual o valor padrão
            //para quando não fornecido parametros
            app.UseEndpoints(endpoints =>
            {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}