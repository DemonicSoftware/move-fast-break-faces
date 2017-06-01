using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Wincondition : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("GameContoller").GetComponent<GameController>().PlayerWon();
        
    }
}
