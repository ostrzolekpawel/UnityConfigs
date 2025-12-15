namespace Osiris.Configs
{
    public interface IConfig<in TType, out TData>
    {
        TData Default { get; }
        TData GetData(TType type);
    }
}