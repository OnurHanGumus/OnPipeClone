using System.Collections;
using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using DG.Tweening;
using System;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private Transform playerTransform;
        #endregion
        #region Private Variables
        private Rigidbody _rig;
        private PlayerManager _manager;
        private PlayerData _data;

        private bool _isClicked = false;
        private bool _isMinimizable = true;

        private bool _isNotStarted = false;
        private bool _isGameOver = false;



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
            MovePlayer();
            ScalePlayer();
        }

        private void MovePlayer()
        {
            if (_isNotStarted)
            {
                return;
            }

            if (_isGameOver)
            {
                if (_rig.velocity.y <= 0)
                {
                    _rig.velocity = Vector3.zero;
                    return;
                }
                _rig.velocity -= new Vector3(0, 0.02f * (1/_data.FailedSlowValue), 0);
            }
            else
            {
                _rig.velocity = new Vector3(0, _data.SpeedY, 0);
            }
        }

        private void ScalePlayer()
        {
            if (_isClicked)
            {
                if (!_isMinimizable)
                {
                    return;
                }
                float currentScale = playerTransform.localScale.x;
                float newScale = currentScale - (0.02f * (1 / _data.SmallingTime));
                playerTransform.localScale = new Vector3(newScale, newScale, newScale);
            }
            else
            {
                playerTransform.localScale = new Vector3(1f, 1f, 1f);
                _isMinimizable = true;
            }
        }


        public void OnClicked(bool clickState)
        {
            _isClicked = clickState;
        }

        public void OnReleased()
        {
            _isClicked = false;
        }
        public void OnPlayPressed()
        {
            _isNotStarted = false;
        }

        public void OnPlay()
        {
            //_isNotStarted = false;

        }

        public void OnCollideCylinder(bool isTrue)
        {
            _isMinimizable = !isTrue;
        }
        public void OnLevelFailed()
        {
            _isGameOver = true;
        }
        public void OnLevelSuccess()
        {
            _isGameOver = true;
        }
        public void OnReset()
        {
            //_isNotStarted = true;
            _isGameOver = false;
            transform.position = new Vector3(_data.InitializePosX,_data.InitializePosY);
        }
    }
}