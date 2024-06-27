using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;

public class Sessao
{
    [Required]
    [Key]
    public int id { get; set; }

    [Required]
    public int filmeID { get; set; }

    public virtual Filme filme { get; set; }

    public int? cinemaID { get; set; }

    public virtual Cinema cinema { get; set; }
}
