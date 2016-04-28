using UnityEngine;

public class App
{
    public GameObject SelectedObject;
    public GameObject SelectedPrefab;

    private static App _instance;
    private ScenesManager _scenesManager;

    public static App Instance
    {
        get { return _instance ?? (_instance = new App()); }
    }

    public ScenesManager ScenesManager
    {
        get { return _scenesManager ?? (_scenesManager = new ScenesManager()); }
    }

    private App()
    {
    }
}