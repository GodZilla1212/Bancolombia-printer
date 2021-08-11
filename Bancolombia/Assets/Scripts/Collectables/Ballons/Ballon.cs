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

        private LevelManager m_LevelManager;
        private PlayerMovement m_PlayerMovement;
        private void Awake() {

            m_Text = transform.GetComponentInChildren<TMP_Text>();
            m_Text.text = m_Answertext;
        }

        private void Start()
        {
            m_LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            m_PlayerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }
        void CorrectBallon() {
            GameManager.CorrectAnswers(m_Answer, m_Answertext);
            m_LevelManager.AddScore();
            transform.DOScale(0, m_Duration).SetEase(m_Ease).OnComplete(()=> Destroy(gameObject));
        }
        void IncorrectBallon() {
            GameManager.IncorrectAnswer();
            m_PlayerMovement.DisableMovement();
            //transform.DOScale(0, m_Duration).SetEase(m_Ease).OnComplete(() => Destroy(gameObject));
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            m_Text.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GetComponent<AudioSource>().Play();
            if (m_IsCorrect) CorrectBallon();
            else IncorrectBallon();
        }

        public void SetAnswer (string a, bool c)
        {
            m_Answertext = a;
            m_Text.text = a;
            m_IsCorrect = c;
        }

        public int CompareTo(Ballon other)
        {
            return UnityEngine.Random.Range(1 , 6);
        }
    }
}
