using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication13.Models
{
    public class Movie : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage ="Title cannot be longer than 100 characters")]
        [Display(Name = "Movie Title")]
        public string Title { get; set; }

        [Display(Name = "Director")]
        [DirectorValidation]
        public string Director { get; set; }

        [Display(Name = "Year")]
        [Range(1850,3000, ErrorMessage = "Wrong year")]
        public int Year { get; set; }

        [Display(Name = "Genre")]
        public string Genre { get; set; }

        public User User { get; set; }
        public string userId { get; set; }

        public Movie()
        {

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Director == "Quentin Tarantino" && Year < 1987)
            {
                yield return new ValidationResult("There are no movies by Tarantino before 1987", new[] { "Director", "Year" });
            }

            if (Director == "Stanley Kubrick" && Year < 1951)
            {
                yield return new ValidationResult("There are no movies by Kubrick before 1951", new[] { "Director", "Year" });
            }

            if (Director == "Martin Scorsese" && Year < 1967)
            {
                yield return new ValidationResult("There are no movies by Scorsese before 1967", new[] { "Director", "Year" });
            }

        }
    }
}
