using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public Inventory inventory;
    private int itemCount;
    public GameObject icon;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        itemCount = inventory.inventory.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.inventory.Count > itemCount) AddItem(inventory.inventory[itemCount]);
    }

    private void AddItem(Item item)
    {
        GameObject itemDisplay = Instantiate(icon, transform.GetChild(0));
        itemDisplay.GetComponent<RawImage>().texture = item.img;
        itemDisplay.GetComponent<RectTransform>().anchoredPosition -= Vector2.up * 100 * (itemCount-2.5f);
        itemCount++;
    }
}
