using System;
using System.Collections.Generic;
using System.Text;
using Irminsul.Application.DTos.External;
using Irminsul.Application.Interfaces;

namespace Irminsul.Infrastructure.External;

public class GenshinApiClient : IGenshinApiClient
{
    private readonly HttpClient _httpClient;

    public GenshinApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GenshinCharacterDto?> GetCharacterAsync(string name)
    {
        throw new NotImplementedException();
    }

}