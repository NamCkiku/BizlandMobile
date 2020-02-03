using Realms;

namespace Bizland.Entities
{
    public interface IRealmEntity : IEntity
    {
        RealmInteger<int> Counter { get; set; }
    }
}