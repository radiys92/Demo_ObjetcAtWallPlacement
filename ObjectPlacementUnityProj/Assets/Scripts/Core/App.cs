using UnityEngine;

public class App
{
    private App() {}
    private static App _instance;
    public static App Instance
    {
        get { return _instance ?? (_instance = new App()); }
    }

    public GameObject SelectedObject;
    public GameObject SelectedPrefab;
}
