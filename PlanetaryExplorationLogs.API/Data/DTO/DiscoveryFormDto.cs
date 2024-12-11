using PlanetaryExplorationLogs.API.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetaryExplorationLogs.API.Data.DTO
{
    public class DiscoveryFormDto
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = "";

        [Required]
        public int MissionId { get; set; }

        [Required]
        public int DiscoveryTypeId { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = "";

        [StringLength(200)]
        public string Location { get; set; } = "";
    }
}
