namespace Perfect.SagaService.Host.Configuration.Models
{
    public class RabbitMqSettings
    {
        public const string Section = "RabbitMq";

        public string ConnectionString { get; set; }
    }
}
