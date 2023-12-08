using Domain.Entities;

namespace CustomerAPI.Specs.Support;

public class CustomerComparer : IEqualityComparer<Customer>
{
    public bool Equals(Customer? x, Customer? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Id == y.Id && x.Firstname==y.Firstname && x.Lastname==y.Lastname && x.DateOfBirth.Equals(y.DateOfBirth) && x.PhoneNumber == y.PhoneNumber && x.Email == y.Email && x.BankAccountNumber==y.BankAccountNumber;
    }

    public int GetHashCode(Customer obj)
    {
        return HashCode.Combine(obj.Id, obj.Firstname, obj.Lastname, obj.DateOfBirth, obj.BankAccountNumber);
    }
}