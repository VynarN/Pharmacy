namespace Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces
{
    public interface ICookieService
    {
        public void CreateCookie(bool isPersistent, string accessToken, string refreshToken);

        public void RefreshCookie(string accessToken, string refreshToken);

        public string GetCookieValue(string cookieName);

        public void CleanCookies();
    }
}
