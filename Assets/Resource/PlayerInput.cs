//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Resource/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Common"",
            ""id"": ""3ee797d0-1260-4e6b-91fb-4ba7de0da1c8"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""57e6ea92-2664-4570-a8ae-95f43409e144"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""9d51cb0a-b369-45e1-87e5-0bf200be610a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""DashRelease"",
                    ""type"": ""Button"",
                    ""id"": ""df466185-80a8-4bca-a32b-4834710b907b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""369e7c3c-4d21-49d2-a20a-d2fb03fe9799"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""AttackRelease"",
                    ""type"": ""Button"",
                    ""id"": ""03d020c6-32ad-41e7-b2ab-87426c59a086"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b7d3240c-ae43-48e9-86f0-a225828c0684"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""46d57b12-163d-43cd-b475-2e53b7094f7a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""052ed093-c235-4cb3-b271-d5cb537b1b44"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""eab7463b-d10d-4bbd-8949-03fe3197513c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""98140a20-24f6-42e4-ac74-6d78bce7aea3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ccd34bb3-9e3b-4a08-9f94-ed5650002458"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42be99b8-f022-4017-b511-0fb705f5c585"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc8c68b9-bdb1-47d5-afc9-394e4d1afb91"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DashRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2a2447f-af88-45e2-a681-a29e17dbf4e9"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Link"",
            ""id"": ""08737f2c-b156-4b81-8e1e-94974e63cb7d"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0a5a8989-fe17-4afd-9698-527dfcdd6723"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""734dc309-c7d9-47e3-9646-95b50f3fca7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d4f715bf-4d4e-4716-a150-9de131530bf4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e150ea4c-cb7c-489d-8f1b-5b44bcd2ecc1"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4dc9e791-d8da-4027-9483-fc238832d9ff"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""42275b5e-b09d-48e8-8a33-bbf46c776c28"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2dd6d08b-48d2-4839-8f9a-591d21724700"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b19c8562-57eb-44d0-86d4-4e6ba77dc8b7"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""4f7e31a0-b203-4d09-81ca-f96620d4dc53"",
            ""actions"": [
                {
                    ""name"": ""DialogNext"",
                    ""type"": ""Button"",
                    ""id"": ""e0986809-d444-4a9f-a5e3-87e4de8eac09"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d87e3acd-3779-4a1a-95ef-40b8e02ce8d7"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DialogNext"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0da27558-8947-4117-a6d6-7ed2071a515a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DialogNext"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Common
        m_Common = asset.FindActionMap("Common", throwIfNotFound: true);
        m_Common_Move = m_Common.FindAction("Move", throwIfNotFound: true);
        m_Common_Dash = m_Common.FindAction("Dash", throwIfNotFound: true);
        m_Common_DashRelease = m_Common.FindAction("DashRelease", throwIfNotFound: true);
        m_Common_Attack = m_Common.FindAction("Attack", throwIfNotFound: true);
        m_Common_AttackRelease = m_Common.FindAction("AttackRelease", throwIfNotFound: true);
        // Link
        m_Link = asset.FindActionMap("Link", throwIfNotFound: true);
        m_Link_Move = m_Link.FindAction("Move", throwIfNotFound: true);
        m_Link_Dash = m_Link.FindAction("Dash", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_DialogNext = m_UI.FindAction("DialogNext", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Common
    private readonly InputActionMap m_Common;
    private List<ICommonActions> m_CommonActionsCallbackInterfaces = new List<ICommonActions>();
    private readonly InputAction m_Common_Move;
    private readonly InputAction m_Common_Dash;
    private readonly InputAction m_Common_DashRelease;
    private readonly InputAction m_Common_Attack;
    private readonly InputAction m_Common_AttackRelease;
    public struct CommonActions
    {
        private @PlayerInput m_Wrapper;
        public CommonActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Common_Move;
        public InputAction @Dash => m_Wrapper.m_Common_Dash;
        public InputAction @DashRelease => m_Wrapper.m_Common_DashRelease;
        public InputAction @Attack => m_Wrapper.m_Common_Attack;
        public InputAction @AttackRelease => m_Wrapper.m_Common_AttackRelease;
        public InputActionMap Get() { return m_Wrapper.m_Common; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CommonActions set) { return set.Get(); }
        public void AddCallbacks(ICommonActions instance)
        {
            if (instance == null || m_Wrapper.m_CommonActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CommonActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Dash.started += instance.OnDash;
            @Dash.performed += instance.OnDash;
            @Dash.canceled += instance.OnDash;
            @DashRelease.started += instance.OnDashRelease;
            @DashRelease.performed += instance.OnDashRelease;
            @DashRelease.canceled += instance.OnDashRelease;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @AttackRelease.started += instance.OnAttackRelease;
            @AttackRelease.performed += instance.OnAttackRelease;
            @AttackRelease.canceled += instance.OnAttackRelease;
        }

        private void UnregisterCallbacks(ICommonActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Dash.started -= instance.OnDash;
            @Dash.performed -= instance.OnDash;
            @Dash.canceled -= instance.OnDash;
            @DashRelease.started -= instance.OnDashRelease;
            @DashRelease.performed -= instance.OnDashRelease;
            @DashRelease.canceled -= instance.OnDashRelease;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @AttackRelease.started -= instance.OnAttackRelease;
            @AttackRelease.performed -= instance.OnAttackRelease;
            @AttackRelease.canceled -= instance.OnAttackRelease;
        }

        public void RemoveCallbacks(ICommonActions instance)
        {
            if (m_Wrapper.m_CommonActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICommonActions instance)
        {
            foreach (var item in m_Wrapper.m_CommonActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CommonActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CommonActions @Common => new CommonActions(this);

    // Link
    private readonly InputActionMap m_Link;
    private List<ILinkActions> m_LinkActionsCallbackInterfaces = new List<ILinkActions>();
    private readonly InputAction m_Link_Move;
    private readonly InputAction m_Link_Dash;
    public struct LinkActions
    {
        private @PlayerInput m_Wrapper;
        public LinkActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Link_Move;
        public InputAction @Dash => m_Wrapper.m_Link_Dash;
        public InputActionMap Get() { return m_Wrapper.m_Link; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LinkActions set) { return set.Get(); }
        public void AddCallbacks(ILinkActions instance)
        {
            if (instance == null || m_Wrapper.m_LinkActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_LinkActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Dash.started += instance.OnDash;
            @Dash.performed += instance.OnDash;
            @Dash.canceled += instance.OnDash;
        }

        private void UnregisterCallbacks(ILinkActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Dash.started -= instance.OnDash;
            @Dash.performed -= instance.OnDash;
            @Dash.canceled -= instance.OnDash;
        }

        public void RemoveCallbacks(ILinkActions instance)
        {
            if (m_Wrapper.m_LinkActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ILinkActions instance)
        {
            foreach (var item in m_Wrapper.m_LinkActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_LinkActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public LinkActions @Link => new LinkActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_DialogNext;
    public struct UIActions
    {
        private @PlayerInput m_Wrapper;
        public UIActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @DialogNext => m_Wrapper.m_UI_DialogNext;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @DialogNext.started += instance.OnDialogNext;
            @DialogNext.performed += instance.OnDialogNext;
            @DialogNext.canceled += instance.OnDialogNext;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @DialogNext.started -= instance.OnDialogNext;
            @DialogNext.performed -= instance.OnDialogNext;
            @DialogNext.canceled -= instance.OnDialogNext;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface ICommonActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnDashRelease(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnAttackRelease(InputAction.CallbackContext context);
    }
    public interface ILinkActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnDialogNext(InputAction.CallbackContext context);
    }
}
