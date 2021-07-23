using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.View;
using System.Threading.Tasks;

namespace SimpleMusicStore.Services
{
    public class LabelService : ILabelService
    {
        private readonly IUnitOfWork _db;
        private readonly ICurrentUserActivities _currentUser;

        public LabelService(IUnitOfWork db, ICurrentUserActivities currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task<LabelView> Find(int id)
        {
            var label = await _db.Labels.Find(id);
            label.IsFollowed = _currentUser.IsLabelFollowed(id);
            return label;
        }
    }
}
