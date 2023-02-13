using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    [SerializeField]
    public string itemName;

    [SerializeField]
    public Sprite itemSprite;

    [SerializeField]
    public Color color;

    //Constructor
    public void SetValues(string itemName, Sprite itemTexture, Color color)
    {
        this.itemName = itemName;
        this.itemSprite = itemTexture;
        this.color = color;
    }
}
