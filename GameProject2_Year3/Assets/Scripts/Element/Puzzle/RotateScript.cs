using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public enum roteType{
        x ,y ,z
    }
    [Header("Rotate Script Setting")]
    public roteType _roteType;
    internal Quaternion rotateTarget;
    internal bool isRo = false;
    public float delayButtonTime;
    internal bool isPressButton = false;
    [Range(0f,100f)]public float rotateSpeed = 10f;

    //Set rotation
    public void _SetRotate(float Deg){
        switch(_roteType){
            case roteType.x :
                if(this.gameObject.transform.localRotation == Quaternion.Euler(360,0,0)){
                    this.gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
                }
                rotateTarget = transform.rotation * Quaternion.Euler(Deg, 0, 0);
                isRo = true;
                break;
            case roteType.y :
                if(this.gameObject.transform.localRotation == Quaternion.Euler(0,360,0)){
                    this.gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
                }
                rotateTarget = transform.rotation * Quaternion.Euler(0, Deg, 0);
                isRo = true;
                break;
            case roteType.z :
                if(this.gameObject.transform.localRotation == Quaternion.Euler(0,0,360)){
                    this.gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
                }
                rotateTarget = transform.rotation * Quaternion.Euler(0, 0, Deg);
                isRo = true;
                break;
        }
    }

    public IEnumerator resetButton(float sec){
        if(!isPressButton) yield break;
        yield return new WaitForSeconds(sec);
        isPressButton = false;
    }

    // rotation OBJ
    public virtual void _RotateObj(){
        if(isRo){
            transform.rotation = Quaternion.Lerp(transform.rotation, rotateTarget, rotateSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, rotateTarget) < 0.01f)
            {
                isRo = false;
            }
        }
    }

    public virtual void _RotateObj(Transform target){
        if(isRo){
            target.localRotation = Quaternion.Lerp(target.localRotation, rotateTarget, rotateSpeed * Time.deltaTime);
            if (Quaternion.Angle(target.localRotation, rotateTarget) < 0.01f)
            {
                isRo = false;
            }
        }
    }
}
