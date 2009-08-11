using System;
using System.Linq;
using System.Linq.Expressions;

namespace Zelda
{
	public abstract class Query<K, T> : IQuery<K, T>
		where T : IEntity<K>
	{
		public abstract Expression<Func<T, bool>> MatchingCriteria { get; }

		public bool Contains(IQueryable<T> candidates)
		{
			return candidates.Any(MatchingCriteria);
		}

		public IQueryable<T> FindAllMatchesFrom(IQueryable<T> candidates)
		{
			return candidates.Where(MatchingCriteria).AsQueryable();
		}

		public T FindOneMatchFrom(IQueryable<T> candidates)
		{
			return candidates.Where(MatchingCriteria).SingleOrDefault();
		}
	}
}