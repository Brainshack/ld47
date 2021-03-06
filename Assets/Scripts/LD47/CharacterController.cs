﻿using UnityEngine;

namespace LD47
{
    public class CharacterController : MonoBehaviour
    {
        public float speed = 5f;
        
        // Update is called once per frame
        void Update () {
            // Input.GetAxis() is used to get the user's input
            // You can furthor set it on Unity. (Edit, Project Settings, Input)
            var translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            var strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(strafe, 0, translation);
        }
    }
}