﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheClient;
using MomentoSdk.Responses;

namespace MomentoSdk.Incubating.Responses;

public class CacheDictionaryGetAllResponse
{
    public CacheGetStatus Status { get; private set; }
    public Dictionary<byte[], byte[]>? ByteArrayDictionary { get; private set; }

    public CacheDictionaryGetAllResponse(_DictionaryGetAllResponse response)
    {
        Status = CacheGetStatusUtil.From(response.Result);
        if (Status == CacheGetStatus.MISS)
        {
            ByteArrayDictionary = null;
        }
        else
        {
            ByteArrayDictionary = new Dictionary<byte[], byte[]>(
                response.DictionaryBody.Select(
                    kv => new KeyValuePair<byte[], byte[]>(
                        kv.Key.ToByteArray(), kv.Value.ToByteArray())));
        }
    }

    public Dictionary<string, string>? StringDictionary()
    {
        if (ByteArrayDictionary == null)
        {
            return null;
        }

        return new Dictionary<string, string>(
            ByteArrayDictionary.Select(kv => new KeyValuePair<string, string>(
                Encoding.UTF8.GetString(kv.Key),
                Encoding.UTF8.GetString(kv.Value)
            ))
        );
    }
}
