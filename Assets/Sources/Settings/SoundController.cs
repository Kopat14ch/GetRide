using UnityEngine;

namespace Sources.Settings
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private AudioClip _glitchSound;
        [SerializeField] private AudioClip _endPanelSound;

        private AudioSource _audioSource;

        public static SoundController Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                _audioSource = GetComponent<AudioSource>();

                Instance = this;
                DontDestroyOnLoad(gameObject);
                transform.parent = null;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayGlitch()
        {
            _audioSource.clip = _glitchSound;
            _audioSource.Play();
        }

        public void PlayEndPanel()
        {
            _audioSource.clip = _endPanelSound;
            _audioSource.Play();
        }

        public void StopPlay() => _audioSource.Stop();

        public void SetMute (bool value) => _audioSource.mute = value;
        public void SetVolume(float value) => _audioSource.volume = value;
        public void Pause() => _audioSource.Pause();
        public void UnPause() => _audioSource.UnPause();
    }
}

