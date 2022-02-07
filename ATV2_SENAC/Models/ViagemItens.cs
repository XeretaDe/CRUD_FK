using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ATV2_SENAC.Models
{
    [Table("pacotesturisticos")]
    public class ViagemItens
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        public string Origem { get; set; }
        [Required]
        public string Destino { get; set; }
        [Required]
        public string Atrativos { get; set; }
        [Required]
        public DateTime Saida { get; set; }
        [Required]
        public DateTime Retorno { get; set; }
        [Required]
        [ForeignKey("aspnetusers")]
        public string Usuario { get; set; }

        [NotMapped]
        public string nomeUsuario { get; set; }
        [NotMapped]
        public string LoginUser { get; set; }




    }
}
