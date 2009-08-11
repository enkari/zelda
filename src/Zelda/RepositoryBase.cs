using System;
using System.Linq;
using System.Linq.Expressions;

namespace Zelda
{
	public abstract class RepositoryBase<K, T> : IRepository<K, T>
		where T : IEntity<K>
	{
		public abstract void Save(T entity);
		public abstract void Delete(T entity);

		public abstract T Load(K id);
		public abstract IQueryable<T> FindAll(IQueryOptions<K, T> options);



		public virtual IQueryable<T> FindAll()
		{
			return FindAll(QueryOptions<K, T>.Empty);
		}

		public virtual bool Exists(IQuery<K, T> query)
		{
			return query.Contains(FindAll());
		}

		public virtual T Find(IQuery<K, T> query)
		{
			return query.FindOneMatchFrom(FindAll());
		}

		public virtual T Find(IQuery<K, T> query, IQueryOptions<K, T> options)
		{
			return query.FindOneMatchFrom(FindAll(options));
		}

		public T Find(Expression<Func<T, bool>> criteria)
		{
			return Find(CreateAdHocQuery(criteria));
		}

		public T Find(Expression<Func<T, bool>> criteria, IQueryOptions<K, T> options)
		{
			return Find(CreateAdHocQuery(criteria), options);
		}

		public virtual IQueryable<T> FindAll(IQuery<K, T> query)
		{
			return query.FindAllMatchesFrom(FindAll());
		}

		public virtual IQueryable<T> FindAll(IQuery<K, T> query, IQueryOptions<K, T> options)
		{
			return query.FindAllMatchesFrom(FindAll(options));
		}

		public IQueryable<T> FindAll(Expression<Func<T, bool>> criteria)
		{
			return FindAll(CreateAdHocQuery(criteria));
		}

		public IQueryable<T> FindAll(Expression<Func<T, bool>> criteria, IQueryOptions<K, T> options)
		{
			return FindAll(CreateAdHocQuery(criteria), options);
		}

		protected AdHocQuery<K, T> CreateAdHocQuery(Expression<Func<T, bool>> criteria)
		{
			return new AdHocQuery<K, T>(criteria);
		}
	}
}