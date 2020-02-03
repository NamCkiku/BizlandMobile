using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bizland.Entities.RealmEntity
{
    public class UserRealm : RealmObject, IRealmEntity
    {
        [Indexed]
        [PrimaryKey]
        public long Id { get; set; }

        public RealmInteger<int> Counter { get; set; }

        public int PK_UserID { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? LastSynchronizationDate { get; set; }

        public DateTimeOffset? SysLastChangeDate { get; set; }

        public DateTimeOffset? SysDeletedDate { get; set; }
    }
}
