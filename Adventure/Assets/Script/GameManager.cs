using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int stage;
    public Transform player;

    void OnTriggerEnter(Collider other)
    {
        Player playerR = GetComponent<Player>();
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Animator>().SetTrigger("DoDie");
            Invoke("ReScene", 2f);
            playerR.isRespawn = true;
            //player.GetComponent<Animator>().SetTrigger("DoRespawn");
        }
    }
    void ReScene()
    {
        SceneManager.LoadScene(stage);

    }
}
