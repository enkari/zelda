using System;
using System.Linq;
using System.Linq.Expressions;

namespace Zelda
{
	public interface IRepository<T>
		where T : IEntity
	{
		void Save(T entity);
		void Delete(T entity);

		T Load(int id);

		bool Exists(IQuery<T> query);

		T Find(IQuery<T> query);
		T Find(IQuery<T> query, IQueryOptions<T> options);

		T Find(Expression<Func<T, bool>> criteria);
		T Find(Expression<Func<T, bool>> criteria, IQueryOptions<T> options);

		IQueryable<T> FindAll();
		IQueryable<T> FindAll(IQueryOptions<T> options);

		IQueryable<T> FindAll(IQuery<T> query);
		IQueryable<T> FindAll(IQuery<T> query, IQueryOptions<T> options);

		IQueryable<T> FindAll(Expression<Func<T, bool>> criteria);
		IQueryable<T> FindAll(Expression<Func<T, bool>> criteria, IQueryOptions<T> options);
	}
}