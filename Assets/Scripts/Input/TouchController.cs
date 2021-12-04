using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    // Properties

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
        Vector3 screenCoordinates = new Vector3(position.x, position.y, Space.Instance.eyeOfGod.transform.position.z);
        int space = LayerMask.GetMask("Space");

        Ray movePlayer = Camera.main.ScreenPointToRay(screenCoordinates);
        if (Physics.Raycast(movePlayer, out RaycastHit cameraHit, Mathf.Infinity, space)) {
            CurrentScreenTouchPoint = new Vector3(cameraHit.point.x, cameraHit.point.y, 0);
        }
    }


    // Private

    private IEnumerator WhileHolding(Vector2 position)
    {
        Vector3 target = new Vector3();

        while (!Released) {
            if (Player.Instance != null) {
                if (Vector3.Distance(CurrentScreenTouchPoint, Player.Instance.transform.position) > 2) {
                    target = (CurrentScreenTouchPoint - Player.Instance.transform.position).normalized;
                    Player.Instance.transform.rotation = Quaternion.FromToRotation(Vector3.up, target);
                    Player.Instance.Body.AddForce(target * 0.1f);
                }
            }

            yield return null;
        }
    }

    private void SetComponents()
    {
        Finger = FingerOfGod.Instance;
        Released = false;
    }
}
