using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Zelda.NHibernate
{
	public class NHibernateRepository<T> : RepositoryBase<T>
		where T : IEntity
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

		public override T Load(int id)
		{
			return Session.Load<T>(id);
		}

		public override IQueryable<T> FindAll(IQueryOptions<T> options)
		{
			QueryOptions nhibernateQueryOptions = GetNHibernateQueryOptions(options);
			var provider = new NHibernateQueryProvider(Session, nhibernateQueryOptions);

			return new global::NHibernate.Linq.Query<T>(provider, nhibernateQueryOptions);
		}

		public QueryOptions GetNHibernateQueryOptions(IQueryOptions<T> options)
		{
			var nhibernateQueryOptions = new QueryOptions();

			foreach (string expansion in options.Expansions)
				nhibernateQueryOptions.AddExpansion(expansion);

			return nhibernateQueryOptions;
		}
	}
}