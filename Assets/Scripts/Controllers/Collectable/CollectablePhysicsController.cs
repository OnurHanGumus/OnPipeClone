using UnityEngine;
using Signals;
using Enums;
using Managers;
using Data.ValueObject;
using Data.UnityObject;
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
        private CollectableData _data;
        private Vector3 _initPos;
        private Vector3 _initRot;

        private bool _isReleased = true;

        #endregion
        #endregion
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rig = GetComponent<Rigidbody>();
            _data = GetData();
        }
        private CollectableData GetData() => Resources.Load<CD_Collectable>("Data/CD_Collectable").Data;

        private void OnTriggerEnter(Collider other)
        {
            if (!_isReleased)
            {
                return;
            }

            if (other.CompareTag("Player"))
            {
                _initPos = transform.localPosition;
                _initRot = transform.localEulerAngles;

                _rig.useGravity = true;
                _rig.AddRelativeForce(new Vector3(0, 0, _data.FallForwardForce), ForceMode.Impulse);
                _rig.AddRelativeTorque(_data.MaksRotation, ForceMode.Impulse); 
                _isReleased = false;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("CollectableDeactivator"))
            {
                _rig.useGravity = false;
                _rig.velocity = Vector3.zero;
                _rig.angularVelocity = Vector3.zero;
                transform.localPosition = _initPos;
                transform.localEulerAngles = _initRot;
                _isReleased = true;

            }

        }
    }
}