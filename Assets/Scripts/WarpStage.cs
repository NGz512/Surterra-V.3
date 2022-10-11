using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpStage : MonoBehaviour
{
    public int ILevelLoad;
    public string SLevelLoad;

    public bool useIntegerToLoadLevel = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.name == "Player")
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        if (useIntegerToLoadLevel)
        {
            SceneManager.LoadScene(ILevelLoad);
        }
        else
        {
            SceneManager.LoadScene(SLevelLoad);
        }
    }
}
