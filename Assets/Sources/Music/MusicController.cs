using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.Music
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _audioClips;

        private Coroutine _audioSwitchWork;
        private AudioSource _audioSource;
        private int _currentClipIndex;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _currentClipIndex = 0;
            _audioSource = GetComponent<AudioSource>();
            
            SetClip();
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
