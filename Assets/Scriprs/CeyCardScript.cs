using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CeyCardScript : MonoBehaviour
{
    public GameObject door;
    public string requiredCardID;
    public KeyCode interactKey = KeyCode.F;
    public InventoryController IC;


    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            GameObject CC = IC.FindItem("CayCard");
        }
    }
}