using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMusicStore.Models.Entities
{
    public abstract class EntityWithCustomId<T>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public T Id { get; set; }
    }
}
