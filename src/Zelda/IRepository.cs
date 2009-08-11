using System;
using System.Linq;
using System.Linq.Expressions;

namespace Zelda
{
	public interface IRepository<K, T>
		where T : IEntity<K>
	{
		void Save(T entity);
		void Delete(T entity);

		T Load(K id);

		bool Exists(IQuery<K, T> query);

		T Find(IQuery<K, T> query);
		T Find(IQuery<K, T> query, IQueryOptions<K, T> options);

		T Find(Expression<Func<T, bool>> criteria);
		T Find(Expression<Func<T, bool>> criteria, IQueryOptions<K, T> options);

		IQueryable<T> FindAll();
		IQueryable<T> FindAll(IQueryOptions<K, T> options);

		IQueryable<T> FindAll(IQuery<K, T> query);
		IQueryable<T> FindAll(IQuery<K, T> query, IQueryOptions<K, T> options);

		IQueryable<T> FindAll(Expression<Func<T, bool>> criteria);
		IQueryable<T> FindAll(Expression<Func<T, bool>> criteria, IQueryOptions<K, T> options);
	}
}