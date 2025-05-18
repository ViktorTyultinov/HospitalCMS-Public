namespace Hospital.IntegrationTests.Configuration
{
    [CollectionDefinition("ServiceProvider collection", DisableParallelization = true)]
    public class ServiceProviderCollection : ICollectionFixture<TestServiceProviderFixture> { }
}