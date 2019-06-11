using SimpleMusicStore.Constants;

namespace SimpleMusicStore.Contracts.Sorting
{
    public interface ISortingStrategyFactory
    {
        SortingStrategy Create(SortTypes sort);
    }
}
