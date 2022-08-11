using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Start is called before the first frame update
    private void OnEnable()
    {
        //TO BE CHANGED
        InputManager.Instance.EnableMultipleActionMaps(
            new HashSet<MapType>() {
                MapType.Movement, 
                MapType.Interaction});
    }


    private void Update()
    {
        
    }
}
