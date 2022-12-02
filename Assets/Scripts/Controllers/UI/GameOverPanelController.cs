using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Data.UnityObject;
using Data.ValueObject;

public class GameOverPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private GameObject successPanel, failPanel;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private int stageNum = 0, levelNum = 0;

    #endregion
    #region Private Variables
    private UIData _data;
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
        InitializeStageNum();
    }

    private void InitializeStageNum()
    {
        stageNum = SaveSignals.Instance.onGetScore(SaveLoadStates.StageNum, SaveFiles.SaveFile);
        levelNum = SaveSignals.Instance.onGetScore(SaveLoadStates.Level, SaveFiles.SaveFile);
    }

    public void CloseGameOverPanel()
    {
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
    }

    public void MenuBtn()
    {
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);

    }

   

    public void OnStageSuccessFul()
    {
        ++stageNum;

        SaveSignals.Instance.onSaveScore?.Invoke(stageNum, SaveLoadStates.StageNum, SaveFiles.SaveFile);

    }

  

    public void OnStageFailed()
    {
        stageNum = 0;
    }


}
