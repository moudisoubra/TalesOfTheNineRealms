using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public bool active = true;
    public bool dealingDamage = true;
    public bool dealingEffect = true;
    public int tutorialStep;
    public int uiStep = -1;
    public int damagedealt = 0;
    public int damageadded = 0;
    public float timer;
    public Dialogue dScript;
    public ClickableTile ct;
    public ClickableTile walkTo;
    public ClickableTile startCT;
    public ClickableTile attackTo;
    public AsgardianMClass ac;
    public TurnController tcScript;
    public InitiativeRoll irScript;
    public CheckHealths chScript;
    public Unit player;
    public Unit enemy;
    public GameObject attackArmor;
    public GameObject attackDamage;
    public GameObject attackEffect;

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
        tcScript.uiStep = uiStep;
        tcScript.tutorialStep = tutorialStep;

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
            EndTurn();
        }
        if (tutorialStep == 11)
        {
            TeachStatusEffect();
        }
        if (tutorialStep == 12)
        {
            TryStatusEffect();
        }
        if (tutorialStep == 13)
        {
            AttackTwo();
        }
        if (tutorialStep == 14)
        {
            AttackTwoStepForward();
        }
        if (tutorialStep == 15)
        {
            AttackTwoCheck();
        }
        if (tutorialStep == 16)
        {
            AttackTwoRolledArmor();
        }
        if (tutorialStep == 17)
        {
            FixedAttackTwo();
        }
        if (tutorialStep == 18)
        {
            RollAttackDiceTwo();
        }
        if (tutorialStep == 19)
        {
            AtTheEnd();
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
        active = true;
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
                    startCT = player.currentNode.ground.GetComponent<ClickableTile>();
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
                active = true;

            if (active)
            {
                timer = 0;
                DoDialouge(dialouge);
                player.targetTile = null;
                active = true;
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
        
        if (player.remainingMovement == 0 && player.currentNode.ground.GetComponent<ClickableTile>() == walkTo)
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
        if (!player.move && player.remainingMovement == 0 && player.currentNode.ground.GetComponent<ClickableTile>() != walkTo)
        {
            string[] dialouge = new string[2];

            dialouge[0] = "Odin please walk to me!";
            dialouge[1] = "Teleporting you back!";

            if (active)
            {
                player.targetTile = null;
                DoDialouge(dialouge);
                active = false;
            }
            if (dScript.done)
            {
                player.GetComponent<AssignTiles>().assign = true;
                player.remainingMovement = 3;
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

                active = true;
            if (active)
            {
                player.targetTile = null;
                timer = 0;
                DoDialouge(dialouge);
                active = true;
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
            enemy.getHit = true;
            OdinWarriorClass reference = player.GetComponent<OdinWarriorClass>();
            reference.DealDamage(reference.attack);
            DoDialouge(dialouge);
            active = false;
        }
        if (dScript.done)
        {
            tutorialStep++;
            active = true;
        }
    }
    public void RollAttackDice()
    {
        if (!dealingDamage && player.attackedAlready)
        {

            string[] dialouge = new string[2];

            dialouge[0] = "Alright so you rolled!";
            dialouge[1] = "You did " + damagedealt + " damage to me.";

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
    public void EndTurn()
    {

        string[] dialouge = new string[2];

        dialouge[0] = "Now that you have moved and dealt damage its time to end you turn!";
        dialouge[1] = "Clicking the end turn button will refresh your movement and calculate any cooldowns for your attacks.";

        if (active)
        {
            player.GetComponent<OdinWarriorClass>().ClearAll();
            DoDialouge(dialouge);
            active = false;
        }

    }
    public void TeachStatusEffect()
    {
        string[] dialouge = new string[6];

        dialouge[0] = "Before we go to your second attack...";
        dialouge[1] = "We need to talk about your third attack, which is a status effect.";
        dialouge[2] = "This attack will give you extra damage";
        dialouge[3] = "The number of extra damage will be determined by the roll";
        dialouge[4] = "Click the attack once to select it and again to confirm it!";
        dialouge[5] = "Go ahead and try that attack before we move on";


        if (active)
        {
            uiStep = 2;
            DoDialouge(dialouge);
            active = false;
        }
        if (dScript.done)
        {
            tutorialStep++;
            active = true;
        }
    }
    public void TryStatusEffect()
    {
        if (player.rageNumber > 0 && !dealingEffect)
        {
            string[] dialouge = new string[1];

            dialouge[0] = "You rolled " + damageadded + " this will be added to any damage you do over the next two turns.";


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
        string[] dialouge = new string[5];

        dialouge[0] = "Youre second attack is an AOE attack";
        dialouge[1] = "For this attack you do not chose the attack tiles";
        dialouge[2] = "You click on the attack button and the tiles that will be affect will show up!";
        dialouge[3] = "If you are happy with your attack tiles you click the button again to trigger the attack";
        dialouge[4] = "Since this attack is shorter range than the other, take a step closer!";


        if (active)
        {
            DoDialouge(dialouge);
            player.attackedAlready = false;
            enemy.hmScript.HIT = HitOrMiss.Hit.none;
            player.remainingMovement = 3;
            uiStep = 1;
            player.GetComponent<OdinWarriorClass>().ClearAll();
            active = false;
        }
        if (dScript.done)
        {
            tutorialStep++;
            active = true;
        }
    }
    public void AttackTwoStepForward()
    {
        if (player.attackMode)
        {
            string[] dialouge = new string[1];

            dialouge[0] = "Take a step closer before you try to attack!";


            if (active)
            {
                DoDialouge(dialouge);
                active = false;
            }
            if (dScript.done)
            {
                player.attackMode = false;
                active = true;
            }
        }
        if (player.targetTile != null && player.targetTile != attackTo)
        {
            string[] dialouge = new string[2];

            dialouge[0] = "Dont walk away Odin!";
            dialouge[1] = "Take a step forward!";

            player.targetTile = null;
            if (active)
            {
                player.targetTile = null;
                DoDialouge(dialouge);
                active = true;
            }
            if (dScript.done)
            {
                player.targetTile = null;
                player.GetComponent<AssignTiles>().x = walkTo.tileX;
                player.GetComponent<AssignTiles>().z = walkTo.tileZ;
                player.GetComponent<AssignTiles>().assign = true;
                player.remainingMovement = 3;
                active = true;
            }
        }
        if (player.currentNode.ground.GetComponent<ClickableTile>() == attackTo)
        {
            string[] dialouge = new string[2];

            dialouge[0] = "Okay now that you are closer, try your second attack";
            dialouge[1] = "Click once to show the attack tiles, and click again to confirm it";



            if (active)
            {
                DoDialouge(dialouge);
                active = false;
            }
            if (dScript.done)
            {
                player.remainingMovement = 3;
                tutorialStep++;
                active = false;
            }
        }
    }
    public void AttackTwoCheck()
    {
        if (player.attackDamaged)
        {
            string[] dialouge = new string[2];

            dialouge[0] = "Like last time, roll the armor class check!";

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
    public void AttackTwoRolledArmor()
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
    public void FixedAttackTwo()
    {
        string[] dialouge = new string[1];

        dialouge[0] = "Now you need to roll the damage dice to see how much damage you can do to me.";

        if (active)
        {
            enemy.hmScript.HIT = HitOrMiss.Hit.hit;
            enemy.getHit = true;
            OdinWarriorClass reference = player.GetComponent<OdinWarriorClass>();
            reference.DealDamage(reference.attack);
            DoDialouge(dialouge);
            active = false;
        }
        if (dScript.done)
        {
            tutorialStep++;
            active = true;
        }

    }
    public void RollAttackDiceTwo()
    {
        if (!dealingDamage)
        {

            string[] dialouge = new string[2];

            dialouge[0] = "Alright so you rolled!";
            dialouge[1] = "You did " + damagedealt + " damage to me.";

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
    public void AtTheEnd()
    {
        if (!dealingDamage)
        {

            string[] dialouge = new string[3];

            dialouge[0] = "Well Odin this is all you need to know!";
            dialouge[1] = "You are ready to head out!";
            dialouge[2] = "Goodluck!";

            if (active)
            {
                DoDialouge(dialouge);
                active = false;
            }
            if (dScript.done)
            {
                chScript.playersWon = true;
                tutorialStep++;
                active = true;
            }
        }
    }

}
