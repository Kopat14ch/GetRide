using System.Collections;
using System.Collections.Generic;
using Sources.Common;
using UnityEngine;

namespace Sources.Music
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _audioClips;

        private AudioSource _audioSource;
        private Coroutine _audioSwitchWork;
        private int _currentClipIndex;


        public void Initialize()
        {
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
            
            _currentClipIndex = 0;
            SetClip();
        }

        public float GetVolume() => _audioSource.volume;
        public void SetVolume(float value)
        {
            _audioSource.volume = value;
            
            Saver.Instance.SaveMusicVolume(value);
        }

        private void SetClip()
        {
            if (_currentClipIndex >= _audioClips.Count)
                _currentClipIndex = 0;

            _audioSwitchWork = StartCoroutine(AudioSwitch());
        }

        private IEnumerator AudioSwitch()
        {
            _audioSource.clip = _audioClips[_currentClipIndex];
            _audioSource.Play();
            _currentClipIndex = ++_currentClipIndex % _audioClips.Count;

            yield return new WaitForSeconds(_audioSource.clip.length);

            _audioSwitchWork = null;
            SetClip();
        }
    }
}
