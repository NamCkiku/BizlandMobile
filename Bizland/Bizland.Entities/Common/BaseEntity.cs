﻿using System;

namespace Bizland.Entities
{
    public class BaseEntity : IEntity
    {
        public long Id { get; set; }
        public DateTimeOffset? LastSynchronizationDate { get; set; }
        public DateTimeOffset? SysLastChangeDate { get; set; }
        public DateTimeOffset? SysDeletedDate { get; set; }
    }
}