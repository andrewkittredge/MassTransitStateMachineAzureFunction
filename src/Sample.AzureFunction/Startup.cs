using System;
using System.Diagnostics;
using MassTransit;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Sample.AzureFunction;
using Sample.AzureFunction.Consumers;
using Sample.AzureFunction.StateMachines;

[assembly: FunctionsStartup(typeof(Startup))]


namespace Sample.AzureFunction
{
    public class Startup :
        FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            Debug.WriteLine("Configing");
            builder.Services
                .AddScoped<InitiatePressReleaseBatchFunction>()
                .AddMassTransitForAzureFunctions(cfg =>
                    {
                        cfg.AddConsumer<InitiatePressReleaseBatch>();
                        cfg.AddRequestClient<StartPressReleaseBatchFromSender>(new Uri("queue:getting-started"));
                        cfg.AddSagaStateMachine<PressReleaseBatchStateMachine, PressReleaseBatchState>(typeof(PressReleaseBatchStateMachineDefinition)).InMemoryRepository();
                    },
                    "AzureWebJobsServiceBus");
        }
    }
}