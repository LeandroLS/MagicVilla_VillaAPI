using System;
using MagicVilla_VillaAPI.Models.DTO;

namespace MagicVilla_VillaAPI.Data;

public static class VillaStore
{
    public static List<VillaDto> villaList = new List<VillaDto> {
        new VillaDto{ Name = "Pool View", Id = 1, Sqft=100, Occupancy=4},
        new VillaDto{ Name = "Beach View", Id = 2, Sqft=150, Occupancy=5 },
    };
}
