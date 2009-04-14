using System.Linq;

namespace Zelda
{
	public interface IQuery<T>
		where T : IEntity
	{
		bool Contains(IQueryable<T> candidates);
		IQueryable<T> FindAllMatchesFrom(IQueryable<T> candidates);
		T FindOneMatchFrom(IQueryable<T> candidates);
	}
}