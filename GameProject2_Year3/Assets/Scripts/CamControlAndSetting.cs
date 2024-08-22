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

    [SerializeField] bool paused = false;

    public void pause(){
        if(!paused) paused = !paused;
    }

    public void resume(){
        if(paused) paused = !paused;
    }    

    private void Start() { 
        c_tran = c_Cam.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void FixedUpdate() {
        c_Cam.m_Lens.FieldOfView = fov_len;
        // c_Cam.m_Lens.FieldOfView = fov_len;
        if(paused) return;
        // float scrollingDelta =Input.GetAxis("Mouse ScrollWheel");

        if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.W)){
            if(c_tran.m_FollowOffset.y > 15f ) return;
            c_tran.m_FollowOffset.y +=  scrollScale * Time.deltaTime;
        }
        else if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.S)){
            if(c_tran.m_FollowOffset.y < 4f) return;
            c_tran.m_FollowOffset.y += -scrollScale * Time.deltaTime;
        }

    }
}