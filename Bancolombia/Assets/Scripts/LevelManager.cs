
using System.Collections;
using Bancolombia.data;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour{

    [SerializeField]
    private int m_GameScore;

    [SerializeField]
    private GameObject m_PlayUI;

    [SerializeField]
    private TMP_Text m_TitleIntro;

    [SerializeField]
    private TMP_Text m_ParagraphIntro;

    [SerializeField]
    private TMP_Text[] m_TextUIPlay;

    [SerializeField]
    private Button m_StartGame;

    [SerializeField]
    private TMP_Text m_UiScore;

    [SerializeField]
    private GameObject m_ohNoUi;

    [SerializeField]
    private Button m_ButtonohNo;

    [SerializeField]
    private GameObject m_WrongAnswersUi;

    [SerializeField]
    private GameObject m_BallonAgain_UI;

    [SerializeField]
    private Button m_ButtonBallonAgainTry;

    [SerializeField]
    private GameObject m_BallonUi;

    [SerializeField]
    private GameObject m_BallonWinUi;

    [SerializeField]
    private Button m_ButtonWin;

    [SerializeField]
    private Button m_ButtonBallonTry;

    [SerializeField]
    private Button m_ButtonBallonExit;


    [System.NonSerialized]
    private int m_Score = 10;


    public void Initlevel() {
        print("game is start");
        print(GameManager.m_GM.Tries);
        m_GameScore = 0;
        for (int i = 0; i < m_TextUIPlay.Length; i++) {
            m_TextUIPlay[i].alpha = 0;
        }
        //m_TitleIntro.alpha = 0;
        //m_ParagraphIntro.alpha = 0;
        m_ButtonBallonTry.interactable = false;
        m_StartGame.gameObject.SetActive(false);
        StartCoroutine(StartDealyGamePlay());
    }

    private void Start() {
        Initlevel();
    }

    private IEnumerator StartDealyGamePlay() {
        m_PlayUI.SetActive(true);
        yield return new WaitForSeconds(2);
        for (int i = 0; i < m_TextUIPlay.Length; i++) {
            m_TextUIPlay[i].DOFade(1, 2);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(2);
        m_StartGame.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        m_StartGame.interactable = true;
    }

    public void AddScore() {
        m_GameScore += m_Score;
        m_UiScore.text = m_GameScore.ToString();
    }

    public void GameLost (){
        m_ohNoUi.SetActive(true);
        if (GameManager.m_GM.Tries < 1) {
            
        }
        else {
            //StartCoroutine(DelayScreenLoser());
        }
    }

    public void WorngAnswer(int a, string b) {
        StartCoroutine(DelayScreenLoser(a,b));
    }

    IEnumerator DelayScreenLoser(int a, string b) {
        if (GameManager.m_GM.Tries < 1) {
            print("Menor que");
            GameManager.IncorrectAnswer(a, b);
            m_WrongAnswersUi.SetActive(true);
            yield return new WaitForSeconds(3);
            m_WrongAnswersUi.SetActive(false);
            m_BallonAgain_UI.SetActive(true);
            yield return new WaitForSeconds(2);
            m_ButtonBallonAgainTry.interactable = true;
        }
        else if(GameManager.m_GM.Tries >= 1 ) {
            print("Mayor Que");
            GameManager.IncorrectAnswer(a,b);
            m_WrongAnswersUi.SetActive(true);
            GameManager.m_GM.SendDataToServer();
            yield return new WaitForSeconds(3);
            m_WrongAnswersUi.SetActive(false);
            m_BallonUi.SetActive(true);
            yield return new WaitForSeconds(2);
            m_ButtonBallonTry.interactable = true;
        }
    }

    public void Win() {
        StartCoroutine(DelayScreenWinner());
    }

    IEnumerator DelayScreenWinner() {
        GameManager.m_GM.SendDataToServer();
        m_BallonWinUi.SetActive(true);
        yield return new WaitForSeconds(2);
        m_ButtonWin.interactable = true;
    }

    public void RestartScene() {
        SceneManager.LoadScene("Level");
    }

    public void changeSceneIntro() {
        SceneManager.LoadScene("Validation");
    }
}
