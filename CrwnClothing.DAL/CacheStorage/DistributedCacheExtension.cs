﻿using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CrwnClothing.DAL.Redis
{
    public static class DistributedCacheExtension
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
          string recordId,
          T data,
          TimeSpan? absoluteExpireTime = null,
          TimeSpan? unusedExpireTime = null)
        {

            var options = new DistributedCacheEntryOptions
            {

                //if absolute expire time is not null use that value if its null add lifespan of 60 seconds
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(300),
                //if you dont use cache item for some time delete it
                //if its null its not going to be set
                SlidingExpiration = unusedExpireTime
            };

            var jsonData = JsonSerializer.Serialize(data);

            await cache.SetStringAsync(recordId, jsonData, options);

        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {

            var jsonData = await cache.GetStringAsync(recordId);

            if (jsonData is null)
            {
                throw new Exception("Record not found!");
            }


            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
