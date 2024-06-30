using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _touchPositionAction = _playerInput.actions["TouchPosition"];
        _touchPressAction = _playerInput.actions["TouchPress"];

        Debug.Log("_touchPositionAction " + _touchPositionAction);
        Debug.Log("_touchPressAction " + _touchPressAction);
    }

    private void OnEnable()
    {
        _touchPressAction.performed += TouchPressed;
    }
    
    private void OnDisable()
    {
        _touchPressAction.performed -= TouchPressed;
    }
    
    private void TouchPressed(InputAction.CallbackContext context)
    {
        Debug.Log("touched");
        // if (!GameManager.isRunning) return;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(_touchPositionAction.ReadValue<Vector2>());
        Vector2 touchPosition = new Vector2(worldPoint.x, worldPoint.y);
    
        int layerMask = LayerMask.GetMask("OpenTile"); // İlgilenilen layer'ın adını "OpenTile" olarak varsayıyoruz
    
        Collider2D hit = Physics2D.OverlapPoint(touchPosition, layerMask);
    
        if (hit != null)
        {
            TileObject tile = hit.GetComponent<TileObject>();
            if (tile != null)
            {
                MatchingArea.Instance.JoinEmptySlot(tile);
            }
        }
    }
}
