using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    [SerializeField]
    public string itemName;

    [SerializeField]
    public Sprite itemSprite;

    //Constructor
    public void SetValues(string itemName, Sprite itemTexture)
    {
        this.itemName = itemName;
        this.itemSprite = itemTexture;
    }
}
