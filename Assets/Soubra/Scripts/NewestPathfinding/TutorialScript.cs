using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public bool active = true;
    public int tutorialStep;
    public int uiStep = -1;
    public float timer;
    public Dialogue dScript;
    public ClickableTile ct;
    public ClickableTile walkTo;
    public ClickableTile attackTo;
    public AsgardianMClass ac;
    public TurnController tcScript;
    public InitiativeRoll irScript;
    public Unit player;
    public Unit enemy;
    public GameObject attackArmor;
    public GameObject attackDamage;

    // Start is called before the first frame update
    void Start()
    {
        dScript = FindObjectOfType<Dialogue>();
        enemy = GetComponent<Unit>();
        ac.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialStep == 0)
        {
            TeachInitiative();
        }
        if (tutorialStep == 1)
        {
            ThrowDice();

            ct = ac.currentNode.neighbours[0].ground.GetComponent<ClickableTile>();
            walkTo = tcScript.tmScript.graph[ct.tileX - 1, ct.tileZ].ground.GetComponent<ClickableTile>();
            attackTo = tcScript.tmScript.graph[ct.tileX, ct.tileZ].ground.GetComponent<ClickableTile>();
            ac.enabled = false;
        }
        if (tutorialStep == 2)
        {
            TeachWalking();
        }
        if (tutorialStep == 3)
        {
            SelectedTile();
        }
        if (tutorialStep == 4)
        {
            FirstAttack();
        }
        if (tutorialStep == 5)
        {
            ExplainAttack();
        }
        if (tutorialStep == 6)
        {
            TryAttack();
        }
        if (tutorialStep == 7)
        {
            DoAttack();
        }        
        if (tutorialStep == 8)
        {
            FixedAttack();
        }        
        if (tutorialStep == 9)
        {
            RollAttackDice();
        }
        if (tutorialStep == 10)
        {

        }
    }

    public void DoDialouge(string[] sentences)
    {
        dScript.done = false;
        dScript.textDisplay.text = ""; //Resets the text to blank

        dScript.sentences = null;
        dScript.sentences = new string[sentences.Length];
        dScript.sentences = sentences;
        dScript.index = 0;
        dScript.panel.SetActive(true);
        dScript.StartCoroutine(dScript.TypeEffect());
        dScript.odinWalk.noWalkie = true;
    }
    public void NextStep()
    {
        tutorialStep++;
    }
    public void TeachInitiative()
    {
        string[] dialouge = new string[5];

        dialouge[0] = "Alright Odin, lets begin with 'Initiative'";
        dialouge[1] = "Before a battle starts everyone must roll initiatives";
        dialouge[2] = "Turns are determined by the initiatives";
        dialouge[3] = "With the highest initiative going first";
        dialouge[4] = "Lets roll!";

        if (active)
        {
            timer = 0;
            DoDialouge(dialouge);
            active = false;
        }

        if (dScript.done)
        {
            Debug.Log("Done");
            timer += Time.deltaTime;
            irScript.spawn = true;

            if (timer > 3)
            {    
                irScript.throwTutorial = true;
                tutorialStep++;
                active = true;
            }
        }
    }
    public void ThrowDice()
    {
        if (player.initiative > 0)
        {
            if (player.initiative > enemy.initiative || player.initiative == enemy.initiative)
            {

                string[] dialouge = new string[2];

                dialouge[0] = "Looks like you got the higher Initiative!";
                dialouge[1] = "This means that you get to go first!";

                if (active)
                {
                    DoDialouge(dialouge);
                    active = false;
                }
                if (dScript.done)
                {
                    player.initiative = enemy.initiative + 1;
                    irScript.ReOrder();
                    tcScript.tmScript.selectedUnit = player.gameObject;
                    irScript.spawn = true;
                    tutorialStep++;
                    active = true;
                }
            }
            else if (enemy.initiative > player.initiative)
            {
                string[] dialouge = new string[2];

                dialouge[0] = "Looks like I got the higher Initiative!";
                dialouge[1] = "Its okay! I'll let you go first!";

                if (active)
                {
                    DoDialouge(dialouge);
                    active = false;
                }

                if (dScript.done)
                {
                    player.initiative = enemy.initiative + 1;
                    irScript.ReOrder();
                    tcScript.tmScript.selectedUnit = player.gameObject;
                    tutorialStep++;
                    active = true;
                }

            }

        }
    }
    public void TeachWalking()
    {
        tcScript.showUI = true;
        string[] dialouge = new string[4];

        dialouge[0] = "Lets go through walking!";
        dialouge[1] = "To walk, you need to select the tile that you want to walk to by clicking on it";
        dialouge[2] = "You have a limited amount of tiles you can walk per turn";
        dialouge[3] = "For now please click the tile right infront of me";

        if (active)
        {
            DoDialouge(dialouge);
            active = false;
        }

        if (dScript.done)
        {
            tutorialStep++;
            active = true;
        }
    }
    public void SelectedTile()
    {
        if (player.targetTile != null && player.targetTile == ct)
        {
            string[] dialouge = new string[5];

            dialouge[0] = "Perfect!";
            dialouge[1] = "A path to the selected Tile will be visualized";
            dialouge[2] = "The green tiles are the tiles you can walk to";
            dialouge[3] = "The red tiles are the tiles that are too far";
            dialouge[4] = "To confirm the tile that you want to walk, click on the same tile!";

            if (active)
            {
                timer = 0;
                DoDialouge(dialouge);
                active = false;
            }

            if (dScript.done)
            {
                tutorialStep++;
                active = true;
            }
        }
        else if(player.targetTile != null && player.targetTile != ct)
        {
            string[] dialouge = new string[1];

            dialouge[0] = "Odin focus! Please click on the tile infront of me.";
            player.targetTile = null;

            if (active)
            {
                timer = 0;
                DoDialouge(dialouge);
                player.targetTile = null;
                active = false;
            }
            if (dScript.done)
            {
                player.targetTile = null;
                active = true;
            }
        }
    }
    public void FirstAttack()
    {
        if (player.currentNode.ground.GetComponent<ClickableTile>() == walkTo)
        {
            string[] dialouge = new string[2];

            dialouge[0] = "Now its time to attack!";
            dialouge[1] = "Please click on your first attack button!";

            if (active)
            {
                player.targetTile = null;
                DoDialouge(dialouge);
                active = false;
            }
            if (dScript.done)
            {
                uiStep++;
                tutorialStep++;
                active = true;
            }
        }
    }
    public void ExplainAttack()
    {
        if (player.attackMode)
        {
            string[] dialouge = new string[3];

            dialouge[0] = "Clicking on any of the four squares connected to you will display what tiles the attack will hit.";
            dialouge[1] = "For now please click the cell between you and I!";
            dialouge[2] = "It will show the affected tiles, when ready click it again to confirm the attack!";

            if (active)
            {
                DoDialouge(dialouge);
                active = false;
            }
            if (dScript.done)
            {
                tutorialStep++;
                active = true;
            }
        }
    }
    public void TryAttack()
    {
        if (player.targetTile != null && player.targetTile == attackTo && player.attackDamaged)
        {
            string[] dialouge = new string[2];

            dialouge[0] = "For you to damage me first you need to roll an armor class check.";
            dialouge[1] = "If the number you get is higher than my armor class, I get damaged!";

            if (active)
            {
                DoDialouge(dialouge);
                active = false;
            }
            if (dScript.done)
            {
                tutorialStep++;
                active = true;
            }
        }
        else if(player.targetTile != null && player.targetTile != attackTo)
        {
            string[] dialouge = new string[1];

            dialouge[0] = "No Odin, click the tile that is between you and I.";
            player.targetTile = null;

            if (active)
            {
                player.targetTile = null;
                timer = 0;
                DoDialouge(dialouge);
                active = false;
            }
            if (dScript.done)
            {
                player.targetTile = null;
                active = true;
            }
        }
    }
    public void DoAttack()
    {
        if (enemy.hmScript.HIT == HitOrMiss.Hit.miss)
        {
            string[] dialouge = new string[2];

            dialouge[0] = "Okay so the number you rolled was less than my armor class...";
            dialouge[1] = "Lets Pretend that never happened!";

            if (active)
            {
                DoDialouge(dialouge);
                active = false;
            }
            if (dScript.done)
            {
                tutorialStep++;
                active = true;
            }
        }
        if (enemy.hmScript.HIT == HitOrMiss.Hit.hit)
        {
            string[] dialouge = new string[2];

            dialouge[0] = "Perfect! The number you rolled is higher than my armor class!";
            dialouge[1] = "Now you need to roll the damage dice to see how much damage you can do to me.";

            if (active)
            {
                DoDialouge(dialouge);
                active = false;
            }
            if (dScript.done)
            {
                tutorialStep += 2;
                active = true;
            }
        }
    }
    public void FixedAttack()
    {
        string[] dialouge = new string[1];

        dialouge[0] = "Now you need to roll the damage dice to see how much damage you can do to me.";

        if (active)
        {
            enemy.hmScript.HIT = HitOrMiss.Hit.hit;
            DoDialouge(dialouge);
            active = false;
        }
        if (dScript.done)
        {
            tutorialStep ++;
            active = true;
        }
    }
    public void RollAttackDice()
    {
        if (!attackDamage.GetComponent<DragObject>().dealingDamage)
        {

            string[] dialouge = new string[2];

            dialouge[0] = "Alright so you rolled!";
            dialouge[1] = "You did " + attackDamage.GetComponent<DragObject>().damageToDeal +" to me.";

            if (active)
            {
                DoDialouge(dialouge);
                active = false;
            }
            if (dScript.done)
            {
                tutorialStep++;
                active = true;
            }
        }
    }
    public void AttackTwo()
    {
        string[] dialouge = new string[4];

        dialouge[0] = "Youre second attack is an AOE attack";
        dialouge[1] = "For this attack you do not chose the attack tiles";
        dialouge[2] = "You click on the attack button and the tiles that will be affect will show up!";
        dialouge[3] = "If you are happy with your attack tiles you click the button again to trigger the attack";


        if (active)
        {
            DoDialouge(dialouge);
            active = false;
        }
        if (dScript.done)
        {
            tutorialStep++;
            active = true;
        }
    }
}
