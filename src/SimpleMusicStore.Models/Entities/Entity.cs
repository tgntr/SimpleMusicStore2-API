namespace SimpleMusicStore.Models.Entities
{
    //todo chec if data entities can inherit from abstract classes
    public abstract class Entity<T>
    {
        public T Id { get; set; }
    }
}
