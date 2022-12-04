using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using Enums;

public class PoolManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private GameObject cylinderPrefab;
    [SerializeField] private GameObject collectablePrefab;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject obstacleLargePrefab;
    [SerializeField] private GameObject finishPrefab;
    [SerializeField] private GameObject particlePrefab;

    [SerializeField] private Dictionary<PoolEnums, List<GameObject>> poolDictionary;


    [SerializeField] private int amountCylinder = 5;
    [SerializeField] private int amountCollectable = 100;
    [SerializeField] private int amountObstacle = 20;
    [SerializeField] private int amountObstacleLarge = 20;
    [SerializeField] private int amountFinishObject = 2;
    [SerializeField] private int amountParticles = 2;



    #endregion
    #region Private Variables
    private int _levelId = 0;
    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _levelId = LevelSignals.Instance.onGetCurrentModdedLevel();
        poolDictionary = new Dictionary<PoolEnums, List<GameObject>>();
        InitializePool(PoolEnums.Cylinder,  cylinderPrefab, amountCylinder);
        InitializePool(PoolEnums.Collectable,  collectablePrefab, amountCollectable);
        InitializePool(PoolEnums.Obstacle,  obstaclePrefab, amountObstacle);
        InitializePool(PoolEnums.ObstacleLarge,  obstacleLargePrefab, amountObstacleLarge);
        InitializePool(PoolEnums.FinishObject,  finishPrefab, amountFinishObject);
        InitializePool(PoolEnums.Particle,  particlePrefab, amountParticles);
    }



    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PoolSignals.Instance.onGetPoolManagerObj += OnGetPoolManagerObj;
        PoolSignals.Instance.onGetObject += OnGetObject;
        CoreGameSignals.Instance.onRestartLevel += OnReset;

    }

    private void UnsubscribeEvents()
    {
        PoolSignals.Instance.onGetPoolManagerObj -= OnGetPoolManagerObj;
        PoolSignals.Instance.onGetObject -= OnGetObject;
        CoreGameSignals.Instance.onRestartLevel -= OnReset;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void InitializePool(PoolEnums type, GameObject prefab, int size)
    {
        List<GameObject> tempList = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < size; i++)
        {
            tmp = Instantiate(prefab, transform);
            tmp.SetActive(false);
            tempList.Add(tmp);
        }
        poolDictionary.Add(type, tempList);
    }

    public GameObject OnGetObject(PoolEnums type)
    {
        for (int i = 0; i < poolDictionary[type].Count; i++)
        {
            if (!poolDictionary[type][i].activeInHierarchy)
            {
                return poolDictionary[type][i];
            }
        }
        return null;
    }

    public Transform OnGetPoolManagerObj()
    {
        return transform;
    }


    private void OnReset()
    {
        //reset
        ResetPool(PoolEnums.Collectable);
        ResetPool(PoolEnums.Cylinder);
        ResetPool(PoolEnums.Obstacle);
        ResetPool(PoolEnums.ObstacleLarge);
        ResetPool(PoolEnums.FinishObject);
        ResetPool(PoolEnums.Particle);
    }

    private void ResetPool(PoolEnums type)
    {
        foreach (var i in poolDictionary[type])
        {
            i.SetActive(false);
        }
    }
}
