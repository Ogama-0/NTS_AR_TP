using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ar_Manager : MonoBehaviour
{
    public void ReturnToMenu()
    {

        SceneManager.LoadSceneAsync(0);
        Debug.Log("Go To Menu");
    }
}
