using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : MonoBehaviour
{
    int count = 0;
    GameObject inventory;
    void addTool(GameObject Object, Image toolimage)
    {
        count++;
        Object.transform.SetParent(inventory.transform);
        toolimage.transform.position = new Vector3(0, 1, 0);
    }
    Vector3 Getposition()
    {
        return new Vector3(0, count * 50, 0);
    }
}
