namespace SportsWorld.Web.App_Start
{
    using System.Web.Mvc;

    public class ViewEngineConfig
    {
        internal static void RegisterViewEngines(ViewEngineCollection viewEngineCollection)
        {
            viewEngineCollection.Clear();
            viewEngineCollection.Add(new RazorViewEngine());
        }
    }
}