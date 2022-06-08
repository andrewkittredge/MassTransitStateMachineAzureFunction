using System;
using System.Diagnostics;
using MassTransit;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.AzureFunction;
using Sample.AzureFunction.Consumers;
using Sample.AzureFunction.StateMachines;
using Serilog;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Sample.AzureFunction
{
    public class Startup :
        FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var serilogger = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.Debug().CreateLogger();
            var loggerFactory = new LoggerFactory().AddSerilog(serilogger);

            LogContext.ConfigureCurrentLogContext(loggerFactory);
            Debug.WriteLine("Configing");
            builder.Services
                .AddScoped<InitiatePressReleaseBatchFunction>()
                .AddMassTransitForAzureFunctions(
                    cfg =>
                    {
                        cfg.AddConsumer<InitiatePressReleaseBatchConsumer>();
                        cfg.AddRequestClient<IStartPressReleaseBatchFromSender>(new Uri("queue:getting-started"));
                        cfg.AddSagaStateMachine<PressReleaseBatchStateMachine, PressReleaseBatchState>(typeof(PressReleaseBatchStateMachineDefinition)).InMemoryRepository();

                    },
                    "AzureWebJobsServiceBus",
                    (x, y) => y.ConfigureEndpoints(x));
        }
    }
}