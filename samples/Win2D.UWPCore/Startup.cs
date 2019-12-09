// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Win2D.UWPCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostEnvironment env)
        //{
        //    //if (env.IsDevelopment())
        //    //{
        //    //    app.UseDeveloperExceptionPage();
        //    //}

        //    //app.UseHttpsRedirection();

        //    //app.UseRouting();

        //    //app.UseAuthorization();

        //    //app.UseEndpoints(endpoints =>
        //    //{
        //    //    endpoints.MapControllers();
        //    //});
        //}
    }
}
