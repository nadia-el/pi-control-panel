namespace PiControlPanel.Api.GraphQL.Types.Output.Cpu
{
    using global::GraphQL.Types;
    using NLog;
    using PiControlPanel.Api.GraphQL.Extensions;
    using PiControlPanel.Domain.Contracts.Application;

    public class CpuLoadType : ObjectGraphType
    {
        public CpuLoadType(ICpuService cpuService, ILogger logger)
        {
            Field<CpuAverageLoadType>()
                .Name("Average")
                .ResolveAsync(async context =>
                {
                    logger.Info("Average Load field");
                    GraphQLUserContext graphQLUserContext = context.UserContext as GraphQLUserContext;
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    return await cpuService.GetAverageLoadAsync(businessContext);
                });
            Field<CpuRealTimeLoadType>()
                .Name("RealTime")
                .ResolveAsync(async context =>
                {
                    logger.Info("Real Time Load field");
                    GraphQLUserContext graphQLUserContext = context.UserContext as GraphQLUserContext;
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    return await cpuService.GetRealTimeLoadAsync(businessContext);
                });
        }
    }
}
