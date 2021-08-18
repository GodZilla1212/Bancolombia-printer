
using Bancolombia.data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSingleton : MonoBehaviour{

    public void NextSceneButton() {
        GameManager.m_GM.NextScene("Level");
    }
}
