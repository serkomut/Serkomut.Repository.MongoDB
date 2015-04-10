using System;

namespace Komut.Data.MongoDB.Core
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}