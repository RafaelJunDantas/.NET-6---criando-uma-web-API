namespace FilmesAPI.Data.DTO;

public class ReadCinemaDTO
{
    public int id { get; set; }

    public string nome { get; set; }

    public ReadEnderecoDTO endereco { get; set; }
}
