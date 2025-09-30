using System.Collections;
using UnityEngine;
using Cinemachine;

public class ChangeHeroHandler : MonoBehaviour
{
    [SerializeField] private PsEnableAfterDoorHandler _psEnableAfterDoorHandlerScript;

    [SerializeField] private GameObject _mainHero;
    [SerializeField] private GameObject _changedHero;

    [SerializeField] private GameObject _door;
    [SerializeField] private ParticleSystem _smokeParticle;
    [SerializeField] private ParticleSystem.MainModule _mainPs;
    [SerializeField] private Animator _changedAnim;
    [SerializeField] private CinemachineVirtualCamera _cmvc;
    [SerializeField] private GameObject _cameraTargetChangedHero;
    [SerializeField] private MouseLook _mouseLookScript;
    [SerializeField] private AudioSource _oldAudioSourse;
    [SerializeField] private AudioSource _newAudioSourse;

    [SerializeField] private Skybox _mainCameraSkyBox;
    [SerializeField] private Material _betweenBlendMaterial;
    [SerializeField] private Material _oldBetweenBlendMaterial;
    [SerializeField] private float _durationBetweenTwoSkyBoxes;

    [SerializeField] private Terrain _terrain;
    [SerializeField] private Color _colorTintGrass;

    [SerializeField] private GameObject _nextDoorScript;

    private Color _psColor;

    private void Update()
    {
        if (_door.activeInHierarchy == false)
        {
            StartCoroutine(DelayHeroCoroutine());
        }
    }
    private async void ChangeHero()
    {
        _changedHero.transform.position = _mainHero.transform.position;

        _psEnableAfterDoorHandlerScript.ParticleSystemHappen();

        _oldAudioSourse.gameObject.SetActive(false);
        _newAudioSourse.gameObject.SetActive(true);

        _terrain.terrainData.wavingGrassTint = _colorTintGrass;

        _changedHero.gameObject.SetActive(true);
        _changedAnim.SetBool("isIdle", true);

        _cmvc.Follow = _changedHero.transform;
        _cmvc.LookAt = _cameraTargetChangedHero.transform;

        _mouseLookScript.playerBody = _changedHero.transform;
        
        StartCoroutine(DelayCoroutine());
    }
    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(1f);

        this.gameObject.SetActive(false);
        _nextDoorScript.SetActive(true);
    }
    private IEnumerator StartFade(Skybox camera, Material _skyBoxBetween, float duration, float target)
    {
        camera.material = _skyBoxBetween;
        float currentTime = 0;
        float start = _skyBoxBetween.GetFloat("_Blend");
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            _skyBoxBetween.SetFloat("_Blend", Mathf.Lerp(start, target, currentTime / duration));
            yield return null;
        }
        yield break;
    }
    private IEnumerator DelayHeroCoroutine()
    {
        _mainHero.gameObject.SetActive(false);

        ParticleRainAppearanceColor();

        _smokeParticle.transform.position = _mainHero.transform.position;
        _smokeParticle.Play();

        _oldBetweenBlendMaterial.SetFloat("_Blend", 0);
        StartCoroutine(StartFade(_mainCameraSkyBox, _betweenBlendMaterial, _durationBetweenTwoSkyBoxes, 1));

        yield return new WaitForSeconds(1f);

        ChangeHero();
    }

    private void ParticleRainAppearanceColor()
    {
        _psColor = new Color(Random.value, Random.value, Random.value);
        _smokeParticle.GetComponent<ParticleSystem>().startColor = _psColor;
    }
}