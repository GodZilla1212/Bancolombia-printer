namespace Bancolombia.data
{
    using System;
    using System.Collections;
    using DG.Tweening;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using UnityEngine;

    public class GameManager : MonoBehaviour {

        [Header("Transition Settings")]
        [Space(5)]
        [SerializeField]
        private GameObject m_CanvasTransition;

        [SerializeField]
        private Image m_Transition;

        [SerializeField]
        private Ease m_TransitionEase;

        [NonSerialized]
        public static GameManager m_GM;

        /// <summary>
        /// Ui info
        /// </summary>
        [NonSerialized]
        private Button m_ContinueButton;

        /// <summary>
        /// User data
        /// </summary>
        [NonSerialized]
        private string m_Email;

        [NonSerialized]
        private string[] m_Answers = new string[11];

        /// <summary>
        /// InputField Control
        /// </summary>
        [NonSerialized]
        private bool m_EnableToAtt;

        [NonSerialized]
        private bool m_EnableToDot;

        [NonSerialized]
        private int m_Tries = 0;

        private void InizializedSetup() {
            m_Email = null;
            m_EnableToAtt = false;
            m_EnableToDot = false;
            for (int i = 0; i < m_Answers.Length; i++) {
                m_Answers[i] = null;
            }
            if (ContinueButton == null) {
                Debug.Log("Dont worry, you be in other scene");
            }
            else {
                ContinueButton.interactable = false;
            }
        }

        public void ValidationEmail(string email) {

            m_Email = email;

            m_EnableToAtt = false;
            m_EnableToDot = false;
            ContinueButton.interactable = false;

            for (int i = 0; i < m_Email.Length; i++) {
                if (m_Email[i] == '@') {
                    m_EnableToAtt = true;
                }
                if (m_Email[i] == '.') {
                    m_EnableToDot = true;
                }
                if (m_EnableToAtt && m_EnableToDot) {
                    ContinueButton.interactable = true;
                }
            }
        }

        public void NextScene(string NameScene) {

            m_CanvasTransition.SetActive(true);
            m_Transition.DOFade(1, 2).SetEase(m_TransitionEase).OnComplete(() => SceneManager.LoadScene(NameScene));
            SceneManager.sceneLoaded += OnSceneLoaded;

        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            Debug.Log("OnSceneLoaded: " + scene.name);
            m_Transition.DOFade(0, 2).SetEase(m_TransitionEase)
                .OnComplete(() => m_CanvasTransition.SetActive(false));
            //if (scene.name == "Level") {
            //    GameObject.Find("LevelManager")
            //        .GetComponent<LevelManager>().Initlevel();
            //}
        }

        public static void CorrectAnswers(int enumAnswer, string Answer) {
            m_GM.m_Answers[enumAnswer] = Answer;
            print(m_GM.m_Answers[enumAnswer]);
        }

        public static void IncorrectAnswer() {
            m_GM.m_Tries++;
            print("Te equibocaste, te queda una oportunidad mas");
        }

        public void StartGameplay() {
        }

        public void ResetAllGame() {
            InizializedSetup();
        }

        private void Awake() {
            if (GameManager.m_GM == null) {
                GameManager.m_GM = this;
                DontDestroyOnLoad(gameObject);
                DOTween.Init();
                InizializedSetup();
            }
            else {
                Destroy(gameObject);
            }
        }

        private void Start() {

            m_CanvasTransition.SetActive(true);
            m_Transition.DOFade(0, 2).SetEase(m_TransitionEase).OnComplete(()=> m_CanvasTransition.SetActive(false));

        }

        public Button ContinueButton {
            get {
                if (GameObject.Find("ValidateButton_inputField") == null) {
                    return null;
                }
                else {
                    return m_ContinueButton = GameObject.Find("ValidateButton_inputField").
                        GetComponent<Button>();
                }
            }
        }

    }
}
