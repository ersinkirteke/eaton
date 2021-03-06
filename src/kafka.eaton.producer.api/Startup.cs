using Confluent.Kafka;
using FluentValidation;
using FluentValidation.AspNetCore;
using kafka.eaton.common.domain.models;
using kafka.eaton.producer.api.extensions;
using kafka.eaton.producer.api.settings;
using kafka.eaton.producer.api.validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace kafka.eaton.producer.api
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
            services.AddMvc().AddFluentValidation();
            services.AddTransient<IValidator<TelemetryDto>, TelemetryValidator>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "kafka.eaton.producer.api", Version = "v1" });
            });

            var producerConfig = new ProducerConfig();
            Configuration.Bind("producer", producerConfig);
            services.AddSingleton<ProducerConfig>(producerConfig);

            services.Configure<ProducerSettings>(options => Configuration.GetSection("kafkasettings").Bind(options));

            services.ConfigureCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "kafka.eaton.producer.api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ConfigureCustomExceptionMiddleware();
        }
    }
}
