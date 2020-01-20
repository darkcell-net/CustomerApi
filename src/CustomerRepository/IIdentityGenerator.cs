namespace CustomerRepository
{
    // This generator is a little overkill, we could just use the DbContext.OnModelCreating and set the
    // property to ValueGeneratedOnAdd.
    public interface IIdentityGenerator
    {
        string GenerateId();
    }
}