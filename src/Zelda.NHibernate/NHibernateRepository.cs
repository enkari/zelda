using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Zelda.NHibernate
{
	public class NHibernateRepository<K, T> : RepositoryBase<K, T>
		where T : IEntity<K>
	{
		public ISessionFactory SessionFactory { get; private set; }

		public ISession Session
		{
			get { return SessionFactory.GetCurrentSession(); }
		}

		public NHibernateRepository(ISessionFactory sessionFactory)
		{
			SessionFactory = sessionFactory;
		}

		public override void Save(T entity)
		{
			Session.SaveOrUpdate(entity);
		}

		public override void Delete(T entity)
		{
			Session.Delete(entity);
		}

		public override T Load(K id)
		{
			return Session.Load<T>(id);
		}

		public override IQueryable<T> FindAll(IQueryOptions<K, T> options)
		{
			QueryOptions nhibernateQueryOptions = GetNHibernateQueryOptions(options);
			var provider = new NHibernateQueryProvider(Session, nhibernateQueryOptions);

			return new global::NHibernate.Linq.Query<T>(provider, nhibernateQueryOptions);
		}

		public QueryOptions GetNHibernateQueryOptions(IQueryOptions<K, T> options)
		{
			var nhibernateQueryOptions = new QueryOptions();

			foreach (string expansion in options.Expansions)
				nhibernateQueryOptions.AddExpansion(expansion);

			return nhibernateQueryOptions;
		}
	}
}