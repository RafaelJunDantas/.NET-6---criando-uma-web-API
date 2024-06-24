using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTO;

public class CreateFilmeDTO
{
    [Required(ErrorMessage = "O titulo do filme e obrigatorio")]
    [StringLength(50, ErrorMessage = "Tamanho do titulo nao pode exceder 50 caracteres")]
    public string titulo { get; set; }

    [Required(ErrorMessage = "O titulo do filme e obrigatorio")]
    [StringLength(50, ErrorMessage = "Tamanho do genero nao pode exceder 50 caracteres")]
    public string genero { get; set; }

    [Required]
    [Range(70, 600, ErrorMessage = "A duracao deve ter entre 70 e 600 minutos")]
    public int duracao { get; set; }
}
