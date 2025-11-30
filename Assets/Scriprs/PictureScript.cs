using Unity.VisualScripting;
using UnityEngine;

public class PictureScript : MonoBehaviour
{
    public bool fool = false;
    private void OnMouseDown()
    {
        if (fool)
        {
            Transform itemTransform = transform.GetChild(0);
            itemTransform.SetParent(null);
            itemTransform.gameObject.SetActive(true);
            GetComponent<UnityEngine.UI.Image>().sprite = null;
            fool = false;
        }
    }
}

