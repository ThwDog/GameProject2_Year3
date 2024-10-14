using Cinemachine;
using UnityEngine;

public class CamControlAndSetting : MonoBehaviour, Ipauseable
{
    [Header("setting")]
    [SerializeField] CinemachineVirtualCamera c_Cam;
    CinemachineTransposer c_tran; // to change body offset
    public Camera cameraFrustum;

    [SerializeField] float fov_len = 30;
    [SerializeField] float scrollScale = 10f;
    [Header("Max up and down cam")]
    [Tooltip("Min value of cam")][SerializeField] float min = 4f;
    [Tooltip("Max value of cam")][SerializeField] float max = 15f; 

    [Header(" ")]
    [SerializeField] bool paused = false;

    public void pause(){
        if(!paused) paused = !paused;
    }

    public void resume(){
        if(paused) paused = !paused;
    }    

    private void Awake() {
        if(!c_Cam.Follow || !c_Cam.LookAt) {
            c_Cam.Follow = FindAnyObjectByType<PlayerController>().transform;
            c_Cam.LookAt = FindAnyObjectByType<PlayerController>().transform;
        }
    }

    private void Start() { 
        c_tran = c_Cam.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void FixedUpdate() {
        if(!c_Cam.Follow || !c_Cam.LookAt) {
            c_Cam.Follow = FindAnyObjectByType<PlayerController>().transform;
            c_Cam.LookAt = FindAnyObjectByType<PlayerController>().transform;
        }

        // c_Cam.m_Lens.FieldOfView = fov_len;
        if(paused) return;
        // float scrollingDelta =Input.GetAxis("Mouse ScrollWheel");

        if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.W)){
            if(c_tran.m_FollowOffset.y > max ) return;
            c_tran.m_FollowOffset.y +=  scrollScale * Time.deltaTime;
        }
        else if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.S)){
            if(c_tran.m_FollowOffset.y < min) return;
            c_tran.m_FollowOffset.y += -scrollScale * Time.deltaTime;
        }

        // mouse scroll 
        if(fov_len > 50) fov_len = 50;
        if(fov_len < 20) fov_len = 20;
        fov_len -= Input.mouseScrollDelta.y * 5;
        c_Cam.m_Lens.FieldOfView = fov_len;
        cameraFrustum.fieldOfView = c_Cam.m_Lens.FieldOfView + 5;
    }

    public void camShake(float value) {
        value = Mathf.Clamp(value,0,1);
        
        CinemachineBasicMultiChannelPerlin c_B = c_Cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        c_B.m_AmplitudeGain = value;
    }
}