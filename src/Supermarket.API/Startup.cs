﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Supermarket.API.Dominio.Persistencia;
using Supermarket.API.Dominio.Repositorios;
using Supermarket.API.Dominio.Repositorio;

namespace Supermarket.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            /// <summary>
            /// Configuracion que ancla el archivo de persistencia a este servicio para poder activarlo o descativarlo "lo centraliza"
            /// </summary>
            /// <typeparam name="SupermarketApiContext">Objeto que se va como parametro options </typeparam>
            /// <returns></returns>
            services.AddDbContext<SupermarketApiContext>(
                op => op.UseInMemoryDatabase("SupermarketApi")
                );
            //Declaracion para el manejo del patron de inyeccion de dependencia DI del repositorio que maneja la logica de negocio de categoria
            services.AddTransient<ICategoriaRepo, CategoriaRepo>(); 

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
