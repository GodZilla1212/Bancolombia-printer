
using Bancolombia.data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ValidationManager : MonoBehaviour
{

    private void Start() {

        InizializedSetup();

    }
    public void InizializedSetup() {
        GameManager.m_GM.Email = "";
        GameManager.m_GM.Question = "";
        GameManager.m_GM.Tries = 0;
        m_EnableToAtt = false;
        m_EnableToDot = false;

        string[] currentStrings = new string[12];
        for (int i = 0; i < currentStrings.Length; i++) {
            currentStrings[i] = "";
        }
        GameManager.m_GM.Answers = currentStrings;
        m_ContinueButton.interactable = false;

    }

    public void ValidationEmail(string email) {

        m_currentEmail = email;
        m_EnableToAtt = false;
        m_EnableToDot = false;
        m_ContinueButton.interactable = false;
        for (int i = 0; i < m_currentEmail.Length; i++) {
            if (m_currentEmail[i] == '@') {
                m_EnableToAtt = true;
            }
            if (m_currentEmail[i] == '.') {
                m_EnableToDot = true;
            }
            if (m_EnableToAtt && m_EnableToDot) {
                m_ContinueButton.interactable = true;
            }
        }
    }

    public void ConfirmEmail() {
        GameManager.m_GM.Email = m_currentEmail;
    }

    public void ValidationQuestion(string question) {

        m_currentQuestion = question;
        m_ContinueButton.interactable = false;
        if (m_currentQuestion.Length > 6) {
            m_QuestionContinueButton.interactable = true;
        }
        else {
            m_QuestionContinueButton.interactable = false;
        }
    }

    public void ConfirmQuestion() {
        GameManager.m_GM.Question = m_currentQuestion;
    }

    [SerializeField]
    private string m_currentEmail;

    [SerializeField]
    private string m_currentQuestion;

    [SerializeField]
    private Button m_ContinueButton;

    [SerializeField]
    private Button m_QuestionContinueButton;

    [System.NonSerialized]
    private bool m_EnableToAtt = false;

    [System.NonSerialized]
    private bool m_EnableToDot = false;

}
