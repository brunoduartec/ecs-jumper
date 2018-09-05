using System.Collections;
using System.Collections.Generic;

public static class LevelGeneratorConfig
{
    public enum ITEMTYPE
    {
        SOLID,
        MOVEABLE,
        BREAKEABLE,
        ENEMY,
        ITEM
    };

    public class ItemProperty
    {
        public ItemProperty(string entityName, ITEMTYPE type, float probability)
        {
            this.entityName = entityName;
            this.type = type;
            this.probability = probability;
        }
        public string entityName;
        public ITEMTYPE type;
        public float probability;

    }

    public static List<ItemProperty> getItemProperties()
    {
        List<ItemProperty> itemProperties = new List<ItemProperty>();

        itemProperties.Add(new ItemProperty("block", ITEMTYPE.SOLID, .4f));
        itemProperties.Add(new ItemProperty("breakeable", ITEMTYPE.BREAKEABLE, .5f));

        return itemProperties;
    }


}