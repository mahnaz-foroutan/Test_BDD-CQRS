using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public abstract class EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
