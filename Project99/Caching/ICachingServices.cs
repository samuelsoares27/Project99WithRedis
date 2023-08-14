namespace Project99.Caching
{
    public interface ICachingServices
    {
        Task SetAsync(string key, string value);
        Task<string> GetAsync(string key);
    }
}
