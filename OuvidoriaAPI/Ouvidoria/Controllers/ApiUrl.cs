namespace Ouvidoria.Controllers
{
    public class ApiUrl
    {
        private static string _urlBase;
        public static void SetUrlBase(string urlBase)
        {
            _urlBase = urlBase;
        }
        public static string Str(string route)
        {
            return $"{_urlBase}{route}";
        }
    }
}
