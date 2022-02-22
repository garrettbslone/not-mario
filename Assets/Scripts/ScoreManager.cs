using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int levelTime = 375;

    private float timeElapsed = 0;
    private TMP_Text timeText;
    
    // Start is called before the first frame update
    void Start()
    {
        timeText = GameObject.Find("Time").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (Mathf.Floor(timeElapsed) == 1)
        {
            timeElapsed = 0f;
            timeText.text = $"Time\n{--levelTime}";
        }
    }
}
