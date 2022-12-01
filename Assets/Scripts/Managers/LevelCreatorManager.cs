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

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
        }
        public PlayerData GetData() => Resources.Load<CD_Player>("Data/CD_Player").Data;

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
            for (int i = 0; i < 5; i++)
            {
                GetCylinderFromPool();

            }
        }

        private void GetCylinderFromPool()
        {
            GameObject temp = PoolSignals.Instance.onGetObject(PoolEnums.Cylinder);
            temp.transform.localScale = new Vector3(1, Random.Range(1, 4), 1);

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