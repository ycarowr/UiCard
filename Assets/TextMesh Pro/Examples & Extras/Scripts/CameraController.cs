using UnityEngine;

namespace TMPro.Examples
{
    public class CameraController : MonoBehaviour
    {
        public enum CameraModes
        {
            Follow,
            Isometric,
            Free
        }

        // Controls for Touches on Mobile devices
        //private float prev_ZoomDelta;


        private const string event_SmoothingValue = "Slider - Smoothing Value";
        private const string event_FollowDistance = "Slider - Camera Zoom";

        public CameraModes CameraMode = CameraModes.Follow;

        public Transform CameraTarget;

        private Transform cameraTransform;

        private Vector3 currentVelocity = Vector3.zero;
        private Vector3 desiredPosition;
        private Transform dummyTarget;

        public float ElevationAngle = 30.0f;

        public float FollowDistance = 30.0f;
        public float MaxElevationAngle = 85.0f;
        public float MaxFollowDistance = 100.0f;
        public float MinElevationAngle = 0f;
        public float MinFollowDistance = 2.0f;
        private float mouseWheel;
        private float mouseX;
        private float mouseY;

        public bool MovementSmoothing = true;

        public float MovementSmoothingValue = 25f;

        public float MoveSensitivity = 2.0f;
        private Vector3 moveVector;

        public float OrbitalAngle;
        private bool previousSmoothing;
        public bool RotationSmoothing = false;
        public float RotationSmoothingValue = 5.0f;


        private void Awake()
        {
            if (QualitySettings.vSyncCount > 0)
                Application.targetFrameRate = 60;
            else
                Application.targetFrameRate = -1;

            if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
                Input.simulateMouseWithTouches = false;

            cameraTransform = transform;
            previousSmoothing = MovementSmoothing;
        }


        // Use this for initialization
        private void Start()
        {
            if (CameraTarget == null)
            {
                // If we don't have a target (assigned by the player, create a dummy in the center of the scene).
                dummyTarget = new GameObject("Camera Target").transform;
                CameraTarget = dummyTarget;
            }
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            GetPlayerInput();


            // Check if we still have a valid target
            if (CameraTarget != null)
            {
                if (CameraMode == CameraModes.Isometric)
                    desiredPosition = CameraTarget.position + Quaternion.Euler(ElevationAngle, OrbitalAngle, 0f) *
                                      new Vector3(0, 0, -FollowDistance);
                else if (CameraMode == CameraModes.Follow)
                    desiredPosition = CameraTarget.position + CameraTarget.TransformDirection(
                                          Quaternion.Euler(ElevationAngle, OrbitalAngle, 0f) *
                                          new Vector3(0, 0, -FollowDistance));

                if (MovementSmoothing)
                    cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, desiredPosition,
                        ref currentVelocity, MovementSmoothingValue * Time.fixedDeltaTime);
                else
                    cameraTransform.position = desiredPosition;

                if (RotationSmoothing)
                    cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation,
                        Quaternion.LookRotation(CameraTarget.position - cameraTransform.position),
                        RotationSmoothingValue * Time.deltaTime);
                else
                    cameraTransform.LookAt(CameraTarget);
            }
        }


        private void GetPlayerInput()
        {
            moveVector = Vector3.zero;

            // Check Mouse Wheel Input prior to Shift Key so we can apply multiplier on Shift for Scrolling
            mouseWheel = Input.GetAxis("Mouse ScrollWheel");

            float touchCount = Input.touchCount;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || touchCount > 0)
            {
                mouseWheel *= 10;

                if (Input.GetKeyDown(KeyCode.I))
                    CameraMode = CameraModes.Isometric;

                if (Input.GetKeyDown(KeyCode.F))
                    CameraMode = CameraModes.Follow;

                if (Input.GetKeyDown(KeyCode.S))
                    MovementSmoothing = !MovementSmoothing;


                // Check for right mouse button to change camera follow and elevation angle
                if (Input.GetMouseButton(1))
                {
                    mouseY = Input.GetAxis("Mouse Y");
                    mouseX = Input.GetAxis("Mouse X");

                    if (mouseY > 0.01f || mouseY < -0.01f)
                    {
                        ElevationAngle -= mouseY * MoveSensitivity;
                        // Limit Elevation angle between min & max values.
                        ElevationAngle = Mathf.Clamp(ElevationAngle, MinElevationAngle, MaxElevationAngle);
                    }

                    if (mouseX > 0.01f || mouseX < -0.01f)
                    {
                        OrbitalAngle += mouseX * MoveSensitivity;
                        if (OrbitalAngle > 360)
                            OrbitalAngle -= 360;
                        if (OrbitalAngle < 0)
                            OrbitalAngle += 360;
                    }
                }

                // Get Input from Mobile Device
                if (touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    var deltaPosition = Input.GetTouch(0).deltaPosition;

                    // Handle elevation changes
                    if (deltaPosition.y > 0.01f || deltaPosition.y < -0.01f)
                    {
                        ElevationAngle -= deltaPosition.y * 0.1f;
                        // Limit Elevation angle between min & max values.
                        ElevationAngle = Mathf.Clamp(ElevationAngle, MinElevationAngle, MaxElevationAngle);
                    }


                    // Handle left & right 
                    if (deltaPosition.x > 0.01f || deltaPosition.x < -0.01f)
                    {
                        OrbitalAngle += deltaPosition.x * 0.1f;
                        if (OrbitalAngle > 360)
                            OrbitalAngle -= 360;
                        if (OrbitalAngle < 0)
                            OrbitalAngle += 360;
                    }
                }

                // Check for left mouse button to select a new CameraTarget or to reset Follow position
                if (Input.GetMouseButton(0))
                {
                    RaycastHit hit;
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 300, (1 << 10) | (1 << 11) | (1 << 12) | (1 << 14)))
                    {
                        if (hit.transform == CameraTarget)
                        {
                            // Reset Follow Position
                            OrbitalAngle = 0;
                        }
                        else
                        {
                            CameraTarget = hit.transform;
                            OrbitalAngle = 0;
                            MovementSmoothing = previousSmoothing;
                        }
                    }
                }


                if (Input.GetMouseButton(2))
                {
                    if (dummyTarget == null)
                    {
                        // We need a Dummy Target to anchor the Camera
                        dummyTarget = new GameObject("Camera Target").transform;
                        dummyTarget.position = CameraTarget.position;
                        dummyTarget.rotation = CameraTarget.rotation;
                        CameraTarget = dummyTarget;
                        previousSmoothing = MovementSmoothing;
                        MovementSmoothing = false;
                    }
                    else if (dummyTarget != CameraTarget)
                    {
                        // Move DummyTarget to CameraTarget
                        dummyTarget.position = CameraTarget.position;
                        dummyTarget.rotation = CameraTarget.rotation;
                        CameraTarget = dummyTarget;
                        previousSmoothing = MovementSmoothing;
                        MovementSmoothing = false;
                    }


                    mouseY = Input.GetAxis("Mouse Y");
                    mouseX = Input.GetAxis("Mouse X");

                    moveVector = cameraTransform.TransformDirection(mouseX, mouseY, 0);

                    dummyTarget.Translate(-moveVector, Space.World);
                }
            }

            // Check Pinching to Zoom in - out on Mobile device
            if (touchCount == 2)
            {
                var touch0 = Input.GetTouch(0);
                var touch1 = Input.GetTouch(1);

                var touch0PrevPos = touch0.position - touch0.deltaPosition;
                var touch1PrevPos = touch1.position - touch1.deltaPosition;

                var prevTouchDelta = (touch0PrevPos - touch1PrevPos).magnitude;
                var touchDelta = (touch0.position - touch1.position).magnitude;

                var zoomDelta = prevTouchDelta - touchDelta;

                if (zoomDelta > 0.01f || zoomDelta < -0.01f)
                {
                    FollowDistance += zoomDelta * 0.25f;
                    // Limit FollowDistance between min & max values.
                    FollowDistance = Mathf.Clamp(FollowDistance, MinFollowDistance, MaxFollowDistance);
                }
            }

            // Check MouseWheel to Zoom in-out
            if (mouseWheel < -0.01f || mouseWheel > 0.01f)
            {
                FollowDistance -= mouseWheel * 5.0f;
                // Limit FollowDistance between min & max values.
                FollowDistance = Mathf.Clamp(FollowDistance, MinFollowDistance, MaxFollowDistance);
            }
        }
    }
}