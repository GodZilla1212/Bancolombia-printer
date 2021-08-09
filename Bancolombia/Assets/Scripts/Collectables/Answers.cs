using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Answers : MonoBehaviour
{
    public string[] correctAnswers;
    public string[] wrongAnswers;
    public List<Bancolombia.Ballon> globos = new List<Bancolombia.Ballon>();

    private void Awake()
    {
        int listPosition = 0;
        globos.Shuffle(6);
        for (int i = 0; i < globos.Count; i++)
        {
            listPosition++;
            globos[i].SetAnswer(correctAnswers[i], true);
            globos.RemoveAt(i);
        }

        for (int i = 0; i< globos.Count; i++)
        {
            globos[i].SetAnswer(wrongAnswers[i], false);
        }
    }
}
