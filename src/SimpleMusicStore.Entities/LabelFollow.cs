using SimpleMusicStore.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace SimpleMusicStore.Entities
{
    public class LabelFollow : UserActivity
    {
        public LabelFollow()
            : base()
        {
        }

        public LabelFollow(int labelId, int userId)
            : base()
        {
            LabelId = labelId;
            UserId = userId;
        }

        [Required]
        public int LabelId { get; set; }
        public virtual Label Label { get; set; }
    }
}
