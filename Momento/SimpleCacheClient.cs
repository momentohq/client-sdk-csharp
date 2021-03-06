using System;
using System.Linq;
using System.Threading.Tasks;
using MomentoSdk.Internal;
using MomentoSdk.Responses;
using MomentoSdk.Exceptions;
using System.Collections.Generic;

namespace MomentoSdk;

public class SimpleCacheClient : ISimpleCacheClient
{
    private readonly ScsControlClient controlClient;
    private readonly ScsDataClient dataClient;

    /// <summary>
    /// Client to perform operations against the Simple Cache Service.
    /// </summary>
    /// <param name="authToken">Momento JWT.</param>
    /// <param name="defaultTtlSeconds">Default time to live for the item in cache.</param>
    /// <param name="dataClientOperationTimeoutMilliseconds">Deadline (timeout) for communicating to the server. Defaults to 5 seconds.</param>
    public SimpleCacheClient(string authToken, uint defaultTtlSeconds, uint? dataClientOperationTimeoutMilliseconds = null)
    {
        ValidateRequestTimeout(dataClientOperationTimeoutMilliseconds);
        Claims claims = JwtUtils.DecodeJwt(authToken);

        this.controlClient = new(authToken, claims.ControlEndpoint);
        this.dataClient = new(authToken, claims.CacheEndpoint, defaultTtlSeconds, dataClientOperationTimeoutMilliseconds);
    }

    public CreateCacheResponse CreateCache(string cacheName)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        return this.controlClient.CreateCache(cacheName);
    }

    public DeleteCacheResponse DeleteCache(string cacheName)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        return this.controlClient.DeleteCache(cacheName);
    }

    public ListCachesResponse ListCaches(string? nextPageToken = null)
    {
        return this.controlClient.ListCaches(nextPageToken);
    }

    public async Task<CacheSetResponse> SetAsync(string cacheName, byte[] key, byte[] value, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(key, nameof(key));
        Utils.ArgumentNotNull(value, nameof(value));

        return await this.dataClient.SetAsync(cacheName, key, value, ttlSeconds);
    }

    public async Task<CacheGetResponse> GetAsync(string cacheName, byte[] key)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(key, nameof(key));

        return await this.dataClient.GetAsync(cacheName, key);
    }

    public async Task<CacheDeleteResponse> DeleteAsync(string cacheName, byte[] key)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(key, nameof(key));

        return await this.dataClient.DeleteAsync(cacheName, key);
    }

    public async Task<CacheSetResponse> SetAsync(string cacheName, string key, string value, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(key, nameof(key));
        Utils.ArgumentNotNull(value, nameof(value));

        return await this.dataClient.SetAsync(cacheName, key, value, ttlSeconds);
    }

    public async Task<CacheGetResponse> GetAsync(string cacheName, string key)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(key, nameof(key));

        return await this.dataClient.GetAsync(cacheName, key);
    }

    public async Task<CacheDeleteResponse> DeleteAsync(string cacheName, string key)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(key, nameof(key));

        return await this.dataClient.DeleteAsync(cacheName, key);
    }

    public async Task<CacheSetResponse> SetAsync(string cacheName, string key, byte[] value, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(key, nameof(key));
        Utils.ArgumentNotNull(value, nameof(value));

        return await this.dataClient.SetAsync(cacheName, key, value, ttlSeconds);
    }

    public async Task<CacheGetBatchResponse> GetBatchAsync(string cacheName, IEnumerable<byte[]> keys)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(keys, nameof(keys));
        Utils.ElementsNotNull(keys, nameof(keys));

        return await this.dataClient.GetBatchAsync(cacheName, keys);
    }

    public async Task<CacheGetBatchResponse> GetBatchAsync(string cacheName, IEnumerable<string> keys)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(keys, nameof(keys));
        Utils.ElementsNotNull(keys, nameof(keys));

        return await this.dataClient.GetBatchAsync(cacheName, keys);
    }

    public async Task<CacheGetBatchResponse> GetBatchAsync(string cacheName, params byte[][] keys)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(keys, nameof(keys));
        Utils.ElementsNotNull(keys, nameof(keys));

        return await this.dataClient.GetBatchAsync(cacheName, keys);
    }

    public async Task<CacheGetBatchResponse> GetBatchAsync(string cacheName, params string[] keys)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(keys, nameof(keys));
        Utils.ElementsNotNull(keys, nameof(keys));

        return await this.dataClient.GetBatchAsync(cacheName, keys);
    }

    public async Task<CacheSetBatchResponse> SetBatchAsync(string cacheName, IEnumerable<KeyValuePair<byte[], byte[]>> items, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(items, nameof(items));
        Utils.KeysAndValuesNotNull(items, nameof(items));

        return await this.dataClient.SetBatchAsync(cacheName, items, ttlSeconds);
    }

    public async Task<CacheSetBatchResponse> SetBatchAsync(string cacheName, IEnumerable<KeyValuePair<string, string>> items, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(items, nameof(items));
        Utils.KeysAndValuesNotNull(items, nameof(items));

        return await this.dataClient.SetBatchAsync(cacheName, items, ttlSeconds);
    }

    public CacheSetResponse Set(string cacheName, byte[] key, byte[] value, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(key, nameof(key));
        Utils.ArgumentNotNull(value, nameof(value));

        return this.dataClient.Set(cacheName, key, value, ttlSeconds);
    }

    public CacheGetResponse Get(string cacheName, byte[] key)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(key, nameof(key));

        return this.dataClient.Get(cacheName, key);
    }

    public CacheDeleteResponse Delete(string cacheName, byte[] key)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(key, nameof(key));

        return this.dataClient.Delete(cacheName, key);
    }

    public CacheSetResponse Set(string cacheName, string key, string value, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(key, nameof(key));
        Utils.ArgumentNotNull(value, nameof(value));

        return this.dataClient.Set(cacheName, key, value, ttlSeconds);
    }

    public CacheGetResponse Get(string cacheName, string key)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(key, nameof(key));

        return this.dataClient.Get(cacheName, key);
    }

    public CacheGetBatchResponse GetBatch(string cacheName, IEnumerable<byte[]> keys)
    {
        try
        {
            return GetBatchAsync(cacheName, keys).Result;
        }
        catch (AggregateException e)
        {
            throw e.GetBaseException();
        }
    }

    public CacheGetBatchResponse GetBatch(string cacheName, IEnumerable<string> keys)
    {
        try
        {
            return GetBatchAsync(cacheName, keys).Result;
        }
        catch (AggregateException e)
        {
            throw e.GetBaseException();
        }
    }

    public CacheGetBatchResponse GetBatch(string cacheName, params byte[][] keys)
    {
        try
        {
            return GetBatchAsync(cacheName, keys).Result;
        }
        catch (AggregateException e)
        {
            throw e.GetBaseException();
        }
    }

    public CacheGetBatchResponse GetBatch(string cacheName, params string[] keys)
    {
        try
        {
            return GetBatchAsync(cacheName, keys).Result;
        }
        catch (AggregateException e)
        {
            throw e.GetBaseException();
        }
    }

    public CacheSetBatchResponse SetBatch(string cacheName, IEnumerable<KeyValuePair<byte[], byte[]>> items, uint? ttlSeconds = null)
    {
        try
        {
            return SetBatchAsync(cacheName, items, ttlSeconds).Result;
        }
        catch (AggregateException e)
        {
            throw e.GetBaseException();
        }
    }

    public CacheSetBatchResponse SetBatch(string cacheName, IEnumerable<KeyValuePair<string, string>> items, uint? ttlSeconds = null)
    {
        try
        {
            return SetBatchAsync(cacheName, items, ttlSeconds).Result;
        }
        catch (AggregateException e)
        {
            throw e.GetBaseException();
        }
    }

    public CacheDeleteResponse Delete(string cacheName, string key)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(key, nameof(key));

        return this.dataClient.Delete(cacheName, key);
    }

    public CacheSetResponse Set(string cacheName, string key, byte[] value, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(key, nameof(key));
        Utils.ArgumentNotNull(value, nameof(value));

        return this.dataClient.Set(cacheName, key, value, ttlSeconds);
    }

    public void Dispose()
    {
        this.controlClient.Dispose();
        this.dataClient.Dispose();
        GC.SuppressFinalize(this);
    }

    private void ValidateRequestTimeout(uint? requestTimeoutMilliseconds = null)
    {
        if (requestTimeoutMilliseconds == null)
        {
            return;
        }
        if (requestTimeoutMilliseconds == 0)
        {
            throw new InvalidArgumentException("Request timeout must be greater than zero.");
        }
    }
}
