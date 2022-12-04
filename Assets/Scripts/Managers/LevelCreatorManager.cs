using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class LevelCreatorManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        #endregion
        #region Serialized Variables
        [SerializeField] private float nextCylinderPosY = 0f;
        #endregion
        #region Private Variables
        private LevelData _data;
        private float _lastXZScale = 0, _lastYScale = 0;
        private float _lastCylinderPosY = 0;
        private bool _isStartPressed = false;
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
            nextCylinderPosY = _data.InitialCylinderPos;
        }
        public LevelData GetData() => Resources.Load<CD_Level>("Data/CD_Level").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onCylinderDisapeared += OnCylinderDisapeared;
            LevelSignals.Instance.onDrinkScoreComplated += OnDrinkValueComplated;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onRestartLevel += OnResetLevel;
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onCylinderDisapeared -= OnCylinderDisapeared;
            LevelSignals.Instance.onDrinkScoreComplated -= OnDrinkValueComplated;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onRestartLevel -= OnResetLevel;
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            InitializeLevel();
        }

        private void InitializeLevel()
        {
            for (int i = 0; i < _data.InitializeCylinderCount; i++)
            {
                GetCylinderFromPool();
                GetCollectablesFromPool();
                GetObstaclesFromPool();
            }
        }

        private void GetCylinderFromPool() //if cylinder scale = 1, then its y is 2 br.
        {
            GameObject cylinder = PoolSignals.Instance.onGetObject(PoolEnums.Cylinder);
            float cylinderXZScale;

            do
            {
                cylinderXZScale = Random.Range(_data.CylinderMinXZScale, _data.CylinderMaxXZScale);
                cylinderXZScale = (float)Math.Round(cylinderXZScale, 1);
            } while (cylinderXZScale == _lastXZScale);

            _lastXZScale = cylinderXZScale;

            cylinder.transform.localScale = new Vector3(cylinderXZScale, Random.Range(_data.CylinderMinYScale, _data.CylinderMaxYScale), cylinderXZScale);
            cylinder.transform.position = new Vector3(0, nextCylinderPosY + (cylinder.transform.localScale.y), 0);

            _lastYScale = cylinder.transform.localScale.y;
            _lastCylinderPosY = cylinder.transform.position.y;

            nextCylinderPosY = nextCylinderPosY + (cylinder.transform.localScale.y * 2);
            cylinder.SetActive(true);
        }

        private void GetCollectablesFromPool()
        {
            for (int i = 0; i < (_lastYScale / _lastXZScale) * _data.RowWeight; i++)
            {
                GameObject temp = PoolSignals.Instance.onGetObject(PoolEnums.Collectable);
                temp.transform.position = new Vector3(0, (_lastCylinderPosY - (_lastYScale/2)) + (((float)i * _data.DistanceBetweenBlocks))*_lastXZScale, 0);
                temp.transform.localScale = new Vector3(_lastXZScale, _lastXZScale, _lastXZScale);
                temp.SetActive(true);
            }
        }

        private void GetObstaclesFromPool()
        {
            int rand = Random.Range(0, _data.ObstacleProbablity);
            GameObject temp;
            if (rand == 0)
            {
                temp = PoolSignals.Instance.onGetObject(PoolEnums.Obstacle);
            }
            else if (rand == 1 &&_isStartPressed)
            {
                temp = PoolSignals.Instance.onGetObject(PoolEnums.ObstacleLarge);
            }
            else
            {
                return;
            }
            temp.transform.position = new Vector3(0, (_lastCylinderPosY - (_lastYScale)), 0);
            temp.SetActive(true);
        }

        private void GetFinishObjectFromPool()
        {
            GameObject temp = PoolSignals.Instance.onGetObject(PoolEnums.FinishObject);
            temp.transform.position = new Vector3(0, nextCylinderPosY + (temp.transform.localScale.y), 0);
            temp.SetActive(true);
        }

        private void OnPlay()
        {
            _isStartPressed = true;
        }
        private void OnLevelInitialize()
        {
            InitializeLevel();
        }
        private void OnCylinderDisapeared()
        {
            if (_isDrinkScoreComplated)
            {
                return;
            }
            GetCylinderFromPool();
            GetCollectablesFromPool();
            GetObstaclesFromPool();
        }
        private void OnDrinkValueComplated()
        {
            _isDrinkScoreComplated = true;
            GetFinishObjectFromPool();
        }
        private void OnResetLevel()
        {
            _isStartPressed = false;
            nextCylinderPosY = _data.InitialCylinderPos;
            _isDrinkScoreComplated = false;
        }
    }
}