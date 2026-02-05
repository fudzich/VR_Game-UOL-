using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScoreManager : MonoBehaviour
{

    [SerializeField] private TextMeshPro scoreText;

    void Start()
    {
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null){
            int score = DataHolder.points;
            scoreText.text = score.ToString();
        }
    }
    
}
