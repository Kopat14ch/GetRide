using System.Collections;
using System.Collections.Generic;
using Sources.Common;
using Sources.StringController;
using UnityEngine;

namespace Sources.Music
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicController : MonoBehaviour
    {
        [Header(HeaderNames.Objects)]
        [SerializeField] private List<AudioClip> _audioClips;

        private AudioSource _audioSource;
        private int _currentClipIndex;

        private static bool s_isInitialize;

        public void Initialize()
        {
            if (s_isInitialize)
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();

            if (Saver.Instance.SaveData.MusicChanged)
                _audioSource.volume = Saver.Instance.SaveData.MusicVolumeValue;

            _currentClipIndex = 0;
            SetClip();

            s_isInitialize = true;
        }

        public float GetVolume() => _audioSource.volume;

        public void SetVolume(float value)
        {
            _audioSource.volume = value;

            Saver.Instance.SaveMusicVolume(value);
        }

        public void DisableMusic() => _audioSource.mute = true;
        public void EnableMusic() => _audioSource.mute = false;

        private void SetClip()
        {
            if (_currentClipIndex >= _audioClips.Count)
                _currentClipIndex = 0;
            
            StartCoroutine(AudioSwitch());
        }

        private IEnumerator AudioSwitch()
        {
            _audioSource.clip = _audioClips[_currentClipIndex];
            _audioSource.Play();
            _currentClipIndex = ++_currentClipIndex % _audioClips.Count;

            yield return new WaitUntil(() => _audioSource.isPlaying == false);
            
            SetClip();
        }
    }
}
