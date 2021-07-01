using Confluent.Kafka;
using kafka.eaton.common.infrastructure.dataaccess;
using kafka.eaton.consumer.api.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace kafka.eaton.consumer.api
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "kafka.eaton.producer.api", Version = "v1" });
            });

            services.AddHostedService<ProcessTelemetryService>();

            var consumerConfig = new ConsumerConfig();
            Configuration.Bind("consumer", consumerConfig);

            var producerConfig = new ProducerConfig();
            Configuration.Bind("producer", producerConfig);

            services.AddSingleton<ConsumerConfig>(consumerConfig);
            services.AddSingleton<ProducerConfig>(producerConfig);
            services.AddAutoMapper(typeof(Startup));
            ;

            //extension method for addtransient with options
            services.AddAccessData(options =>
            {
                options.KeySpace = Configuration.GetValue<string>("keyspace");
            });
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
        }
    }
}
