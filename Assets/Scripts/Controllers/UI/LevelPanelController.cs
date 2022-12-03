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

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI scoreText;
    #endregion
    #region Private Variables
    private LevelData _data;
    private int _score;
    private bool _isDrinkScoreComplated = false;

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
    private LevelData GetData() => Resources.Load<CD_Level>("Data/CD_Level").Data;
    
    public void OnPlayerInteractedWithCollectable()
    {
        _score += _data.PlayerDrinkScoreIncreaseValue;
        scoreText.text = _score.ToString() + "/100";
        if (_isDrinkScoreComplated)
        {
            return;
        }
        if (_score >= _data.PlayerDrinkScoreMaksValue)
        {
            LevelSignals.Instance.onDrinkScoreComplated?.Invoke();
            _isDrinkScoreComplated = true;
        }
    }

    public void OnRestartLevel()
    {
        _isDrinkScoreComplated = false;
        _score = 0;
        scoreText.text = _score.ToString() + "/100";
    }


}
