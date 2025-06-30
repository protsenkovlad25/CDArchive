using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VP
{
    public class Pool<T> where T : Component
    {
        public event System.Action<T> OnCreateNew;
        public event System.Action<T> OnReturned;

        private readonly T prefab;
        private readonly Transform parent;
        private readonly Dictionary<T, bool> objects;

        public Dictionary<T, bool> Objects => objects;
        public List<T> ObjectsList => objects.Keys.ToList();

        public Pool(T prefab, Transform parent = null, int count = 10)
        {
            this.prefab = prefab;
            this.parent = parent;

            objects = new Dictionary<T, bool>();

            for (int i = 0; i < count; i++)
                CreateObject();
        }

        private T CreateObject(bool isNew = false)
        {
            T clone = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);

            clone.transform.SetParent(parent);
            clone.gameObject.SetActive(false);

            if (isNew) OnCreateNew?.Invoke(clone);
            
            objects.Add(clone, false);

            return clone;
        }
        private void ActivateObject(T obj)
        {
            objects[obj] = true;

            obj.gameObject.SetActive(true);
            obj.transform.SetParent(null);
        }
        private void DeactivateObject(T obj)
        {
            objects[obj] = false;
            
            obj.gameObject.SetActive(false);
            obj.transform.localScale = Vector3.one;

            if (parent)
                obj.transform.SetParent(parent);
        }

        public T Take()
        {
            T freeObject = null;
            
            foreach (var obj in objects)
            {
                if (obj.Value == true)
                {
                    continue;
                }

                freeObject = obj.Key;
                break;
            }

            if (freeObject != null)
            {
                ActivateObject(freeObject);

                return freeObject;
            }

            T newObject = CreateObject(true);

            ActivateObject(newObject);

            return newObject;
        }
        public void Return(T obj)
        {
            DeactivateObject(obj);

            OnReturned?.Invoke(obj);
        }
        public void ReturnAll()
        {
            foreach (var obj in objects)
                Return(obj.Key);
        }

        public void ClearPool()
        {
            ReturnAll();

            for (int i = objects.Count - 1; i >= 0; i--)
                Object.Destroy(objects.ElementAt(i).Key.gameObject);

            objects.Clear();
        }
    }
}
