using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUnitInfo
{
    public readonly int id;
    public readonly string name;

    public ItemUnitInfo(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}
public class ItemController : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    private List<ItemUnit> itemUnits = new List<ItemUnit>();
    private List<ItemUnitInfo> itemUnitInfos = new List<ItemUnitInfo>();    // 정보
    void Start()
    {
        itemUnitInfos.Add(new ItemUnitInfo(1, "Sword"));
        itemUnitInfos.Add(new ItemUnitInfo(2, "Shield"));
        itemUnitInfos.Add(new ItemUnitInfo(3, "Potion"));
        itemUnitInfos.Add(new ItemUnitInfo(4, "Bow"));
        itemUnitInfos.Add(new ItemUnitInfo(5, "Arrow"));
        itemUnitInfos.Add(new ItemUnitInfo(6, "Helmet"));
        itemUnitInfos.Add(new ItemUnitInfo(7, "Armor"));
        itemUnitInfos.Add(new ItemUnitInfo(8, "Boots"));
        itemUnitInfos.Add(new ItemUnitInfo(9, "Ring"));
        itemUnitInfos.Add(new ItemUnitInfo(10, "Amulet"));

        foreach (var unit in itemUnitInfos)
        {
            GameObject obj = Instantiate(itemPrefab, transform);
            ItemUnit itemUnit = obj.GetComponent<ItemUnit>();
            itemUnit.SetInit(unit, RemoveItem);
            itemUnits.Add(itemUnit);
        }
    }

    public void RemoveItem(ItemUnit itemUnit)
    {
        itemUnits.Remove(itemUnit);
        Destroy(itemUnit.gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
