using System.Collections;
using UnityEngine;

namespace LD47
{
    public class GameFeel : MonoBehaviour
    {

        public float longFreezeTime = 0.5f;
        public float shortFreezeTime = 0.1f;
        public enum FreezeFrameType
        {
            Short,
            Long
        }

        private bool _canFreeze = true;
        
        public void FreezeFrame(FreezeFrameType type)
        {
            if (_canFreeze)
                StartCoroutine(Freeze(type));
        }

        IEnumerator Freeze(FreezeFrameType type)
        {
            _canFreeze = false;
            
            float duration;
            if (type == FreezeFrameType.Long)
                duration = longFreezeTime;
            else
                duration = shortFreezeTime;

            Time.timeScale = 0;
            
            yield return new WaitForSecondsRealtime(duration);
            
            Time.timeScale = 1;
            _canFreeze = true;
        }
    }
}