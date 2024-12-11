using PlanetaryExplorationLogs.API.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanetaryExplorationLogs.API.Data.DTO
{
    public class MissionFormDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int PlanetId { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = "";
    }
}
