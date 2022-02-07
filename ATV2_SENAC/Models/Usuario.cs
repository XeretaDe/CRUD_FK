using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ATV2_SENAC.Models
{
    [Table("usuario")]
    public class Usuario
    {

        [Required]
        public string Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [NotMapped]
        public List<Usuario> ListUsers { get; set; }

    }
}
