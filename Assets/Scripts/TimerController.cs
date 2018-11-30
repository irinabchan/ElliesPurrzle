using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour {

    public float timeLeft = 90f;
    public Text TimerText;
    public Text ObjectiveText;

    // Use this for initialization
	void Start () {

        Invoke("DisableObjectiveText", 4f);
	}
	
	// Update is called once per frame
	void Update () {
        // Stop the timer if player found lost toy
        if (GameObject.Find("Player").GetComponent<PlayerController>().win == false)
        {
            timeLeft -= Time.deltaTime;
            TimerText.text = "Timer: " + (timeLeft).ToString("0");

            if (timeLeft < 10)
            {
                TimerText.color = Color.red;
            }

            if (timeLeft < 0)
            {
                TimerText.text = "GAME OVER";
                SceneManager.LoadScene("GameOver");
            }
        }
	}

    void DisableObjectiveText()
    {
        ObjectiveText.enabled = false;
    }
}
