using UnityEngine;
using UnityEngine.UI;

public class UiButtonLagrangeSpot : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
        GetComponent<Button>().onClick.AddListener(OpenTwitter);
    }

    void OpenTwitter() => Application.OpenURL("https://twitter.com/LagrangeSpot");
}