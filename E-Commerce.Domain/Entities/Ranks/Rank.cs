using E_Commerce.Domain.Common;
using E_Commerce.Domain.Entities.Users;

namespace E_Commerce.Domain.Entities.Ranks
{
    public class Rank : BaseEntity
    {
        public Guid Name { get; set; }
        public virtual ICollection<User> Users{ get; set; }
    }
}
