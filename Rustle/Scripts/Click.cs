using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetMouseButtonDown(0)&&CharacterScreen.test==0)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null && Main.ext==0)
                {
                    if ((hit.transform.name=="HAP"|| hit.transform.name == "CAP" || hit.transform.name == "PAP" || hit.transform.name == "BAP")&&!CharacterScreen.isselected)
                    {
                        CharacterScreen.itemx = hit.transform.position.x;
                        CharacterScreen.itemy = hit.transform.position.y;
                        CharacterScreen.test = 20;
                        CharacterScreen.carried = true;
                    }
                    else if (!CharacterScreen.isselected)
                    {
                        CharacterScreen.selectedx = hit.transform.position.x;
                        CharacterScreen.selectedy = hit.transform.position.y;
                        CharacterScreen.itemx = hit.transform.position.x;
                        CharacterScreen.itemy = hit.transform.position.y;
                        CharacterScreen.isselected = true;
                        CharacterScreen.test = 20;
                    }
                    else
                    {
                        CharacterScreen.item2x = hit.transform.position.x;
                        CharacterScreen.item2y = hit.transform.position.y;
                        CharacterScreen.isselected = false;
                        CharacterScreen.selectedx = -100;
                        CharacterScreen.selectedy = -100;
                        CharacterScreen.test = 20;
                        CharacterScreen.carried = true;
                    }
                }
            }
        }
    }
}
