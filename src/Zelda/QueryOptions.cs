using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Zelda
{
	public class QueryOptions<K, T> : IQueryOptions<K, T>
		where T : IEntity<K>
	{
		public static readonly QueryOptions<K, T> Empty = new QueryOptions<K, T>();

		public QueryOptions()
		{
			Expansions = new List<string>();
		}

		public ICollection<string> Expansions { get; private set; }

		public IQueryOptions<K, T> AddExpansion(Expression<Func<T, object>> path)
		{
			Expansions.Add(String.Join(".", GetPathTokens(path).Reverse().ToArray()));
			return this;
		}

		public IQueryOptions<K, T> AddExpansion(string path)
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