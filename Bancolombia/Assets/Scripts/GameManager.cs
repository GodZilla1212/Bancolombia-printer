namespace Bancolombia.data
{
    using System;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using UnityEngine;

    public class GameManager : MonoBehaviour {

        [Header("UI Settings")]
        [Space(5)]
        [SerializeField]
        private Button m_ContinueButton;

        [NonSerialized]
        private static GameManager m_GM;

        /// <summary>
        /// User data
        /// </summary>
        [NonSerialized]
        private string m_Email;

        [NonSerialized]
        private string[] m_Answers;

        /// <summary>
        /// InputField Control
        /// </summary>
        [NonSerialized]
        private bool m_EnableToAtt;

        [NonSerialized]
        private bool m_EnableToDot;

        public void ValidationEmail(string email) {

            m_Email = email;

            m_EnableToAtt = false;
            m_EnableToDot = false;
            m_ContinueButton.interactable = false;

            for (int i = 0; i < m_Email.Length; i++) {
                if (m_Email[i] == '@') {
                    m_EnableToAtt = true;
                }
                if (m_Email[i] == '.') {
                    m_EnableToDot = true;
                }
                if (m_EnableToAtt && m_EnableToDot) {
                    m_ContinueButton.interactable = true;
                }
            }
        }

        public void NextScene(string NameScene) {

            print("Loading scene " + NameScene);
            SceneManager.LoadScene(NameScene);

        }

        private void InizializedSetup() {
            m_Email = null;
            m_EnableToAtt = false;
            m_EnableToDot = false;
            m_ContinueButton.interactable = false;
        }

        private void Awake() {
            if (GameManager.m_GM == null) {
                GameManager.m_GM = this;
                DontDestroyOnLoad(gameObject);
                InizializedSetup();
            }
            else {
                Destroy(gameObject);
            }
        }

    }
}
