using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Camera _camera;
    private TMP_Text coinsText;
    private int coins = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
        coinsText = GameObject.Find("Coins").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray pos = _camera.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit;
            if (Physics.Raycast(pos, out hit))
            {
                Debug.DrawRay(pos.origin, pos.direction * 100, Color.yellow);
                Debug.Log($"Hit: {hit.collider.gameObject.name}");

                if (hit.collider.gameObject && hit.collider.gameObject.CompareTag("QuestionBlock"))
                {
                    GameObject.Destroy(hit.collider.gameObject);
                    coinsText.text = $"{++coins:00}";
                }
            }
            else
            {
                Debug.DrawRay(pos.origin, pos.direction * 100, Color.white);
                Debug.Log("Did not Hit");
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            // _rigidbody.velocity = Vector3.left;
            transform.position += Vector3.left;
        } 
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // _rigidbody.velocity = Vector3.right;
            transform.position += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            // _rigidbody.velocity = Vector3.up;
            transform.position += Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // _rigidbody.velocity = Vector3.up;
            transform.position += Vector3.down;
        }
        // else
        // {
        //     _rigidbody.velocity = Vector3.zero;
        // }
    }
    
}
