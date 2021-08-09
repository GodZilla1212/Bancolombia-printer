using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShuffleAnswers
{
    public static void Shuffle<t>(this List<t> list, int shuffleValue)
    {
        for (int i = 0; i < shuffleValue; i++)
        {
            int randomIndex = Random.Range(1, list.Count);
            t temp = list[randomIndex];
            list[randomIndex] = list[0];
            list[0] = temp;
        }
    }
}
