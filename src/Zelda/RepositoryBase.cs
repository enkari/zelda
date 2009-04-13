using System;
using System.Linq;
using System.Linq.Expressions;

namespace Zelda
{
	public abstract class RepositoryBase<T> : IRepository<T>
		where T : IEntity
	{
		public abstract void Save(T entity);
		public abstract void Delete(T entity);

		public abstract T Load(int id);
		public abstract IQueryable<T> FindAll(IQueryOptions<T> options);

		public virtual IQueryable<T> FindAll()
		{
			return FindAll(QueryOptions<T>.Empty);
		}

		public virtual bool Exists(IQuery<T> query)
		{
			return query.Contains(FindAll());
		}

		public virtual T Find(IQuery<T> query)
		{
			return query.FindOneMatchFrom(FindAll());
		}

		public virtual T Find(IQuery<T> query, IQueryOptions<T> options)
		{
			return query.FindOneMatchFrom(FindAll(options));
		}

		public T Find(Expression<Func<T, bool>> criteria)
		{
			return Find(CreateAdHocQuery(criteria));
		}

		public T Find(Expression<Func<T, bool>> criteria, IQueryOptions<T> options)
		{
			return Find(CreateAdHocQuery(criteria), options);
		}

		public virtual IQueryable<T> FindAll(IQuery<T> query)
		{
			return query.FindAllMatchesFrom(FindAll());
		}

		public virtual IQueryable<T> FindAll(IQuery<T> query, IQueryOptions<T> options)
		{
			return query.FindAllMatchesFrom(FindAll(options));
		}

		public IQueryable<T> FindAll(Expression<Func<T, bool>> criteria)
		{
			return FindAll(CreateAdHocQuery(criteria));
		}

		public IQueryable<T> FindAll(Expression<Func<T, bool>> criteria, IQueryOptions<T> options)
		{
			return FindAll(CreateAdHocQuery(criteria), options);
		}

		protected AdHocQuery<T> CreateAdHocQuery(Expression<Func<T, bool>> criteria)
		{
			return new AdHocQuery<T>(criteria);
		}
	}
}