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

        private void Awake() {
            m_Spikes_Front = transform.GetChild(1);
            m_Spikes_Back = transform.GetChild(0);
        }
        void Start() {
            Sequence SpikesSequence = DOTween.Sequence();
            SpikesSequence.Append(m_Spikes_Front.DOLocalMoveY(0.1f, m_Duration).SetEase(m_Ease));
            SpikesSequence.Join(m_Spikes_Back.DOLocalMoveY(-0.1f, m_Duration).SetEase(m_Ease));
            SpikesSequence.SetLoops(-1, LoopType.Yoyo);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            print("murio en la trampa");
        }
    }
}
