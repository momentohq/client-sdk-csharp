﻿using System;
using CacheClient;
namespace MomentoSdk.Responses
{
    public class BaseCacheResponse
    {
        public BaseCacheResponse()
        {
        }

        protected MomentoCacheResult ResultMapper(ECacheResult result)
        {
            switch (result)
            {
                case ECacheResult.Ok: return MomentoCacheResult.Ok;
                case ECacheResult.Hit: return MomentoCacheResult.Hit;
                case ECacheResult.Miss: return MomentoCacheResult.Miss;
                default: return MomentoCacheResult.Unknown;
            }
        }
    }
}
