using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfect.SagaService.Host.Configuration.Models
{
    public class AzureServiceBusSettings
    {
        public const string Section = "AzureServiceBus";

        public string ConnectionString { get; set; }
    }
}
