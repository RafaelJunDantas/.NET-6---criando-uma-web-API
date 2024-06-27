using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTO;

public class CreateCinemaDTO
{
    [Required(ErrorMessage = "O nome do cinema e obrigatorio")]
    [MaxLength(50, ErrorMessage = "Tamanho do nome do cinema nao pode exceder 50 caracteres")]
    public string nome { get; set; }

    public int enderecoID { get; set; }
}
