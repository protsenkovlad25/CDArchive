using UnityEngine.Events;

namespace VP
{
    public class Timer
    {
        public UnityEvent OnTimesUp;

        private bool _isPause;

        private float _time;
        private float _passedTime;

        public bool IsPause => _isPause;
        public float Time => _time;
        public float PassedTime => _passedTime;
        public float CurrentTime => _time - _passedTime;

        public Timer(float time, float passedTime = 0)
        {
            _passedTime = passedTime;
            _time = time;

            OnTimesUp = new();
        }

        public void SetTime(float time)
        {
            _time = time;
        }
        public void AddTime(float time)
        {
            _passedTime -= time;
            
            if (_passedTime < 0)
                _passedTime = 0;
        }

        public void Reset()
        {
            _passedTime = 0;
            _isPause = false;
        }
        public void Pause()
        {
            _isPause = true;
        }
        public void Play()
        {
            _isPause = false;
        }

        public void Update(bool isScaled = true)
        {
            if (!_isPause)
            {
                _passedTime += isScaled ? UnityEngine.Time.deltaTime : UnityEngine.Time.unscaledDeltaTime;

                if (_passedTime >= _time)
                {
                    _isPause = true;
                    OnTimesUp?.Invoke();
                }
            }
        }
    }
}
