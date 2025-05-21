using UnityEngine;

public abstract class ManagerBase : MonoBehaviour
{
    #region Shared Methods
    
    protected void DontDestroy<T>() where T : Object
    {
        T[] managers = FindObjectsByType<T>(FindObjectsSortMode.None);

        if (managers.Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public bool IsInstance()
    {
        return true;
    }
    
    #endregion
}
