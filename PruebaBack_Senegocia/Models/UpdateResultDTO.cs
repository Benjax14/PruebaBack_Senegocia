using System.ComponentModel.DataAnnotations;

namespace PruebaBack_Senegocia.Models
{
    public class UpdateResultDTO
    {
        [Required]
        public decimal Score { get; set; }
        public DateOnly Date { get; set; }
        [Required]
        public string Observation { get; set; }
    }
}
