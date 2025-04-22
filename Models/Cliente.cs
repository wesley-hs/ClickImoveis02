using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickImoveis01.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]   
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "o telefone é obrigatório.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = " A senha é obrigatória.")]
        public string Senha { get; set; }
    }
}
