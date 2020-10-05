using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Diamond : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Player") { 
            GameManager.Instance.AddScore();
            Destroy(this.gameObject);
            if (GameManager.Instance.GetCurrentLevelName() == "TutorialLevel") GameManager.Instance.WinGame();
        }
    }
}
