using UnityEngine;
using UnityEngine.UI;

public class InfoPanelController : MonoBehaviour
{
    private static InfoPanelController _instance;

    public Text InfoLabel;

    void Start()
    {
        _instance = this;
        SetCaption("");
    }

    public static void SetCaption(string caption)
    {
        _instance.gameObject.SetActive(!string.IsNullOrEmpty(caption));
        _instance.InfoLabel.text = caption;
    }
}
