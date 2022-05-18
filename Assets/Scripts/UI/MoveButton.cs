using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveButton : Button
{
    private Move Move;

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        ControlManager.Instance.SetSelectedMove(this.Move);
    }

    public void SetMove(Move move)
    {
        this.Move = move;
        Text text = this.GetComponentInChildren<Text>();
        text.text = move.Name;
    }
}