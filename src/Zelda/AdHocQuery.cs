using System;
using System.Linq.Expressions;

namespace Zelda
{
	public class AdHocQuery<T> : Query<T>
		where T : IEntity
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