namespace SimpleMusicStore.Contracts.Auth
{
    public interface IClaimAccessor
    {
        int Id { get; }
        //string Email { get; }
        //string FullName { get; }
        bool IsAuthenticated { get; }
    }
}
