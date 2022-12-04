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

namespace Managers
{
    public class StorePanelManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

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

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {


        }

        private void UnsubscribeEvents()
        {



        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        public void CloseStoreButton()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StorePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void OnSelectCollectableType(int id)
        {
            LevelSignals.Instance.onChangeCollectableType?.Invoke(id);
        }

        private void OnPlay()
        {
            
        }

        private void OnResetLevel()
        {
            
        }


    }
}