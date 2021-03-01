using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    private int count = -1;
    [SerializeField]
    public TextMeshProUGUI txt;
    public GameObject disableTutorialText;
    public GameObject enablePlacementCircle;
    public GameObject continueArrow;
    public GameObject beaconindicator;
    public static TutorialText singleton;
    private bool blinking = true;
    private void Awake()
    {
        singleton = this;
    }

    private void Update()
    {
        if(blinking)
        StartCoroutine(Blink());
    }
    IEnumerator Blink()
    {
        blinking = false;
        continueArrow.SetActive(!continueArrow.activeSelf);
        yield return new WaitForSeconds(0.7f);
        blinking = true;
    }

    public void ChangeText()
    {
        count++;
        if (count == 0)
        {
            txt.text = "See those white buttons with an icon? Try clicking one!";
        }
        if (count == 1)//and player is holding tower
        {
            txt.text = "You're now holding a tower. Drop it at the indicated circle!(This costs Power!)";
            enablePlacementCircle.SetActive(true);
        }
        if (count == 2)//and player has clicked on tower(upgrademenu is open)
        {
            txt.text = "Now click on the tower you just dropped.";
            enablePlacementCircle.SetActive(false);

        }
        if (count == 3)
            txt.text = "You can upgrade the tower through this menu. Try upgrading something!";
        if (count == 4)
            txt.text = "Now, press the send button! (you can also click the X to close the menu!)";
        if (count == 5)
        {
            txt.text = "Your towers only hit enemies of the same color as themselves. When you kill them, they release some color into the world!.";
        }
        if (count == 6)
            txt.text = "Next wave is green enemies. Did I mention their name yet? These bouncing balls are called shades.";
        if (count == 7)
            txt.text = "Anyway, you need to change the color of your tower!";
        if (count == 8)
        {
            txt.text = "Let's go back to the upgrade menu!";
        }
        if (count == 9)
        {
            txt.text = "Try changing the Hue! You can click, or press '` (tilde key)', 1, 2 or 3 on the keyboard!";
        }
        if (count == 10)
        {
        }
        if (count == 11)
            txt.text = "If you die, you'll have to restart from the beginning! Watch your 'light' carefully!";
        if (count == 12)
            txt.text = "You can also press P to pause! There's a handy menu there too.";
        if (count == 13)
            txt.text = "In case it's in the way, the tower menu can be hidden by pressing H or the X, try it!";
        if (count == 14)
        {
            txt.text = "You can win the game by clearing all the waves, or lighting up beacons. They cost 250 power each! Remember that!";
            beaconindicator.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (count == 15)
            txt.text = "Good luck! Light the world back up! Get the colour back from those shades!";
        if (count == 16)
        {
            disableTutorialText.SetActive(false);
        }
    }
    
}
