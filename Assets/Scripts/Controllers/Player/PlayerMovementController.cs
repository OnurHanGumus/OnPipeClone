using System.Collections;
using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using DG.Tweening;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        #endregion
        #region Private Variables
        private Rigidbody _rig;
        private PlayerManager _manager;
        private PlayerData _data;

        private bool _isClicked = false;
        private bool _isNotStarted = false;


        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rig = GetComponent<Rigidbody>();
            _manager = GetComponent<PlayerManager>();
            _data = _manager.GetData();
        }


        private void FixedUpdate()
        {

            if (_isNotStarted)
            {
                return;
            }
            _rig.velocity = new Vector3(0, _data.SpeedY, 0);
            //if (_isClicked)
            //{
            //    _rig.velocity = Vector3.zero;
            //    _rig.AddForce(new Vector3(_data.ForceX * (_isOnRight ? 1 : -1), _data.ForceY, 0), ForceMode.Impulse);
            //    _isClicked = false;
            //}
        }


        public void OnClicked(int direction)
        {
            _isClicked = true;
        }
        public void OnPlayPressed()
        {
            //transform.DOMoveX(_data.StartPosX, _data.PlayerInitializeAnimDelay).SetEase(Ease.InOutBack);
        }

        public void OnPlay()
        {
            _isNotStarted = false;
            _rig.useGravity = true;
        }

        public void OnReset()
        {
            _isNotStarted = true;
            _rig.useGravity = false;
            transform.position = new Vector3(_data.InitializePosX,_data.InitializePosY);
            _rig.velocity = Vector3.zero;
            _rig.angularVelocity = Vector3.zero;
            _rig.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}