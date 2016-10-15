using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VrMenuCanvas : MonoBehaviour
{
    public Transform cameraParent;
    public Transform menuParent;
    private Vector3 localPosition;
    private Vector3 localScale;
    private Vector3 localRotation;

    void Start()
    {
        localPosition = transform.localPosition;
        localScale = transform.localScale;
        localRotation = transform.transform.localEulerAngles;

        Hide();
    }

    public void Show()
    {
        transform.SetParent(menuParent);
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        transform.SetParent(cameraParent);
        transform.localPosition = localPosition;
        transform.localScale = localScale;
        transform.localEulerAngles = localRotation;
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
