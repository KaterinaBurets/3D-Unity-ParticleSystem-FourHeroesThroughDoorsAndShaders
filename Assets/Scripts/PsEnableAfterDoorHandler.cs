using UnityEngine;

public class PsEnableAfterDoorHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _ps;
    [SerializeField] private ParticleSystem _oldPs;
    [SerializeField] private GameObject _door;

    public void ParticleSystemHappen()
    {
        if (_door.activeInHierarchy == false)
        {
            if (_oldPs != null)
                _oldPs.gameObject.SetActive(false);

            _ps.gameObject.SetActive(true);
            _ps.Play();
        }
    }
}
