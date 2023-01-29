using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Services.BaseService
{
    public class DistributedCacheServices
    {
        private readonly IDistributedCache _distributedCache;

        
        public async Task<bool> LoadForecast()
        {
            // LocationData = null;
            // IsCachedData = null;
            //
            // string recordKey = "WeatherForecast_" + DateTime.Now.ToString("yyyyMMdd_hhmm");
            //
            
            // Forecast = await _distributedCache.GetRecordAsync<WeatherForecast[]>(recordKey);
            //
            // if (Forecast is null)
            // {
            //     Forecast = _forecastServices.GetForecastAsync(DateTime.Now);
            //     LocationData = $"Loaded From API at {DateTime.Now}";
            //     IsCachedData = "";
            //
            //     await _distributedCache.SetRecordAsync(recordKey, Forecast, TimeSpan.FromHours(1),
            //         TimeSpan.FromMinutes(30));
            // }
            // else
            // {
            //     LocationData = $"Loaded From Cache at {DateTime.Now}";
            //     IsCachedData = "text-danger";
            // }
            //
            // return Forecast;
            return false;
        }
    }
}