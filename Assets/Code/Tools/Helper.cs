using System;

namespace Code.Tools
{
    public static class Helper
    {
        public static bool CheckOnNull(object obj, string objName, string name)
        {
            if (obj == null) throw new NullReferenceException($"{objName} is null. {name}");

            return true;
        }
    }
}
