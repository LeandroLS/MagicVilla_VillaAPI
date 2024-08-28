using System;
using MagicVilla_VillaAPI.Models.DTO;

namespace MagicVilla_VillaAPI.Data;

public static class VillaStore
{
    public static List<VillaDto> villaList = new List<VillaDto> {
        new VillaDto{ Name = "Pool View", Id = 1 },
        new VillaDto{ Name = "Beach View", Id = 2 },
    };
}
