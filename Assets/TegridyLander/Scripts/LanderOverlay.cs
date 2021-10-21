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
    public class LanderOverlay : MonoBehaviour
    {
        private ShipStats startingStats;
        private LanderVessel ship;
        private LanderLevel level;
        private LanderGame contoller;
        private GUILevelOverlay gui;
        private float delay;

        float healthPercentage;
        float fuelPercentage;
        float timePercentage;

        public void StartDisplay(float updateDelay, LanderVessel vessel, LanderLevel levelInfo, GUILevelOverlay overlay, LanderGame control)
        {
            startingStats = vessel.shipConfig;
            ship = vessel;
            level = levelInfo;
            contoller = control;

            gui = overlay;
            delay = updateDelay;

            healthPercentage = startingStats.health / 100;
            fuelPercentage = startingStats.fuel / 100;
            timePercentage = level.levelTime / 100;

            gui.shipName.text = ship.vesselName;
            gui.shipImage.sprite = ship.uiPicture;
            StartCoroutine(UpdateDisplay());
        }
        IEnumerator UpdateDisplay()
        {
            gui.timeText.text = (level.levelTime - contoller.currentTime).ToString();
            gui.timeImage.fillAmount = 1 - ((contoller.currentTime / timePercentage) / 100);

            gui.healthText.text = ship.shipConfig.health.ToString() + "/" + startingStats.health;
            gui.healthImage.fillAmount = (ship.shipConfig.health / healthPercentage) / 100;

            gui.fuelText.text = ship.shipConfig.fuel.ToString() + "/" + startingStats.fuel;
            gui.fuelImage.fillAmount = (ship.shipConfig.fuel / fuelPercentage) / 100;
            yield return new WaitForSeconds(delay);
            StartCoroutine(UpdateDisplay());
        }
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}