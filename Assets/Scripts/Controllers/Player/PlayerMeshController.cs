using UnityEngine;
using Signals;
using Enums;
using Managers;
using Data.ValueObject;
using System;

namespace Controllers
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private PlayerManager manager;

        #endregion
        #region Private Variables
        private PlayerData _data;

        #endregion
        #endregion
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _data = manager.GetData();
        }

        public void OnLevelFailed()
        {
            meshRenderer.enabled = false;
        }

        public void OnRestartLevel()
        {
            meshRenderer.enabled = true;
        }
    }
}