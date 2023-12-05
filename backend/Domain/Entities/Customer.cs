using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Common;
using System.Net.Mail;

namespace Domain.Entities
{
    [Table("Customers")]
    [Index(nameof(Email), IsUnique = true)]
    [Index("Firstname", "Lastname", "DateOfBirth", IsUnique = true, Name = "Unique_IndexAccount")]
    public class Customer: EntityBase
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(50)]
       
        public string Lastname { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(20)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid phone number")]
        // Assuming phoneNumber is stored in E.164 format
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{9,18}$", ErrorMessage = "Invalid Bank Account number")]
        public string BankAccountNumber { get; set; }

    }
}
