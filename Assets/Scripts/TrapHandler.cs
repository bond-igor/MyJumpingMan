using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHandler : MonoBehaviour
{
    private PlayerLive _playerLive;

    public void Start()
    {
        _playerLive = GameObject.Find("Player").GetComponent<PlayerLive>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerLive.Hit();            
        }
    }
}
