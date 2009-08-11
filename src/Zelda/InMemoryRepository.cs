using System;
using System.Collections.Generic;
using System.Linq;

namespace Zelda
{
    public abstract class InMemoryRepository<K, T> : RepositoryBase<K, T>
        where T : IEntity<K>
    {
        protected readonly Dictionary<K, T> _items = new Dictionary<K, T>();

        public InMemoryRepository()
        {
        }

        public InMemoryRepository(IEnumerable<T> items)
        {
            foreach(var item in items)
            {
                Save(item);
            }
        }

        public abstract K GetKey(T entity);

        public override void Save(T entity)
        {
            entity.Id = GetKey(entity);

            
            _items[entity.Id] = entity;
        }

        public override void Delete(T entity)
        {
            if (_items.ContainsKey(entity.Id))
                _items.Remove(entity.Id);
        }

        public override T Load(K id)
        {
            return !_items.ContainsKey(id) ? default(T) : _items[id];
        }

        public override IQueryable<T> FindAll(IQueryOptions<K, T> options)
        {
            return new List<T>(_items.Values).AsQueryable();
        }
    }
}