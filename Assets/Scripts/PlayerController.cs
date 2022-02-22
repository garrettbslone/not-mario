using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray pos = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit;
            if (Physics.Raycast(pos, out hit))
            {
                Debug.DrawRay(pos.origin, pos.direction * 100, Color.yellow);
                Debug.Log($"Hit: {hit.collider.gameObject.name}");

                if (hit.collider.gameObject)
                {
                    GameObject.Destroy(hit.collider.gameObject);
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
