namespace Bancolombia.data
{
    using System;
    using System.Collections;
    using DG.Tweening;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using UnityEngine.Networking;
    using UnityEngine;

    [Serializable]
    public class Data {

        public string SaveToString() {
            return JsonUtility.ToJson(this);
        }

        public string email = "";
        public string initial_question = "";
        public string question_1 = "";
        public string question_2 = "";
        public string question_3 = "";
        public string question_4 = "";
        public string question_5 = "";
        public string question_6 = "";
        public string question_7 = "";
        public string question_8 = "";
        public string question_9 = "";
        public string question_10 = "";
        public string question_11 = "";
        public string question_12 = "";
    }
    public class GameManager : MonoBehaviour {

        #region Pirvate Fields

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

        [NonSerialized]
        private string m_Email;

        [NonReorderable]
        private string m_InitialQuestion;

        [NonSerialized]
        private string[] m_Answers = new string[12];

        [NonSerialized]
        private int m_Tries = 0;

        #endregion

        public void NextScene(string NameScene) {

            m_CanvasTransition.SetActive(true);
            m_Transition.DOFade(1, 2).SetEase(m_TransitionEase).OnComplete(() => SceneManager.LoadScene(NameScene));
            SceneManager.sceneLoaded += OnSceneLoaded;

        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            Debug.Log("OnSceneLoaded: " + scene.name);
            m_Transition.DOFade(0, 2).SetEase(m_TransitionEase)
                .OnComplete(() => m_CanvasTransition.SetActive(false));
        }

        public static void CorrectAnswers(int enumAnswer, string Answer) {
            m_GM.m_Answers[enumAnswer] = Answer;
            print("Correct answer " + m_GM.m_Answers[enumAnswer]);
        }

        public static void IncorrectAnswer(int enumAnswer, string Answer) {
            m_GM.m_Answers[enumAnswer] = Answer;
            print("Incorret answer " + m_GM.m_Answers[enumAnswer]);
            m_GM.m_Tries++;
        }

        public void SendDataToServer() {

            Data data = new Data();

            data.email = m_Email;
            data.initial_question = m_InitialQuestion;
            data.question_1 = m_Answers[0];
            data.question_2 = m_Answers[1];
            data.question_3 = m_Answers[2];
            data.question_4 = m_Answers[3];
            data.question_5 = m_Answers[4];
            data.question_6 = m_Answers[5];
            data.question_7 = m_Answers[6];
            data.question_8 = m_Answers[7];
            data.question_9 = m_Answers[8];
            data.question_10 = m_Answers[9];
            data.question_11 = m_Answers[10];
            data.question_12 = m_Answers[11];

            print("Este es el data");
            //StartCoroutine(PostAnswers(data));
        }

        IEnumerator PostAnswers(Data data) {

            string url = "https://game-api-answer.herokuapp.com/register/info";
            var request = new UnityWebRequest(url,"POST");

            request.SetRequestHeader("Content-Type", "application/json");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(data.SaveToString());
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            yield return request.SendWebRequest();
            var response = request.result;
            if (response == UnityWebRequest.Result.Success) {
                print("¡All perfect!");
            }
            else {
                print("Error 404");
            }
        }

        private void Awake() {
            if (GameManager.m_GM == null) {
                GameManager.m_GM = this;
                DontDestroyOnLoad(gameObject);
                DOTween.Init();
            }
            else {
                Destroy(gameObject);
            }
        }

        private void Start() {

            m_CanvasTransition.SetActive(true);
            m_Transition.DOFade(0, 2).SetEase(m_TransitionEase).OnComplete(()=> m_CanvasTransition.SetActive(false));

        }

        public string Email {
            get{
                return m_Email;
            }
            set {
                m_Email = value;
                Debug.Log("the email has been saved " + m_Email);
            }
        }

        public string Question {
            get {
                return m_InitialQuestion;
            }
            set {
                m_InitialQuestion = value;
                Debug.Log("the answer of initial question has been saved " + m_InitialQuestion);
            }
        }

        public string[] Answers {
            get {
                return m_Answers;
            }
            set {
                m_Answers = value;
            }
        }

        public int Tries {
            get {
                return m_Tries;
            }
            set {
                m_Tries = value;
            }
        }

    }
}
