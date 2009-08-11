using System.Linq;

namespace Zelda
{
	public interface IQuery<K, T>
		where T : IEntity<K>
	{
		bool Contains(IQueryable<T> candidates);
		IQueryable<T> FindAllMatchesFrom(IQueryable<T> candidates);
		T FindOneMatchFrom(IQueryable<T> candidates);
	}
}