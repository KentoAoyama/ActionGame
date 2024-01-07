using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PlayerStateMachine
{
    private IPlayerState _currentState;

    [SerializeField]
    private string _currenStateName;

    public void Initialized(IPlayerState state)
    {
        _currentState = state;
        state.Enter();

        _currenStateName = state.GetType().Name;
    }

    /// <summary>
    /// State��ύX����ۂɌĂт������\�b�h
    /// </summary>
    /// <param name="nextState">�ύX����State</param>
    public void TransitionState(IPlayerState nextState)
    {
        if (_currentState == nextState) return;

        _currentState.Exit();
        _currentState = nextState;
        nextState.Enter();

        _currenStateName = nextState.GetType().Name;
    }

    /// <summary>
    /// ���݂�State��Update�������s�����\�b�h
    /// </summary>
    public void Update()
    {
        if (_currentState != null)
        {
            _currentState.Update();
        }
    }
}