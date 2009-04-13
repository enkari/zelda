using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Zelda
{
	public class QueryOptions<T> : IQueryOptions<T>
		where T : IEntity
	{
		public static readonly QueryOptions<T> Empty = new QueryOptions<T>();

		public QueryOptions()
		{
			Expansions = new List<string>();
		}

		public ICollection<string> Expansions { get; private set; }

		public IQueryOptions<T> AddExpansion(Expression<Func<T, object>> path)
		{
			Expansions.Add(String.Join(".", GetPathTokens(path).Reverse().ToArray()));
			return this;
		}

		public IQueryOptions<T> AddExpansion(string path)
		{
			Expansions.Add(path);
			return this;
		}

		private static IEnumerable<string> GetPathTokens(LambdaExpression path)
		{
			Expression current = path.Body;

			while (current.NodeType == ExpressionType.MemberAccess)
			{
				var memberExpression = current as MemberExpression;
				yield return memberExpression.Member.Name;
				current = memberExpression.Expression;
			}
		}
	}
}