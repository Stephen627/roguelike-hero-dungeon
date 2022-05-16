using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private GameObject selectedHeroObject, tileObject, tileUnitObject;
    [SerializeField] private GameObject Moves;

    private void Awake()
    {
        MenuManager.Instance = this;
    }

    public void ShowTileInfo(Tile tile)
    {
        if (tile == null) {
            this.tileObject.SetActive(false);
            this.tileUnitObject.SetActive(false);
            return;
        }

        this.tileObject.GetComponentInChildren<Text>().text = tile.TileName;
        this.tileObject.SetActive(true);

        if (tile.OccupiedUnit) {
            this.tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
            this.tileUnitObject.SetActive(true);
        }
    }

    public void ShowSelectedHero(BaseHero hero)
    {
        if (hero == null) {
            this.selectedHeroObject.SetActive(false);
            return;
        }

        this.selectedHeroObject.GetComponentInChildren<Text>().text = hero.UnitName;
        this.selectedHeroObject.SetActive(true);
    }

    public void ShowMoves(BaseHero hero)
    {
        this.Moves.gameObject.SetActive(true);
        MoveButton[] buttons = this.Moves.GetComponentsInChildren<MoveButton>();
        if (buttons.Length != hero.Moves.Length)
            throw new System.Exception("Incorrect number of moves");

        for (int i = 0; i < hero.Moves.Length; i++) {
            Move move = hero.Moves[i];
            MoveButton button = buttons[i];

            button.SetMove(move);
        }
    }
}
