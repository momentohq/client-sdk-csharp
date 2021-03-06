using System.Collections.Generic;
using System.Linq;

namespace MomentoSdk.Responses;

public class CacheGetBatchResponse
{
    public List<CacheGetResponse> Responses { get; }

    public CacheGetBatchResponse(IEnumerable<CacheGetResponse> responses)
    {
        this.Responses = new(responses);
    }

    public List<CacheGetStatus> Status
    {
        get => Responses.Select(response => response.Status).ToList();
    }

    public List<string?> Strings()
    {
        return Responses.Select(response => response.String()).ToList();
    }

    public List<byte[]?> Bytes
    {
        get => Responses.Select(response => response.Bytes).ToList();
    }
}
