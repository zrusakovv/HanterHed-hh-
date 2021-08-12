using System;

namespace HH.Core.Domain
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}