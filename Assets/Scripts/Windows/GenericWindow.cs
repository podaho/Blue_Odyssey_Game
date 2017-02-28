using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GenericWindow : MonoBehaviour {

    public GameObject firstSelectedP1;
    public GameObject firstSelectedP2;
    public GameObject startGame;
    public static WindowManager manager;


    protected EventSystem eventSystem
    {
        get
        {
            return GameObject.Find("EventSystem").GetComponent<EventSystem>();
        }
    }

    public void OnFocus()
    {
        eventSystem.SetSelectedGameObject(firstSelectedP1);
    }

    protected void Display(bool value)
    {
        gameObject.SetActive(value);
    }

    public virtual void Open()
    {
        Display(true);
        OnFocus();
    }

    public void Close()
    {
        Display(false);
    }
	
	protected virtual void Awake () {
        Close();
	}
}
