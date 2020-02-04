namespace PiControlPanel.API.GraphQL.Schemas
{
    using global::GraphQL;
    using global::GraphQL.Types;

    public class ControlPanelSchema : Schema
    {
        public ControlPanelSchema(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {
            Query = dependencyResolver.Resolve<ControlPanelQuery>();
            //Mutation = dependencyResolver.Resolve<PiControlPanelMutation>();
            //Subscription = dependencyResolver.Resolve<PiControlPanelSubscription>();
        }
    }
}
