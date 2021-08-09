namespace Bancolombia{
    using System;
    using Bancolombia.data;
    using DG.Tweening;
    using TMPro;
    using UnityEngine;

    public class Ballon : MonoBehaviour{

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
        public void CorrectBallon() {
            GameManager.CorrectAnswers(m_Answer, m_Answertext);
            transform.DOScale(0, m_Duration).SetEase(m_Ease).OnComplete(()=> Destroy(gameObject));
        }
        public void IncorrectBallon() {
            GameManager.IncorrectAnswer();
            transform.DOScale(0, m_Duration).SetEase(m_Ease).OnComplete(() => Destroy(gameObject));
        }
    }
}
