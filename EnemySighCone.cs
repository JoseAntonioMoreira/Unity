//Simple mechanic for enemies to detect player bones.
using UnityEngine;

namespace Enemy
{
    public class EnemySighCone : MonoBehaviour
    {
        [SerializeField]
        private Collider[] _playerBones;//colliders of the bones you want the enemy to detect

        [SerializeField]
        private float _rateIncreasePerSecond = 5f;//rate at wich the detection will increase

        [SerializeField]
        private float _rateDecreasePerSecond = 5f;//rate at wich the detection will decrease

        [SerializeField]
        private float _maxDetectionRate = 20f; //maximum detection rate the enemy can increase

        [SerializeField]
        private float _distanceInstantDetecction = 3f; //if the distance between the player and the enemy is less than this the enemy will instant detect

        private bool _onCollision = false;// if the player is in the sightcone

        private Collider _other;

        public float[] _detectionRate; // current rate the enemy as of seeing the player


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

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))//On collision with the player
            {
                if (Vector3.Distance(other.transform.position, transform.position) < _distanceInstantDetecction)//if the distance is larger than the instant detection
                {
                    for (int i = 0; i < _playerBones.Length; i++)//start increasing the detection for each bone found
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
            if (other.CompareTag("Player"))//on trigger exit change the onCollision state for decreasing the detection
            {
                _onCollision = false;
            }
        }

        private void DetectBones()//search for the bones in the detected player
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

        private void BonesDetected(int i)//function for increasing the detection rate
        {
            if (_detectionRate[i] < _maxDetectionRate)//condition to keep increasing the detection rate and if the detection rate is equal to the max detection the player is detected
                _detectionRate[i] += _rateIncreasePerSecond * Time.deltaTime;
            else
                IsDetected = true;

            if (_detectionRate[i] >= _maxDetectionRate)//just some visual indicator for debuging and testing while in the editor, remove before building
            {
                Debug.DrawRay(transform.position, _playerBones[i].transform.position - transform.position, Color.red);
            }
            else
            {
                Debug.DrawRay(transform.position, _playerBones[i].transform.position - transform.position, Color.blue);
            }
        }

        private void LoseDetection()//function to remove the detection rate if the player is no more in the sight cone
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
