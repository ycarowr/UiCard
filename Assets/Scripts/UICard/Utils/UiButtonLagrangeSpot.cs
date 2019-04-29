using UnityEngine;
using UnityEngine.UI;

public class UiButtonLagrangeSpot : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        GetComponent<Button>().onClick.AddListener(OpenTwitter);
    }

    private void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/LagrangeSpot");
    }
}