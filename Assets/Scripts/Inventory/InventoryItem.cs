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

	[SerializeField]
	public DialogueTrigger dialogue;

	TextAsset TextFileAsset;

	//Constructor
	public void SetValues(GameObject item, string itemName, Sprite itemTexture, Color color)
	{
		this.item = item;
		this.dialogue = item.GetComponent<DialogueTrigger>();
		TextFileAsset = dialogue.TextFileAsset;
		this.itemName = itemName;
		this.itemSprite = itemTexture;
		this.color = color;
	}

	public void ShowDialogue()
    {
		dialogue.TextFileAsset = this.TextFileAsset;
		dialogue.ShowDialogue();
	}
}
