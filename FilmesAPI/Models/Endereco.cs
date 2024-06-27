using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Endereco
{
    [Required]
    [Key]
    public int id { get; set; }

    [Required(ErrorMessage = "O logradouro e obrigatorio")]
    [MaxLength(100, ErrorMessage = "Tamanho do logradouro nao pode exceder 100 caracteres")]
    public string logradouro { get; set; }

    public int numero { get; set; }

    public virtual Cinema Cinema { get; set; }
}
