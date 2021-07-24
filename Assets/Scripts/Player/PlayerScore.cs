using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField]
    private Text _scoreCount;
    private int _playerScore = 0;

    public void AddScore(int score = 1)
    {
        _playerScore += score;
        _scoreCount.text = _playerScore.ToString();
        Debug.Log(_playerScore);
    }
}
