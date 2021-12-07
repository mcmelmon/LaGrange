using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class FingerOfGod : MonoBehaviour
{
    // Properties

    public static FingerOfGod Instance { get; set; }

    // Unity

    public delegate void PerformedDoubleEvent(Vector2 position, float time);
    public event PerformedDoubleEvent OnPerformedDoubleEvent;

    public delegate void PerformedHoldEvent(Vector2 position, float time);
    public event PerformedHoldEvent OnPerformedHoldEvent;

    public delegate void PerformedReleaseEvent(Vector2 position, float time);
    public event PerformedReleaseEvent OnPerformedReleaseEvent;

    public delegate void PerformedPositionEvent(Vector2 position, float time);
    public event PerformedPositionEvent OnPerformedPositionEvent;

    public delegate void StartHoldEvent(Vector2 position, float time);
    public event StartHoldEvent OnStartHoldEvent;

    InputControls controls;


    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("More than one Finger Of God instance!");
            Destroy(this);
            return;
        }
        Instance = this;
        controls = new InputControls();
    }

    private void OnDisable() {
        controls.Disable();
    }
    private void OnEnable() {
        controls.Enable();
    }

    private void Start() {
        controls.Space.Double.performed += DoublePerformed;
        controls.Space.Hold.performed += HoldPerformed;
        controls.Space.Hold.started += HoldStarted;
        controls.Space.Position.performed += PositionPerformed;
        controls.Space.Release.performed += ReleasePerformed;
    }


    // Private

    private void DoublePerformed(InputAction.CallbackContext context)
    {
        if (OnPerformedDoubleEvent != null) OnPerformedDoubleEvent(controls.Space.Position.ReadValue<Vector2>(), (float)context.startTime);
    }

    private void HoldPerformed(InputAction.CallbackContext context)
    {
        if (OnPerformedHoldEvent != null) OnPerformedHoldEvent(controls.Space.Position.ReadValue<Vector2>(), (float)context.startTime);
    }

    private void HoldStarted(InputAction.CallbackContext context)
    {
        if (OnStartHoldEvent != null) OnStartHoldEvent(controls.Space.Position.ReadValue<Vector2>(), (float)context.startTime);
    }

    private void PositionPerformed(InputAction.CallbackContext context)
    {
        if (OnPerformedPositionEvent != null) OnPerformedPositionEvent(controls.Space.Position.ReadValue<Vector2>(), (float)context.startTime);
    }

    private void ReleasePerformed(InputAction.CallbackContext context)
    {
        if (OnPerformedReleaseEvent != null) OnPerformedReleaseEvent(controls.Space.Position.ReadValue<Vector2>(), (float)context.startTime);
    }
}
