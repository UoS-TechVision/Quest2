/**
* Battle Scene Game Controller
* Controls the flow of battle
*/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public enum BattleState {Start, PlayerTurn, PlayerAction, EnemyTurn, EnemyAction, WON, LOST}

public class GC_Battle : MonoBehaviour
{

    public BattleState state;

    public GameObject playerObj;
    public GameObject enemyObj;

    public Transform playerSpawnPos;
    public Transform enemySpawnPos;

    public Transform playerSkillSpawnPos;
    public Transform enemySkillSpawnPos;

    Unit playerUnit;
    public GC_BattleHUD playerHUD;
    public GC_BattleHUD enemyHUD;

    Unit enemyUnit;

    public TMP_Text dialogueText;

    [SerializeField] private string overworldScene;    //Name of the Overworld Scene to transition into

    [SerializeField] private bool actionClicked = false;

    [SerializeField] private float projectileSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Loaded with Scaling of " + BattleInfo.difficultyScaling);
        state = BattleState.Start;
        Transfer transfer = GameObject.FindAnyObjectByType<Transfer>();

        playerObj = GameObject.FindGameObjectWithTag("Player");
        enemyObj = GameObject.FindGameObjectWithTag("Enemy");

/*        GameObject playerGO = Instantiate(playerObj, playerSpawnPos);
        playerGO.transform.parent = playerSpawnPos;
        playerGO.transform.position = new Vector3(0, 0, 0);
        playerGO.transform.rotation = Quaternion.identity;*/
        playerUnit = playerObj.GetComponent<Unit>();

/*        GameObject enemyGO = Instantiate(enemyObj, enemySpawnPos);
        enemyGO.transform.parent = enemySpawnPos;
        enemyGO.transform.position = new Vector3(0, 0, 0);
        enemyGO.transform.rotation = Quaternion.identity;*/
        enemyUnit = enemyObj.GetComponent<Unit>();
        
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        StartCoroutine(SetUpBattle());

    }


    IEnumerator SetUpBattle()
    {

        //Delete message - When testing in devOverworld: when performing collision, if you are still holding onto object during transition -> enemy will not be placed on platform
        dialogueText.text = "You have encountered a " + enemyObj.name + " . FIGHT!!";

        yield return new WaitForSeconds(2f);
        state = BattleState.PlayerTurn;
        dialogueText.text = "Player turn....";
        
        yield return new WaitForSeconds(2f);
        PlayerTurn();
    }

    void PlayerTurn()
    {
        // Set player dialogue text

        dialogueText.text = "Choose an action: ";
        actionClicked = false;
    }

    IEnumerator PlayerAttack()
    {
        // Damage Enemy
        state = BattleState.PlayerAction;
        dialogueText.text = "Player attacks...";

        bool isDead = enemyUnit.TakeDamage(playerUnit.Strength);
        
        // testing player win - transition back to overworld
        //bool isDead = enemyUnit.TakeDamage(100);

        enemyHUD.SetHP(enemyUnit);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            Debug.Log("Player Won");
            EndBattle();
        }
        else
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }


        yield return new WaitForSeconds(2f);
    }

    IEnumerator PlayerSkill()
    {
        // Damage Enemy
        state = BattleState.PlayerAction;
        GameObject skillProjectile = Instantiate(playerUnit.skill.gameObject, playerSkillSpawnPos.transform.position,  Quaternion.identity);
        skillProjectile.GetComponent<Rigidbody>().AddRelativeForce(ComputeVector(true)); //true = Player

        dialogueText.text = "Player used " + playerUnit.skill.skillName;
        yield return new WaitUntil(() => skillProjectile == false);
        int skillDamage;
        if (Random.value < playerUnit.skill.critChance)
        {
            skillDamage = Mathf.RoundToInt(playerUnit.skill.critDamage * playerUnit.skill.skillDamage);
        }
        else
        {
            skillDamage = playerUnit.skill.skillDamage;
        }
   
        bool isDead = enemyUnit.TakeDamage(skillDamage);

        playerUnit.DeductMana();

        enemyHUD.SetHP(enemyUnit);
        playerHUD.SetMana(playerUnit);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    private Vector3 ComputeVector(bool PlayerOrEnemy) // True is Player, False is enemy
    {
        Vector3 dir = enemySkillSpawnPos.position - playerSkillSpawnPos.position; //Player to enemy
        dir.Normalize();
        if (!PlayerOrEnemy)
        {
            dir *= -1; // Inverse vector for enemy to player
        }
        
        dir *= projectileSpeed;

        return dir;
    }


    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.name + "'s turn.....";

        yield return new WaitForSeconds(2f);

        int randUpper = 0;
        if (enemyUnit.Mana >= enemyUnit.skill.skillCost)
        {
            randUpper = 2;
        }

        int move = Random.Range(0, randUpper);
        int dmg = 0;
        // Basic Attack
        if (move == 0)
        {
            dialogueText.text = enemyUnit.name + " attacks!";

            yield return new WaitForSeconds(2f);

            dmg = enemyUnit.Strength;
        }

        // Skill Attack
        else
        {
            GameObject skillProjectile = Instantiate(enemyUnit.skill.gameObject, enemySkillSpawnPos.transform.position, Quaternion.identity);
            skillProjectile.GetComponent<Rigidbody>().AddRelativeForce(ComputeVector(false)); //true = Enemy

            dialogueText.text = enemyUnit.name + " uses " + enemyUnit.skill.skillName;
            yield return new WaitUntil(() => skillProjectile == false);
            if (Random.value < playerUnit.skill.critChance)
            {
                dmg = Mathf.RoundToInt(enemyUnit.skill.critDamage * enemyUnit.skill.skillDamage);
            }
            else
            {
                dmg = enemyUnit.skill.skillDamage;
            }
            enemyUnit.DeductMana();
        }

        bool isDead = playerUnit.TakeDamage(dmg);
         
        playerHUD.SetHP(playerUnit);
        enemyHUD.SetMana(enemyUnit);

        yield return new WaitForSeconds(2f);



        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }

    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You have defeated " + enemyUnit.name + "!!";

            //Enemy defeated - will be removed in OverworldManager.loadOverworldState()
            OverworldManager overworldManager = GameObject.FindFirstObjectByType<OverworldManager>();
            overworldManager.MarkEnemyAsDefeated(enemyObj.name);

            TransitionToOverworld();
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You have lost....";
        }
    }

    public void onAttackButton()
    {
        if (state == BattleState.PlayerTurn & !actionClicked)
        {
            actionClicked = true;

            StartCoroutine(PlayerAttack());
        }
        else
        {
            return;
        }

    }


    public void onSkillButton()
    {
        if (state == BattleState.PlayerTurn & playerUnit.Mana > playerUnit.skill.skillCost & !actionClicked)
        {
            actionClicked = true;

            StartCoroutine(PlayerSkill());
        }
        else
        {
            return;
        }


    }

    void TransitionToOverworld()
    {
        overworldScene = "devOverworld";
        //Ensure we're not trying to transition to a scene that doesn't exist
        if (string.IsNullOrEmpty(overworldScene))
        {
            Debug.LogWarning("Warning: Invalid Overworld Scene Name!");
            return;
        }

        //Destroying the enemy object to prevent it from persisting in the Overworld scene
        Destroy(enemyObj);

        //Transition to the Overworld scene
        Debug.Log("Transitioning to Overworld Scene!");
        SceneManager.LoadScene(overworldScene, LoadSceneMode.Single);
    }
}   
