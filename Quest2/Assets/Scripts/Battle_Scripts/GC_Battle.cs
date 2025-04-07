/**
* Battle Scene Game Controller
* Controls the flow of battle
*/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public enum BattleState {Start, PlayerTurn, EnemyTurn, WON, LOST}

public class GC_Battle : MonoBehaviour
{

    public BattleState state;

    public GameObject playerObj;
    public GameObject enemyObj;

    public Transform playerSpawnPos;
    public Transform enemySpawnPos;

    Unit playerUnit;
    public GC_BattleHUD playerHUD;
    public GC_BattleHUD enemyHUD;

    Unit enemyUnit;

    public TMP_Text dialogueText;

    [SerializeField] private string overworldScene;    //Name of the Overworld Scene to transition into

    [SerializeField] private bool attackClicked = false;
    [SerializeField] private bool skillClicked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Loaded with Scaling of " + BattleInfo.difficultyScaling);
        state = BattleState.Start;
        Transfer transfer = GameObject.FindAnyObjectByType<Transfer>(); 
        //playerObj = transfer.playerObj; GameObject.FindGameObjectWithTag("Player");�
        enemyObj = transfer.enemyObj; //GameObject.FindGameObjectWithTag("Enemy");
        StartCoroutine(SetUpBattle());

    }


    IEnumerator SetUpBattle()
    {
        //GameObject playerGO = Instantiate(playerObj, playerSpawnPos);
        playerUnit = playerObj.GetComponent<Unit>();

        //GameObject enemyGO = Instantiate(enemyObj, enemySpawnPos);
        //Delete message - When testing in devOverworld: when performing collision, if you are still holding onto object during transition -> enemy will not be placed on platform
        enemyObj.transform.position = enemySpawnPos.position;
        enemyObj.transform.rotation = enemySpawnPos.rotation;
        enemyUnit = enemyObj.GetComponent<Unit>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);
        state = BattleState.PlayerTurn;

        dialogueText.text = "You have encountered a " + enemyObj.name + " . FIGHT!!";
        yield return new WaitForSeconds(2f);
        PlayerTurn();
    }

    void PlayerTurn()
    {
        // Set player dialogue text

        dialogueText.text = "Choose an action: ";


    }

    IEnumerator PlayerAttack()
    {
        // Damage Enemy

        // bool isDead = enemyUnit.TakeDamage(playerUnit.Strength);
        
        // testing player win - transition back to overworld
        bool isDead = enemyUnit.TakeDamage(100);

        enemyHUD.SetHP(enemyUnit);

        dialogueText.text = "Player attacks...";
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

        GameObject skillProjectile = Instantiate(playerUnit.skill.gameObject, playerUnit.transform.position, Quaternion.identity);

        Debug.Log("Skill Name: " + playerUnit.skill.skillName);
        Debug.Log("Skill Cost: " + playerUnit.skill.skillCost);
        Debug.Log("Skill Damage: " + playerUnit.skill.skillDamage);

        dialogueText.text = "Player used " + playerUnit.skill.skillName;
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


        yield return new WaitForSeconds(2f);
    }


    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.name + "'s turn.....";

        yield return new WaitForSeconds(2f);

        int randUpper = 0;
        if (enemyUnit.Mana >= enemyUnit.skill.skillCost)
        {
            randUpper = 1;
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
            dialogueText.text = enemyUnit.name + " uses " + enemyUnit.skill.skillName;
            yield return new WaitForSeconds(2f);
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

        attackClicked = false;
        skillClicked = false;

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
        if (state != BattleState.PlayerTurn && attackClicked)
        {
            return;
        }

        attackClicked = true;

        StartCoroutine(PlayerAttack());
    }


    public void onSkillButton()
    {
        if (state != BattleState.PlayerTurn && playerUnit.Mana < playerUnit.skill.skillCost && skillClicked)
        {
            return;
        }

        skillClicked = true;

        StartCoroutine(PlayerSkill());
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
