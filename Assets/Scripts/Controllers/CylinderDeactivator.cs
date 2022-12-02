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
            other.transform.parent.gameObject.SetActive(false);
        }
        else if (other.CompareTag("CollectableBlocks"))
        {
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Obstacle"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
