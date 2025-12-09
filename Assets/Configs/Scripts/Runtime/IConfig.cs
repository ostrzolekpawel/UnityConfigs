using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Osiris.Configs
{
    public interface IConfig<in TType, out TData>
    {
        TData Default { get; }
        TData GetData(TType type);
    }
}