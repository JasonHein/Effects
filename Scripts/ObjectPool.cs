/*
 * By Jason Hein
 * 
 */


using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for objects to inherit from to become poolable.
/// </summary>
public interface Poolable
{
    bool IsAvailable();
}

/// <summary>
/// Singleton object pool system.
/// 
/// Use instance to get the object pooler, CreatePool () to create pools of objects, and GetObject() to grab one from the pool.
/// </summary>
public class ObjectPool : MonoBehaviour {

    /// <summary>
    /// The instance of the object pooler.
    /// </summary>
    public static ObjectPool instance
    {
        get
        {
            return m_Instance;
        }
    }
    static ObjectPool m_Instance;

    // List of pooled objects
    List<List<Poolable>> m_Pool;

    // List of used paths is used to make sure we don't load the same path and amount multiple times.
    List<string> m_UsedPaths;

    // Setup instance and reset the lists on awake.
    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            m_Pool = new List<List<Poolable>>();
            m_UsedPaths = new List<string>();
            DontDestroyOnLoad(gameObject);
        }
        else if(m_Instance != this)
        {
            m_Instance.m_Pool.Clear();
            m_Instance.m_UsedPaths.Clear();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Creates a pool of gameobjects made up of copies of a gameobject in the resources folder.
    /// To be usable, the loaded gameobject must have a script that impliments the Poolable interface and its IsAvailable() function.
    /// The amount provided is the amount you want in the pool.
    /// If you call this function a second time for less than or the same amount as those objects already pooled, nothing will happen.
    /// </summary>
    /// <returns>The index you can use with the GetObject(int index) function to get an available object from this pool. If the load fails, -1 is returned instead.</returns>
    public int CreatePool (string path, int amount)
    {
        if (path == null || path == string.Empty || amount <= 0)
        {
#if UNITY_EDITOR
            Debug.LogError("Creating object pool failed because the path was null, empty, or less than one object was requested. " + path);
#endif
            return -1;
        }

        // Check if this path has already been loaded.
        for (int i = 0; i < m_UsedPaths.Count; ++i)
        {
            if (m_UsedPaths[i] == path)
            {
                // If more of those objects are requested, add that many objects to the pool.
                if (m_Pool[i].Count < amount)
                {
                    AddObjectToPool(path, amount - m_Pool[i].Count, i);
                }

                // Return the index of the pool that already exists.
                return i;
            }
        }

        // Create a new pool and fill it with copies of the loaded object.
        m_UsedPaths.Add(path);
        m_Pool.Add(new List<Poolable>());
        AddObjectToPool(path, amount, m_Pool.Count - 1);

        // Return the index of the newly added pool.
        return m_Pool.Count - 1;
    }

    /// <summary>
    /// Returns an object from a pool at the given index.
    /// You may need to cast the returned component as your intended component type to use it. It is returned as a 'Poolable' object.
    /// If the index is invalid, or no poolable objects are available, null is returned.
    /// </summary>
    /// <returns>A component of type Poolable. You may need to cast this as your intended component type to use it.
    /// If the index is invalid, or no poolable objects are available, null is returned.</returns>
    public Poolable GetObject(int index)
    {
        if (index < 0 || index >= m_Pool.Count)
        {
#if UNITY_EDITOR
            Debug.LogError("Getting pooled object at index " + index + " failed because of an invalid index.");
#endif
            return null;
        }

        for (int i = 0; i < m_Pool[index].Count; ++i)
        {
            if (m_Pool[index][i].IsAvailable())
            {
                return m_Pool[index][i];
            }
        }
        return null;
    }

    /// <summary>
    /// Adds copies of a loaded object to a pool at a given index.
    /// If the path, amount, or index is invalid, or the object doesn't have a Poolable script attached, nothing happens.
    /// </summary>
    void AddObjectToPool(string path, int amount, int index)
    {
        // Load the game object
        GameObject original = (GameObject)Resources.Load(path);
        if (original != null)
        {
            Poolable script;
            GameObject obj;
            for (int i = 0; i < amount; ++i)
            {
                // Create the copies and fill the list with poolable components on those copies.
                obj = Instantiate(original, transform);
                script = obj.GetComponent<Poolable>();
                if (script != null)
                {
                    m_Pool[index].Add(script);
                }
#if UNITY_EDITOR
                else
                {
                    Debug.LogError(original.name + " was unable to be added to the list because it does not have a poolable script attached.");
                    return;
                }
#endif
            }
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogError("Failed to load instance from resources folder at path: " + path);
        }
#endif
    }
}
