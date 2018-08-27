using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class LevelGenerator
{
    public int minX, maxX, minY, maxY, maxHeight;

    public int blockSize = 1;

    private EntityManager _entityManager;

    public LevelGenerator(int minX, int maxX, int minY, int maxY, int maxHeight)
    {
        this.minX = minX;
        this.maxX = maxX;

        this.minY = minY;
        this.maxY = maxY;

        this.maxHeight = maxHeight;

        this._entityManager = World.Active.GetExistingManager<EntityManager>();
    }

    private void addBlock(float3 blockPosition)
    {
        Entity block = EntityFactory.Instance.createEntityByName("block");
        this._entityManager.SetComponentData(block, new Position { Value = blockPosition });
    }

    public void buildLevel()
    {
        int size = 6;
        for (int i = 0; i < maxX; i++)
        {
            addBlock(new float3(i * size, i * size, 0));
        }

        // int vectorSize = maxX - minX;
        // int[] possibilityVector = new int[vectorSize];

        // for (int i = 0; i < vectorSize; i++)
        // {
        //     possibilityVector[i] = 0;
        // }

        // int currentY = minY;

        // bool checkLineSanity = true;

        // while (currentY < maxY)
        // {
        //     int height = maxHeight;//Random.Range(1, maxHeight);

        //     if (checkLineSanity)
        //     {
        //         currentY += height;
        //         checkLineSanity = false;
        //     }
        //     for (int i = 0; i < possibilityVector.Length; i = i + blockSize)
        //     {
        //         if (possibilityVector[i] > 0)
        //         {
        //             int subtractor = height > 0 ? (height - 1) : 1;

        //             for (int j = 0; j < blockSize; j++)
        //             {
        //                 possibilityVector[i + j] -= subtractor;
        //             }
        //         }
        //         else
        //         {
        //             bool chance = (UnityEngine.Random.value > 0.5f);

        //             if (chance)
        //             {
        //                 for (int j = 0; j < blockSize; j++)
        //                 {
        //                     possibilityVector[i] = maxHeight;

        //                     float3 blockPosition = new float3((float)(minX + i + j), (float)(currentY), 0.0f);

        //                     addBlock(blockPosition);
        //                 }

        //                 checkLineSanity = true;
        //             }
        //         }
        //     }
        // }

    }
}
