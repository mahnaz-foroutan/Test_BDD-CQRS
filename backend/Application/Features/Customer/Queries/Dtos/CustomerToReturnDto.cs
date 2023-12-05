using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customer.Queries.Dtos
{
    public class CustomerToReturnDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }

        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
