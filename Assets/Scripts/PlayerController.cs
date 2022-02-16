using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse button pressed");
            Vector3 pos = Input.mousePosition, dir = new Vector3(0f, 1f, 0f);
            
            RaycastHit hit;
            if (Physics.Raycast(pos, dir, out hit))
            {
                Debug.DrawRay(pos, dir * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(pos, dir  * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
    }
    
}
