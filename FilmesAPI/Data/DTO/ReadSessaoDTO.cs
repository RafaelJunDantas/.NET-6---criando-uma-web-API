using FilmesAPI.Models;

namespace FilmesAPI.Data.DTO;

public class ReadSessaoDTO
{
    public int id { get; set; }

    public  int cinemaID { get; set; }

    public int filmeID { get; set; }
}
