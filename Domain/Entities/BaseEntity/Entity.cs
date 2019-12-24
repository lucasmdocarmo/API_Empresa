using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.BaseEntity
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}
