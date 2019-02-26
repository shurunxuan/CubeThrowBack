using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesController : MonoBehaviour
{
    public int doorHealth = 2;
    public GameObject hitEffect;
    public GameObject breakEffect;

    private float targetTime = 60.0f;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Check if the door is smashed or if the time has run out
        if (doorHealth == 0)
        {
            GameObject.Find("HealthBar").gameObject.transform.localScale = Vector3.zero;
            GameObject doorPieces = Instantiate(breakEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GameObject.Find("GameOverText").GetComponent<UnityEngine.UI.Text>().text = "ROBOT IS VICTORIOUS!";
            Time.timeScale = 0.25f;
        }
        if (targetTime <= 0)
        {
            GameObject.Find("GameOverText").GetComponent<UnityEngine.UI.Text>().text = "TIMES UP!";
            Time.timeScale = 0;
        }

        // Update time
        targetTime -= Time.deltaTime;
        int minutesTime = (int)targetTime / 60;
        int secondsTime = (int)targetTime % 60;
        if(targetTime > 60)
        {

            if (secondsTime < 10)
            {
                string correctedSecondsTime = "0" + secondsTime.ToString();
                GameObject.Find("TimerText").GetComponent<UnityEngine.UI.Text>().text = minutesTime + ":" + correctedSecondsTime;
            }
            else if (secondsTime > 10)
            {
                GameObject.Find("TimerText").GetComponent<UnityEngine.UI.Text>().text = minutesTime + ":" + secondsTime;
            }
        }
        else if(targetTime <= 60.0f)
        {
            int seconds = (int)targetTime;
            if (secondsTime < 10)
            {
                string correctedSecondsTime = "0" + seconds.ToString();
                GameObject.Find("TimerText").GetComponent<UnityEngine.UI.Text>().text = "0:" + correctedSecondsTime;
            }
            else if (secondsTime > 10)
            {
                GameObject.Find("TimerText").GetComponent<UnityEngine.UI.Text>().text = "0:" + seconds.ToString();
            }
            
        }

        // Update health bar
        Vector3 healthScale = GameObject.Find("HealthBar").gameObject.transform.localScale;
        healthScale.x = (doorHealth) / 20.0f;
        GameObject.Find("HealthBar").gameObject.transform.localScale = healthScale;

    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.name.Contains("HitboxSpot")) return;

        // Get Damage
        Instantiate(hitEffect, other.transform.position, Quaternion.identity);
        
        doorHealth--;
    }
}
