using UnityEngine;
using UnityEngine.UI;

public class UiButtonLagrangeSpot : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OpenTwitter);
    }

    private void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/LagrangeSpot");
    }
}