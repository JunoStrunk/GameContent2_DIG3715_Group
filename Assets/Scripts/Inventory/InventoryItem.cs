using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    [SerializeField]
    public GameObject item;

    [SerializeField]
    public string itemName;

    [SerializeField]
    public Sprite itemSprite;

    [SerializeField]
    public Color color;

    //Constructor
    public void SetValues(GameObject item, string itemName, Sprite itemTexture, Color color)
    {
        this.item = item;
        this.itemName = itemName;
        this.itemSprite = itemTexture;
        this.color = color;
    }
}
