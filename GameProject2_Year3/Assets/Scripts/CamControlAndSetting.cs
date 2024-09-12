using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamControlAndSetting : MonoBehaviour, Ipauseable
{
    [Header("setting")]
    [SerializeField] CinemachineVirtualCamera c_Cam;
    CinemachineTransposer c_tran; // to change body offset

    [SerializeField] float fov_len = 60;
    [SerializeField] float scrollScale = 10f;
    [Header(" ")]
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

        c_Cam.m_Lens.FieldOfView = fov_len;
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

    }
}