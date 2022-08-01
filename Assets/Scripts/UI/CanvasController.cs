using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private string _canvasName;
    public string canvasName
    {
        get { return _canvasName; }
    }

    public CanvasController(string name)
    {
        _canvasName = name;
    }

    public void OnCanvas()
    {
        this.gameObject.SetActive(true);
    }

    public void OffCanvas()
    {
        this.gameObject.SetActive(false);
    }
}
