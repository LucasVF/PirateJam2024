using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public virtual void DisplayView()
    {
        ChangeViewDisplay(true);
    }

    public virtual void HideView()
    {
        ChangeViewDisplay(false);
    }

    protected void ChangeViewDisplay(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public abstract void SetUpView();
}
