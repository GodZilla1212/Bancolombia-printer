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
        private string[] m_Answers = new string[12];

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

        public void SendDataToServer() {

            Data data = new Data();

            data.email = m_Email;
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

            StartCoroutine(PostAnswers(data));
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
