using System;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.DTO;

public class VillaDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public int Occupancy { get; set; }
    public int Sqft  {get; set; }
}
