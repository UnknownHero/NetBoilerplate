using Infrastructure.Domain.Specification;

namespace Extensions
{
    public static class ExtensionMethods
    {
        public static Specification<TEntity> And<TEntity>(this Specification<TEntity> s1, Specification<TEntity> s2)
        {
            return new AndSpecification<TEntity>(s1, s2);
        }

        public static Specification<TEntity> Or<TEntity>(this Specification<TEntity> s1, Specification<TEntity> s2)
        {
            return new OrSpecification<TEntity>(s1, s2);
        }

        public static Specification<TEntity> Not<TEntity>(this Specification<TEntity> s)
        {
            return new NotSpecification<TEntity>(s);
        }
    }
}