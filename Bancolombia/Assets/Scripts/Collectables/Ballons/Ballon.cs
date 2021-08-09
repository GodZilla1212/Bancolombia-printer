namespace Bancolombia{
    using System;
    using Bancolombia.data;
    using DG.Tweening;
    using TMPro;
    using UnityEngine;

    public class Ballon : MonoBehaviour, IComparable<Ballon>{

        [SerializeField]
        private bool m_IsCorrect;

        [SerializeField]
        [Range(0,11)]
        private int m_Answer;

        [SerializeField]
        [TextArea]
        private string m_Answertext;

        [SerializeField]
        private float m_Duration = 1;

        [SerializeField]
        private Ease m_Ease;

        [NonSerialized]
        private TMP_Text m_Text;
        private void Awake() {

            m_Text = transform.GetComponentInChildren<TMP_Text>();
            m_Text.text = m_Answertext;
        }
        void CorrectBallon() {
            GameManager.CorrectAnswers(m_Answer, m_Answertext);
            transform.DOScale(0, m_Duration).SetEase(m_Ease).OnComplete(()=> Destroy(gameObject));
        }
        void IncorrectBallon() {
            GameManager.IncorrectAnswer();
            //transform.DOScale(0, m_Duration).SetEase(m_Ease).OnComplete(() => Destroy(gameObject));
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_IsCorrect) CorrectBallon();
            else IncorrectBallon();
        }

        public void SetAnswer (string a, bool c)
        {
            m_Answertext = a;
            m_IsCorrect = c;
        }

        public int CompareTo(Ballon other)
        {
            return UnityEngine.Random.Range(1 , 6);
        }
    }
}
