using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	public Animator          dialogueBoxAnimator;
	public Text              dialogueText;
	public Text              npcName;
	public DialogueTrigger[] oneTimeDialogues;

	private Queue<string> sentences;

	private void Awake()
	{
		if (SystemVariables.playerData.oneTimeDialogues == null)
			SystemVariables.playerData.oneTimeDialogues = new Dictionary<Dialogue, bool>();
		foreach (DialogueTrigger d in oneTimeDialogues)
			if (!SystemVariables.playerData.oneTimeDialogues.ContainsKey(d.dialogue))
				SystemVariables.playerData.oneTimeDialogues.Add(d.dialogue, false);
	}

	private void Start()
	{
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue d)
	{
		dialogueBoxAnimator.SetBool("isOpen", true);

		npcName.text = d.NPCName;

		sentences.Clear();

		foreach (string s in d.sentances) sentences.Enqueue(s);

		DisplayNextLine();
	}

	public void DisplayNextLine()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}


		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(displayText(sentence));
	}

	private IEnumerator displayText(string sentence)
	{
		dialogueText.text = "";
		foreach (char c in sentence)
		{
			dialogueText.text += c;
			yield return null;
		}
	}

	public void EndDialogue()
	{
		sentences.Clear();
		dialogueBoxAnimator.SetBool("isOpen", false);
	}
}