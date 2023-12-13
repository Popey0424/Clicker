using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pages : MonoBehaviour
{
    [SerializeField] private GameObject AchivementPage;
    private Button ButtonAchivement;
    public GameObject Scrollview;
    
    public void OnClickAchivement()
    {
        AchivementPage.SetActive(true);
        Scrollview.gameObject.SetActive(false);
  
        
       
        
    }
    public void OnClickUpgrades()
    {
        AchivementPage.SetActive(false);
        Scrollview.gameObject.SetActive(true);
    }


}
