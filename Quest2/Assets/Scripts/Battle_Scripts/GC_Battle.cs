/**
* Battle Scene Game Controller
* Controls the flow of battle
*/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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

    [SerializeField] private string overworldScene;    //Name of the Overworld Scene to transition into

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Loaded with Scaling of " + BattleInfo.difficultyScaling);
        state = BattleState.Start;
        Transfer transfer = GameObject.FindAnyObjectByType<Transfer>(); 
        playerObj = transfer.playerObj;
        enemyObj = transfer.enemyObj;
        StartCoroutine(SetUpBattle());

    }


    IEnumerator SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerObj, playerSpawnPos);
        playerUnit = playerGO.GetComponent<Unit>();


        GameObject enemyGO = Instantiate(enemyObj, enemySpawnPos);
        enemyUnit = enemyObj.GetComponent<Unit>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);
        state = BattleState.PlayerTurn;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        // Damage Enemy


        yield return new WaitForSeconds(2f);

        // CHeck if enemy is dead
        // Change State based on what happened
    }


    private int PlayerSelectAttack()
    {
        return 0;
    }

    void PlayerTurn()
    {
        // Set player dialogue text
    }


    void onAttackButton()
    {
        if (state != BattleState.PlayerTurn)
        {
            return;
        }

        int damage = PlayerSelectAttack();

        StartCoroutine(PlayerAttack());
    }

    void TransitionToOverworld()
    {
        //Ensure we're not trying to transition to a scene that doesn't exist
        if (string.IsNullOrEmpty(overworldScene))
        {
            Debug.LogWarning("Warning: Invalid Overworld Scene Name!");
            return;
        }

        //Transition to the Overworld scene
        SceneManager.LoadScene(overworldScene, LoadSceneMode.Single);
    }
}
