using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController<T>
{
    public T state;

    public StateController(T state)
    {
        this.state = state;
    }

    public UnitState<T>.OnEnter onEnter;
    public UnitState<T>.OnExit onExit;
    public UnitState<T>.OnUpdateState onUpdateState;
    public UnitState<T>.EnableChangeState enableChangeState;
}

public class UnitState<T>
{
    public T CurrentState { get; private set; }

    public delegate void OnEnter(T from_state);
    public delegate void OnExit(T to_state);
    public delegate void OnUpdateState(T current_state, float time);
    public delegate bool EnableChangeState(T to_state);

    private Dictionary<T, StateController<T>> unitStateDictionary = new Dictionary<T, StateController<T>>();

    public UnitState()
    {

    }

    private void DefaultEnterState(T from_state)
    {

    }

    private void DefaultExitState(T to_state)
    {

    }

    private void DefaultUpdateState(T current_state, float time)
    {

    }

    private bool DefaultEnableChangeState(T to_state)
    {
        return true;
    }

    public void RegisterState(T state, OnEnter onEnter = null, OnExit onExit = null, OnUpdateState onUpdateState = null, EnableChangeState enableChangeState = null)
    {
        if (onEnter == null)
        {
            onEnter = this.DefaultEnterState;
        }
        if (onExit == null)
        {
            onExit = this.DefaultExitState;
        }
        if (onUpdateState == null)
        {
            onUpdateState = this.DefaultUpdateState;
        }
        if (enableChangeState == null)
        {
            enableChangeState = this.DefaultEnableChangeState;
        }

        StateController<T> stateController = new StateController<T>(state)
        {
            onEnter = onEnter,
            onUpdateState = onUpdateState,
            onExit = onExit,
            enableChangeState = enableChangeState
        };

        if (this.CurrentState == null)
        {
            CurrentState = state;
        }

        this.unitStateDictionary[state] = stateController;
    }

    public void UnRegisterState(T state)
    {
        if (this.unitStateDictionary.ContainsKey(state))
        {
            this.unitStateDictionary.Remove(state);
        }
        if (this.CurrentState.Equals(state))
        {
            this.CurrentState = default;
        }
    }

    public void UpdateState(float time = 0f)
    {
        var current_state_controller = this.unitStateDictionary[this.CurrentState];
        current_state_controller.onUpdateState(this.CurrentState, time);
    }

    public bool IsChangeState(T to_state)
    {
        if (this.unitStateDictionary.ContainsKey(to_state) == false)
        {
            return false;
        }

        var previous_state_controller = this.unitStateDictionary[this.CurrentState];
        var next_state_controller = this.unitStateDictionary[to_state];

        if (previous_state_controller.enableChangeState(to_state) == false)
        {
            return false;
        }

        previous_state_controller.onExit(to_state);
        this.CurrentState = to_state;
        next_state_controller.onEnter(previous_state_controller.state);
        return true;
    }
}
