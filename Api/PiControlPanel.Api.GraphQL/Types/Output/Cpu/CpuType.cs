namespace PiControlPanel.Api.GraphQL.Types.Output.Cpu
{
    using global::GraphQL.DataLoader;
    using global::GraphQL.Types;
    using global::GraphQL.Relay.Types;
    using NLog;
    using PiControlPanel.Api.GraphQL.Extensions;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models.Hardware.Cpu;

    public class CpuType : ObjectGraphType<Cpu>
    {
        public CpuType(IDataLoaderContextAccessor accessor, ICpuService cpuService,
            ILogger logger)
        {
            Field(x => x.Cores);
            Field(x => x.Model);

            Field<CpuAverageLoadType, CpuAverageLoad>()
                .Name("AverageLoad")
                .ResolveAsync(context =>
                {
                    logger.Info("Average Load field");
                    GraphQLUserContext graphQLUserContext = context.UserContext as GraphQLUserContext;
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    var cores = context.Source.Cores;
                    var loader = accessor.Context.GetOrAddLoader(
                        "GetAverageLoadAsync",
                        () => cpuService.GetAverageLoadAsync(businessContext, cores));

                    return loader.LoadAsync();
                });

            Field<CpuRealTimeLoadType>()
                .Name("RealTimeLoad")
                .ResolveAsync(async context =>
                {
                    logger.Info("Real Time Load field");
                    GraphQLUserContext graphQLUserContext = context.UserContext as GraphQLUserContext;
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    return await cpuService.GetRealTimeLoadAsync(businessContext);
                });

            Field<CpuTemperatureType>()
                .Name("Temperature")
                .ResolveAsync(async context =>
                {
                    logger.Info("Temperature field");
                    GraphQLUserContext graphQLUserContext = context.UserContext as GraphQLUserContext;
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    return await cpuService.GetLastTemperatureAsync();
                });

            Connection<CpuTemperatureType>()
                .Name("Temperatures")
                .Bidirectional()
                .ResolveAsync(async context =>
                {
                    logger.Info("Temperatures connection");
                    GraphQLUserContext graphQLUserContext = context.UserContext as GraphQLUserContext;
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    var temperatures = await cpuService.GetTemperaturesAsync();

                    return ConnectionUtils.ToConnection(temperatures, context);
                });
        }
    }
}
