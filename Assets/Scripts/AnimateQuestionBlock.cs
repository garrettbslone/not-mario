using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateQuestionBlock : MonoBehaviour
{
    public float scrollSpeed = 1f, scrollJump = 0.2f;

    private float _texY = 0f, _timeSince = 0f;
    private Material _material;
    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _timeSince += Time.deltaTime;

        if (_timeSince >= scrollSpeed)
        {
            _texY += scrollJump;
            _material.mainTextureOffset = new Vector2(0f, _texY);
            _timeSince = 0f;
        }
    }
}
