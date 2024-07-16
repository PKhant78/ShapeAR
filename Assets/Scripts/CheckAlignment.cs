using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CheckAlignment : MonoBehaviour
{
    private bool alignment;
    public AudioSource _audioSource;
    public Text alignmentText;
    public Text angleText;
    public Text angleTextTwo;
    public Text volumeText;
    public float audioPitch = 3.0f;
    private Vector3 hitN;

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100f, Color.red);
        Vector3 cameraForward = Camera.main.transform.forward;

        Vector3 referenceAxis = Vector3.down;

        float cameraAngle = Vector3.Angle(cameraForward, referenceAxis);

        angleText.text = "Angle: " + cameraAngle.ToString();

        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {

            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }


            Vector3 hitNormal = hit.normal;
            storeHitNormal(hitNormal);

            float angleDifference = Vector3.Angle(cameraForward, hitNormal);
            Debug.Log("Angle difference: " + angleDifference);

            float pitch = (1 - Mathf.Abs(angleDifference - 174) / 174.0f) * audioPitch;
            _audioSource.pitch = pitch;
            Debug.Log(pitch);

            volumeText.text = "Pitch: " + pitch.ToString();

            angleTextTwo.text = "Angle Difference: " + angleDifference.ToString();

            if (angleDifference <= 179.0f && angleDifference >= 174.0f)
            {
                alignmentText.text = "Phone is aligned";
                Handheld.Vibrate();
            }
            else
            {
                alignmentText.text = "Phone is not aligned";
            }
        }


        else
        {

            alignment = false;
            Debug.Log("Phone is not aligned");
            _audioSource.Stop();
            alignmentText.text = "Phone is Not Aligned";

            float angleDifference = Vector3.Angle(cameraForward, hitN);
            angleTextTwo.text = "Angle Difference: " + angleDifference.ToString();

        }
        }

        void storeHitNormal(Vector3 hit)
        {
            hitN = hit;
        }
    }

