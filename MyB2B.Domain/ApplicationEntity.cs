using System;
using System.ComponentModel.DataAnnotations;

namespace myB2B.Domain
{
    public abstract class ApplicationEntity
    {
        [Key]
        public int Id { get; set; }
    }

    public interface IImmutableEntity
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
    }

    public interface IAuditableEntity
    {
        DateTime? ModifiedAt { get; set; }
        string ModifiedBy { get; set; }
    }

    public abstract class ImmutableEntity : ApplicationEntity, IImmutableEntity
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }

    public abstract class AuditableEntity : ApplicationEntity, IAuditableEntity
    {
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }

    public abstract class AuditableImmutableEntity : ApplicationEntity, IImmutableEntity, IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}