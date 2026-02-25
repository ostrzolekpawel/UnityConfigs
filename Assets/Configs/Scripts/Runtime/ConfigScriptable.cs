using System;
using System.Collections.Generic;
using UnityEngine;

namespace Osiris.Configs
{
    public abstract class ConfigScriptable<TType, TData> : ScriptableObject, IConfig<TType, TData>
    {
        [SerializeField] protected TData _default;
        [SerializeField] protected List<ConfigElement> _data;

        public virtual TData Default => _default;

        public virtual TData GetData(TType type)
        {
            var element = _data?.Find(x => Equals(x.Type, type));

            if (element != null)
                return element.Data;

            return _default;
        }

        protected virtual void OnValidate()
        {
            if (_data.Count > 1)
            {
                foreach (var view in _data)
                {
                    var count = _data.FindAll(x => x.Type.Equals(view.Type)).Count;
                    if (count > 1)
                    {
                        throw new ArgumentException($"An item with the same key ({view.Type}) has already been added.");
                    }
                }
            }
        }

        [Serializable]
        public class ConfigElement
        {
            [SerializeField] private TType _type;
            [SerializeField] private TData _data;

            public TType Type => _type;
            public TData Data => _data;
        }
    }
}