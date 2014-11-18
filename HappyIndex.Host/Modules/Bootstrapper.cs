using HappyIndex.Host.Lib;
using Nancy;
using Nancy.Conventions;

public class CustomBoostrapper : DefaultNancyBootstrapper
{
    protected override void ConfigureConventions(NancyConventions conventions)
    {
        base.ConfigureConventions(conventions);
    }

    protected override IRootPathProvider RootPathProvider
    {
        get { return new CustomRootPathProvider(); }
    }
}