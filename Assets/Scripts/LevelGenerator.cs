using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.InteropServices;

using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class LevelGenerator
{
    public int minX, maxX, minY, maxY, maxDistanceBetweenRows, size, maxItemsByColumn;

    private List<LevelGeneratorConfig.ItemProperty> itemProperties;
    public int itemSize = 1;

    public LevelGenerator(int minX, int maxX, int minY, int maxY, int maxDistanceBetweenRows, int maxItemsByColumn, int itemSize, int size)
    {
        this.minX = minX;
        this.maxX = maxX;

        this.minY = minY;
        this.maxY = maxY;

        this.size = size;

        this.maxItemsByColumn = maxItemsByColumn;

        this.maxDistanceBetweenRows = maxDistanceBetweenRows;
        this.itemSize = itemSize;

        this.itemProperties = LevelGeneratorConfig.getItemProperties();

        this.itemProperties.Sort(delegate (LevelGeneratorConfig.ItemProperty x, LevelGeneratorConfig.ItemProperty y)
        {
            return x.probability.CompareTo(y.probability);
        });
    }

    public struct Item
    {
        public float3 position;
        public LevelGeneratorConfig.ItemProperty itemProperty;
    };

    public LevelGeneratorConfig.ItemProperty getItem()
    {
        foreach (LevelGeneratorConfig.ItemProperty item in itemProperties)
        {
            int probability = (int)(100 * item.probability);
            int prob = UnityEngine.Random.Range(0, 100);
            if (prob <= probability)
                return item;
        }

        return itemProperties[0];
    }


    public List<Item> buildItems()
    {
        List<Item> items = new List<Item>();

        int currentY = minY;

        while (currentY < maxY)
        {
            int distance = UnityEngine.Random.Range(2, this.maxDistanceBetweenRows);
            currentY += (distance * size);

            int currentX = minX;

            int itemsPlaced = 0;

            while (currentX < maxX && itemsPlaced < this.maxItemsByColumn)
            {
                bool canInsertBlock = (UnityEngine.Random.Range(0, 10.0f) > 5) && (currentX + itemSize * size < maxX);
                if (canInsertBlock)
                {
                    LevelGeneratorConfig.ItemProperty itemProperty = getItem();
                    for (int i = 0; i < itemSize; i++)
                    {
                        float3 position = new float3(currentX, currentY, 0);
                        Item item = new Item();
                        item.itemProperty = itemProperty;
                        item.position = position;

                        items.Add(item);
                        itemsPlaced++;
                        currentX += size;
                    }
                }
                currentX += size;
            }

        }
        return items;
    }
}
