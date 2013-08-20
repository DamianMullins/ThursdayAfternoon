using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Conventions;
using Nancy.Hosting.Aspnet;
using Nancy.TinyIoc;
using ThursdayAfternoon.Infrastructure.Data;
using ThursdayAfternoon.Infrastructure.Services;

namespace ThursdayAfternoon.Nancy
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        //protected override IRootPathProvider RootPathProvider
        //{
        //    get { return new CustomRootPathProvider(); }
        //}

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            Conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("styles", @"/Content", allowedExtensions: new[] { "css", "png" }));
            Conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("scripts", @"/Scripts", allowedExtensions: new[] { "js", "css", "svg", "woff", "ttf" }));
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<IDbContext, ThursdayContext>().AsPerRequestSingleton();
            container.Register(typeof(IRepository<>), typeof(EfRepository<>)).AsMultiInstance();

            //container.Register<IRepository<User>, EfRepository<User>>().AsPerRequestSingleton();
            //container.Register<IRepository<Presentation>, EfRepository<Presentation>>().AsPerRequestSingleton();

            container.Register<IPresentationService, PresentationService>().AsPerRequestSingleton();
            container.Register<IUserService, UserService>().AsPerRequestSingleton();
        }

        protected override void RequestStartup(TinyIoCContainer container, global::Nancy.Bootstrapper.IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            var formsAuthConfig = new FormsAuthenticationConfiguration
            {
                RedirectUrl = "~/login",
                UserMapper = container.Resolve<IUserMapper>()
            };

            FormsAuthentication.Enable(pipelines, formsAuthConfig);
        }
    }
}
