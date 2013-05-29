using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Infrastructure.Domain;
using Test.Infrastructure.Domain.Fakes;

namespace Test.Infrastructure.DAL
{
    internal class InMemoryDbSet<T> : IDbSet<T> where T : Entity<Guid>
    {
        private readonly HashSet<T> _data;

        public InMemoryDbSet()
        {
            _data = new HashSet<T>();
        }
         
        public virtual T Find(params object[] keyValues)
        {
            try
            {
                if (keyValues == null || keyValues.Length == 0)
                    return null;

                var key = keyValues.Single();
                if (key == null)
                    return null;

                return _data.FirstOrDefault(x => x.Id == (Guid)key);
            }
            catch (Exception e)
            {

                throw e;
            }
        
        }


        public T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        Type IQueryable.ElementType
        {
            get { return _data.AsQueryable().ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _data.AsQueryable().Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _data.AsQueryable().Provider; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public ObservableCollection<T> Local
        {
            get { return new ObservableCollection<T>(_data); }
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public void Detach(T item)
        {
            _data.Remove(item);
        }
    }
}