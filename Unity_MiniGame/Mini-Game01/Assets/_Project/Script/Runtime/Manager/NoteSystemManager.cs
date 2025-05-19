using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSystemManager : ManagerBase
{
    #region Singleton
    private static NoteSystemManager instance = null;

    public static NoteSystemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NoteSystemManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("NoteSystemManager");
                    instance = obj.AddComponent<NoteSystemManager>();
                }
            }
            return instance;
        }
    }
    #endregion
}
