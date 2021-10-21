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
using UnityEngine.UI;
using TMPro;
namespace Tegridy.Lander
{
    public class GUIMenu : MonoBehaviour
    {
        public TextMeshProUGUI title;
        public Button startGame;
        public Button exit;

        [Header("Levels")]
        public TextMeshProUGUI levelName;
        public Image levelPic;
        public Button levelChange;

        [Header("Ships")]
        public Image shipPic;
        public TextMeshProUGUI shipName;

        public Image healthImage;
        public Image armourImage;
        public Image fuelImage;
        public Image fuelBurnImage;
        public Image mainThrustImage;
        public Image sideThrustImage;

        public TextMeshProUGUI healthText;
        public TextMeshProUGUI armourText;
        public TextMeshProUGUI fuelText;
        public TextMeshProUGUI fuelBurnText;
        public TextMeshProUGUI mainThrustText;
        public TextMeshProUGUI sideThrustText;

        public Button changeShip;
    }
}
