using UnityEngine;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop;
using DaggerfallWorkshop.Game.Items;
using DaggerfallWorkshop.Utility;
using DaggerfallWorkshop.Game.Utility.ModSupport;
using System;
using System.Collections.Generic;
using DaggerfallWorkshop.Game.Serialization;

namespace AlchemyOverhaul
{
    public class ClosedChestData
    {
        public ulong loadID;
        public Vector3 currentPosition;
        public Quaternion currentRotation;
        public int[] recentInspectValues;
        public bool isLockJammed;
        public bool hasBeenBashed;
        public bool hasBeenInspected;
        public int chestSturdiness;
        public int chestMagicResist;
        public int chestStartHP;
        public int chestCurrentHP;
        public int lockSturdiness;
        public int lockMagicResist;
        public int lockComplexity;
        public int jamResist;
        public int lockStartHP;
        public int lockCurrentHP;
        public int picksAttempted;
        public int lockMechStartHP;
        public int lockMechCurrentHP;
        public ItemData_v1[] attachedLoot;
    }

    public class OpenChestData
    {
        public ulong loadID;
        public Vector3 currentPosition;
        public Quaternion currentRotation;
        public int textureArchive;
        public int textureRecord;
        public ItemData_v1[] items;
    }

    [FullSerializer.fsObject("v1")]
    public class AlchemyOverhaulSaveData : IHasModSaveData
    {
        public Dictionary<ulong, ClosedChestData> ClosedChests;
        public Dictionary<ulong, OpenChestData> OpenChests;

        public Type SaveDataType
        {
            get { return typeof(AlchemyOverhaulSaveData); }
        }

        public object NewSaveData()
        {
            AlchemyOverhaulSaveData emptyData = new AlchemyOverhaulSaveData();
            emptyData.ClosedChests = new Dictionary<ulong, ClosedChestData>();
            emptyData.OpenChests = new Dictionary<ulong, OpenChestData>();
            return emptyData;
        }

        public object GetSaveData()
        {
            return null;
        }

        public void RestoreSaveData(object dataIn)
        {
            //
        }
    }
}