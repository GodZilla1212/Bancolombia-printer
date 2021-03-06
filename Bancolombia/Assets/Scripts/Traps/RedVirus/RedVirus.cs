namespace Bancolombia.Traps
{
    using System;
    using DG.Tweening;
    using UnityEngine;

    public class RedVirus : MonoBehaviour
    {
        [SerializeField]
        private float m_speed = 100;

        [SerializeField]
        private float m_Duration = 1;

        [SerializeField]
        private Ease m_Ease;

        private LevelManager m_LevelManager;
        private PlayerMovement m_PlayerMovement;
        void Start() {
            m_LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            m_PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
            transform.DORotate(Vector3.forward, m_speed)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            print("murio en la trampa");
            m_LevelManager.GameLost();
            m_PlayerMovement.DisableMovement();
            GetComponent<AudioSource>().Play();
        }
    }
}
