namespace GoodHamburger.Models
{
    public record ApiDataResult<T>(bool Success, string Message, T? Data);
}
