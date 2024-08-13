using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PostProcessingController : MonoBehaviour
{
    [Header("Vignette - Start Cutscene")]
    private Vignette vignette;
    private float elapsedTime;
    [Range(1f, 10f)][SerializeField] private float startAnimationTime;
    [Range(1f, 10f)][SerializeField] private float vigneteTimerMax;
    [Range(1f, 100f)][SerializeField] private float cutsceneTimerMax;
    [Range(0.1f, 1f)][SerializeField]private float startVigneteValue;
    [Range(0.1f, 1f)][SerializeField] private float endVigneteValue;
    [SerializeField] private Animator photoAnimator;
    [SerializeField] private GameObject photoObject;
    [SerializeField] private AnimationEvent startAnimFinished;

    private bool trepassStartAnimValue;
    

    private void Start() 
    {
        GameController.Instance.globalVolume.profile.TryGet<Vignette>(out vignette);
        vignette.intensity.value = startVigneteValue;

        SoundManager.Instance.PlayBedSound(photoObject.transform.position, 2f);
    }

    private void Update() 
    {
        if(!GameController.Instance.IsGameStarted)
        {
            UpdateCutscene();
        }
    }
    
    private void UpdateCutscene()
    {
        elapsedTime += Time.deltaTime;


        float percentageComplete = elapsedTime / vigneteTimerMax;
        vignette.intensity.value = Mathf.Lerp(startVigneteValue, endVigneteValue, percentageComplete);


        if(elapsedTime >= startAnimationTime && !trepassStartAnimValue)
        {
            trepassStartAnimValue = true;

            photoAnimator.SetTrigger("StartAnimation");
        }
        
        
        
        if(elapsedTime >= cutsceneTimerMax)
        {
            GameController.Instance.IsGameStarted = true;
            GameController.Instance.PlayIntialTipText();

            Destroy(photoAnimator.gameObject);
            photoObject.gameObject.SetActive(true);
        }
    }
}
