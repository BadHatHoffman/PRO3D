using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    public delegate void touchDelegate(Vector2 position);

    public event touchDelegate touchStartEvent;
    public event touchDelegate touchStopEvent;
    public event touchDelegate touchUpdateEvent;

    TouchControls touchControls;
    bool touched = false;

    #region Singleton
    static TouchManager instance;
    static public TouchManager Instance
    {
        get { if (instance == null) { instance = FindObjectOfType<TouchManager>(); } return instance; }
    }
    #endregion

    public Vector2 position { get => touchControls.Touch.TouchPossition.ReadValue<Vector2>(); }

    private void Awake()
    {
        touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }


    void Start()
    {
        instance = this;
        touchControls.Touch.TouchPress.started += TouchPressStart;
        touchControls.Touch.TouchPress.canceled += TouchPressStop;
    }

    void Update()
    {
        if(touched)
        {
            touchUpdateEvent?.Invoke(position);
        }
    }

    void TouchPressStart(InputAction.CallbackContext context)
    {
        //print($"dez nutz {position}");
        touchStartEvent?.Invoke(position);
        touched = true;
    }

    void TouchPressStop(InputAction.CallbackContext context)
    {
        //print($"no nutz {position}");
        touchStopEvent?.Invoke(position);
        touched = false;
    }
}
