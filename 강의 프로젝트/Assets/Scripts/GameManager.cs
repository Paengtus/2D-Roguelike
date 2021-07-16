using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager intstance = null;

    BoardManager boardScript;

    int level = 3;
    void Awake()
    {
        if (intstance == null)
            intstance = this;
        else if (intstance != null)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

        boardScript = this.gameObject.GetComponent<BoardManager>();

        boardScript.SetupScene(level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
