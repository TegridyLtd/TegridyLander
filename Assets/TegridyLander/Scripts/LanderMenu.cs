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
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Tegridy.Lander
{
    public class LanderMenu : MonoBehaviour
    {
        //max values used for image fill
        private float maxHealth;
        private float maxArmour;
        private float maxFuel;
        private float maxFuelBurn;
        private float maxMainThrust;
        private float maxSideThrust;

        //Lander Systems
        public GUIMenu gui;
        LanderGame controller;
        GameObject hostMenu;

        private int indexLevel;
        private int indexShip;

        public void OpenMenu(LanderGame control, GUIMenu menu, GameObject host)
        {
            //setup
            gui = menu;
            controller = control;
            hostMenu = host;
            GetMaxValues();

            gui.healthText.text = LanderLanguage.health;
            gui.armourText.text = LanderLanguage.armour;
            gui.fuelText.text = LanderLanguage.fuel;
            gui.fuelBurnText.text = LanderLanguage.fuelBurn;
            gui.mainThrustText.text = LanderLanguage.mainThrust;
            gui.sideThrustText.text = LanderLanguage.sideThrust;

            gui.exit.onClick.AddListener(() => CloseMenu());
            gui.exit.GetComponentInChildren<TextMeshProUGUI>().text = LanderLanguage.exit;

            gui.startGame.onClick.AddListener(() => LaunchGame());
            gui.startGame.GetComponentInChildren<TextMeshProUGUI>().text = LanderLanguage.start;

            gui.changeShip.onClick.AddListener(() => ChangeShip());
            gui.changeShip.GetComponentInChildren<TextMeshProUGUI>().text = LanderLanguage.changeChar;

            gui.levelChange.onClick.AddListener(() => ChangeLevel());
            gui.levelChange.GetComponentInChildren<TextMeshProUGUI>().text = LanderLanguage.changeLevel;

            indexLevel = controller.levels.Length;
            ChangeLevel();
            gui.SetActive(true);
        }
        private void CloseMenu() 
        {
            gui.exit.onClick.RemoveAllListeners();
            gui.startGame.onClick.RemoveAllListeners();
            gui.changeShip.onClick.RemoveAllListeners();
            gui.levelChange.onClick.RemoveAllListeners();

            gui.SetActive(false);
            if (hostMenu != null) hostMenu.SetActive(true);
            else Application.Quit();
        }

        private void ChangeLevel()
        {
            indexLevel++;
            if (indexLevel >= controller.levels.Length) indexLevel = 0;
            gui.levelName.text = controller.levels[indexLevel].levelName;
            gui.levelPic.sprite = controller.levels[indexLevel].levelPic;
            indexShip = controller.ships.Length;
            ChangeShip();
        }
        private void ChangeShip()
        {
            indexShip++;
            if (indexShip >= controller.levels[indexLevel].ships.Length) indexShip = 0;

            LanderVessel thisShip = controller.levels[indexLevel].ships[indexShip];

            gui.shipName.text = thisShip.vesselName;
            gui.shipPic.sprite = thisShip.uiPicture;

            gui.healthImage.fillAmount = (thisShip.shipConfig.health / (maxHealth / 100)) / 100;
            gui.armourImage.fillAmount = (thisShip.shipConfig.armour / (maxArmour / 100)) / 100;
            gui.fuelImage.fillAmount = (thisShip.shipConfig.fuel / (maxFuel / 100)) / 100;
            gui.fuelBurnImage.fillAmount = (thisShip.shipConfig.fuelBurnRate / (maxFuelBurn / 100)) / 100;
            gui.mainThrustImage.fillAmount = (thisShip.shipConfig.mainThrustPower / (maxMainThrust / 100)) / 100;
            gui.sideThrustImage.fillAmount = (thisShip.shipConfig.sideThrustPower / (maxSideThrust / 100)) / 100;
        }

        private void LaunchGame()
        {
            controller.StartGame(controller.levels[indexLevel].ships[indexShip], controller.levels[indexLevel], gui.gameObject);
        }

        private void GetMaxValues()
        {
            maxHealth = 0;
            maxArmour = 0;
            maxFuel = 0;
            maxFuelBurn = 0;
            maxMainThrust = 0;
            maxSideThrust = 0;

            foreach (LanderVessel ship in controller.ships)
            {
                maxHealth = CheckHigh(maxHealth, ship.shipConfig.health);
                maxArmour = CheckHigh(maxArmour, ship.shipConfig.armour);
                maxFuel = CheckHigh(maxFuel, ship.shipConfig.fuel);
                maxFuelBurn = CheckHigh(maxFuelBurn, ship.shipConfig.fuelBurnRate);
                maxMainThrust = CheckHigh(maxMainThrust, ship.shipConfig.mainThrustPower);
                maxSideThrust = CheckHigh(maxSideThrust, ship.shipConfig.sideThrustPower);
            }
        }
        private float CheckHigh(float max, float check)
        {
            if (max >= check) return max;
            else return check;
        }
    }
}
