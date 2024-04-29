using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using CustomExtensions;
using UnityEngine.UI;

public class GameBehavior : MonoBehaviour, IManager
{
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public string labelText = "Collect both items to escape!";
    public int maxItems = 2;
    private int _itemsCollected = 0;
    private string _state;
    public Text ScoreText;
    public Text ItemText;

    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            if(_itemsCollected >= maxItems)
            {
                labelText = "You've found all the items!";
                showWinScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
            Debug.LogFormat("Items: {0}", _itemsCollected);
            ItemText.text = "" + _itemsCollected;
        }
    }

    void Start()
    {
        Initialize();
        InventoryList<string> inventoryList = new InventoryList<string>();
        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);
    }
    public void Initialize()
    {
        _state = "Manager initialized..";
        _state.FancyDebug();
        debug(_state);
        LogWithDelegate(debug);
        GameObject player = GameObject.Find("Player");
        PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();
        playerBehavior.playerJump += HandlePlayerJump;
        Debug.Log(_state);
        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Bracers");
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }

    private int _playerHP = 3;
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives: {0}", _playerHP);
            if (_playerHP <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
            }
            else
            {
                labelText = "Ouch... that's gotta hurt.";
            }
            ScoreText.text = ""+_playerHP;
        }
        

    }

    private void OnGUI()
    {
        

        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width/2 - 100,
                Screen.height/2 - 50, 200, 100), "YOU WON!"))
            {
                Utilities.RestartLevel(0);
            }
        }
        if(showLossScreen)
        {
            StartCoroutine(pk_hospital_lol_XD());
        }
    }
    public Stack<string> lootStack = new Stack<string>();

    public void PrintLootReport()
    {
        var currentItem = lootStack.Pop();
        var nextItem = lootStack.Peek();
        Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!", currentItem, nextItem);
        Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);
    }

    private IEnumerator pk_hospital_lol_XD()
    {
        Debug.Log("gyuzsdfgkbudfscbugczdfghkjcxfzg");
        yield return new WaitForSecondsRealtime(1.5f);
        Debug.Log("peepee fard");
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You Lose..."))
        {
            try
            {
                Utilities.RestartLevel(-1);
                debug("Level restarted successfully...");
            }
            catch (System.ArgumentException e)
            {
                Utilities.RestartLevel(0);
                debug("Reverting to scene 0: " + e.ToString());
            }
            finally
            {
                debug("Restart handled...");
            }
        }
        yield return null;
    }
}
