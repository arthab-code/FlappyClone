using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnController : MonoBehaviour
{
    private Transform parentObject;
    private Vector3 parentPosition;
    private SpriteRenderer spriteRenderer;
    private float heightColumn;
    private Vector3 cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        parentObject = GetComponentInParent<Transform>();
        parentPosition = parentObject.transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraPosition = Camera.main.transform.position;
     
        SetColumnHeight();
    }

    private void SetColumnHeight()
    {
        float cameraHeight = Camera.main.pixelHeight;
        float value = 0f;

        if (gameObject.name == "UpColumn")
            value = cameraHeight - (parentPosition.y * 100) + 0.3f;

        if (gameObject.name == "DownColumn")
            value = cameraHeight + (parentPosition.y * 100) - 0.5f;

        spriteRenderer.size = new Vector2(spriteRenderer.size.x, (value / 100));

    }


}
