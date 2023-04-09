using Hungdv.InputController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [Header("Button Controller")]
    [SerializeField] ButtonInputHandler _btnMoveLeft;
    [SerializeField] ButtonInputHandler _btnMoveRight;
    [SerializeField] ButtonInputHandler _btnMoveUp;
    [SerializeField] ButtonInputHandler _btnMoveDown;

    private void Start()
    {
        AddButtonInputHandlerEvents();
    }

    private void AddButtonInputHandlerEvents()
    {
        if (_btnMoveLeft != null)
        {
            _btnMoveLeft.OnPointerDownEvent += () => { GameplayController.Instance.PlayerMovement(Vector2.left); };
            _btnMoveLeft.OnPointerUpEvent += () => { GameplayController.Instance.PlayerMovement(Vector2.zero); };
        }
        if (_btnMoveRight != null)
        {
            _btnMoveRight.OnPointerDownEvent += () => { GameplayController.Instance.PlayerMovement(Vector2.right); };
            _btnMoveRight.OnPointerUpEvent += () => { GameplayController.Instance.PlayerMovement(Vector2.zero); };
        }
        if (_btnMoveUp != null)
        {
            _btnMoveUp.OnPointerDownEvent += () => { GameplayController.Instance.PlayerMovement(Vector2.up); };
            _btnMoveUp.OnPointerUpEvent += () => { GameplayController.Instance.PlayerMovement(Vector2.zero); };
        }
        if (_btnMoveDown != null)
        {
            _btnMoveDown.OnPointerDownEvent += () => { GameplayController.Instance.PlayerMovement(Vector2.down); };
            _btnMoveDown.OnPointerUpEvent += () => { GameplayController.Instance.PlayerMovement(Vector2.zero); };
        }
    }

    /// <summary>
    /// Called at each Update(), it checks for various key presses
    /// </summary>
    protected virtual void HandleKeyboard()
    {
        if (Input.GetButtonDown("Pause")) { PauseButtonDown(); }
        if (Input.GetButtonUp("Pause")) { PauseButtonUp(); }
        if (Input.GetButton("Pause")) { PauseButtonPressed(); }

        if (Input.GetButtonDown("MainAction")) { MainActionButtonDown(); }
        if (Input.GetButtonUp("MainAction")) { MainActionButtonUp(); }
        if (Input.GetButton("MainAction")) { MainActionButtonPressed(); }

        if (Input.GetButtonDown("Left")) { LeftButtonDown(); }
        if (Input.GetButtonUp("Left")) { LeftButtonUp(); }
        if (Input.GetButton("Left")) { LeftButtonPressed(); }

        if (Input.GetButtonDown("Right")) { RightButtonDown(); }
        if (Input.GetButtonUp("Right")) { RightButtonUp(); }
        if (Input.GetButton("Right")) { RightButtonPressed(); }

        if (Input.GetButtonDown("Up")) { UpButtonDown(); }
        if (Input.GetButtonUp("Up")) { UpButtonUp(); }
        if (Input.GetButton("Up")) { UpButtonPressed(); }

        if (Input.GetButtonDown("Down")) { DownButtonDown(); }
        if (Input.GetButtonUp("Down")) { DownButtonUp(); }
        if (Input.GetButton("Down")) { DownButtonPressed(); }

    }

    /// PAUSE BUTTON ----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Triggered once when the pause button is pressed down
    /// </summary>
    public virtual void PauseButtonDown() { }
    /// <summary>
    /// Triggered once when the pause button is released
    /// </summary>
    public virtual void PauseButtonUp() { }
    /// <summary>
    /// Triggered while the pause button is being pressed
    /// </summary>
    public virtual void PauseButtonPressed() { }


    /// MAIN ACTION BUTTON ----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Triggered once when the main action button is pressed down
    /// </summary>
    public virtual void MainActionButtonDown()
    {

    }

    /// <summary>
    /// Triggered once when the main action button button is released
    /// </summary>
    public virtual void MainActionButtonUp()
    {

    }

    /// <summary>
    /// Triggered while the main action button button is being pressed
    /// </summary>
    public virtual void MainActionButtonPressed()
    {

    }



    /// LEFT BUTTON ----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Triggered once when the left button is pressed down
    /// </summary>
    public virtual void LeftButtonDown()
    {

    }

    /// <summary>
    /// Triggered once when the left button is released
    /// </summary>
    public virtual void LeftButtonUp()
    {

    }

    /// <summary>
    /// Triggered while the left button is being pressed
    /// </summary>
    public virtual void LeftButtonPressed()
    {

    }


    /// RIGHT BUTTON ----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Triggered once when the right button is pressed down
    /// </summary>
    public virtual void RightButtonDown()
    {

    }

    /// <summary>
    /// Triggered once when the right button is released
    /// </summary>
    public virtual void RightButtonUp()
    {

    }

    /// <summary>
    /// Triggered while the right button is being pressed
    /// </summary>
    public virtual void RightButtonPressed()
    {

    }



    /// DOWN BUTTON ----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Triggered once when the down button is pressed down
    /// </summary>
    public virtual void DownButtonDown()
    {

    }

    /// <summary>
    /// Triggered once when the down button is released
    /// </summary>
    public virtual void DownButtonUp()
    {

    }

    /// <summary>
    /// Triggered while the down button is being pressed
    /// </summary>
    public virtual void DownButtonPressed()
    {

    }



    /// UP BUTTON ----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Triggered once when the up button is pressed down
    /// </summary>
    public virtual void UpButtonDown()
    {

    }

    /// <summary>
    /// Triggered once when the up button is released
    /// </summary>
    public virtual void UpButtonUp()
    {

    }

    /// <summary>
    /// Triggered while the up button is being pressed
    /// </summary>
    public virtual void UpButtonPressed()
    {

    }
}
