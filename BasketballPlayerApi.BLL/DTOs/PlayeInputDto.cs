using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballPlayerApi.BLL.DTOs
{
    public class PlayerInputDto
    {
        [Required(ErrorMessage = "First name is mandatory.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is mandatory.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Position selection is required.")]
        [RegularExpression("^(Guard|Forward|Center)$", ErrorMessage = "Position must be Guard, Forward, or Center.")]
        public string? Position { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "A valid Team identity is required.")]
        public int TeamId { get; set; }
    }
}
