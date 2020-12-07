namespace BeautyStore.Interfaces.Helpers
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
