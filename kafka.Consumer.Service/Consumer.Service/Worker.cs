using System.Text.Json;
using Confluent.Kafka;
using Consumer.Database;
using Consumer.Database.Entities;
using Consumer.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = "myconsumerclient",
                GroupId = "employeeConsumerGroup",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build())
            {
                consumer.Subscribe("employeeTopic");

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        try
                        {
                            var consumerData = consumer.Consume(stoppingToken);

                            if (consumerData != null)
                            {
                                using (var scope = _serviceProvider.CreateScope())
                                {
                                    var dbContext = scope.ServiceProvider.GetRequiredService<EmployeeReportDbContext>();

                                    var employee = JsonSerializer.Deserialize<Employee>(consumerData.Message.Value);
                                    _logger.LogInformation("Consumed: {employee}", employee);

                                    var employeeReport = new EmployeeReport(Guid.NewGuid(), employee.Id, employee.Name, employee.Surname);
                                    dbContext.Reports.Add(employeeReport);
                                    await dbContext.SaveChangesAsync();
                                }
                            }
                        }
                        catch (ConsumeException ex)
                        {
                            _logger.LogError(ex, "Error occurred while consuming messages.");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Consumer cancellation requested.");
                }
                finally
                {
                    consumer.Close();
                }
            }

        }
    }
}
