using UnityEngine;

namespace Db
{
    public abstract class DataBase<TData> : ScriptableObject, IDataBase
    {
        public TData data;
        public abstract string ConfigName { get; }
    }
}
