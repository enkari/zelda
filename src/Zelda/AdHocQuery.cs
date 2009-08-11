using System;
using System.Linq.Expressions;

namespace Zelda
{
	public class AdHocQuery<K, T> : Query<K, T>
		where T : IEntity<K>
	{
		private readonly Expression<Func<T, bool>> _criteria;

		public override Expression<Func<T, bool>> MatchingCriteria
		{
			get { return _criteria; }
		}

		public AdHocQuery(Expression<Func<T, bool>> criteria)
		{
			_criteria = criteria;
		}
	}
}