using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public int enemiesKilled = 0;


    public GameObject Player;
    public void KillPlayer()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }
}
