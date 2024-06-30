using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixer : MonoBehaviour
{
    private void Start()
    {
        float rat = (float)Screen.width / (float)Screen.height;

        if (rat < .37f)
        {
            Camera.main.orthographicSize += 2;
        }
        else if (rat > .70f)
        {
            Camera.main.orthographicSize -= 2;
        }
    }
}
