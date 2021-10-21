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
    [System.Serializable] public class TerrainItem
    {
        [Header("WorldObjects")]
        public GameObject objects;
        public AudioClip[] soundFX;

        [Header("GroupStats")]
        public TerrainStats stats = new TerrainStats();
    }
    [System.Serializable] public class TerrainStats
    {
        public float damage;
        public float health;
        public float armour;
        public float fuel;
        public float time;
        public bool destroy;
    }
    [System.Serializable] public class ShipStats
    {
        [Header("Lander Options")]
        public float health;
        public float armour;
        public float fuel;
        public float fuelBurnRate;
        public float mainThrustPower;
        public float sideThrustPower;
    }
    [System.Serializable] public class MissionResults
    {
        public int reason = 0;
        public float health = 0;
        public float fuel = 0;
        public float timeLeft = 0;
    }
}
