using Nancy;
using Nancy.Responses.Negotiation;

namespace ThursdayAfternoon.Nancy.Modules
{
    public class PageModule : NancyModule
    {
        public PageModule()
        {
            Get["/"] = HomePage;
        }

        private Negotiator HomePage(dynamic parameters)
        {
            return View["/index/index"];
        }
    }
}