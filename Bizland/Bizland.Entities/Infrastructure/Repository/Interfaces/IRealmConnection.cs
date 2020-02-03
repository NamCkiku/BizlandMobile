using Realms;

namespace Bizland.Entities.Infrastructure.Repository
{
    public interface IRealmConnection
    {
        Realm Connection { get; }

        void Destroy();
    }
}