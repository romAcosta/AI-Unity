/* Scripted by Omabu - omabuarts@gmail.com */

using UnityEngine;

public class RotateOnScroll : MonoBehaviour
{
    public float rotationSpeed = 2000f;

    private void Update()
    {
        var scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheelInput != 0f)
        {
            var rotationAmount = scrollWheelInput * rotationSpeed * Time.deltaTime * 10;
            transform.Rotate(Vector3.up, rotationAmount);
        }
    }
}