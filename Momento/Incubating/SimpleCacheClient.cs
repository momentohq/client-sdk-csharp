using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MomentoSdk.Responses;
using Utils = MomentoSdk.Internal.Utils;
using MomentoSdk.Incubating.Internal;
using MomentoSdk.Incubating.Responses;

namespace MomentoSdk.Incubating;

public class SimpleCacheClient : ISimpleCacheClient
{
    private readonly ISimpleCacheClient simpleCacheClient;
    private readonly ScsDataClient dataClient;

    public SimpleCacheClient(ISimpleCacheClient simpleCacheClient, string authToken, uint defaultTtlSeconds, uint? dataClientOperationTimeoutMilliseconds = null)
    {
        this.simpleCacheClient = simpleCacheClient;

        var claims = MomentoSdk.Internal.JwtUtils.DecodeJwt(authToken);
        this.dataClient = new(authToken, claims.CacheEndpoint, defaultTtlSeconds, dataClientOperationTimeoutMilliseconds);
    }

    public CreateCacheResponse CreateCache(string cacheName)
    {
        return this.simpleCacheClient.CreateCache(cacheName);
    }

    public DeleteCacheResponse DeleteCache(string cacheName)
    {
        return this.simpleCacheClient.DeleteCache(cacheName);
    }

    public ListCachesResponse ListCaches(string? nextPageToken = null)
    {
        return this.simpleCacheClient.ListCaches(nextPageToken);
    }

    public async Task<CacheSetResponse> SetAsync(string cacheName, byte[] key, byte[] value, uint? ttlSeconds = null)
    {
        return await this.simpleCacheClient.SetAsync(cacheName, key, value, ttlSeconds);
    }

    public async Task<CacheGetResponse> GetAsync(string cacheName, byte[] key)
    {
        return await this.simpleCacheClient.GetAsync(cacheName, key);
    }

    public async Task<CacheDeleteResponse> DeleteAsync(string cacheName, byte[] key)
    {
        return await this.simpleCacheClient.DeleteAsync(cacheName, key);
    }

    public async Task<CacheSetResponse> SetAsync(string cacheName, string key, string value, uint? ttlSeconds = null)
    {
        return await simpleCacheClient.SetAsync(cacheName, key, value, ttlSeconds);
    }

    public async Task<CacheGetResponse> GetAsync(string cacheName, string key)
    {
        return await this.simpleCacheClient.GetAsync(cacheName, key);
    }

    public async Task<CacheDeleteResponse> DeleteAsync(string cacheName, string key)
    {
        return await this.simpleCacheClient.DeleteAsync(cacheName, key);
    }

    public async Task<CacheSetResponse> SetAsync(string cacheName, string key, byte[] value, uint? ttlSeconds = null)
    {
        return await this.simpleCacheClient.SetAsync(cacheName, key, value, ttlSeconds);
    }

    public async Task<CacheGetMultiResponse> GetMultiAsync(string cacheName, IEnumerable<byte[]> keys)
    {
        return await this.simpleCacheClient.GetMultiAsync(cacheName, keys);
    }

    public async Task<CacheGetMultiResponse> GetMultiAsync(string cacheName, IEnumerable<string> keys)
    {
        return await this.simpleCacheClient.GetMultiAsync(cacheName, keys);
    }

    public async Task<CacheGetMultiResponse> GetMultiAsync(string cacheName, params byte[][] keys)
    {
        return await this.simpleCacheClient.GetMultiAsync(cacheName, keys);
    }

    public async Task<CacheGetMultiResponse> GetMultiAsync(string cacheName, params string[] keys)
    {
        return await this.simpleCacheClient.GetMultiAsync(cacheName, keys);
    }

    public async Task<CacheSetMultiResponse> SetMultiAsync(string cacheName, IDictionary<byte[], byte[]> items, uint? ttlSeconds = null)
    {
        return await this.simpleCacheClient.SetMultiAsync(cacheName, items, ttlSeconds);
    }

    public async Task<CacheSetMultiResponse> SetMultiAsync(string cacheName, IDictionary<string, string> items, uint? ttlSeconds = null)
    {
        return await this.simpleCacheClient.SetMultiAsync(cacheName, items, ttlSeconds);
    }

    public CacheSetResponse Set(string cacheName, byte[] key, byte[] value, uint? ttlSeconds = null)
    {
        return this.simpleCacheClient.Set(cacheName, key, value, ttlSeconds);
    }

    public CacheGetResponse Get(string cacheName, byte[] key)
    {
        return this.simpleCacheClient.Get(cacheName, key);
    }

    public CacheDeleteResponse Delete(string cacheName, byte[] key)
    {
        return this.simpleCacheClient.Delete(cacheName, key);
    }

    public CacheSetResponse Set(string cacheName, string key, string value, uint? ttlSeconds = null)
    {
        return this.simpleCacheClient.Set(cacheName, key, value, ttlSeconds);
    }

    public CacheGetResponse Get(string cacheName, string key)
    {
        return this.simpleCacheClient.Get(cacheName, key);
    }

    public CacheGetMultiResponse GetMulti(string cacheName, IEnumerable<byte[]> keys)
    {
        return this.simpleCacheClient.GetMulti(cacheName, keys);
    }

    public CacheGetMultiResponse GetMulti(string cacheName, IEnumerable<string> keys)
    {
        return this.simpleCacheClient.GetMulti(cacheName, keys);
    }

    public CacheGetMultiResponse GetMulti(string cacheName, params byte[][] keys)
    {
        return this.simpleCacheClient.GetMulti(cacheName, keys);
    }

    public CacheGetMultiResponse GetMulti(string cacheName, params string[] keys)
    {
        return this.simpleCacheClient.GetMulti(cacheName, keys);
    }

    public CacheSetMultiResponse SetMulti(string cacheName, IDictionary<byte[], byte[]> items, uint? ttlSeconds = null)
    {
        return this.simpleCacheClient.SetMulti(cacheName, items, ttlSeconds);
    }

    public CacheSetMultiResponse SetMulti(string cacheName, IDictionary<string, string> items, uint? ttlSeconds = null)
    {
        return this.simpleCacheClient.SetMulti(cacheName, items, ttlSeconds);
    }

    public CacheDeleteResponse Delete(string cacheName, string key)
    {
        return this.simpleCacheClient.Delete(cacheName, key);
    }

    public CacheSetResponse Set(string cacheName, string key, byte[] value, uint? ttlSeconds = null)
    {
        return this.simpleCacheClient.Set(cacheName, key, value, ttlSeconds);
    }

    /// <summary>
    /// Set the dictionary field to a value with a given time to live (TTL) seconds.
    ///
    /// Creates the dictionary if it does not exist and sets the TTL.
    /// If the dictionary already exists and `refreshTtl` is `true`, then update the
    /// TTL to `ttlSeconds`, otherwise leave the TTL unchanged.
    /// </summary>
    /// <param name="cacheName">Name of the cache to store the dictionary in.</param>
    /// <param name="dictionaryName">The dictionary to set.</param>
    /// <param name="field">The field in the dictionary to set.</param>
    /// <param name="value">The value to be stored.</param>
    /// <param name="refreshTtl">Update the dictionary TTL if the dictionary already exists.</param>
    /// <param name="ttlSeconds">TTL for the dictionary in cache. This TTL takes precedence over the TTL used when initializing a cache client. Defaults to client TTL.</param>
    /// <returns>Result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `field`, `value` is `null`.</exception>
    public CacheDictionarySetResponse DictionarySet(string cacheName, string dictionaryName, byte[] field, byte[] value, bool refreshTtl, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(field, nameof(field));
        Utils.ArgumentNotNull(value, nameof(value));

        return this.dataClient.DictionarySet(cacheName, dictionaryName, field, value, refreshTtl, ttlSeconds);
    }

    /// <summary>
    /// Set the dictionary field to a value with a given time to live (TTL) seconds.
    ///
    /// Creates the dictionary if it does not exist and sets the TTL.
    /// If the dictionary already exists and `refreshTtl` is `true`, then update the
    /// TTL to `ttlSeconds`, otherwise leave the TTL unchanged.
    /// </summary>
    /// <param name="cacheName">Name of the cache to store the dictionary in.</param>
    /// <param name="dictionaryName">The dictionary to set.</param>
    /// <param name="field">The field in the dictionary to set.</param>
    /// <param name="value">The value to be stored.</param>
    /// <param name="refreshTtl">Update the dictionary TTL if the dictionary already exists.</param>
    /// <param name="ttlSeconds">TTL for the dictionary in cache. This TTL takes precedence over the TTL used when initializing a cache client. Defaults to client TTL.</param>
    /// <returns>Result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `field`, `value` is `null`.</exception>
    public CacheDictionarySetResponse DictionarySet(string cacheName, string dictionaryName, string field, string value, bool refreshTtl, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(field, nameof(field));
        Utils.ArgumentNotNull(value, nameof(value));

        return this.dataClient.DictionarySet(cacheName, dictionaryName, field, value, refreshTtl, ttlSeconds);
    }

    /// <summary>
    /// Set the dictionary field to a value with a given time to live (TTL) seconds.
    ///
    /// Creates the dictionary if it does not exist and sets the TTL.
    /// If the dictionary already exists and `refreshTtl` is `true`, then update the
    /// TTL to `ttlSeconds`, otherwise leave the TTL unchanged.
    /// </summary>
    /// <param name="cacheName">Name of the cache to store the dictionary in.</param>
    /// <param name="dictionaryName">The dictionary to set.</param>
    /// <param name="field">The field in the dictionary to set.</param>
    /// <param name="value">The value to be stored.</param>
    /// <param name="refreshTtl">Update the dictionary TTL if the dictionary already exists.</param>
    /// <param name="ttlSeconds">TTL for the dictionary in cache. This TTL takes precedence over the TTL used when initializing a cache client. Defaults to client TTL.</param>
    /// <returns>Task representing the result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `field`, `value` is `null`.</exception>
    public async Task<CacheDictionarySetResponse> DictionarySetAsync(string cacheName, string dictionaryName, byte[] field, byte[] value, bool refreshTtl, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(field, nameof(field));
        Utils.ArgumentNotNull(value, nameof(value));

        return await this.dataClient.DictionarySetAsync(cacheName, dictionaryName, field, value, refreshTtl, ttlSeconds);
    }

    /// <summary>
    /// Set the dictionary field to a value with a given time to live (TTL) seconds.
    ///
    /// Creates the dictionary if it does not exist and sets the TTL.
    /// If the dictionary already exists and `refreshTtl` is `true`, then update the
    /// TTL to `ttlSeconds`, otherwise leave the TTL unchanged.
    /// </summary>
    /// <param name="cacheName">Name of the cache to store the dictionary in.</param>
    /// <param name="dictionaryName">The dictionary to set.</param>
    /// <param name="field">The field in the dictionary to set.</param>
    /// <param name="value">The value to be stored.</param>
    /// <param name="refreshTtl">Update the dictionary TTL if the dictionary already exists.</param>
    /// <param name="ttlSeconds">TTL for the dictionary in cache. This TTL takes precedence over the TTL used when initializing a cache client. Defaults to client TTL.</param>
    /// <returns>Task representing the result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `field`, `value` is `null`.</exception>
    public async Task<CacheDictionarySetResponse> DictionarySetAsync(string cacheName, string dictionaryName, string field, string value, bool refreshTtl, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(field, nameof(field));
        Utils.ArgumentNotNull(value, nameof(value));

        return await this.dataClient.DictionarySetAsync(cacheName, dictionaryName, field, value, refreshTtl, ttlSeconds);
    }

    /// <summary>
    /// Get the cache value stored for the given dictionary and field.
    /// </summary>
    /// <param name="cacheName">Name of the cache to perform the lookup in.</param>
    /// <param name="dictionaryName">The dictionary to lookup.</param>
    /// <param name="field">The field in the dictionary to lookup.</param>
    /// <returns>Object with the status of the get operation and the associated value.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `field` is `null`.</exception>
    public CacheDictionaryGetResponse DictionaryGet(string cacheName, string dictionaryName, byte[] field)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(field, nameof(field));

        return this.dataClient.DictionaryGet(cacheName, dictionaryName, field);
    }

    /// <summary>
    /// Get the cache value stored for the given dictionary and field.
    /// </summary>
    /// <param name="cacheName">Name of the cache to perform the lookup in.</param>
    /// <param name="dictionaryName">The dictionary to lookup.</param>
    /// <param name="field">The field in the dictionary to lookup.</param>
    /// <returns>Object with the status of the get operation and the associated value.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `field` is `null`.</exception>
    public CacheDictionaryGetResponse DictionaryGet(string cacheName, string dictionaryName, string field)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(field, nameof(field));

        return this.dataClient.DictionaryGet(cacheName, dictionaryName, field);
    }

    /// <summary>
    /// Get the cache value stored for the given dictionary and field.
    /// </summary>
    /// <param name="cacheName">Name of the cache to perform the lookup in.</param>
    /// <param name="dictionaryName">The dictionary to lookup.</param>
    /// <param name="field">The field in the dictionary to lookup.</param>
    /// <returns>Task representing the status of the get operation and the associated value.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `field` is `null`.</exception>
    public async Task<CacheDictionaryGetResponse> DictionaryGetAsync(string cacheName, string dictionaryName, byte[] field)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(field, nameof(field));

        return await this.dataClient.DictionaryGetAsync(cacheName, dictionaryName, field);
    }

    /// <summary>
    /// Get the cache value stored for the given dictionary and field.
    /// </summary>
    /// <param name="cacheName">Name of the cache to perform the lookup in.</param>
    /// <param name="dictionaryName">The dictionary to lookup.</param>
    /// <param name="field">The field in the dictionary to lookup.</param>
    /// <returns>Task representing the status of the get operation and the associated value.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `field` is `null`.</exception>
    public async Task<CacheDictionaryGetResponse> DictionaryGetAsync(string cacheName, string dictionaryName, string field)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(field, nameof(field));

        return await this.dataClient.DictionaryGetAsync(cacheName, dictionaryName, field);
    }

    /// <summary>
    /// Set several dictionary field-value pairs in the cache.
    ///
    /// Creates the dictionary if it does not exist and sets the TTL.
    /// If the dictionary already exists and `refreshTtl` is `true`, then update the
    /// TTL to `ttlSeconds`, otherwise leave the TTL unchanged.
    /// </summary>
    /// <param name="cacheName">Name of the cache to store the dictionary in.</param>
    /// <param name="dictionaryName">The dictionary to set.</param>
    /// <param name="items">The field-value pairs in the dictionary to set.</param>
    /// <param name="refreshTtl">Update the dictionary TTL if the dictionary already exists.</param>
    /// <param name="ttlSeconds">TTL for the dictionary in cache. This TTL takes precedence over the TTL used when initializing a cache client. Defaults to client TTL.</param>
    /// <returns>Result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `items` is `null`.</exception>
    public CacheDictionarySetMultiResponse DictionarySetMulti(string cacheName, string dictionaryName, IEnumerable<KeyValuePair<byte[], byte[]>> items, bool refreshTtl, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(items, nameof(items));
        Utils.ValuesNotNull(items, nameof(items));

        return this.dataClient.DictionarySetMulti(cacheName, dictionaryName, items, refreshTtl, ttlSeconds);
    }

    /// <summary>
    /// Set several dictionary field-value pairs in the cache.
    ///
    /// Creates the dictionary if it does not exist and sets the TTL.
    /// If the dictionary already exists and `refreshTtl` is `true`, then update the
    /// TTL to `ttlSeconds`, otherwise leave the TTL unchanged.
    /// </summary>
    /// <param name="cacheName">Name of the cache to store the dictionary in.</param>
    /// <param name="dictionaryName">The dictionary to set.</param>
    /// <param name="items">The field-value pairs in the dictionary to set.</param>
    /// <param name="refreshTtl">Update the dictionary TTL if the dictionary already exists.</param>
    /// <param name="ttlSeconds">TTL for the dictionary in cache. This TTL takes precedence over the TTL used when initializing a cache client. Defaults to client TTL.</param>
    /// <returns>Result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `items` is `null`.</exception>
    public CacheDictionarySetMultiResponse DictionarySetMulti(string cacheName, string dictionaryName, IEnumerable<KeyValuePair<string, string>> items, bool refreshTtl, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(items, nameof(items));
        Utils.ValuesNotNull(items, nameof(items));

        return this.dataClient.DictionarySetMulti(cacheName, dictionaryName, items, refreshTtl, ttlSeconds);
    }

    /// <summary>
    /// Set several dictionary field-value pairs in the cache.
    ///
    /// Creates the dictionary if it does not exist and sets the TTL.
    /// If the dictionary already exists and `refreshTtl` is `true`, then update the
    /// TTL to `ttlSeconds`, otherwise leave the TTL unchanged.
    /// </summary>
    /// <param name="cacheName">Name of the cache to store the dictionary in.</param>
    /// <param name="dictionaryName">The dictionary to set.</param>
    /// <param name="items">The field-value pairs in the dictionary to set.</param>
    /// <param name="refreshTtl">Update the dictionary TTL if the dictionary already exists.</param>
    /// <param name="ttlSeconds">TTL for the dictionary in cache. This TTL takes precedence over the TTL used when initializing a cache client. Defaults to client TTL.</param>
    /// <returns>Task representing the result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `items` is `null`.</exception>
    public async Task<CacheDictionarySetMultiResponse> DictionarySetMultiAsync(string cacheName, string dictionaryName, IEnumerable<KeyValuePair<byte[], byte[]>> items, bool refreshTtl, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(items, nameof(items));
        Utils.ValuesNotNull(items, nameof(items));

        return await this.dataClient.DictionarySetMultiAsync(cacheName, dictionaryName, items, refreshTtl, ttlSeconds);
    }

    /// <summary>
    /// Set several dictionary field-value pairs in the cache.
    ///
    /// Creates the dictionary if it does not exist and sets the TTL.
    /// If the dictionary already exists and `refreshTtl` is `true`, then update the
    /// TTL to `ttlSeconds`, otherwise leave the TTL unchanged.
    /// </summary>
    /// <param name="cacheName">Name of the cache to store the dictionary in.</param>
    /// <param name="dictionaryName">The dictionary to set.</param>
    /// <param name="items">The field-value pairs in the dictionary to set.</param>
    /// <param name="refreshTtl">Update the dictionary TTL if the dictionary already exists.</param>
    /// <param name="ttlSeconds">TTL for the dictionary in cache. This TTL takes precedence over the TTL used when initializing a cache client. Defaults to client TTL.</param>
    /// <returns>Task representing the result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `items` is `null`.</exception>
    public async Task<CacheDictionarySetMultiResponse> DictionarySetMultiAsync(string cacheName, string dictionaryName, IEnumerable<KeyValuePair<string, string>> items, bool refreshTtl, uint? ttlSeconds = null)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(items, nameof(items));
        Utils.ValuesNotNull(items, nameof(items));

        return await this.dataClient.DictionarySetMultiAsync(cacheName, dictionaryName, items, refreshTtl, ttlSeconds);
    }

    /// <summary>
    /// Get several values from a dictionary.
    /// </summary>
    /// <param name="cacheName">Name of the cache to perform the lookup in.</param>
    /// <param name="dictionaryName">The dictionary to lookup.</param>
    /// <param name="fields">The fields in the dictionary to lookup.</param>
    /// <returns>Object with the status and associated value for each field.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public CacheDictionaryGetMultiResponse DictionaryGetMulti(string cacheName, string dictionaryName, params byte[][] fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return this.dataClient.DictionaryGetMulti(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Get several values from a dictionary.
    /// </summary>
    /// <param name="cacheName">Name of the cache to perform the lookup in.</param>
    /// <param name="dictionaryName">The dictionary to lookup.</param>
    /// <param name="fields">The fields in the dictionary to lookup.</param>
    /// <returns>Object with the status and associated value for each field.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public CacheDictionaryGetMultiResponse DictionaryGetMulti(string cacheName, string dictionaryName, params string[] fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return this.dataClient.DictionaryGetMulti(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Get several values from a dictionary.
    /// </summary>
    /// <param name="cacheName">Name of the cache to perform the lookup in.</param>
    /// <param name="dictionaryName">The dictionary to lookup.</param>
    /// <param name="fields">The fields in the dictionary to lookup.</param>
    /// <returns>Object with the status and associated value for each field.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public CacheDictionaryGetMultiResponse DictionaryGetMulti(string cacheName, string dictionaryName, IEnumerable<byte[]> fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return this.dataClient.DictionaryGetMulti(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Get several values from a dictionary.
    /// </summary>
    /// <param name="cacheName">Name of the cache to perform the lookup in.</param>
    /// <param name="dictionaryName">The dictionary to lookup.</param>
    /// <param name="fields">The fields in the dictionary to lookup.</param>
    /// <returns>Object with the status and associated value for each field.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public CacheDictionaryGetMultiResponse DictionaryGetMulti(string cacheName, string dictionaryName, IEnumerable<string> fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return this.dataClient.DictionaryGetMulti(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Get several values from a dictionary.
    /// </summary>
    /// <param name="cacheName">Name of the cache to perform the lookup in.</param>
    /// <param name="dictionaryName">The dictionary to lookup.</param>
    /// <param name="fields">The fields in the dictionary to lookup.</param>
    /// <returns>Task representing the status and associated value for each field.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public async Task<CacheDictionaryGetMultiResponse> DictionaryGetMultiAsync(string cacheName, string dictionaryName, params byte[][] fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return await this.dataClient.DictionaryGetMultiAsync(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Get several values from a dictionary.
    /// </summary>
    /// <param name="cacheName">Name of the cache to perform the lookup in.</param>
    /// <param name="dictionaryName">The dictionary to lookup.</param>
    /// <param name="fields">The fields in the dictionary to lookup.</param>
    /// <returns>Task representing the status and associated value for each field.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public async Task<CacheDictionaryGetMultiResponse> DictionaryGetMultiAsync(string cacheName, string dictionaryName, params string[] fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return await this.dataClient.DictionaryGetMultiAsync(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Get several values from a dictionary.
    /// </summary>
    /// <param name="cacheName">Name of the cache to perform the lookup in.</param>
    /// <param name="dictionaryName">The dictionary to lookup.</param>
    /// <param name="fields">The fields in the dictionary to lookup.</param>
    /// <returns>Task representing the status and associated value for each field.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public async Task<CacheDictionaryGetMultiResponse> DictionaryGetMultiAsync(string cacheName, string dictionaryName, IEnumerable<byte[]> fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return await this.dataClient.DictionaryGetMultiAsync(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Get several values from a dictionary.
    /// </summary>
    /// <param name="cacheName">Name of the cache to perform the lookup in.</param>
    /// <param name="dictionaryName">The dictionary to lookup.</param>
    /// <param name="fields">The fields in the dictionary to lookup.</param>
    /// <returns>Task representing the status and associated value for each field.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public async Task<CacheDictionaryGetMultiResponse> DictionaryGetMultiAsync(string cacheName, string dictionaryName, IEnumerable<string> fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return await this.dataClient.DictionaryGetMultiAsync(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Fetch the entire dictionary from the cache.
    /// </summary>
    /// <param name="cacheName">Name of the cache to perform the lookup in.</param>
    /// <param name="dictionaryName">The dictionary to fetch.</param>
    /// <returns>Object with the status of the fetch operation and the associated dictionary.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName` is `null`.</exception>
    public CacheDictionaryGetAllResponse DictionaryGetAll(string cacheName, string dictionaryName)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));

        return this.dataClient.DictionaryGetAll(cacheName, dictionaryName);
    }

    /// <summary>
    /// Fetch the entire dictionary from the cache.
    /// </summary>
    /// <param name="cacheName">Name of the cache to perform the lookup in.</param>
    /// <param name="dictionaryName">The dictionary to fetch.</param>
    /// <returns>Task representing with the status of the fetch operation and the associated dictionary.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName` is `null`.</exception>
    public async Task<CacheDictionaryGetAllResponse> DictionaryGetAllAsync(string cacheName, string dictionaryName)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));

        return await this.dataClient.DictionaryGetAllAsync(cacheName, dictionaryName);
    }

    /// <summary>
    /// Remove the dictionary from the cache.
    ///
    /// Performs a no-op if `dictionaryName` does not exist.
    /// </summary>
    /// <param name="cacheName">Name of the cache to delete the dictionary from.</param>
    /// <param name="dictionaryName">Name of the dictionary to delete.</param>
    /// <returns>Result of the delete operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName` or `dictionaryName` is `null`.</exception>
    public CacheDictionaryDeleteResponse DictionaryDelete(string cacheName, string dictionaryName)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));

        return this.dataClient.DictionaryDelete(cacheName, dictionaryName);
    }

    /// <summary>
    /// Remove the dictionary from the cache.
    ///
    /// Performs a no-op if `dictionaryName` does not exist.
    /// </summary>
    /// <param name="cacheName">Name of the cache to delete the dictionary from.</param>
    /// <param name="dictionaryName">Name of the dictionary to delete.</param>
    /// <returns>Task representing the result of the delete operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName` or `dictionaryName` is `null`.</exception>
    public async Task<CacheDictionaryDeleteResponse> DictionaryDeleteAsync(string cacheName, string dictionaryName)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));

        return await this.dataClient.DictionaryDeleteAsync(cacheName, dictionaryName);
    }

    /// <summary>
    /// Remove a field from a dictionary.
    ///
    /// Performs a no-op if `dictionaryName` or `field` does not exist.
    /// </summary>
    /// <param name="cacheName">Name of the cache to lookup the dictionary in.</param>
    /// <param name="dictionaryName">Name of the dictionary to remove the field from.</param>
    /// <param name="field">Name of the field to remove from the dictionary.</param>
    /// <returns>Result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `field` is `null`.</exception>
    public CacheDictionaryRemoveFieldResponse DictionaryRemoveField(string cacheName, string dictionaryName, byte[] field)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(field, nameof(field));

        return this.dataClient.DictionaryRemoveField(cacheName, dictionaryName, field);
    }

    /// <summary>
    /// Remove a field from a dictionary.
    ///
    /// Performs a no-op if `dictionaryName` or `field` does not exist.
    /// </summary>
    /// <param name="cacheName">Name of the cache to lookup the dictionary in.</param>
    /// <param name="dictionaryName">Name of the dictionary to remove the field from.</param>
    /// <param name="field">Name of the field to remove from the dictionary.</param>
    /// <returns>Result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `field` is `null`.</exception>
    public CacheDictionaryRemoveFieldResponse DictionaryRemoveField(string cacheName, string dictionaryName, string field)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(field, nameof(field));

        return this.dataClient.DictionaryRemoveField(cacheName, dictionaryName, field);
    }

    /// <summary>
    /// Remove a field from a dictionary.
    ///
    /// Performs a no-op if `dictionaryName` or `field` does not exist.
    /// </summary>
    /// <param name="cacheName">Name of the cache to lookup the dictionary in.</param>
    /// <param name="dictionaryName">Name of the dictionary to remove the field from.</param>
    /// <param name="field">Name of the field to remove from the dictionary.</param>
    /// <returns>Task representing the result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `field` is `null`.</exception>
    public async Task<CacheDictionaryRemoveFieldResponse> DictionaryRemoveFieldAsync(string cacheName, string dictionaryName, byte[] field)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(field, nameof(field));

        return await this.dataClient.DictionaryRemoveFieldAsync(cacheName, dictionaryName, field);
    }

    /// <summary>
    /// Remove a field from a dictionary.
    ///
    /// Performs a no-op if `dictionaryName` or `field` does not exist.
    /// </summary>
    /// <param name="cacheName">Name of the cache to lookup the dictionary in.</param>
    /// <param name="dictionaryName">Name of the dictionary to remove the field from.</param>
    /// <param name="field">Name of the field to remove from the dictionary.</param>
    /// <returns>Task representing the result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `field` is `null`.</exception>
    public async Task<CacheDictionaryRemoveFieldResponse> DictionaryRemoveFieldAsync(string cacheName, string dictionaryName, string field)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(field, nameof(field));

        return await this.dataClient.DictionaryRemoveFieldAsync(cacheName, dictionaryName, field);
    }

    /// <summary>
    /// Remove fields from a dictionary.
    ///
    /// Performs a no-op if `dictionaryName` or a particular field does not exist.
    /// </summary>
    /// <param name="cacheName">Name of the cache to lookup the dictionary in.</param>
    /// <param name="dictionaryName">Name of the dictionary to remove the field from.</param>
    /// <param name="fields">Name of the fields to remove from the dictionary.</param>
    /// <returns>Result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public CacheDictionaryRemoveFieldsResponse DictionaryRemoveFields(string cacheName, string dictionaryName, params byte[][] fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return this.dataClient.DictionaryRemoveFields(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Remove fields from a dictionary.
    ///
    /// Performs a no-op if `dictionaryName` or a particular field does not exist.
    /// </summary>
    /// <param name="cacheName">Name of the cache to lookup the dictionary in.</param>
    /// <param name="dictionaryName">Name of the dictionary to remove the field from.</param>
    /// <param name="fields">Name of the fields to remove from the dictionary.</param>
    /// <returns>Result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public CacheDictionaryRemoveFieldsResponse DictionaryRemoveFields(string cacheName, string dictionaryName, params string[] fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));

        return this.dataClient.DictionaryRemoveFields(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Remove fields from a dictionary.
    ///
    /// Performs a no-op if `dictionaryName` or a particular field does not exist.
    /// </summary>
    /// <param name="cacheName">Name of the cache to lookup the dictionary in.</param>
    /// <param name="dictionaryName">Name of the dictionary to remove the field from.</param>
    /// <param name="fields">Name of the fields to remove from the dictionary.</param>
    /// <returns>Result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public CacheDictionaryRemoveFieldsResponse DictionaryRemoveFields(string cacheName, string dictionaryName, IEnumerable<byte[]> fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return this.dataClient.DictionaryRemoveFields(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Remove fields from a dictionary.
    ///
    /// Performs a no-op if `dictionaryName` or a particular field does not exist.
    /// </summary>
    /// <param name="cacheName">Name of the cache to lookup the dictionary in.</param>
    /// <param name="dictionaryName">Name of the dictionary to remove the field from.</param>
    /// <param name="fields">Name of the fields to remove from the dictionary.</param>
    /// <returns>Result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public CacheDictionaryRemoveFieldsResponse DictionaryRemoveFields(string cacheName, string dictionaryName, IEnumerable<string> fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return this.dataClient.DictionaryRemoveFields(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Remove fields from a dictionary.
    ///
    /// Performs a no-op if `dictionaryName` or a particular field does not exist.
    /// </summary>
    /// <param name="cacheName">Name of the cache to lookup the dictionary in.</param>
    /// <param name="dictionaryName">Name of the dictionary to remove the field from.</param>
    /// <param name="fields">Name of the fields to remove from the dictionary.</param>
    /// <returns>Task representing the result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public async Task<CacheDictionaryRemoveFieldsResponse> DictionaryRemoveFieldsAsync(string cacheName, string dictionaryName, params byte[][] fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return await this.dataClient.DictionaryRemoveFieldsAsync(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Remove fields from a dictionary.
    ///
    /// Performs a no-op if `dictionaryName` or a particular field does not exist.
    /// </summary>
    /// <param name="cacheName">Name of the cache to lookup the dictionary in.</param>
    /// <param name="dictionaryName">Name of the dictionary to remove the field from.</param>
    /// <param name="fields">Name of the fields to remove from the dictionary.</param>
    /// <returns>Task representing the result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public async Task<CacheDictionaryRemoveFieldsResponse> DictionaryRemoveFieldsAsync(string cacheName, string dictionaryName, params string[] fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return await this.dataClient.DictionaryRemoveFieldsAsync(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Remove fields from a dictionary.
    ///
    /// Performs a no-op if `dictionaryName` or a particular field does not exist.
    /// </summary>
    /// <param name="cacheName">Name of the cache to lookup the dictionary in.</param>
    /// <param name="dictionaryName">Name of the dictionary to remove the field from.</param>
    /// <param name="fields">Name of the fields to remove from the dictionary.</param>
    /// <returns>Task representing the result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public async Task<CacheDictionaryRemoveFieldsResponse> DictionaryRemoveFieldsAsync(string cacheName, string dictionaryName, IEnumerable<byte[]> fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return await this.dataClient.DictionaryRemoveFieldsAsync(cacheName, dictionaryName, fields);
    }

    /// <summary>
    /// Remove fields from a dictionary.
    ///
    /// Performs a no-op if `dictionaryName` or a particular field does not exist.
    /// </summary>
    /// <param name="cacheName">Name of the cache to lookup the dictionary in.</param>
    /// <param name="dictionaryName">Name of the dictionary to remove the field from.</param>
    /// <param name="fields">Name of the fields to remove from the dictionary.</param>
    /// <returns>Task representing the result of the cache operation.</returns>
    /// <exception cref="ArgumentNullException">Any of `cacheName`, `dictionaryName`, `fields` is `null`.</exception>
    public async Task<CacheDictionaryRemoveFieldsResponse> DictionaryRemoveFieldsAsync(string cacheName, string dictionaryName, IEnumerable<string> fields)
    {
        Utils.ArgumentNotNull(cacheName, nameof(cacheName));
        Utils.ArgumentNotNull(dictionaryName, nameof(dictionaryName));
        Utils.ArgumentNotNull(fields, nameof(fields));
        Utils.ElementsNotNull(fields, nameof(fields));

        return await this.dataClient.DictionaryRemoveFieldsAsync(cacheName, dictionaryName, fields);
    }

    public void Dispose()
    {
        this.simpleCacheClient.Dispose();
        this.dataClient.Dispose();
    }
}
