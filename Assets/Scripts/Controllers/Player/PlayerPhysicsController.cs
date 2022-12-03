using UnityEngine;
using Signals;
using Enums;
using Managers;
using Data.ValueObject;
using System;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private Rigidbody rig;
        [SerializeField] private PlayerManager manager;
        #endregion
        #region Private Variables
        private PlayerData _data;
        private bool _isEnteredNew = false;
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Cylinder"))
            {
                PlayerSignals.Instance.onPlayerCollideWithCylinder?.Invoke(true);
            }
            else if (other.CompareTag("Obstacle"))
            {
                CoreGameSignals.Instance.onLevelFailed?.Invoke();
            }
            else if (other.CompareTag("CollectableBlocks"))
            {
                PlayerSignals.Instance.onPlayerInteractedWithCollectable?.Invoke();
            }
            else if (other.CompareTag("Finish"))
            {
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Cylinder"))
            {
                PlayerSignals.Instance.onPlayerCollideWithCylinder?.Invoke(false);
            }
            
        }
    }
}