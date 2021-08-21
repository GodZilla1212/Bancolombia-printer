namespace Bancolombia.Traps
{
    using System;
    using DG.Tweening;
    using UnityEngine;

    public class Spikes : MonoBehaviour{

        [SerializeField]
        private float m_Duration = 0.5f;

        [SerializeField]
        private Ease m_Ease;

        [NonSerialized]
        private Transform m_Spikes_Front;

        [NonSerialized]
        private Transform m_Spikes_Back;

        private LevelManager m_LevelManager;
        private PlayerMovement m_PlayerMovement;

        private void Awake() {
            m_Spikes_Front = transform.GetChild(1);
            m_Spikes_Back = transform.GetChild(0);
        }

        void Start() {
            m_LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            m_PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
            Sequence SpikesSequence = DOTween.Sequence();
            SpikesSequence.Append(m_Spikes_Front.DOLocalMoveY(0.1f, m_Duration).SetEase(m_Ease));
            SpikesSequence.Join(m_Spikes_Back.DOLocalMoveY(-0.1f, m_Duration).SetEase(m_Ease));
            SpikesSequence.SetLoops(-1, LoopType.Yoyo);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            print("murio en los picos");
            m_LevelManager.GameLost();
            m_PlayerMovement.DisableMovement();
            GetComponent<AudioSource>().Play();
        }
    }
}
