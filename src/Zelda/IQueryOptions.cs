using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Zelda
{
	public interface IQueryOptions<T>
		where T : IEntity
	{
		ICollection<string> Expansions { get; }
		IQueryOptions<T> AddExpansion(Expression<Func<T, object>> path);
		IQueryOptions<T> AddExpansion(string path);
	}
}