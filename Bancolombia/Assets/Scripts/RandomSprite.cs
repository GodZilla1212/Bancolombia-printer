using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    [SerializeField]
    Sprite[] spriteOptions = new Sprite[5];

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = spriteOptions[Random.Range(0,4)];
    }
}
