using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour {

    public void OnLobbySingleClick(Touch touch)
    {
        Debug.Log("Click Working!");
    }

    public void SetInGaze(bool isin)
    {
        if (isin)
        {
            TouchListener.OnSingleClick += OnLobbySingleClick;
        }
        else
        {
            TouchListener.OnSingleClick -= OnLobbySingleClick;
        }
    }
}
