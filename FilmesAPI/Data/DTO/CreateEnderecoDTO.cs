using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTO;

public class CreateEnderecoDTO
{
    [Required(ErrorMessage = "O logradouro e obrigatorio")]
    [MaxLength(100, ErrorMessage = "Tamanho do logradouro nao pode exceder 100 caracteres")]
    public string logradouro { get; set; }

    public int numero { get; set; }
}
