using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    // Properties

    private Body Body { get; set; }
    private Vector3 CurrentScreenTouchPoint { get; set; }
    private CinemachineVirtualCamera Eye { get; set; }
    private FingerOfGod Finger { get; set; }
    private bool Released { get; set; }


    // Unity

    private void Awake()
    {
        SetComponents();
    }

    private void OnDisable() {
        Finger.OnPerformedHoldEvent -= HoldPerformed;
        Finger.OnPerformedPositionEvent -= PositionPerformed;
        Finger.OnPerformedReleaseEvent -= HoldReleased;
        // Finger.OnStartHoldEvent -= HoldStart;
    }

    private void OnEnable() {
        Finger.OnPerformedHoldEvent += HoldPerformed;
        Finger.OnPerformedPositionEvent += PositionPerformed;
        Finger.OnPerformedReleaseEvent += HoldReleased;
        // Finger.OnStartHoldEvent += HoldStart;
    }

    private void Start() {
        Eye = Space.Instance.eyeOfGod;
    }

    // Public

    public void HoldPerformed(Vector2 position, float time)
    {
        // The user has pressed against the screen longer than 0.5 seconds.  However, the "Hold" action
        // then completes (it does not wait until the user releases, as one might expect...)

        Released = false;
        StartCoroutine(WhileHolding(position));
    }

    public void HoldReleased(Vector2 position, float time)
    {
        Vector3 screenCoordinates = new Vector3(position.x, position.y, Camera.main.nearClipPlane);
        Released = true;
    }

    public void HoldStart(Vector2 position, float time)
    {
        // May use as a "Press" method eventually...
    }

    public void PositionPerformed(Vector2 position, float time)
    {
        Vector3 screenCoordinates = new Vector3(position.x, position.y, Camera.main.nearClipPlane);
        int space = LayerMask.GetMask("Space");
        int interactable = LayerMask.GetMask("Interactable");

        Ray moveBody = Camera.main.ScreenPointToRay(screenCoordinates);
        if (Physics.Raycast(moveBody, out RaycastHit cameraHit, Mathf.Infinity, space)) {
            CurrentScreenTouchPoint = new Vector3(cameraHit.point.x, 0, cameraHit.point.z);
        }

        Ray select = Camera.main.ScreenPointToRay(screenCoordinates);
        if (Physics.Raycast(select, out RaycastHit selectHit, Mathf.Infinity, interactable)) {
            GameObject hitObject = selectHit.collider.gameObject;

            if (hitObject != null) {
                Body = hitObject.GetComponent<Body>();
            }
        }
    }


    // Private

    private IEnumerator WhileHolding(Vector2 position)
    {
        float smoothSpeed = 1.5f;
        Vector3 smoothedPosition = new Vector3();
        Vector3 direction = new Vector3();
        Vector3 target = new Vector3();

        while (!Released) {
            if (Body != null) {
                direction = CurrentScreenTouchPoint - Body.transform.position;
                target = CurrentScreenTouchPoint + (direction.normalized * 2);
                smoothedPosition = Vector3.Lerp(Body.transform.position, target, smoothSpeed * Time.deltaTime);
                Body.transform.position = smoothedPosition;
            }

            yield return null;
        }

        Body = null;
    }

    private void SetComponents()
    {
        Finger = FingerOfGod.Instance;
        Released = false;
    }
}
