using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;

public class CylinderDeactivator : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables



    #endregion
    #region Private Variables
    #endregion
    #endregion

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cylinder"))
        {
            LevelSignals.Instance.onCylinderDisapeared?.Invoke();
            other.gameObject.SetActive(false);
        }
    }
}
