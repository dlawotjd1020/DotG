﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    //셰이크 효과를 줄 카메라의 Transform을 저장할 변수
    public Transform shakeCamera;
    //회전 시킬 것인지 판단할 변수
    public bool shakeRotate = false;
    //초기 좌표와 회전값을 저장할 변수
    private Vector3 originPos;
    private Quaternion originRot;

    // Start is called before the first frame update
    void Start()
    {
        //카메라 초기값 저장
        originPos = shakeCamera.localPosition;
        originRot = shakeCamera.localRotation;

    }

    public IEnumerator ShakeCamera(float duratoin = 0.05f,
        float magnitudepos = 0.03f, float magnitudeRot = 0.1f )
    {
        //지나간 시간을 누적할 변수
        float passtime = 0.0f;
        //진동시간 동안 루프를 순회함
        while (passtime < duratoin)
        {
            //불규칙한 위치를 산출
            Vector3 shakePos = Random.insideUnitSphere;
            //카메라 위치를 변경
            shakeCamera.localPosition = shakePos * magnitudepos;

            //불규칙한 회전을 할 경우
            if (shakeRotate)
            {
                //불규칙한 회전값을 펄린 노이즈 함수를 이용해 추출
                Vector3 shakeRot = new Vector3(0, 0, Mathf.PerlinNoise
                    (Time.deltaTime * magnitudeRot,0.0f));
                //카메라 회전값 변경
                shakeCamera.localRotation = Quaternion.Euler(shakeRot);
            }
            //진동 시간을 ㄴ적
            passtime += Time.deltaTime;

            yield return null;
        }
        //진동이 끝난 후 카메라의 초기값으로 설정
        shakeCamera.localPosition = originPos;
        shakeCamera.localRotation = originRot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
