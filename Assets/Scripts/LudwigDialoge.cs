using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LudwigDialoge : MonoBehaviour
{
    public Text textField;
    public GameObject eKeyPrompt;

    public int dialogGroupID = 0;

    private List<string> dialog = new List<string>();

    private bool inProximity = false;
    private bool inDialog = false;
    
    private int dialogID = 0;

    private void Start()
    {
        switch(dialogGroupID)
        {
            case 0:
                dialog.Add("oh hey, slime.\ni havent seen you in a while.\nhow are you doing?");
                dialog.Add("Great... great.\nanyways it seems you will need to\nclimb to the top to get out of here.");
                dialog.Add("you still got that massive dynamite supply\nfrom my last sponsor, right?\ndo you remember how to use it?");
                dialog.Add("just tap left click to place TNT\nor hold left click to throw TNT.\nuse the explosion to launch you through the air.");
                dialog.Add("press 'q' to look up and 'space' to jump.\nif you ever forget you can always\nlook it up by pressing 'ESC'.");
                dialog.Add("and remember this:\njumping right before detonation\nsignificantly increases velocity gain!");
                break;
            case 1:
                dialog.Add("Slime...");
                dialog.Add("I'm here.");
                dialog.Add("I'm waiting for you.");
                break;
            case 2:
                dialog.Add("Slime!");
                dialog.Add("Come back, Slime!");
                dialog.Add("You need to come back to me!");
                dialog.Add("Don't you remember?");
                break;
            case 3:
                dialog.Add("Remember, Slime.");
                dialog.Add("Do you remember what happened to me?");
                dialog.Add("Do you remember what happned to you?");
                break;
            case 4:
                dialog.Add("It wasn't your fault, Slime.");
                dialog.Add("I need you, Slime.");
                dialog.Add("I'm hurt.");
                break;
            case 5:
                dialog.Add("This is it, Slime.");
                dialog.Add("I'm losing too much blood.");
                dialog.Add("See you soon.");
                break;
        }
    }

    private void Update()
    {
        if(inProximity && Input.GetKeyDown(Settings.accept))
        {
            if (inDialog && dialogID + 1 < dialog.Count)
            {
                textField.text = dialog[++dialogID];
            }
            else if(inDialog)
            {
                endDialog();
            }
            else if(!inDialog)
            {
                startDialog();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inProximity = true;
        eKeyPrompt.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inProximity = false;
        eKeyPrompt.SetActive(false);
    }

    private void startDialog()
    {
        MenuManager.current.gotoMenu(4);
        inDialog = true;
        textField.text = dialog[dialogID];
        GameMemory.current.paused = true;
    }

    private void endDialog()
    {
        MenuManager.current.gotoMenu(0);
        inDialog = false;
        dialogID = 0;
        GameMemory.current.paused = false;
    }
}
