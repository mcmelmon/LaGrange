// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/InputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""Space"",
            ""id"": ""60da0a9f-861e-4427-90b8-2768a9454e72"",
            ""actions"": [
                {
                    ""name"": ""Position"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ee6fdfa5-e7f2-48a7-b045-f040418013c8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hold"",
                    ""type"": ""PassThrough"",
                    ""id"": ""64690e8c-b7d5-4212-a040-05b2a2733712"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press""
                },
                {
                    ""name"": ""Release"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8535119a-3901-40df-9eb0-91880d4b9f12"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c679fa4b-3ce1-4e15-b0d9-6e3930651301"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b704570-aef2-4af4-8f78-9da5597043e0"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2394e65-e95b-4611-a3bc-e72f9a697b5f"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Space
        m_Space = asset.FindActionMap("Space", throwIfNotFound: true);
        m_Space_Position = m_Space.FindAction("Position", throwIfNotFound: true);
        m_Space_Hold = m_Space.FindAction("Hold", throwIfNotFound: true);
        m_Space_Release = m_Space.FindAction("Release", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Space
    private readonly InputActionMap m_Space;
    private ISpaceActions m_SpaceActionsCallbackInterface;
    private readonly InputAction m_Space_Position;
    private readonly InputAction m_Space_Hold;
    private readonly InputAction m_Space_Release;
    public struct SpaceActions
    {
        private @InputControls m_Wrapper;
        public SpaceActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Position => m_Wrapper.m_Space_Position;
        public InputAction @Hold => m_Wrapper.m_Space_Hold;
        public InputAction @Release => m_Wrapper.m_Space_Release;
        public InputActionMap Get() { return m_Wrapper.m_Space; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SpaceActions set) { return set.Get(); }
        public void SetCallbacks(ISpaceActions instance)
        {
            if (m_Wrapper.m_SpaceActionsCallbackInterface != null)
            {
                @Position.started -= m_Wrapper.m_SpaceActionsCallbackInterface.OnPosition;
                @Position.performed -= m_Wrapper.m_SpaceActionsCallbackInterface.OnPosition;
                @Position.canceled -= m_Wrapper.m_SpaceActionsCallbackInterface.OnPosition;
                @Hold.started -= m_Wrapper.m_SpaceActionsCallbackInterface.OnHold;
                @Hold.performed -= m_Wrapper.m_SpaceActionsCallbackInterface.OnHold;
                @Hold.canceled -= m_Wrapper.m_SpaceActionsCallbackInterface.OnHold;
                @Release.started -= m_Wrapper.m_SpaceActionsCallbackInterface.OnRelease;
                @Release.performed -= m_Wrapper.m_SpaceActionsCallbackInterface.OnRelease;
                @Release.canceled -= m_Wrapper.m_SpaceActionsCallbackInterface.OnRelease;
            }
            m_Wrapper.m_SpaceActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Position.started += instance.OnPosition;
                @Position.performed += instance.OnPosition;
                @Position.canceled += instance.OnPosition;
                @Hold.started += instance.OnHold;
                @Hold.performed += instance.OnHold;
                @Hold.canceled += instance.OnHold;
                @Release.started += instance.OnRelease;
                @Release.performed += instance.OnRelease;
                @Release.canceled += instance.OnRelease;
            }
        }
    }
    public SpaceActions @Space => new SpaceActions(this);
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    public interface ISpaceActions
    {
        void OnPosition(InputAction.CallbackContext context);
        void OnHold(InputAction.CallbackContext context);
        void OnRelease(InputAction.CallbackContext context);
    }
}
