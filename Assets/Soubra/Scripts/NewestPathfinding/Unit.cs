using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int initiative;
    public int tileX;
    public int tileZ;
    public int index = 0;
    public int moveSpeed = 3;
    public int remainingMovement = 3;
    public int attackType = 1;
    public int health = 10;
    public int coolDown = 0;
    public int armorClass = 10;
    public int attackHit = 4;
    public int rageNumber = 0;
    public int rageTime = 0;

    public int ogAttack2CoolDown = 2;
    public int ogAttack3CoolDown = 5;
    public int attack2CoolDown = 0;
    public int attack3CoolDown = 0;

    public float distance = 0.5f;
    public float heightOffset = 0.5f;
    public bool move;
    public bool reset;
    public bool enemy;
    public bool attackNow;
    public bool attackDamaged;
    public bool attackMode;
    public bool attackedAlready;
    public bool preAttack = true;
    public bool raging;
    public bool dead;
    public bool getHit;
    public Unit targetEnemy;
    public ClickableTile ct;
    public ClickableTile targetTile;
    public TileMap map;
    public GameObject cubeBase;
    public Animator animator;
    public List<GAction> actions;
    public List<TileMap.Node> currentPath = null;
    public TileMap.Node currentNode;
    public List<string> attackNames;
    public enum EnemyType { AsgardianMelee, AsgardianRanged, GiantMelee, GiantRanged, TreePerson, Dragon, Player };
    public EnemyType enemyType;

    public CellPositions.Direction direction;
    public CellPositions.Attacks attack;

    public AssignTiles atScript;
    public HitOrMiss hmScript;
    private void Start()
    {
        atScript = GetComponent<AssignTiles>();
        hmScript = GetComponentInChildren<HitOrMiss>();
    }
    public void Update()
    {
        if (health <= 0)
        {
            this.enabled = false;
            dead = true;
        }
        if (currentPath != null)
        {
            int currNode = 0;

            while(currNode < currentPath.Count - 1)
            {
                Vector3 start = map.TileToWorld(currentPath[currNode].x, currentPath[currNode].y);
                Vector3 end = map.TileToWorld(currentPath[currNode + 1].x, currentPath[currNode + 1].y);

                Debug.DrawLine(start, end, Color.red);
                
                currNode++;
            }

        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            MoveToNextTile();
            //move = true;
        }

        if (move)
        {
            RecursiveMoveToNextTile();
            if (enemy)
            {
                animator.SetBool("Walking", true);
            }
        }
        else
        {
            if (enemy)
            {
                animator.SetBool("Walking", false);
            }
        }
    }

    public void ResetAnimation()
    {
        animator.ResetTrigger("Attack1");
        attackNow = false;
    }
    public void ChangeAttack(int i)
    {
        if (i == 1)
        {
            attack = CellPositions.Attacks.First;
        }
        if (i == 2)
        {
            attack = CellPositions.Attacks.Second;
        }
        if (i == 3)
        {
            attack = CellPositions.Attacks.Third;
        }
    }
    public void CheckAttackStatus()
    {
        if (this.CompareTag("Player"))
        {
            if(!attackedAlready)
            {
                attackMode = true;
            }
            else
            {
                attackMode = false;
            }
        }
    }
    public void CoolDownCheck()
    {
        if (coolDown > 0)
        {
            coolDown--;
        }
    }
    public void FinishAttackAnimation()
    {
        Debug.Log("Finished!!");
        animator.SetBool("Idle", true);
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].done = true;
        }
    }
    public void MoveCharacter()
    {
        if (currentPath != null)
        {
            if (index > currentPath.Count - 1)
            {
                Debug.Log("Reached Destintation");
                move = false;
                return;
            }
            Vector3 position = currentPath[index].ground.transform.position;
            if (transform.position != position)
            {
                
                transform.position = Vector3.MoveTowards(transform.position, position, 0.1f);
                if (transform.position == position)
                {
                    Debug.Log("Went To Another Node");
                    index++;
                    MoveCharacter();
                }
            }
        }
    }
    public void MoveToNextTile()
    {
        int remainingMovement = moveSpeed;
        while(remainingMovement > 0)
        {
            if (currentPath == null)
                return;

            remainingMovement -= (int)map.CostToEnterTile(currentPath[0].x, currentPath[0].y);

            currentPath.RemoveAt(0);

            transform.position = currentPath[0].ground.transform.position; //Vector3.MoveTowards(transform.position, currentPath[0].ground.transform.position, 0.1f);

            if (currentPath.Count == 1)
            {
                currentPath = null;
            }
        }
    }
    public void RecursiveMoveToNextTile()
    {
        if (currentPath != null)
        {
            if (Vector3.Distance(cubeBase.transform.position, currentPath[0].ground.transform.position) < distance)
            {
                if (currentPath == null)
                    return;

                if (remainingMovement <= 0)
                {
                    move = false;
                    currentPath = null;
                    return;
                }


                //transform.position = currentPath[0].ground.transform.position;

                remainingMovement -= (int)map.CostToEnterTile(currentPath[0].x, currentPath[0].y);

                currentPath.RemoveAt(0);
                Debug.Log("Still Asking TO Walk");
                if (currentPath.Count == 1)
                {
                    Debug.Log("Current Path Count: " + currentPath.Count);
                    currentPath = null;
                }
            }
            else
            {
                Debug.Log("Still Walking");
                transform.LookAt(currentPath[0].ground.transform.position + atScript.offset);
                transform.position = Vector3.Lerp(transform.position, currentPath[0].ground.transform.position + atScript.offset, 5f * Time.deltaTime);
            }
        }
        else
        {
            move = false;
        }
    }
}
