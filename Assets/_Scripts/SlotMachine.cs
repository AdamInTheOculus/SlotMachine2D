using UnityEngine;
using System.Collections;
using UnityEditor;

using UnityEngine.UI;

public class SlotMachine : MonoBehaviour {

	public Sprite bananaSprite, barSprite, bellSprite, blankSprite; 
	public Sprite cherrySprite, grapeSprite, orangeSprite, sevenSprite;

	private Text mainMessageText;
	private Text totalCreditsText;
	private Text betAmountText;
	private Text winnerAmountText;

	private Image leftReelSprite;
	private Image middleReelSprite;
	private Image rightReelSprite;


	// Use this for initialization
	void Start () {

		// Initialize all text objects
		mainMessageText = GameObject.Find("MainMessage").GetComponent<Text>();
		totalCreditsText = GameObject.Find ("TotalCredits").GetComponent<Text> ();
		betAmountText = GameObject.Find ("BetAmount").GetComponent<Text> ();
		winnerAmountText = GameObject.Find ("WinnerAmount").GetComponent<Text> ();

		// Initialize all Sprite objects
		leftReelSprite = GameObject.Find("LeftReelSprite").GetComponent<Image>();
		middleReelSprite = GameObject.Find("MiddleReelSprite").GetComponent<Image>();
		rightReelSprite = GameObject.Find("RightReelSprite").GetComponent<Image>();

		// Set default text values to 0
		totalCreditsText.text = playerMoney.ToString();
		betAmountText.text = "0";
		winnerAmountText.text = "0";
	}

	void Update() {

		// Continuously update the fields according to SlotMachine data
		this.betAmountText.text = playerBet.ToString ();
		this.totalCreditsText.text = playerMoney.ToString ();
		this.winnerAmountText.text = winnings.ToString ();
	}

	private float MAX_BET = 100000;
	private int playerMoney = 1000;
	private int winnings = 0;
	private int jackpot = 5000;
	private float turn = 0.0f;
	private int playerBet = 0;
	private float winNumber = 0.0f;
	private float lossNumber = 0.0f;
	private string[] spinResult;
	private string fruits = "";
	private float winRatio = 0.0f;
	private float lossRatio = 0.0f;
	private int grapes = 0;
	private int bananas = 0;
	private int oranges = 0;
	private int cherries = 0;
	private int bars = 0;
	private int bells = 0;
	private int sevens = 0;
	private int blanks = 0;

	/* Utility function to increase the bet amount. 
       This function is called in BetHandler.cs
	*/
	public void setBet(int amount)
	{
		if ((amount + this.playerBet) < MAX_BET) {
			this.playerBet += amount;
		} else {
			this.mainMessageText.text = "Max Bet Hit";
		}
	}

	/* Utility function to set bet amount to zero. 
       This function is called in BetHandler.cs
	*/
	public void resetBet()
	{
		this.playerBet = 0;
	}

	/* Utility function to show Player Stats */
	private void showPlayerStats()
	{
		winRatio = winNumber / turn;
		lossRatio = lossNumber / turn;
		string stats = "";
		stats += ("Jackpot: " + jackpot + "\n");
		stats += ("Player Money: " + playerMoney + "\n");
		stats += ("Turn: " + turn + "\n");
		stats += ("Wins: " + winNumber + "\n");
		stats += ("Losses: " + lossNumber + "\n");
		stats += ("Win Ratio: " + (winRatio * 100) + "%\n");
		stats += ("Loss Ratio: " + (lossRatio * 100) + "%\n");
		Debug.Log(stats);
	}

	/* Utility function to reset all fruit tallies*/
	private void resetFruitTally()
	{
		grapes = 0;
		bananas = 0;
		oranges = 0;
		cherries = 0;
		bars = 0;
		bells = 0;
		sevens = 0;
		blanks = 0;
	}

	/* Utility function to reset the player stats */
	private void resetAll()
	{
		playerMoney = 1000;
		winnings = 0;
		jackpot = 5000;
		turn = 0;
		playerBet = 0;
		winNumber = 0;
		lossNumber = 0;
		winRatio = 0.0f;

		mainMessageText.text = "Welcome!";
		// Set default text values to 0
		totalCreditsText.text = "0";
		betAmountText.text = "0";
		winnerAmountText.text = "0";
	}

	/* Check to see if the player won the jackpot */
	private void checkJackPot()
	{
		/* compare two random values */
		var jackPotTry = Random.Range (1, 51);
		var jackPotWin = Random.Range (1, 51);

		if (jackPotTry == jackPotWin) {
			Debug.Log ("You Won the $" + jackpot + " Jackpot!!");
			playerMoney += jackpot * playerBet;
			jackpot = 1000;
		}
	}

	/* Utility function that updates the jackpot */
	private void updateJackpot()
	{
		jackpot += (playerBet * 2);
	}

	/* Utility function to show a win message and increase player money */
	private void showWinMessage()
	{
		playerMoney += winnings;
		Debug.Log("You Won: $" + winnings);

		// TODO: Slide text to left to fit winning message
		mainMessageText.text = "You Won: $" + winnings;

		resetFruitTally();
		updateJackpot ();
		checkJackPot();
	}

	/* Utility function to show a loss message and reduce player money */
	private void showLossMessage()
	{
		playerMoney -= playerBet;
		Debug.Log("You Lost!");

		// TODO: Slide text to middle to fit losing message
		mainMessageText.text = "You Lost!";

		resetFruitTally();
		updateJackpot ();
	}

	/* Utility function to check if a value falls within a range of bounds */
	private bool checkRange(int value, int lowerBounds, int upperBounds)
	{
		return (value >= lowerBounds && value <= upperBounds) ? true : false;

	}

	/* When this function is called it determines the betLine results.
    e.g. Bar - Orange - Banana */
	private string[] Reels()
	{
		string[] betLine = { " ", " ", " " };
		int[] outCome = { 0, 0, 0 };

		for (var spin = 0; spin < 3; spin++)
		{
			outCome[spin] = Random.Range(1,65);

			if (checkRange(outCome[spin], 1, 27)) {  // 41.5% probability
				betLine[spin] = "blank";
				blanks++;
			}
			else if (checkRange(outCome[spin], 28, 37)){ // 15.4% probability
				betLine[spin] = "Grapes";
				grapes++;
			}
			else if (checkRange(outCome[spin], 38, 46)){ // 13.8% probability
				betLine[spin] = "Banana";
				bananas++;
			}
			else if (checkRange(outCome[spin], 47, 54)){ // 12.3% probability
				betLine[spin] = "Orange";
				oranges++;
			}
			else if (checkRange(outCome[spin], 55, 59)){ //  7.7% probability
				betLine[spin] = "Cherry";
				cherries++;
			}
			else if (checkRange(outCome[spin], 60, 62)){ //  4.6% probability
				betLine[spin] = "Bar";
				bars++;
			}
			else if (checkRange(outCome[spin], 63, 64)){ //  3.1% probability
				betLine[spin] = "Bell";
				bells++;
			}
			else if (checkRange(outCome[spin], 65, 65)){ //  1.5% probability
				betLine[spin] = "Seven";
				sevens++;
			}

		}

		return betLine;
	}

	/* This function calculates the player's winnings, if any */
	private void determineWinnings()
	{
		if (blanks == 0)
		{
			if (grapes == 3)
			{
				winnings = playerBet * 10;
			}
			else if (bananas == 3)
			{
				winnings = playerBet * 20;
			}
			else if (oranges == 3)
			{
				winnings = playerBet * 30;
			}
			else if (cherries == 3)
			{
				winnings = playerBet * 40;
			}
			else if (bars == 3)
			{
				winnings = playerBet * 50;
			}
			else if (bells == 3)
			{
				winnings = playerBet * 75;
			}
			else if (sevens == 3)
			{
				winnings = playerBet * 100;
			}
			else if (grapes == 2)
			{
				winnings = playerBet * 2;
			}
			else if (bananas == 2)
			{
				winnings = playerBet * 2;
			}
			else if (oranges == 2)
			{
				winnings = playerBet * 3;
			}
			else if (cherries == 2)
			{
				winnings = playerBet * 4;
			}
			else if (bars == 2)
			{
				winnings = playerBet * 5;
			}
			else if (bells == 2)
			{
				winnings = playerBet * 10;
			}
			else if (sevens == 2)
			{
				winnings = playerBet * 20;
			}
			else if (sevens == 1)
			{
				winnings = playerBet * 5;
			}
			else
			{
				winnings = playerBet * 1;
			}
			winNumber++;
			showWinMessage();
		}
		else
		{
			lossNumber++;
			showLossMessage();
		}
	}

	public void OnSpinButtonClick()
	{

		if (playerMoney == 0)
		{
			this.mainMessageText.text = "No more credits!";
		}
		else if (playerBet > playerMoney)
		{
			this.mainMessageText.text = "Not enough credits";
			Debug.Log("You don't have enough Money to place that bet.");
		}
		else if (playerBet <= 0)
		{
			this.mainMessageText.text = "No bet entered!";
			Debug.Log("All bets must be a positive $ amount.");
		}
		else if (playerBet <= playerMoney)
		{
			spinResult = Reels();

			// Update fruit sprites here
			updateReelSprites(spinResult);

			fruits = spinResult[0] + " - " + spinResult[1] + " - " + spinResult[2];
			Debug.Log(fruits);
			determineWinnings();
			turn++;
			showPlayerStats();
		}
		else
		{
			Debug.Log("Please enter a valid bet amount");
		}
	}

	private void updateReelSprites(string[] sprites)
	{
		updateReel (sprites [0], leftReelSprite);
		updateReel (sprites [1], middleReelSprite);
		updateReel (sprites [2], rightReelSprite);
	}

	private void updateReel(string sprite, Image reel)
	{
		switch (sprite) 
		{
		case "Banana":
			reel.sprite = bananaSprite;
			break;

		case "blank":
			reel.sprite = blankSprite;
			break;

		case "Bar":
			reel.sprite = barSprite;
			break;

		case "Bell":
			reel.sprite = bellSprite;
			break;

		case "Cherry":
			reel.sprite = cherrySprite;
			break;

		case "Grapes":
			reel.sprite = grapeSprite;
			break;

		case "Seven":
			reel.sprite = sevenSprite;
			break;

		default: 
			Debug.Log("Nuttin...");
			break;
		}
	}

	/* This function turns off the slot machine, quitting the application */
	public void OnPowerButtonClick()
	{
		Debug.Log ("Thanks for playing! Goodbye");

		if (EditorApplication.isPlaying) {
			EditorApplication.isPlaying = false;
		} else {
			Application.Quit ();
		}
	}

	/* This function resets all numbers to default when reset button is pressed. */
	public void OnResetButtonClick()
	{
		Debug.Log ("Resetting all values.");
		resetAll ();
	}
}
