using System;

namespace Data
{
    /// <summary>
    /// Prefixes: Resource 110, Stuff 220, Tool 330, Skill 440, Building 550
    /// </summary>
    public static class GameItemTypes
    {
        public static Enum getEnum(int id)
        {
            // Преобразуем ID в строку
            string idString = id.ToString();

            // Извлекаем первые 3 символа
            string prefixString = idString.Substring(0, 3);

            // Преобразуем префикс в число
            int prefix = int.Parse(prefixString);

            // Проверка на соответствие префиксу и возврат нужного enum
            return prefix switch
            {
                110 => (EResourceItem)id,
                220 => (EStuffItem)id,
                330 => (EToolItem)id,
                440 => (ESkillItem)id,
                550 => (EBuildingItem)id,
                _ => throw new ArgumentException($"Invalid ID: {id}")
            };
        }

        public static string GetEnumName(int id)
        {
            string idString = id.ToString();

            // Извлекаем первые 3 символа
            string prefixString = idString.Substring(0, 3);

            // Преобразуем префикс в число
            int prefix = int.Parse(prefixString);

            return prefix switch
            {
                110 => Enum.GetName(typeof(EResourceItem), id),
                220 => Enum.GetName(typeof(EStuffItem), id),
                330 => Enum.GetName(typeof(EToolItem), id),
                440 => Enum.GetName(typeof(ESkillItem), id),
                550 => Enum.GetName(typeof(EBuildingItem), id),
                _ => throw new ArgumentException($"Invalid ID: {id}")
            };
        }

        public enum EResourceItem
        {
            // Resources prefix "110"
            NotSet = 0,
            WoodenStick = 1101,
            CooperOre = 1102,
        }

        public enum EStuffItem
        {
            // Stuff prefix "220"
            NotSet = 0,
            WoodenBox = 2201,
        }

        public enum EToolItem
        {
            // Tools prefix "330"
            NotSet = 0,
            Axe = 3301,
            Pickaxe = 3302,
        }

        public enum ESkillItem
        {
            // Skills prefix "440"
            NotSet = 0,
            Gathering = 4401,
            Mining = 4402,
        }


        public enum EBuildingItem
        {
            // Buildings prefix "550"
            NotSet = 0,
            Shelter = 5501,
        }
    }
}
