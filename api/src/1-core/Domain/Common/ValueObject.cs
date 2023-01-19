namespace Resto.Domain.Common;

// Learn more: https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects
public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        // ^ -> XOR
        // either left OR right is null, not both
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            return false;

        return ReferenceEquals(left, null) // they're both null
               || left.Equals(right); // use 'normal' equals
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var other = (ValueObject) obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        foreach (var equalityComponent in GetEqualityComponents())
        {
            hashCode.Add(equalityComponent?.GetHashCode() ?? 0);
        }

        return hashCode.ToHashCode();
        // return GetEqualityComponents()
        //     .Select(x => x != null ? x.GetHashCode() : 0)
        //     .Aggregate((x, y) => x ^ y);
    }
}
