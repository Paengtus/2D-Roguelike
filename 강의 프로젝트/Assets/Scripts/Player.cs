using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int hor = 0;
        int ver = 0;

        hor = (int)Input.GetAxisRaw("Horizontal");
        ver = (int)Input.GetAxisRaw("Vertical");

        if (hor != 0) ver = 0;
        if (ver != 0) hor = 0;

        this.gameObject.GetComponent<Rigidbody2D>().position += new Vector2(hor * 0.01f, ver * 0.01f);
    }
}
