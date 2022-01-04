using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public Text titleText;
    public Text dialogText;
    public Text dialogButtonText;
    public Text dialogCount;
    public Queue<Dialog> dialogues = new Queue<Dialog>();

    public Dialog[] debugDialogues;

    private void Start()
    {
        Continue();
    }

    public void AddDebugDialog()
    {
        foreach(Dialog d in debugDialogues)
        {
            AddDialog(d);
        }
    }

    public void AddDialog(Dialog d)
    {
        int index = 0;
        foreach(string line in d.lines)
        {
            Dialog tempDialog = new Dialog();
            tempDialog.lines = new string[1];

            if (d.onlyShowTitleOnFirstLine && index > 0)
            {
                tempDialog.title = "";
            }
            else
            {
                tempDialog.title = d.title;
            }

            tempDialog.lines[0] = line;
            Debug.Log("Added: " + line);
            dialogues.Enqueue(tempDialog);

            index++;
        }

        if (dialogCount)
        {
            dialogCount.text = "Queued Lines: " + dialogues.Count;
        }
    }

    public void Continue()
    {
        if (dialogCount)
        {
            dialogCount.text = "Queued Lines: " + dialogues.Count;
        }

        if (dialogues.Count > 0)
        {
            Dialog d = dialogues.Dequeue();

            titleText.text = d.title;
            dialogText.text = d.lines[0];
        }
        else
        {
            titleText.text = "";
            dialogText.text = "";
        }

        if (dialogText.text == "")
        {
            dialogButtonText.text = "Start Dialog";
        }
        else
        {
            dialogButtonText.text = "Continue";
        }
    }
}
