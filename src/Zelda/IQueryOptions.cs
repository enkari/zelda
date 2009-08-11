using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Zelda
{
	public interface IQueryOptions<K, T>
		where T : IEntity<K>
	{
		ICollection<string> Expansions { get; }
		IQueryOptions<K, T> AddExpansion(Expression<Func<T, object>> path);
		IQueryOptions<K, T> AddExpansion(string path);
	}
}