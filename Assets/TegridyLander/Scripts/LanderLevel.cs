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
    public class LanderLevel : MonoBehaviour
    {
        [Header("LevelSettings")]
        public string levelName;
        public Transform startPosition;
        public Transform endPosition;
        public float gravity = 1;
        public float levelTime = 30;
        public float landedTimer = 0.5f;
        
        [Header("Level Items")]
        public LanderVessel[] ships;
        public TerrainItem[] worldItems;

        [Header("Mission Over")]
        public int fuelOutDelay = 4;
        public int missionOverDelay = 3;

        [Header("GUI")]
        public GUILevelOverlay gui;
        public Sprite levelPic;
        public void StartUp()
        {
            if (ships.Length == 0)
            {
                ships = FindObjectOfType<LanderGame>().ships;
            }
            foreach(TerrainItem thisGroup in worldItems)
            {
                PolygonCollider2D[] tempObjects = GetComponentsInChildren<PolygonCollider2D>();
                foreach (PolygonCollider2D thisItem in  tempObjects)
                {
                    LanderCollider newItem = thisItem.gameObject.AddComponent<LanderCollider>();
                    newItem.stats = thisGroup.stats;
                    newItem.soundFX = thisGroup.soundFX;
                }
            }
        }
    }
}
