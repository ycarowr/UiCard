using System.Collections.Generic;
using Patterns;
using UnityEngine;

public class PrefabPooler : SingletonMB<PrefabPooler>
{
    //--------------------------------------------------------------------------------------------------------------

    #region Fields

    //StatesRegister of the already pooled objects
    readonly Dictionary<GameObject, List<GameObject>> busyObjects =
        new Dictionary<GameObject, List<GameObject>>();

    //StatesRegister of the pooled available objects
    readonly Dictionary<GameObject, List<GameObject>> poolAbleObjects =
        new Dictionary<GameObject, List<GameObject>>();

    [Tooltip("How many objects will be created as soon as the game loads")] [SerializeField]
    readonly int startSize = 10;

    [Tooltip("All pooled models have to be inside this array before the initialization")] [SerializeField]
    GameObject[] modelsPooled;

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Initialization

    /// <summary>
    ///     I am initializing it as soon as possible. You can move it to Awake or Start calls. It's up to you.
    /// </summary>
    void OnEnable()
    {
        //avoiding execution when the game isn't playing
        if (!Application.isPlaying)
            return;

        //initialize the pool system
        Initialize();
    }

    /// <summary>
    ///     Here is the initialization of the pooler. All the models/prefabs which you need to pool have to be inside
    ///     the modelPooled array. They will be keys for the Lists inside the pool system.
    /// </summary>
    void Initialize()
    {
        foreach (var model in modelsPooled)
        {
            //list for pool
            var pool = new List<GameObject>();

            //list for busy
            var busy = new List<GameObject>();

            //create the initial amount and add them to the pool
            for (var i = 0; i < startSize; i++)
            {
                var obj = Instantiate(model, transform);
                pool.Add(obj);
                obj.SetActive(false);
            }

            poolAbleObjects.Add(model, pool);
            busyObjects.Add(model, busy);
        }
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Operations

    /// <summary>
    ///     Here you can pool the prefab objects. Currently the key is a reference to the prefab that you need to get.
    ///     Although I haven't had problems with this approach, you can come up with a solution that performs better
    ///     using an enumeration as key.
    /// </summary>
    /// <param name="prefabModel"></param>
    /// <returns></returns>
    public GameObject Get(GameObject prefabModel)
    {
        GameObject pooledObj = null;

        if (poolAbleObjects == null)
            Debug.LogError("Nop! PoolAble objects list is not created yet!");

        if (busyObjects == null)
            Debug.LogError("Nop! Busy objects list is not created yet!");

        //if prefabModel is not contained inside the StatesRegister
        if (!poolAbleObjects.ContainsKey(prefabModel))
            return null;

        //try to grab the last element of the available objects
        if (poolAbleObjects[prefabModel].Count > 0)
        {
            var size = poolAbleObjects[prefabModel].Count;
            pooledObj = poolAbleObjects[prefabModel][size - 1];
        }

        if (pooledObj != null)
            poolAbleObjects[prefabModel].Remove(pooledObj);
        else
            pooledObj = Instantiate(prefabModel, transform);

        //add the pooled object to the used objects list
        busyObjects[prefabModel].Add(pooledObj);

        pooledObj.SetActive(true);
        OnPool(pooledObj);
        return pooledObj;
    }

    /// <summary>
    ///     Here you pool back objects that you no longer use. They are deactivated and
    ///     stored back for future usage using the prefab model as key to get it back later on.
    /// </summary>
    /// <param name="prefabModel"></param>
    /// <param name="pooledObj"></param>
    public void ReleasePooledObject(GameObject prefabModel, GameObject pooledObj)
    {
        if (poolAbleObjects == null)
            Debug.LogError("Nop! PoolAble objects list is not created yet!");

        if (busyObjects == null)
            Debug.LogError("Nop! Busy objects list is not created yet!");

        pooledObj.SetActive(false);
        busyObjects[prefabModel].Remove(pooledObj);
        poolAbleObjects[prefabModel].Add(pooledObj);
        pooledObj.transform.parent = transform;
        pooledObj.transform.localPosition = Vector3.zero;
        OnRelease(pooledObj);
    }

    /// <summary>
    ///     Here you pool back objects that you no longer use. They are deactivated and
    ///     stored back for future usage using the prefab model as key to get it back later on.
    /// </summary>
    /// <param name="pooledObj"></param>
    public void ReleasePooledObject(GameObject pooledObj)
    {
        if (poolAbleObjects == null)
            Debug.LogError("Nop! PoolAble objects list is not created yet!");

        if (busyObjects == null)
            Debug.LogError("Nop! Busy objects list is not created yet!");

        pooledObj.SetActive(false);

        foreach (var model in busyObjects.Keys)
            if (busyObjects[model].Contains(pooledObj))
            {
                busyObjects[model].Remove(pooledObj);
                poolAbleObjects[model].Add(pooledObj);
            }

        pooledObj.transform.parent = transform;
        pooledObj.transform.localPosition = Vector3.zero;
        OnRelease(pooledObj);
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Events

    /// <summary>
    /// </summary>
    /// <param name="prefabModel"></param>
    void OnPool(GameObject prefabModel)
    {
        // If you need to execute some code right BEFORE the object is pooled, you can do it here.
        // Clean references or reset variables are very common cases.
    }

    /// <summary>
    /// </summary>
    /// <param name="prefabModel"></param>
    void OnRelease(GameObject prefabModel)
    {
        // If you need to execute some code right AFTER the object is released, you can do it here.
        // Clean references or reset variables are very common cases.
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------
}