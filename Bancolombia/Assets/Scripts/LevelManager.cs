
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
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
    private Button m_StartGame;

    [SerializeField]
    private TMP_Text m_UiScore;

    [SerializeField]
    private GameObject m_ohNoUi;

    [SerializeField]
    private Button m_ButtonohNo;

    int score = 10;

    public void Initlevel() {
        print("game is start");
        m_GameScore = 0;
        m_TitleIntro.alpha = 0;
        m_ParagraphIntro.alpha = 0;
        m_StartGame.gameObject.SetActive(false);
        StartCoroutine(StartDealyGamePlay());
    }

    private void Start() {
        Initlevel();
    }

    private IEnumerator StartDealyGamePlay() {
        m_PlayUI.SetActive(true);
        yield return new WaitForSeconds(2);
        m_TitleIntro.DOFade(1, 2);
        yield return new WaitForSeconds(0.2f);
        m_ParagraphIntro.DOFade(1, 2);
        yield return new WaitForSeconds(2);
        m_StartGame.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        m_StartGame.interactable = true;
    }

    public void AddScore() {
        m_GameScore += score;
        m_UiScore.text = m_GameScore.ToString();
    }

    public void GameLost ()
    {
        //tocó una trampa
        print("levelmanager trampa");
    }

}
