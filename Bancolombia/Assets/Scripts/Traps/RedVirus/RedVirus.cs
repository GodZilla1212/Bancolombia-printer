namespace Bancolombia.Traps
{
    using System;
    using DG.Tweening;
    using UnityEngine;

    public class RedVirus : MonoBehaviour
    {
        [SerializeField]
        private float m_speed = 100;

        void Start() {

            transform.DORotate(Vector3.forward, m_speed)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            print("murio en la trampa");
        }
    }
}
