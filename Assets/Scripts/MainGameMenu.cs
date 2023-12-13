using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Image ImageFade;
    [SerializeField] private GameObject _ImageFade;
    [SerializeField] private GameObject ImageCredits;
    // Start is called before the first frame update
    public void OnClickPlay()
    {
        ImageFade.DOFade(1, 2.9f).OnComplete(FadeComplete);
    }

    private void FadeComplete()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnClickCredits()
    {
        ImageFade.DOFade(1, 2.9f).OnComplete(FadeCreditsComplete);
        
    }

    public void FadeCreditsComplete()
    {
        _ImageFade.SetActive(false);
        ImageCredits.SetActive(true);
    }


    
    // Update is called once per frame
    void Update()
    {
        
    }
}
