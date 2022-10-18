namespace MidasShopSolution.Application.Dtos;

public class PagedResult<T>
{
    public List<T> Items { get; set; }
    public int TotalRecord { get; set; }
}