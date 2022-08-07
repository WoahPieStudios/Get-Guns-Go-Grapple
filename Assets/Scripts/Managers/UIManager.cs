using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager :  Singleton<UIManager>
{
    
    [SerializeField]
    private HashSet<CanvasController> canvasSets = new HashSet<CanvasController>();

    void Start()
    {
        AddCanvas();
        ChangeCanvas(StringManager.MAINMENU);
    }

    //call the name of the canvas using StringManager to prevent from mispelled names
    public void ChangeCanvas(string name)
    {
        //looping the hashset for enabling the canvas being called
        foreach(var canvas in canvasSets)
        {
            canvas.gameObject.SetActive(false);
            if (canvas.canvasName == name)
            {
                canvas.OnCanvas();
                return;
            }
        }
    }


    private void AddCanvas()
    {
        //clears all current canvas inside the hashsets
        canvasSets.Clear();

        CanvasController[] canvasControllers = GetComponentsInChildren<CanvasController>();

        foreach (var canvasController in canvasControllers)
        {
            canvasController.OffCanvas();
            canvasSets.Add(canvasController);
            Debug.Log($"canvas :{canvasController.name} added");
        }
    }

    //clear Hashsets when game object is destroyed
    private void OnDestroy()
    {
        canvasSets.Clear();
    }
}
