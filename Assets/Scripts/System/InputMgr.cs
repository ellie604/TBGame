using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputMgr : Singleton<InputMgr>,TBGame.IGameplayActions
{
    public UnityAction<Vector2> OnMove;

    private TBGame m_TBGame;

    protected override void Awake()
    {
        base.Awake();
        if(m_TBGame==null)
        {
            m_TBGame = new TBGame();
            m_TBGame.Gameplay.SetCallbacks(this);
            m_TBGame.Enable();
        }
    }

    private void OnEnable()
    {
        m_TBGame.Enable();
    }

    private void OnDisable()
    {
        m_TBGame.Disable();
    }

    public void SetInputEnabled(bool enable)
    {
        if(enable)
        {
            m_TBGame.Enable();
        }
        else
        {
            m_TBGame.Disable();
        }
    }

    void TBGame.IGameplayActions.OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Vector2 value = context.ReadValue<Vector2>();
            OnMove?.Invoke(value);
        }
    }
}
