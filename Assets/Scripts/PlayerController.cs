using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f, runForce = 10f, turboForce = 15f, maxRunSpeed = 100f;
    public GameObject explosion, turboTrail;

    private ParticleSystem _turboTrail;
    private Animator _animator;
    private float _height;
    private Rigidbody _rigidbody;
    private Camera _camera;
    private TMP_Text _coinsText, _scoreText;
    private int _coins = 0, _score;
    private bool _jumpQueued = false, _isJumping = false;
    private float _timeAfterJump = 0;

    private const float TIME_WAIT_AFTER_JUMP = 0.25f;
    
    // Start is called before the first frame update
    void Start()
    {
        _turboTrail = Instantiate(turboTrail, transform).GetComponent<ParticleSystem>();
        _turboTrail.Clear();
        _animator = GetComponent<Animator>();
        _height = GetComponent<Collider>().bounds.extents.y;
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
        _coinsText = GameObject.Find("Coins").GetComponent<TMP_Text>();
        _scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeAfterJump > 0)
        {
            _timeAfterJump -= Mathf.Max(Time.deltaTime, 0f);
        }

        if (_isJumping)
        {
            if (_timeAfterJump <= 0 &&
                Physics.Raycast(transform.position, Vector3.down, _height + 0.01f))
            {
                _isJumping = false;
                _animator.SetBool("Jumping", false);
            } 
            else if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit,_height + 1f))
            {
                if (hit.collider.gameObject && hit.collider.gameObject.CompareTag("Brick"))
                {
                    var ex = Instantiate(explosion, hit.transform.position, Quaternion.identity);
                    ex.GetComponent<ParticleSystem>().Play();
                    
                    Destroy(hit.collider.gameObject);
                    _coinsText.text = $"{++_coins:00}";
                    _score += 100;
                    _scoreText.text = $"Mario\n{_score:00000}";
                }
            }
        }

        float axis = Input.GetAxis("Horizontal");
        _rigidbody.AddForce(Vector3.right * axis * runForce, ForceMode.Force);
        
        if (Input.GetKeyDown(KeyCode.Space) || _jumpQueued)
        {
            if (!_isJumping)
            {
                _rigidbody.AddForce(Vector3.up * (_jumpQueued ? jumpForce * 1.2f : jumpForce), ForceMode.Impulse);
                _timeAfterJump = TIME_WAIT_AFTER_JUMP;
                _isJumping = true;
                _jumpQueued = false;
                _animator.SetBool("Jumping", true);
            } else if (!_jumpQueued)
            {
                _jumpQueued = true;
            }
        }

        if (Mathf.Abs(_rigidbody.velocity.x) > maxRunSpeed)
        {
            float x = maxRunSpeed * Mathf.Sign(_rigidbody.velocity.x);
            _rigidbody.velocity = new Vector3(x, _rigidbody.velocity.y, 0f);
        }

        if (Mathf.Abs(axis) < 0.1f)
        {
            float x = _rigidbody.velocity.x * (1f - Time.deltaTime * 5f);
            _rigidbody.velocity = new Vector3(x, _rigidbody.velocity.y, 0f);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            _animator.SetBool("Running", true);
            _turboTrail.Play();
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) ||
                 Input.GetKeyUp(KeyCode.RightShift) ||
                 _rigidbody.velocity.magnitude < 0.1f)
        {
            _animator.SetBool("Running", false);
            _turboTrail.Pause();
            _turboTrail.Clear();
        }
        
        _animator.SetFloat("Speed", _rigidbody.velocity.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            Debug.Log("You fell in the water and died!");
            SceneManager.LoadScene("LevelParser");
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("You Won!!!!!!!!!!!!!!!!");
            SceneManager.LoadScene("LevelParser");
        }
    }

}
