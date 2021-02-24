using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication13.Models
{
    public class User : IdentityUser
    {
        //public override string Id { set; get; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

    
        public List<Movie> listOfMovies { get; set; }

        public User()
        {
            listOfMovies = new List<Movie>();
        }
    
    }
}
