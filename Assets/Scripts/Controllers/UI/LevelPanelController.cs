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
using UnityEngine.UI;

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI drinkScoreText;
    [SerializeField] private TextMeshProUGUI levelText, scoreText;
    [SerializeField] private Image filledSquareRenderer;
    #endregion
    #region Private Variables
    private LevelData _data;
    private int _drinkScore;
    private int _levelId, _score;
    private bool _isDrinkScoreComplated = false;
    

    #endregion
    #endregion
    private void Awake()
    {
        Init();
        UpdateLevelText();

    }
    private void Init()
    {
        _data = GetData();
        _levelId = GetLevelId();
    }
    private LevelData GetData() => Resources.Load<CD_Level>("Data/CD_Level").Data;
    private int GetLevelId() => SaveSignals.Instance.onGetScore(SaveLoadStates.Level, SaveFiles.SaveFile);

    private void UpdateLevelText()
    {
        levelText.text = "LEVEL " + (_levelId + 1);

    }

    public void SetRadialFilletAmount()
    {
        float filletAmount = 360 - _drinkScore * 3.6f;
        filledSquareRenderer.material./*SetFloat("_Arc2", filletAmount);*/ DOFloat(filletAmount, "_Arc2", 0.2f);
    }

    public void OnPlayerInteractedWithCollectable()
    {
        _drinkScore += _data.PlayerDrinkScoreIncreaseValue;
        drinkScoreText.text = _drinkScore.ToString() + "/100";
        SetRadialFilletAmount();

        if (_isDrinkScoreComplated)
        {
            return;
        }
        if (_drinkScore >= _data.PlayerDrinkScoreMaksValue)
        {
            LevelSignals.Instance.onDrinkScoreComplated?.Invoke();
            _isDrinkScoreComplated = true;
        }
    }


    public void OnSetChangedText(ScoreTypeEnums scoreType, int value)
    {
        if (scoreType == ScoreTypeEnums.Score)
        {
            _score += _data.PlayerScoreIncreaseValue;
            scoreText.text = _score.ToString();
        }
    }
    
    public void OnNextLevel()
    {
        ++_levelId;
        UpdateLevelText();
    }


    public void OnRestartLevel()
    {
        _isDrinkScoreComplated = false;
        _drinkScore = 0;
        drinkScoreText.text = _drinkScore.ToString() + "/100";
        _score = 0;
        scoreText.text = _score.ToString();
        filledSquareRenderer.material.SetFloat("_Arc2", 360);
    }
}