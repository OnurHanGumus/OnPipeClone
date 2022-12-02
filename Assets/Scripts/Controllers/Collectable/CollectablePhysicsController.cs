using UnityEngine;
using Signals;
using Enums;
using Managers;
using Data.ValueObject;
using System;

namespace Controllers
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        #endregion
        #region Private Variables
        private Rigidbody _rig;

        #endregion
        #endregion
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rig = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _rig.useGravity = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("CollectableDeactivator"))
            {
                _rig.useGravity = false;
                _rig.velocity = Vector3.zero;
                transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
            }

        }
    }
}