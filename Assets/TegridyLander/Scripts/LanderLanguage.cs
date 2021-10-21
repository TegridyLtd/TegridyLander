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

namespace Tegridy.Lander
{
    public static class LanderLanguage
    {
        public static string endHealth = "Vessel Destroyed";
        public static string endFuel = "Fuel Expired";
        public static string endTime = "Air Expired";
        public static string endTarget = "Mission Control we made it";

        public static string health = "Health";
        public static string armour = "Armour";
        public static string fuel = "Fuel";
        public static string fuelBurn = "Fuel Burn";
        public static string mainThrust = "Main Thruster";
        public static string sideThrust = "Side Thrusters";

        public static string exit = "Exit";
        public static string start = "Start";
        public static string changeChar = "Change Character";
        public static string changeLevel = "Change Level";

        public static string gameOverTitle = "Mission Ended";
        public static string[] gameOverReason = { "Aborted","Fuel Expirer","Damage limit Reacher","Out of Time", "landed" };
        public static string distanceRemain = "Distance";
    }
}
