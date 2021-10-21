/////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2021 Tegridy Ltd                                          //
// Author: Darren Braviner                                                 //
// Contact: db@tegridygames.co.uk                                          //
/////////////////////////////////////////////////////////////////////////////
//                                                                         //
// This program is free software; you can redistribute it and/or modify    //
// it under the terms of the GNU General Public License as published by    //
// the Free Software Foundation; either version 2 of the License, or       //
// (at your option) any later version.                                     //
//                                                                         //
// This program is distributed in the hope that it will be useful,         //
// but WITHOUT ANY WARRANTY.                                               //
//                                                                         //
/////////////////////////////////////////////////////////////////////////////
//                                                                         //
// You should have received a copy of the GNU General Public License       //
// along with this program; if not, write to the Free Software             //
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,              //
// MA 02110-1301 USA                                                       //
//                                                                         //
/////////////////////////////////////////////////////////////////////////////

using UnityEngine;
namespace Tegridy.Lander
{
    public class LanderVessel : MonoBehaviour
    {
        [HideInInspector] public bool active;
        [HideInInspector] public float timeBoost;

        public string vesselName;
        public ShipStats shipConfig;

        [Header("Thrust Config")]
        public Transform mainThruster;
        public Animator mainThrusterAnim;
        public Transform leftThruster;
        public Animator leftThrusterAnim;
        public Transform rightThruster;
        public Animator rightThrusterAnim;

        [Header("")]
        public AudioClip thrusterNoise;
        public GameObject explosionPrefab;
        public Sprite uiPicture;

        private Rigidbody2D landerRigidBody2D;
        AudioSource audioSource;
        bool audioPlay;

        public void StartUp()
        {
            landerRigidBody2D = GetComponent<Rigidbody2D>();
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = thrusterNoise;
        }
        void FixedUpdate()
        {
            if (active) CheckInput();
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.GetComponent<LanderCollider>() != null) CheckCollision(collision);
        }
        private void CheckInput()
        {
            audioPlay = false;
            if (Input.GetAxis("Vertical") > 0) ApplyThrust(mainThruster, shipConfig.mainThrustPower, mainThrusterAnim);
            else SetThrustAnim(mainThrusterAnim, false);

            if (Input.GetAxis("Horizontal") > 0) ApplyThrust(leftThruster, shipConfig.sideThrustPower, leftThrusterAnim);
            else SetThrustAnim(leftThrusterAnim, false);
            if (Input.GetAxis("Horizontal") < 0) ApplyThrust(rightThruster, shipConfig.sideThrustPower, rightThrusterAnim);
            else SetThrustAnim(rightThrusterAnim, false);
            PlaySfx(audioPlay);
        }
        private void ApplyThrust(Transform thrusterTransform, float thrustPower, Animator thrustAnim)
        {
            if (shipConfig.fuel > 0f)
            {
                Vector3 direction = transform.position - thrusterTransform.position;
                landerRigidBody2D.AddForceAtPosition(direction.normalized * thrustPower, thrusterTransform.position);
                shipConfig.fuel -= shipConfig.fuelBurnRate;
                audioPlay = true;
                SetThrustAnim(thrustAnim, true);
            }
            else SetThrustAnim(thrustAnim, false);
        }
        private void CheckCollision(Collision2D collision)
        {
            LanderCollider thisImpact = collision.gameObject.GetComponent<LanderCollider>();
            if (collision.relativeVelocity.magnitude > shipConfig.armour )
            {
                shipConfig.health -= collision.relativeVelocity.magnitude * thisImpact.stats.damage;
            }
            shipConfig.armour += thisImpact.stats.armour;
            shipConfig.health += thisImpact.stats.health;
            shipConfig.fuel += thisImpact.stats.fuel;
            timeBoost += thisImpact.stats.time;
            AudioTools.PlayOneShot(thisImpact.soundFX, audioSource);
            if (thisImpact.stats.destroy == true) Destroy(collision.gameObject);
        }
        private void SetThrustAnim(Animator thruster, bool state)
        {
            if (thruster != null && thruster.runtimeAnimatorController != null)
            {
                thruster.SetBool("Thrust", state);
            }
        }
        private void PlaySfx(bool play)
        {
            if (play)
            {
                if (audioSource.isPlaying)
                {
                    return;
                }
                audioSource.Play();
            }
            else audioSource.Pause();
        }
    }
}