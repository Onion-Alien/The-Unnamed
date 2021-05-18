using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgPopUp : MonoBehaviour
{
// Creates damage pop up number

    public static DmgPopUp Create(Vector3 position, int damageAmount)
    {
        Transform dmgPopUpTransform = Instantiate(GameAssets.i.pfDmgPopup, Vector3.zero, Quaternion.identity);
        DmgPopUp dmgPopUp = dmgPopUpTransform.GetComponent<DmgPopUp>();
        dmgPopUp.Setup(300);

        return dmgPopUp;
    }

    private TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int dmgAmount)
    {
        textMesh.SetText(dmgAmount.ToString());
    }
 
    
}
