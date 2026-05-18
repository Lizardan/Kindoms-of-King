using UnityEngine;
using PurrNet;
using PurrNet.Modules;
using UnityEngine.SceneManagement;

public class SceneManager : NetworkBehaviour
{
    [PurrScene] public string sceneToChange;
    public void ChangeScene()
    {
        PurrSceneSettings settings = new()
        {
            isPublic = true,
            mode = LoadSceneMode.Single
        };
        networkManager.sceneModule.LoadSceneAsync(sceneToChange, settings);
    }
}
