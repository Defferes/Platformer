using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnSceneAction : MonoBehaviour
{
    public void Reload()
    {
        MoneyData.Money = 0;
        var Scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(Scene.name);
    }
}
