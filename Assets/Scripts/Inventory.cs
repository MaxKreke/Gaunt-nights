using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            inventory.Add(other.gameObject.GetComponent<Item>());
            other.gameObject.SetActive(false);
        }
    }

}
