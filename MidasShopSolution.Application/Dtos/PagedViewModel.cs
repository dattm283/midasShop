namespace MidasShopSolution.Application.Dtos;

public class PagedViewModel<T>
{
    public List<T> Items { get; set; }
    public int TotalRecord { get; set; }
}