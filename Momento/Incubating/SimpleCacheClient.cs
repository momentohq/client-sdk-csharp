using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MomentoSdk.Incubating.Responses;

namespace MomentoSdk.Incubating
{
    public class SimpleCacheClient : MomentoSdk.SimpleCacheClient
    {
        public SimpleCacheClient(string authToken, uint defaultTtlSeconds) : base(authToken, defaultTtlSeconds)
        {
        }

        public CacheDictionarySetResponse DictionarySet(string cacheName, string dictionaryName, byte[] key, byte[] value, bool refreshTtl, uint? ttlSeconds = null)
        {
            throw new NotImplementedException();
        }

        public CacheDictionarySetResponse DictionarySet(string cacheName, string dictionaryName, string key, string value, bool refreshTtl, uint? ttlSeconds = null)
        {
            throw new NotImplementedException();
        }

        public async Task<CacheDictionarySetResponse> DictionarySetAsync(string cacheName, string dictionaryName, byte[] key, byte[] value, bool refreshTtl, uint? ttlSeconds = null)
        {
            return await Task.FromException<CacheDictionarySetResponse>(new NotImplementedException());
        }

        public async Task<CacheDictionarySetResponse> DictionarySetAsync(string cacheName, string dictionaryName, string key, string value, bool refreshTtl, uint? ttlSeconds = null)
        {
            return await Task.FromException<CacheDictionarySetResponse>(new NotImplementedException());
        }

        public CacheDictionaryGetResponse DictionaryGet(string cacheName, string dictionaryName, byte[] key)
        {
            throw new NotImplementedException();
        }

        public CacheDictionaryGetResponse DictionaryGet(string cacheName, string dictionaryName, string key)
        {
            throw new NotImplementedException();
        }

        public async Task<CacheDictionaryGetResponse> DictionaryGetAsync(string cacheName, string dictionaryName, byte[] key)
        {
            return await Task.FromException<CacheDictionaryGetResponse>(new NotImplementedException());
        }

        public async Task<CacheDictionaryGetResponse> DictionaryGetAsync(string cacheName, string dictionaryName, string key)
        {
            return await Task.FromException<CacheDictionaryGetResponse>(new NotImplementedException());
        }

        public CacheDictionarySetMultiResponse DictionarySetMulti(string cacheName, string dictionaryName, IDictionary<byte[], byte[]> dictionary, bool refreshTtl, uint? ttlSeconds = null)
        {
            throw new NotImplementedException();
        }

        public CacheDictionarySetMultiResponse DictionarySetMulti(string cacheName, string dictionaryName, IDictionary<string, string> dictionary, bool refreshTtl, uint? ttlSeconds = null)
        {
            throw new NotImplementedException();
        }

        public async Task<CacheDictionarySetMultiResponse> DictionarySetMultiAsync(string cacheName, string dictionaryName, IDictionary<string, string> dictionary, bool refreshTtl, uint? ttlSeconds = null)
        {
            return await Task.FromException<CacheDictionarySetMultiResponse>(new NotImplementedException());
        }

        public async Task<CacheDictionarySetMultiResponse> DictionarySetMultiAsync(string cacheName, string dictionaryName, IDictionary<byte[], byte[]> dictionary, bool refreshTtl, uint? ttlSeconds = null)
        {
            return await Task.FromException<CacheDictionarySetMultiResponse>(new NotImplementedException());
        }

        public CacheDictionaryGetMultiResponse DictionaryGetMulti(string cacheName, string dictionaryName, params byte[][] keys)
        {
            throw new NotImplementedException();
        }

        public CacheDictionaryGetMultiResponse DictionaryGetMulti(string cacheName, string dictionaryName, params string[] keys)
        {
            throw new NotImplementedException();
        }

        public async Task<CacheDictionaryGetMultiResponse> DictionaryGetMultiAsync(string cacheName, string dictionaryName, params byte[][] keys)
        {
            return await Task.FromException<CacheDictionaryGetMultiResponse>(new NotImplementedException());
        }

        public async Task<CacheDictionaryGetMultiResponse> DictionaryGetMultiAsync(string cacheName, string dictionaryName, params string[] keys)
        {
            return await Task.FromException<CacheDictionaryGetMultiResponse>(new NotImplementedException());
        }

        public CacheDictionaryGetAllResponse DictionaryGetAll(string cacheName, string dictionaryName)
        {
            throw new NotImplementedException();
        }

        public async Task<CacheDictionaryGetAllResponse> DictionaryGetAllAsync(string cacheName, string dictionaryName)
        {
            return await Task.FromException<CacheDictionaryGetAllResponse>(new NotImplementedException());
        }
    }
}

