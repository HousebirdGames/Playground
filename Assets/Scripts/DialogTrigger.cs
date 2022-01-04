using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogController dialogController;
    [Space(10)]
    public string fillBlankTitlesWith;
    public Dialog[] dialogues;

    private void OnEnable()
    {
        if (!dialogController)
        {
            dialogController = FindObjectOfType<DialogController>();
            Debug.LogWarning("DialogController needed for DialogTrigger. It was automatically added.");
        }
    }

    public void TriggerDialog(int dialogIndex)
    {
        CheckTitle();

        if (dialogIndex < dialogues.Length && dialogIndex >= 0)
        {
            dialogController.AddDialog(dialogues[dialogIndex]);
        }
        else
        {
            Debug.LogWarning("No dialogue with index of " + dialogIndex + " found.");
        }
    }

    public void TriggerDialog(string dialogName)
    {
        CheckTitle();

        foreach (Dialog d in dialogues)
        {
            if(d.dialogName == dialogName)
            {
                dialogController.AddDialog(d);
            }
        }
    }

    public void TriggerAllDialogues()
    {
        CheckTitle();

        foreach (Dialog d in dialogues)
        {
            dialogController.AddDialog(d);
        }
    }

    private void CheckTitle()
    {
        foreach(Dialog d in dialogues)
        {
            if (d.title == "" && fillBlankTitlesWith != "")
            {
                d.title = fillBlankTitlesWith;
            }
        }
    }
}
