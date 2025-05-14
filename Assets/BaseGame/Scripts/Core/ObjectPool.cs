using System.Collections.Generic;
using UnityEngine;

namespace BaseGame.Scripts.Core
{
    public class ObjectPool<T>
        where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Queue<T> _available = new Queue<T>();

        public ObjectPool(T prefab, int initialSize, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                T inst = GameObject.Instantiate(_prefab, _parent);
                inst.gameObject.SetActive(false);
                _available.Enqueue(inst);
            }
        }

        public T Get()
        {
            if(_available.Count > 0)
            {
                T item = _available.Dequeue();
                item.gameObject.SetActive(true);

                return item;
            }
            
            T newInst = GameObject.Instantiate(_prefab, _parent);

            return newInst;
        }

        public void Return(T item)
        {
            item.gameObject.SetActive(false);
            _available.Enqueue(item);
        }
    }
}