namespace Ecommerce_API.Data.DTOS.Links;

public class LinksDto
{
    public string Href { get; private set; }
    public string Rel { get; private set; }
    public string Metodo { get; private set; }

    public LinksDto(string href, string rel, string metodo)
    {
        Href = href;
        Rel = rel;
        Metodo = metodo;
    }
       
}
