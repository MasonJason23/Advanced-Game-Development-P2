using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // From Olise's GameManager
    [SerializeField] private GameObject levelUpScreen;
    public static bool isGamePaused = false;
    private UpgradeUIManager _upgradeUIManager;
    
    private int u1;
    private int u2;
    private int u3;

    private string u1str;
    private string u2str;
    private string u3str;

    public void pauseGame()
    {
        u1str = TranslateNumToAbility(u1);
        u2str = TranslateNumToAbility(u2);
        u3str = TranslateNumToAbility(u3);
        
        _upgradeUIManager.ChangeCardName(1, u1str);
        _upgradeUIManager.ChangeCardName(2, u2str);
        _upgradeUIManager.ChangeCardName(3, u3str);
        
        levelUpScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isGamePaused = true;
    }
    
    public void playGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        levelUpScreen.SetActive(false);
        isGamePaused = false;
    }
    
    // -------------------------------------------------------------
    
    // Jason's GameManager Code
    // Reference to the player game object
    [SerializeField] private GameObject player;

    // Reference to player abilities game object
    [SerializeField] private PlayerAbilities PlayerAbilities;

    // Instantiate a new AbilityClass
    private AbilityClass _abilityClass;

    // Obtain PlayerStats from the player (player instantiates it)
    private PlayerStats _playerStats;
    
    // Game timer
    [SerializeField] private float gameTimer;

    // Reference to the player's current level
    // Used to check if the player has leveled up
    private int _currentPlayerLevel;
    
    // Check to see if the game is still ongoing or ended
    public enum GamePhase
    {
        Alive,
        Dead
    }
    public static GamePhase _isPlayerAlive;
    
    private void Start()
    {
        // Initializing game timer at start
        gameTimer = 0f;

        _abilityClass = new AbilityClass();

        _playerStats = player.GetComponent<Player>().GetPlayerStats();
        
        _isPlayerAlive = GamePhase.Alive;
        _currentPlayerLevel = _playerStats.GetPlayerLevel();

        _upgradeUIManager = GetComponent<UpgradeUIManager>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.U)) 
        {
            pauseGame();
        }
        
        // Condition to check when the run is finished (player dies)
        if (_isPlayerAlive == GamePhase.Dead)
        {
            // End Game Here (DYLAN!
            return;
        }
        
        // Updating game timer every frame
        gameTimer += Time.deltaTime;
        
        // Did the player level up?
        DidLevelUp();
        
        // Does not allow the ActivateAbilities() function to run because, at the start of the game,
        // the player shouldn't have any access to any abilities
        if (_currentPlayerLevel == 1)
        {
            return;
        }
        
        // Activates any abilities the player has access to
        ActivateAbilities();
    }

    // This function checks if the player has the ability before "activating" them
    // Each ability might be implemented differently based on their own behaviour
    public void ActivateAbilities()
    {
        // The explosion ability happens on cooldown, therefore when the timer reaches a threshold, an explosion activates
        if (_abilityClass.a1)
        {
            _abilityClass.cdTimer1 += Time.deltaTime;
            if (_abilityClass.cdTimer1 >= _abilityClass.abilityStats[0][1])
            {
                _abilityClass.cdTimer1 = 0;
                PlayerAbilities.Explosion(player.transform, (int)_abilityClass.abilityStats[0][0], (int)_abilityClass.abilityStats[0][2]);
            }
        }
        
        // The orb ability just initializes the orbs, therefore we only need to activate each orb sequentially per upgrade.
        if (_abilityClass.a2)
        {
            PlayerAbilities.Orb(player.transform, (int)_abilityClass.abilityStats[1][0], (int)_abilityClass.abilityStats[1][2]);
            _abilityClass.a2 = false;
        }
        
        if (_abilityClass.a3)
        {
            PlayerAbilities.Aura(player.transform, (int)_abilityClass.abilityStats[2][0], (int)_abilityClass.abilityStats[2][2], _abilityClass.abilityStats[2][1]);
            _abilityClass.a3 = false;
        }
    }

    // This function checks if the player leveled up or not
    // If the player did level up, an upgrade has to be chosen (ChooseUpgrade() is called)
    public void DidLevelUp()
    {
        if (_currentPlayerLevel < _playerStats.GetPlayerLevel())
        {
            _currentPlayerLevel = _playerStats.GetPlayerLevel();
            ChooseUpgrade();
        }
    }
    
    // Ideally this is how we are getting the random choices to choose what upgrade the player wants
    // But current the system randomly chooses an upgrade for the player
    public void ChooseUpgrade()
    {
        // List of Upgrades
        // {0, "Hp"},
        // {1, "Shield"},
        // {2, "FireRate"},
        // {3, "MovementSpeed"},
        // {4, "CrtRate"},
        // {5, "CrtDamage"},
        // {6, "DamageMultiplier"},
        // {7, "Explosion"},
        // {8, "Orb"},
        // {9, "Aura"},
        // {10, "Lighting"}
        
        // This is how u1..2..3 should be created
        // "u1..2..3" are the options for the player to choose from
        // int u1 = Random.Range(0, 11);
        // int u2 = Random.Range(0, 11);
        // int u3 = Random.Range(0, 11);
        
        // Testing ability upgrades
        u1 = Random.Range(7, 10);
        
        u2 = Random.Range(7, 10);
        while (u2 == u1) u2 = Random.Range(7, 10);
        
        u3 = Random.Range(7, 10);
        while (u3 == u1 || u3 == u2) u3 = Random.Range(7, 10);

        // Bring up upgrade menu whilst pausing the game
        pauseGame();
        
        // ChooseButton() function will unpause the game
    }

    // The Upgrade() function doesn't do any upgrading but instead distinguishes
    // whether to upgrade a player stat or and actual ability
    public void Upgrade(int indicator)
    {
        if (indicator <= 6)
        {
            _playerStats.UpgradeStat(indicator);
        }
        else
        {
            _abilityClass.UpgradeAbility(indicator);
        }
    }

    // Debugging
    private void DebuggingAbility()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            // Do something here
        }
    }

    // Upgrade button
    public void ChooseButton(int indicator)
    {
        // Based on the chosen upgrade, proceed with the upgrade process
        switch (indicator)
        {
            case(1):
                Upgrade(u1);
                break;
            case(2):
                Upgrade(u2);
                break;
            case(3):
                Upgrade(u3);
                break;
        }
        
        playGame();
    }

    public string TranslateNumToAbility(int indicator)
    {
        switch (indicator)
        {
            case(0):
                return "Hp";
            case(1):
                return "Shield";
            case(2):
                return "FireRate";
            case(3):
                return "MovementSpeed";
            case(4):
                return "CrtRate";
            case(5):
                return "CrtDamage";
            case(6):
                return "DamageMultiplier";
            case(7):
                return "Explosion";
            case(8):
                return "Orbs";
            case(9):
                return "Aura";
            default:
                Debug.Log("Ability/Stat does not exists");
                return "NULL";
        }
    }
}