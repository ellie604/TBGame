using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelMgr : Singleton<LevelMgr>
{
    public UnityAction<Scene> OnSceneLoadedAction;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene curScene, LoadSceneMode arg1)
    {
        Debug.Log(curScene.name);

        OnSceneLoadedAction?.Invoke(curScene);

        switch (curScene.buildIndex)
        {
            case 0:
                {
                    UIMgr.Instance.OpenPage("MainMenu");
                    break;
                }
            default:
                break;
        }
    }
}
