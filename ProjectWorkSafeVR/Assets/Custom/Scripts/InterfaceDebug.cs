using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InterfaceDebug : MonoBehaviour
{
	private const string DISPLAY_TEXT_FORMAT = "{0} msf\n({1} FPS)";
	private const string MSF_FORMAT = "#.#";
	private const float MS_PER_SEC = 1000f;
	private float fps = 60;

    public Text debugText;
    private int i = 0;

    void LateUpdate()
    {
        if (Input.anyKey)
        {
            i++;
        }

		debugText.text = "Any Input: " + i;
        
        var axisH = Input.GetAxisRaw("Horizontal");
        var axisV = Input.GetAxisRaw("Vertical");
        debugText.text += "\nAxisH: " + axisH + " AxisV: " + axisV;

		float interp = Time.deltaTime / (0.5f + Time.deltaTime);
		float currentFPS = 1.0f / Time.deltaTime;
		fps = Mathf.Lerp(fps, currentFPS, interp);
		float msf = MS_PER_SEC / fps;
		debugText.text += "\n" + string.Format(DISPLAY_TEXT_FORMAT,
			msf.ToString(MSF_FORMAT), Mathf.RoundToInt(fps));
    }
}
