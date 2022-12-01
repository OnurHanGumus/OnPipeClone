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
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onCylinderDisapeared -= OnCylinderDisapeared;
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

            }
        }

        private void GetCylinderFromPool()
        {
            GameObject temp = PoolSignals.Instance.onGetObject(PoolEnums.Cylinder);
            float xzScale = Random.Range(_data.CylinderMinXZScale, _data.CylinderMaxXZScale);
            xzScale = (float) Math.Round(xzScale, 1);
            Debug.Log(xzScale);
            temp.transform.localScale = new Vector3(xzScale, Random.Range(_data.CylinderMinYScale, _data.CylinderMaxYScale), xzScale);

            temp.transform.position = new Vector3(0, nextCylinderPosY + (temp.transform.localScale.y), 0);
            temp.SetActive(true);
            nextCylinderPosY = nextCylinderPosY + (temp.transform.localScale.y * 2);
        }

        private void OnCylinderDisapeared()
        {
            GetCylinderFromPool();
        }

        private void OnPlay()
        {
        }
        private void OnResetLevel()
        {
        }
    }
}