using System;
using System.Collections.Generic;

namespace Osiris.Configs
{
    public interface IConfig<in TType, TData>
    {
        TData Default { get; }
        TData GetData(TType type);
        IReadOnlyList<TData> GetData(Func<TData, bool> predicate);
    }
}
