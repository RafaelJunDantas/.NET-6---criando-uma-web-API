using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Cinema
{
    [Required]
    [Key]
    public int id { get; set; }

    [Required(ErrorMessage = "O nome do cinema e obrigatorio")]
    [MaxLength(50, ErrorMessage = "Tamanho do nome do cinema nao pode exceder 50 caracteres")]
    public string nome { get; set; }

    public int enderecoID { get; set; }
    public virtual Endereco endereco { get; set; }

    public virtual ICollection<Sessao> sessoes { get; set; }
}
