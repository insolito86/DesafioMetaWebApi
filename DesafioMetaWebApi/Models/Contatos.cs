using System.ComponentModel.DataAnnotations;

namespace DesafioMetaWebApi.Models
{
    public class Contatos
    {
        [Key]
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Canal { get; set; }
        public string Valor { get; set; }
        public string Obs { get; set; }
    }
}