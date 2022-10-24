namespace MidasShopSolution.Api.Utilities.Exceptions;

public class MidasShopException : Exception
{
    public MidasShopException()
    {
        
    }
    public MidasShopException(string message)
    {
        
    }
    public MidasShopException(string message, Exception inner) 
        : base(message, inner) 
    {
        
    }
}