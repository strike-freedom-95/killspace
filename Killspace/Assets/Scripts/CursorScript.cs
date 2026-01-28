using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
        if(Application.isMobilePlatform)
            gameObject.SetActive(false);
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
