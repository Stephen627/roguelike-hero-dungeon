using System;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public static ControlManager Instance;
    public Move SelectedMove;
    
    private void Awake()
    {
        ControlManager.Instance = this;    
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
            this.SelectedMove = null;
    }

    public void SetSelectedMove(Move move)
    {
        this.SelectedMove = move;
    }
}