using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;

public class GameOverPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private GameObject successPanel, failPanel;
    [SerializeField] private TextMeshProUGUI scoreText, bestScoreText, levelText;
    [SerializeField] private int stageNum = 0, levelNum = 0;
    [SerializeField] private Transform stageLiquidTransform;
    [SerializeField] private Button nextLevelButton, tryAgainButton, sellButton;

    #endregion
    #region Private Variables
    private UIData _data;
    private int _highScore;
    private bool _isLiquidAnimationDone = false;
    #endregion
    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _data = GetData();

    }
    private UIData GetData() => Resources.Load<CD_UI>("Data/CD_UI").Data;
    private void Start()
    {
        
    }

    private void InitializeSaveValues()
    {
        stageNum = SaveSignals.Instance.onGetScore(SaveLoadStates.StageNum, SaveFiles.SaveFile);
        levelNum = SaveSignals.Instance.onGetScore(SaveLoadStates.Level, SaveFiles.SaveFile);
        _highScore = SaveSignals.Instance.onGetScore(SaveLoadStates.Score, SaveFiles.SaveFile);
    }

    public void NextLevelButton()
    {
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
        CoreGameSignals.Instance.onNextLevel?.Invoke();
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);

    }

    public void TryAgainButton()
    {
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);

    }
    

    public async void EnableButtons()
    {
        while (!_isLiquidAnimationDone)
        {
            await Task.Yield();
        }
        nextLevelButton.transform.localScale = Vector3.one;
        tryAgainButton.transform.localScale = Vector3.one;
        _isLiquidAnimationDone = false;
    }

    public void SellButton()
    {
        _isLiquidAnimationDone = true;
        ScoreSignals.Instance.onScoreIncrease?.Invoke(ScoreTypeEnums.Gold, 100);
        stageLiquidTransform.DOScaleY(0, 0.5f);
        sellButton.transform.DOScale(0, 0.5f);

        stageNum = 0;
        SaveSignals.Instance.onSaveScore?.Invoke(stageNum, SaveLoadStates.StageNum, SaveFiles.SaveFile);

    }
    public void OnPlay()
    {
        InitializeSaveValues();
        stageLiquidTransform.localScale = new Vector3(1, stageNum * 1.6f, 1);

        nextLevelButton.transform.localScale = new Vector3(0, 0, 0);
        tryAgainButton.transform.localScale = new Vector3(0, 0, 0);
        sellButton.transform.localScale = new Vector3(0, 0, 0);
    }

    public void OnLevelSuccessFul()
    {
        StageIncrease();

        bestScoreText.text = "BEST " + _highScore.ToString();
        levelText.text = "LEVEL " + levelNum;

        successPanel.SetActive(true);
        failPanel.SetActive(false);
        scoreText.text = ScoreSignals.Instance.onGetScore().ToString();

        int currentScore = int.Parse(scoreText.text);
        if (currentScore >= _highScore)
        {
            SaveSignals.Instance.onSaveScore?.Invoke(currentScore, SaveLoadStates.Score, SaveFiles.SaveFile);
        }
        EnableButtons();
    }

    private void StageIncrease()
    {
        ++stageNum;
        stageLiquidTransform.DOScaleY(stageNum * 1.6f, 0.5f).OnComplete(() => {
            if (stageNum == 3)
            {
                stageNum = 0;
                sellButton.transform.localScale = Vector3.one;
            }
            else
            {
                _isLiquidAnimationDone = true;
            }
        });

        SaveSignals.Instance.onSaveScore?.Invoke(stageNum, SaveLoadStates.StageNum, SaveFiles.SaveFile);
    }

    public void OnLevelFailed()
    {
        bestScoreText.text = "BEST " + _highScore.ToString();
        levelText.text = "LEVEL " + levelNum;
        successPanel.SetActive(false);
        failPanel.SetActive(true);
        scoreText.text = ScoreSignals.Instance.onGetScore().ToString();
        _isLiquidAnimationDone = true;
        EnableButtons();
    }








}
