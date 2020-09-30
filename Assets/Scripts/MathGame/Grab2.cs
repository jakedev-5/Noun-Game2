using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab2 : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZcoord;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        if (gameObject.GetComponent<MathGameGamePiece>().answered == false)
        {


            mZcoord = Camera.main.WorldToScreenPoint(
                gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
        }
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZcoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        if (gameObject.GetComponent<MathGameGamePiece>().answered == false)
        {

            transform.position = GetMouseAsWorldPoint() + mOffset;
        }
    }
}
