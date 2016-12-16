using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetHandler : MonoBehaviour {

	// Access our SlotMachine script
	private GameObject slotMachine;
	private SlotMachine slotScript;

	void Start()
	{
		slotMachine = GameObject.FindGameObjectWithTag ("SlotMachine");
		slotScript = slotMachine.GetComponent<SlotMachine> ();

		if (slotMachine == null) {
			Debug.Log("Slot machine object doesn't exist.");
		}

		if (slotScript == null) {
			Debug.Log ("Slot machine script doesn't exist.");
		}
	}

	// Button Event Handlers
	public void OnBetResetPress() { slotScript.resetBet(); }
	public void OnBet1Press() { slotScript.setBet (1); }
	public void OnBet2Press() { slotScript.setBet (2); }
	public void OnBet5Press() { slotScript.setBet (5); }
	public void OnBet10Press() { slotScript.setBet (10); }
	public void OnBet25Press() { slotScript.setBet (25); }
	public void OnBet50Press() { slotScript.setBet (50); }
	public void OnBet100Press() { slotScript.setBet (100); }
	public void OnBet500Press() { slotScript.setBet (500); }
}
