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

using System.Collections;
using UnityEngine;

namespace Tegridy.Lander
{
    public class LanderGame : MonoBehaviour
    {
        [Header("World Settings")]
        public GameObject levelHolder;
        public GameObject shipHolder;
        public AudioClip defaultThrusterAudio;
        public GUILevelOverlay defaultOverlay;

        [Header("Mission Info")]
        public MissionResults results;
        public bool gameOver = true;

        [HideInInspector] public LanderLevel[] levels;
        [HideInInspector] public LanderVessel[] ships;

        private LanderVessel playerShip;
        private LanderLevel level;
        private GUILevelOverlay gui;
        private GameObject hostMenu;

        private Vector3 lastPosition;
        private float startTime;
        public float currentTime;
        public LanderPad home;

        private void Awake()
        {
            ships = shipHolder.GetComponentsInChildren<LanderVessel>();
            foreach (LanderVessel thisShip in ships)
            {
                if (thisShip.thrusterNoise == null) thisShip.thrusterNoise = defaultThrusterAudio;
                //thisShip.StartUp();
                thisShip.SetActive(false);
            }

            levels = levelHolder.GetComponentsInChildren<LanderLevel>();
            foreach (LanderLevel thisLevel in levels)
            {
                if (thisLevel.ships == null) thisLevel.ships = ships;
                if (thisLevel.gui == null) thisLevel.gui = defaultOverlay;
                thisLevel.StartUp();
                thisLevel.SetActive(false);
            }
        }

        private void Start()
        {
            //remove after testing
            FindObjectOfType<LanderMenu>().OpenMenu(this, FindObjectOfType<LanderMenu>().gui, null);
        }
        public void StartGame(LanderVessel ship, LanderLevel thisLevel, GameObject host)
        {
            gameOver = false;

            playerShip = Instantiate(ship.gameObject).GetComponent<LanderVessel>();
            playerShip.StartUp();
            level = Instantiate(thisLevel.gameObject).GetComponent<LanderLevel>();

            home = level.endPosition.gameObject.AddComponent<LanderPad>();
            home.landedDelay = level.landedTimer;

            hostMenu = host;
            hostMenu.SetActive(false);

            gui = level.gui;

            startTime = Time.time;
            results = new MissionResults();

            playerShip.gameObject.transform.position = level.startPosition.position;

            playerShip.SetActive(true);
            level.SetActive(true);

            playerShip.active = true;
            StartCoroutine(CheckHealth());
            StartCoroutine(CheckFuel());
            StartCoroutine(CheckTime());
            StartCoroutine(CheckLanded());
            //start the UI Overlay
            gui.SetActive(true);
            level.gameObject.AddComponent<LanderOverlay>().StartDisplay(0.1f, playerShip, level, level.gui, this);
        }
        IEnumerator CheckFuel()
        {
            yield return new WaitUntil(() => playerShip.shipConfig.fuel <= 0);
            Debug.Log("Out of fuel wait for player to stop");
            lastPosition = playerShip.transform.position;
            yield return new WaitForSeconds(level.fuelOutDelay);
            if (lastPosition == playerShip.transform.position) StartCoroutine(EndGame(1));
            else StartCoroutine(CheckFuel());
        }
        IEnumerator CheckHealth()
        {
            yield return new WaitUntil(() => playerShip.shipConfig.health <= 0);
            playerShip.active = false;
            var explosion = Instantiate(playerShip.explosionPrefab, playerShip.transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
            Debug.Log("Wait for the explosion to play");
            yield return new WaitForSeconds(1f);
            StartCoroutine(EndGame(2));
        }
        IEnumerator CheckTime()
        {
            startTime += playerShip.timeBoost;
            playerShip.timeBoost = 0;
            currentTime = Time.time - startTime;
            
            if (currentTime >= level.levelTime)
            {
                playerShip.active = false;
                yield return new WaitForSeconds(level.missionOverDelay);
                Debug.Log("ran out of time");
                StartCoroutine(EndGame(3));
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(CheckTime());
            }
        }
        IEnumerator CheckLanded()
        {
            yield return new WaitUntil(() => home.landed == true);
            StartCoroutine(EndGame(4));
        }
        IEnumerator EndGame(int reason)
        {
            //reason 
            //1 = fuel
            //2 = damage
            //3 = time
            //4 = landed

            StopCoroutine(CheckFuel());
            StopCoroutine(CheckTime());
            StopCoroutine(CheckHealth());

            results.timeLeft = Time.deltaTime - startTime;
            results.fuel = playerShip.shipConfig.fuel;
            results.health = playerShip.shipConfig.health;
            results.reason = reason;

            gui.gameOverTitle.text = LanderLanguage.gameOverTitle;
            gui.gameOverText.text = LanderLanguage.gameOverReason[reason];
            gui.gameOver.SetActive(true);

            yield return new WaitForSeconds(level.missionOverDelay);
            Destroy(playerShip.gameObject);
            Destroy(level.gameObject);

            gameOver = true;
            gui.gameOver.SetActive(false);
            gui.SetActive(false);
            if (hostMenu != null) hostMenu.SetActive(true);

            StopAllCoroutines();

        }
    }
}
