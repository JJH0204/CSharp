using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SystemManager : ManagerBase
{
    public bool IsInit { get; set; } = false;

    private void Awake()
    {
        DontDestroy<SystemManager>();
    }

    public void SetInit()
    {
        IsInit = true;
    }
}
