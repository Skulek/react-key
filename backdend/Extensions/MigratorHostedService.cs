using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using backend.Dtos;
using backend.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace backend.Extensions
{
    public class MigratorHostedService: IHostedService
    {
        private readonly IKeyValuePairService keyValuePairService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MigratorHostedService(IWebHostEnvironment webHostEnvironment,
            IKeyValuePairService keyValuePairService)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.keyValuePairService = keyValuePairService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var path = Path.Combine(webHostEnvironment.ContentRootPath, "KeyValuePairs.json");
                var jsonFile = System.IO.File.ReadAllText(path);
                var orders = JsonConvert.DeserializeObject<List<KeyValuePairDto>>(jsonFile);
                foreach (var order in orders)
                {
                    keyValuePairService.InsertKeyValuePair(order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Task.FromException(ex);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
