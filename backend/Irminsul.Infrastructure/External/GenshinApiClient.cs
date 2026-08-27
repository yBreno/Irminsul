using Irminsul.Application.DTos.External;
using Irminsul.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

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
        var response = await _httpClient.GetAsync(
            $"api/v5/characters?query={name}");

        response.EnsureSuccessStatusCode();

        var character = await response.Content
            .ReadFromJsonAsync<GenshinCharacterDto>();

        return character;
    }

}