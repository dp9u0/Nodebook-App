using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsulSample
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
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }


      app.UseRouting();

      // register this service
      ServiceEntity serviceEntity = new ServiceEntity
      {
        IP = GetLocalIP(),
        Port = Convert.ToInt32(Configuration["Service:Port"]),
        ServiceName = Configuration["Service:Name"],
        ConsulIP = Configuration["Consul:IP"],
        ConsulPort = Convert.ToInt32(Configuration["Consul:Port"])
      };
      app.RegisterConsul(lifetime, serviceEntity);

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }


    /// <summary>
    /// GET Local IP
    /// </summary>
    /// <returns></returns>
    private static string GetLocalIP()
    {
      try
      {

        string HostName = Dns.GetHostName();
        IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
        for (int i = 0; i < IpEntry.AddressList.Length; i++)
        {
          if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
          {
            string ip = "";
            ip = IpEntry.AddressList[i].ToString();
            return IpEntry.AddressList[i].ToString();
          }
        }
        return "127.0.0.1";
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
    }
  }
}
