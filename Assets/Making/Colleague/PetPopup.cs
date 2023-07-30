using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PetPopup : MonoBehaviour 
{
    private List<GameObject> children = new List<GameObject>();
    private Action<int> oneMoreTimeAction;

    public void Initialize(PetGachaResult petgachaResult, Action<int> oneMoreTime)
    {
        foreach(GameObject child in children)
        {
            Destroy(child);
        }
        children.Clear();

    }

}

