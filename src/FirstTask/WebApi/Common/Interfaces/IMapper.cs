namespace WebApi.Common.Interfaces
{
    public interface IMapper<TRequest, TResponce>
    {
        IEnumerable<TResponce> MapWith(IEnumerable<TRequest> request);
    }
}
