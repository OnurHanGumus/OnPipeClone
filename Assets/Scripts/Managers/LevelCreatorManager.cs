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
        public LevelData GetData() => Resources.Load<CD_Level>("Data/CD_Level").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onCylinderDisapeared += OnCylinderDisapeared;
            CoreGameSignals.Instance.onPlay += OnStartPressed;
            CoreGameSignals.Instance.onRestartLevel += OnResetLevel;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onCylinderDisapeared -= OnCylinderDisapeared;
            CoreGameSignals.Instance.onPlay -= OnStartPressed;
            CoreGameSignals.Instance.onRestartLevel -= OnResetLevel;
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

        private void GetCylinderFromPool()
        {
            GameObject temp = PoolSignals.Instance.onGetObject(PoolEnums.Cylinder);
            float xzScale;

            do
            {
                xzScale = Random.Range(_data.CylinderMinXZScale, _data.CylinderMaxXZScale);
                xzScale = (float)Math.Round(xzScale, 1);
            } while (xzScale == _lastXZScale);

            _lastXZScale = xzScale;

            temp.transform.localScale = new Vector3(xzScale, Random.Range(_data.CylinderMinYScale, _data.CylinderMaxYScale), xzScale);
            temp.transform.position = new Vector3(0, nextCylinderPosY + (temp.transform.localScale.y), 0);

            _lastYScale = temp.transform.localScale.y;
            _lastCylinderPosY = temp.transform.position.y;

            temp.SetActive(true);
            nextCylinderPosY = nextCylinderPosY + (temp.transform.localScale.y * 2);
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
            int rand = Random.Range(0, 5);
            Debug.Log(rand);
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

        private void OnCylinderDisapeared()
        {
            GetCylinderFromPool();
            GetCollectablesFromPool();
            GetObstaclesFromPool();
        }

        private void OnPlay()
        {
        }
        private void OnStartPressed()
        {
            _isStartPressed = true;
        }
        private void OnResetLevel()
        {
            _isStartPressed = false;

        }
    }
}