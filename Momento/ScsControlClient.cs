﻿using System;
using MomentoSdk.Exceptions;
using ControlClient;

namespace MomentoSdk
{
    internal sealed class ScsControlClient : IDisposable
    {
        private readonly ControlGrpcManager grpcManager;
        private readonly string authToken;
        private readonly string endpoint;

        public ScsControlClient(string authToken, string endpoint)
        {
            this.grpcManager = new ControlGrpcManager(authToken, endpoint);
            this.authToken = authToken;
            this.endpoint = endpoint;
        }

        public Responses.CreateCacheResponse CreateCache(string cacheName)
        {
            CheckValidCacheName(cacheName);
            try
            {
                _CreateCacheRequest request = new _CreateCacheRequest() { CacheName = cacheName };
                this.grpcManager.Client().CreateCache(request);
                return new Responses.CreateCacheResponse();
            }
            catch (Exception e)
            {
                throw CacheExceptionMapper.Convert(e);
            }
        }

        public Responses.DeleteCacheResponse DeleteCache(string cacheName)
        {
            _DeleteCacheRequest request = new _DeleteCacheRequest() { CacheName = cacheName };
            try
            {
                this.grpcManager.Client().DeleteCache(request);
                return new Responses.DeleteCacheResponse();
            }
            catch (Exception e)
            {
                throw CacheExceptionMapper.Convert(e);
            }
        }

        public Responses.ListCachesResponse ListCaches(string nextPageToken = null)
        {
            _ListCachesRequest request = new _ListCachesRequest() { NextToken = nextPageToken == null ? "" : nextPageToken };
            try
            {
                ControlClient._ListCachesResponse result = this.grpcManager.Client().ListCaches(request);
                return new Responses.ListCachesResponse(result);
            }
            catch (Exception e)
            {
                throw CacheExceptionMapper.Convert(e);
            }
        }

        private bool CheckValidCacheName(string cacheName)
        {
            if (string.IsNullOrWhiteSpace(cacheName))
            {
                throw new InvalidArgumentException("Cache name must be nonempty");
            }
            return true;
        }

        public void Dispose()
        {
            this.grpcManager.Dispose();
        }
    }
}
