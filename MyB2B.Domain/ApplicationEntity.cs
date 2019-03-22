using System;
using System.ComponentModel.DataAnnotations;

namespace MyB2B.Domain
{
    public abstract class ApplicationEntity
    {
        [Key]
        public int Id { get; set; }
    }

    public interface IImmutableEntity
    {
        DateTime CreatedAt { get; set; }
        int CreatedBy { get; set; }

        void AuditCreated(DateTime time, int creator);
    }

    public interface IAuditableEntity
    {
        DateTime? ModifiedAt { get; set; }
        int ModifiedBy { get; set; }

        void AuditModified(DateTime time, int modifier);
    }

    public abstract class ImmutableEntity : ApplicationEntity, IImmutableEntity
    {
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public void AuditCreated(DateTime time, int creator)
        {
            if (CreatedBy > 0)
                return;

            CreatedAt = time;
            CreatedBy = creator;
        }
    }

    public abstract class AuditableEntity : ApplicationEntity, IAuditableEntity
    {
        public DateTime? ModifiedAt { get; set; }
        public int ModifiedBy { get; set; }

        public void AuditModified(DateTime time, int modifier)
        {
            ModifiedAt = time;
            ModifiedBy = modifier;
        }
    }

    public abstract class AuditableImmutableEntity : ApplicationEntity, IImmutableEntity, IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }

        public void AuditCreated(DateTime time, int creator)
        {
            if (CreatedBy > 0)
                return;

            CreatedAt = time;
            CreatedBy = creator;
        }

        public DateTime? ModifiedAt { get; set; }
        public int ModifiedBy { get; set; }

        public void AuditModified(DateTime time, int modifier)
        {
            ModifiedAt = time;
            ModifiedBy = modifier;
        }
    }
}