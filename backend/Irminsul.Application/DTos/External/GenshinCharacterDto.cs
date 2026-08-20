using System;
using System.Collections.Generic;
using System.Text;

namespace Irminsul.Application.DTos.External
{
    //Nomes que vem da API Externa
    public record GenshinCharacterDto(
        string name,
        string title,
        int rarity,
        string elementText,
        string weaponText,
        string region,
        string imageUrl,
        string description
    );

    public record GenshinCharacterImagesDto(
         string hoyowiki_icon
    );
}