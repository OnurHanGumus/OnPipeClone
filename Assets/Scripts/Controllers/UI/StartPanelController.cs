using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;

public class StartPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI goldText;
    #endregion
    #region Private Variables
    private LevelData _data;
    private int _levelId, _stageId, _goldCount;
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
    private void Start()
    {
        GetValues();
        UpdateTexts();
    }
    private void GetValues()
    {
        _goldCount = InitializeValue(SaveLoadStates.Gold);
    }
    private LevelData GetData() => Resources.Load<CD_Level>("Data/CD_Level").Data;

    private int InitializeValue(SaveLoadStates type) => SaveSignals.Instance.onGetScore(type, SaveFiles.SaveFile);

    private void UpdateTexts()
    {
        goldText.text = _goldCount.ToString();
    }

    public void OnSetChangedText(ScoreTypeEnums type, int amount)
    {
        if (type == ScoreTypeEnums.Gold)
        {
            goldText.text = amount.ToString();
        }
    }

    public void OnRestartLevel()
    {
        GetValues();
        UpdateTexts();
    }

    public void OnPlay()
    {
        //UpdateTexts();
    }

}
