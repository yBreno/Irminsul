
using System;
using System.Collections.Generic;
using System.Text;
using Irminsul.Application.DTos.External;

namespace Irminsul.Application.Interfaces;

public interface IGenshinApiClient
{
    Task<GenshinCharacterDto?> GetCharacterAsync(string name);
}