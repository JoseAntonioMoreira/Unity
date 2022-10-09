using UnityEngine;

namespace Enemy
{
    public class EnemySighCone : MonoBehaviour
    {
        [SerializeField]
        private Collider[] _playerBones;

        [SerializeField]
        private float _rateIncreasePerSecond = 5f;

        [SerializeField]
        private float _rateDecreasePerSecond = 5f;

        [SerializeField]
        private float _maxDetectionRate = 20f;

        [SerializeField]
        private float _distanceInstantDetecction = 3f;

        private bool _onCollision = false;

        private Collider _other;

        public float[] _detectionRate;


        public static bool IsDetected = false;

        public static bool IsProximityDetected = false;

        public float[] DetectionRate { get => _detectionRate; }
        public float MaxDetectionRate { get => _maxDetectionRate; set => _maxDetectionRate = value; }

        private void Start()
        {
            _detectionRate = new float[_playerBones.Length];
        }

        private void Update()
        {
            if (_onCollision)
                DetectBones();
            else
                LoseDetection();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (Vector3.Distance(other.transform.position, transform.position) < _distanceInstantDetecction)
                {
                    Debug.Log("OGGA");
                    for (int i = 0; i < _playerBones.Length; i++)
                    {
                        _detectionRate[i] = _maxDetectionRate + 5;
                        BonesDetected(i);
                        _onCollision = true;
                    }


                    Debug.Log(IsDetected);
                    return;
                }

                _onCollision = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _onCollision = false;
            }
        }

        private void DetectBones()
        {
            if (_playerBones.Length > 0)
                for (int i = 0; i < _playerBones.Length; i++)
                {
                    if (Physics.Raycast(transform.position, _playerBones[i].transform.position - transform.position, out RaycastHit hit))
                    {
                        if (hit.collider.name == _playerBones[i].name)
                        {
                            BonesDetected(i);
                        }
                    }
                }
        }

        private void BonesDetected(int i)
        {
            if (_detectionRate[i] < _maxDetectionRate)
                _detectionRate[i] += _rateIncreasePerSecond * Time.deltaTime;
            else
                IsDetected = true;

            if (_detectionRate[i] >= _maxDetectionRate)
            {
                Debug.DrawRay(transform.position, _playerBones[i].transform.position - transform.position, Color.red);
            }
            else
            {
                Debug.DrawRay(transform.position, _playerBones[i].transform.position - transform.position, Color.blue);
            }
        }

        private void LoseDetection()
        {
            float index = 0;
            for (int i = 0; i < _detectionRate.Length; i++)
            {
                if (_detectionRate[i] > 0)
                {
                    _detectionRate[i] -= _rateDecreasePerSecond * Time.deltaTime;
                    index++;
                }
                else if (_detectionRate[i] < 0)
                    _detectionRate[i] = 0;
            }
            if (0 == index)
                IsDetected = false;
        }
    }
}
