using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject target;
    public float heightOffset = 1.5f;
    public float distance = 12.0f;
    public float maxDistance = 20.0f;
    public float minDistance = 0.5f;
    public float offsetFromWall = 0.1f;
    public float yMaxLimit = 80f;
    public float yMinLimit = -80f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public float zoomSpeed = 40.0f;
    public float autoZoomSpeed = 5.0f;
    public float autoRotationSpeed = 3.0f;
    public bool alwaysRotateBehind = false;
    public bool allowMouseInputX = true;
    public bool allowMouseInputY = true;
    public LayerMask collisionLayers = -1;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private float correctedDistance;
    private bool rotateBehind = false;
    private bool mouseSideButton = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        xDeg = angles.x;
        yDeg = angles.y;

        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;

        if (alwaysRotateBehind)
        {
            rotateBehind = true;
        }
    }

    // LateUpdate is called once per frame
    void LateUpdate()
    {
        if (GUIUtility.hotControl == 0)
        {
            rotateBehind = alwaysRotateBehind;

            // check for left or right mouse input
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                if (allowMouseInputX)
                {
                    xDeg += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                }
                else
                {
                    RotateBehindTarget();
                }

                if (allowMouseInputY)
                {
                    yDeg -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
                }
            }
            else if (rotateBehind)
            {
                RotateBehindTarget();
            }

            yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(yDeg, xDeg, 0);
            desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime * Mathf.Abs(desiredDistance);
            desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
            correctedDistance = desiredDistance;

            Vector3 targetOffset = new Vector3(0, -heightOffset, 0);
            Vector3 position = target.transform.position - (rotation * Vector3.forward * desiredDistance + targetOffset);

            RaycastHit hit;
            Vector3 trueTargetPosition = new Vector3(target.transform.position.x,
                                                     target.transform.position.y + heightOffset,
                                                     target.transform.position.z);
            bool isCorrected = false;
            if (Physics.Linecast(trueTargetPosition, position, out hit, collisionLayers))
            {
                correctedDistance = Vector3.Distance(trueTargetPosition, hit.point) - offsetFromWall;
                isCorrected = true;
            }

            if (!isCorrected || correctedDistance > currentDistance)
            {
                currentDistance = Mathf.Lerp(currentDistance, correctedDistance, autoRotationSpeed * Time.deltaTime);
            }
            else
            {
                currentDistance = correctedDistance;
            }

            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
            position = target.transform.position - (rotation * Vector3.forward * currentDistance + targetOffset);

            transform.position = position;
            transform.rotation = rotation;
        }
    }

    private void RotateBehindTarget()
    {
        float targetRotation = target.transform.eulerAngles.y;
        float currentRotation = transform.eulerAngles.y;

        xDeg = Mathf.LerpAngle(currentRotation, targetRotation, autoRotationSpeed * Time.deltaTime);

        if (targetRotation == currentRotation)
        {
            if (!alwaysRotateBehind)
            {
                rotateBehind = false;
            }
        }
        else
        {
            rotateBehind = true;
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        while (angle > 360.0f)
        {
            angle -= 360.0f;
        }

        while (angle < -360.0f)
        {
            angle += 360.0f;
        }

        return Mathf.Clamp(angle, min, max);
    }
}
