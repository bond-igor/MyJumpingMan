using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private PlayerScore _playerScore;

    public void Start()
    {
        _playerScore = GameObject.Find("Player").GetComponent<PlayerScore>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            _playerScore.AddScore();
        }
    }
}
