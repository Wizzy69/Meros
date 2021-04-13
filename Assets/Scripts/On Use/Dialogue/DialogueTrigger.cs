using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public                   Dialogue dialogue;
	[SerializeField] private bool     oneTime;

	private void Start()
	{
		if (oneTime)
			if (SystemVariables.playerData.oneTimeDialogues[dialogue] == false)
			{
				TriggerDialogue();
				Debug.LogError("started dialogue");
				oneTime                                               = false;
				SystemVariables.playerData.oneTimeDialogues[dialogue] = true;
			}
	}

	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

	public static void TriggerDialogue(Dialogue d)
	{
		FindObjectOfType<DialogueManager>().StartDialogue(d);
	}
}