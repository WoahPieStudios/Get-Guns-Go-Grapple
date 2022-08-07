using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


// Creating IDs for different ActionMaps
public enum MapType { 
None = 0,
Movement = 2,
Interaction = 4,
UI = 8,
}

public struct ActionMap {
    public MapType type;
    public InputActionMap map;

    public ActionMap(MapType type, InputActionMap map) { 
    this.type = type;
    this.map = map;
    }
}




public class InputManager : Singleton<InputManager>
{

    private PlayerInputs _playerInputs;
    private HashSet<ActionMap> _actionMapsHash = new HashSet<ActionMap> ();

    #region -= EVENTS =-
    public event Action<Vector2> onGroundMovement;
    public event Action<Vector2> onMouseMovement;

    public event Action onReload;
    public event Action onMouseShoot;

    #endregion

    private void Awake()
    {
        base.Awake();
        _playerInputs = new PlayerInputs(); //Instantiate Player Inputs
    }

    void Start()
    {

        // add all types of action maps 
        _actionMapsHash.Add(new ActionMap(MapType.Movement, _playerInputs.Movement));
        _actionMapsHash.Add(new ActionMap(MapType.Interaction, _playerInputs.Interaction));
        _actionMapsHash.Add(new ActionMap(MapType.UI, _playerInputs.UI));


        #region -= METHODS =-
        Movement(); // player movement and mouse look
        Interactions(); // other player controls appart from movement related and UI
        MyGUI(); // all UI controls for main menus and the like
        #endregion


    }


    #region -= FUNCTIONS FOR ENABLING AND DISABLING ACTION MAPS =-

    public void EnableActionMap(MapType getType)
    {
        foreach(var actionMap in _actionMapsHash)
        {
            actionMap.map.Disable();
            if (actionMap.type == getType)
            {
                actionMap.map.Enable();
                return;
            }
        }
    }


    public void EnableMultipleActionMaps(HashSet<MapType> getTypes)
    {
        //looping the _actionMapsHash to check all 
        foreach(var actionMap in _actionMapsHash)
        {
            if (getTypes.Contains(actionMap.type))
            {
                actionMap.map.Enable();
            }
            else
            {
                actionMap.map.Disable();
            }
        }
    }  
    public void DisableActionMap(MapType getType)
    {
        foreach (var actionMap in _actionMapsHash)
        {
            if (actionMap.type == getType)
            {
                actionMap.map.Disable();
                return;
            }
        }
    }
      
    public void DisableMultipleActionMaps(HashSet<MapType> getTypes)
    {
        foreach(var actionMap in _actionMapsHash)
        {
            if (getTypes.Contains(actionMap.type))
            {
                actionMap.map.Disable();
            }
        }
    }


    public void DisableAll()
    {
        foreach(var actionMap in _actionMapsHash)
        {
            actionMap.map.Disable();
        }
    }
    #endregion

    #region -= ACTIONS =-

    private void Movement()
    {
        _playerInputs.Movement.GroundMovement.performed += axis =>
        {
            Vector2 inputAxis = axis.ReadValue<Vector2>();
            onGroundMovement?.Invoke(inputAxis);
        };

        _playerInputs.Movement.GroundMovement.canceled += axis =>
        {
            Vector2 inputAxis = axis.ReadValue<Vector2>();
            onGroundMovement?.Invoke(inputAxis);
        };

        _playerInputs.Movement.MouseLook.performed += delta =>
        {
            Vector2 inputAxis = delta.ReadValue<Vector2>();
            onMouseMovement?.Invoke(inputAxis);
        };

        _playerInputs.Movement.MouseLook.canceled += delta =>
        {
            Vector2 inputAxis = delta.ReadValue<Vector2>();
            onMouseMovement?.Invoke(inputAxis);
        };
    }

    private void Interactions()
    {
        _playerInputs.Interaction.Shoot.performed += _ =>
        {

        };

        _playerInputs.Interaction.Reload.performed += _ =>
        {

        };

        _playerInputs.Interaction.Grapple.performed += _ =>
        {

        };

        _playerInputs.Interaction.PullGrapple.performed += _ =>
        {

        };

        _playerInputs.Interaction.PullGrapple.canceled += _ =>
        {

        };

        _playerInputs.Interaction.Ability.performed += _ =>
        {

        };

        _playerInputs.Interaction.Interact.performed += _ =>
        {

        };

        _playerInputs.Interaction.SlowMotion.performed += _ =>
        {

        };
    }

    private void MyGUI()
    {
        
    }

    #endregion

}
