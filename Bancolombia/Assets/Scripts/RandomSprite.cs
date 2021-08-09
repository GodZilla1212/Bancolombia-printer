using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    [SerializeField]
    Sprite[] spriteOptions = new Sprite[5];

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = spriteOptions[Random.Range(0,4)];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
