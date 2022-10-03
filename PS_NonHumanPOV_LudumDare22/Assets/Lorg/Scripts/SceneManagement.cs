using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] string gameSceneName;
    void Update()
    {
        if(Input.anyKey)
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
