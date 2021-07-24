using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLive : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidBody2D;
    private PlayerMovement _playerMovement;

    private enum MovementState { Idle, Running, Jumping, Falling }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void Hit()
    {
        Debug.Log("Hit");
        _animator.SetTrigger("Hit");
        _rigidBody2D.bodyType = RigidbodyType2D.Static;
        _playerMovement.FreezePlayer(true);
        //_rigidBody2D.velocity = new Vector2(0, -1f);
        //Destroy(this.gameObject);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
