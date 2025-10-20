using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public ScoreScriptableObjects scoreData;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // pegar o score inicial e diminuir ao decorrer do tempo 
        scoreData.scorePoints -= Mathf.FloorToInt(Time.deltaTime * 3);
    }
    public void SetScoreMultiplier(int multiplier)
    {
        scoreData.scoreMultiplier = multiplier;
        scoreData.scorePoints *= multiplier; // aplicar o multiplicador ao score atual
    }

    public int GetScoreMultiplier()
    {
        return scoreData.scoreMultiplier;
    }

}
