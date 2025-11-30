using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class InventoryController : MonoBehaviour
{
    [Header("Grid Settings")]
    public int rows = 5;
    public int columns = 5;
    public Vector2 cellSize = new Vector2(64f, 64f);
    public Vector2 spacing = new Vector2(8f, 8f);
    public GameObject player;

    [Header("References")]
    public GameObject slotPrefab; // UI slot prefab (RectTransform)
    public RectTransform inventoryParent; // панель в Canvas
    public GameObject inventoryUI; // Панель інвентаря

    // Внутрішні структури



    void Start()
    {
        CreateGrid();
    }

 private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

        Vector3 origin = player != null ? player.transform.position : transform.position;
        Vector3 dir = player != null ? player.transform.forward : transform.forward;
        Ray ray = new Ray(origin, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000f))
        {
            ItemScript itemPickup = hit.collider.GetComponent<ItemScript>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.DrawLine(origin, hit.point, Color.red, 1000f);
                if (itemPickup != null)
                {
                    AddItemToInventory(itemPickup.item);
                }
            }
        }
    }


    void CreateGrid()
    {


        // Очистити наявні діти (якщо повторний виклик)
        for (int i = inventoryParent.childCount - 1; i >= 0; i--)
            Destroy(inventoryParent.GetChild(i).gameObject);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                GameObject slotGO = Instantiate(slotPrefab, inventoryParent);
                slotGO.name = $"Slot_{r}_{c}";
                RectTransform rt = slotGO.GetComponent<RectTransform>();

                // Встановлюємо anchors/pivot для простого позиціонування
                rt.anchorMin = rt.anchorMax = new Vector2(0f, 1f);
                rt.pivot = new Vector2(0f, 1f);

                Vector2 pos = GetPosition(r, c);
                rt.anchoredPosition = pos;
                rt.sizeDelta = cellSize;
            }
        }
    }

    Vector2 GetPosition(int row, int col)
    {
        float x = col * (cellSize.x + spacing.x);
        float y = -row * (cellSize.y + spacing.y);
        return new Vector2(x, y);
    }

    void AddItemToInventory(GameObject item)
    {
        for (int r = 0; r < rows * columns; r++) 
        { 
            PictureScript picScript = inventoryParent.GetChild(r).GetComponent<PictureScript>();
            if (!picScript.fool)
            {
                Image img = inventoryParent.GetChild(r).GetComponent<Image>();
                Sprite itemSprite = item.GetComponent<ItemScript>().icon;
                img.sprite = itemSprite;
                picScript.fool = true;
                item.transform.SetParent(inventoryParent.GetChild(r));
                item.SetActive(false);
                Destroy(item);
                break;
            }
        }
    }
    public GameObject FindItem(string Name)
    {
        for (int r = 0; r < rows * columns; r++)
        {
            PictureScript picScript = inventoryParent.GetChild(r).GetComponent<PictureScript>();
            if (picScript.fool)
            {
                Transform itemTransform = inventoryParent.GetChild(r).GetChild(0);
                if (itemTransform.name == Name)
                {
                    return itemTransform.gameObject;
                }
            }
        }
        return null;
    }
}