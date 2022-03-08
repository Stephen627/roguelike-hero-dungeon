using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {
    public static UnitManager Instance;

    private void Awake() {
        UnitManager.Instance = this;
    }
}
