using System;
using System.Collections.Generic;
using System.Linq;

namespace Zelda
{
    public class InMemoryRepository<T> : RepositoryBase<T>
        where T : IEntity
    {
        private readonly Dictionary<int, T> _items = new Dictionary<int, T>();

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

        public override void Save(T entity)
        {
            if (entity.Id == 0)
                entity.Id = _items.Count + 1;

            _items[entity.Id] = entity;
        }

        public override void Delete(T entity)
        {
            if (entity.Id == 0)
                throw new InvalidOperationException("Cannot delete an entity that has an id of 0");

            if (_items.ContainsKey(entity.Id))
                _items.Remove(entity.Id);
        }

        public override T Load(int id)
        {
            return !_items.ContainsKey(id) ? default(T) : _items[id];
        }

        public override IQueryable<T> FindAll(IQueryOptions<T> options)
        {
            return new List<T>(_items.Values).AsQueryable();
        }
    }
}