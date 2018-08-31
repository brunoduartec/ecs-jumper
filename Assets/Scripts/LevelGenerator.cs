using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.InteropServices;

using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class LevelGenerator
{
    public int minX, maxX, minY, maxY, maxDistanceBetweenRows, size;

    public int blockSize = 1;

    public LevelGenerator(int minX, int maxX, int minY, int maxY, int maxDistanceBetweenRows, int blockSize, int size)
    {
        this.minX = minX;
        this.maxX = maxX;

        this.minY = minY;
        this.maxY = maxY;

        this.size = size;

        this.maxDistanceBetweenRows = maxDistanceBetweenRows;
        this.blockSize = blockSize;
    }

    public List<float3> buildLevel()
    {
        List<float3> level = new List<float3>();

        int currentY = minY;

        while (currentY < maxY)
        {
            currentY += (maxDistanceBetweenRows * size);

            int currentX = minX;

            while (currentX < maxX)
            {
                bool canInsertBlock = (UnityEngine.Random.Range(0, 10.0f) > 5) && (currentX + blockSize * size < maxX);
                if (canInsertBlock)
                {
                    for (int i = 0; i < blockSize; i++)
                    {
                        float3 blockPosition = new float3(currentX, currentY, 0);
                        level.Add(blockPosition);
                        currentX += size;
                    }
                }
                currentX += size;
            }

        }
        return level;
    }
}
