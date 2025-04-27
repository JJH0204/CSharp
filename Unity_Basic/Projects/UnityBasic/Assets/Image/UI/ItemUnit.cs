using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ItemUnit : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private ItemUnitInfo itemUnitInfo;
    private Action<ItemUnit> removeAction;

    public void SetInit(ItemUnitInfo itemUnitInfo, Action<ItemUnit> removeAction)
    {
        this.itemUnitInfo = itemUnitInfo;
        this.removeAction = removeAction;
        text.text = itemUnitInfo.name;
    }

    public void OnClick_remove()
    {
        Debug.Log($"Item {text.text} removed: {itemUnitInfo.id}");
        // Destroy(gameObject);
        // 부모에 직접 접근해서 삭제할 수 있지만 효과적인 방법은 아님
        // transform.parent.GetComponent<ItemController>().RemoveItem(this);
        this.removeAction?.Invoke(this);        // 콜백 함수를 이용해서 부모에 삭제를 요청함

    }
}
