﻿using HarmonyLib;
using LevelImposter.DB;
using LevelImposter.Map;
using LevelImposter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace LevelImposter.Builders
{
    class ShipRoomBuilder : Builder
    {
        private PolusHandler polus;
        private int roomId;
        public static Dictionary<long, SystemTypes> db;

        public ShipRoomBuilder(PolusHandler polus)
        {
            this.polus = polus;
            roomId = 1;
            db = new Dictionary<long, SystemTypes>();

            // Make Default Room
            GameObject defaultObj = new GameObject("Default Room");
            BoxCollider2D defaultCollider = defaultObj.AddComponent<BoxCollider2D>();
            PlainShipRoom defaultRoom = defaultObj.AddComponent<PlainShipRoom>();
            defaultRoom.RoomId = 0;
            defaultRoom.roomArea = defaultCollider;
            defaultObj.transform.SetParent(polus.gameObject.transform);
        }

        public bool Build(MapAsset asset)
        {
            // Check Collider Count
            if (asset.colliders.Length <= 0)
            {
                LILogger.LogWarn(asset.name + " does not have any colliders!");
                return false;
            }

            // Object
            GameObject obj = new GameObject(asset.name);

            // Collider
            PolygonCollider2D mainCollider = null;
            foreach (MapCollider collider in asset.colliders)
            {
                PolygonCollider2D polyCollider = obj.AddComponent<PolygonCollider2D>();
                polyCollider.isTrigger = true;
                polyCollider.SetPath(0, collider.GetPoints(asset.xScale, asset.yScale));
                mainCollider = polyCollider;
            }

            // Room
            PlainShipRoom room = obj.AddComponent<PlainShipRoom>();
            room.RoomId = (SystemTypes)roomId;
            if (asset.colliders.Length > 0)
                room.roomArea = mainCollider;

            // Room DB
            db.Add(asset.id, (SystemTypes)roomId);

            // Polus
            polus.shipStatus.AllRooms = AssetBuilder.AddToArr(polus.shipStatus.AllRooms, room);
            polus.shipStatus.FastRooms.Add((SystemTypes)roomId, room);
            polus.minimap.Generate(asset);
            polus.Add(obj, asset);

            roomId++;
            return true;
        }
    }
}