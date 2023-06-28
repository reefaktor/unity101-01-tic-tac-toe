using System.Collections.Generic;
using UnityEngine;

public enum PlayerMarking
{
    One, Two, Empty
}

public class GameGrid
{
    public PlayerMarking[] Slots;
    
    public GameGrid()
    {
        Slots = new PlayerMarking[9];
        for (var i = 0; i < Slots.Length; i++)
        {
            Slots[i] = PlayerMarking.Empty;
        }
    }

    public bool IsFull()
    {
        foreach (var cell in Slots)
        {
            if (cell == PlayerMarking.Empty)
                return false;
        }

        return true;
    }

    public int FindRandomPosition()
    {
        var freeIndices = new List<int>();
        
        for (var i = 0; i < Slots.Length; i++)
        {
            if (Slots[i] != PlayerMarking.Empty) continue;
            
            freeIndices.Add(i);
        }

        if (freeIndices.Count == 0) return -1;

        var r = Random.Range(0, freeIndices.Count - 1);
        return freeIndices[r];
    }
}
