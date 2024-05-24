using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using NetcodePlus;

namespace SurvivalEngine
{

    [System.Serializable]
    public enum RefreshType
    {
        None = 0,
        All = 100, //IDs start at 100 to avoid ushort conflict with NetworkActionType
        RefreshObject = 105,

        GameTime = 110,
        Player = 112,
        Inventory = 114,
        Attributes = 116,
        Levels = 118,

        CustomInt = 130,
        CustomFloat = 131,
        CustomString = 132,
        ObjectRemoved = 133,


    }

    /// <summary>
    /// List of serializable data
    /// </summary>
    /// 

    [System.Serializable]
    public class RefreshPlayerAttributes : INetworkSerializable
    {
        public int player_id;
        public int gold;
        public Dictionary<AttributeType, float> attributes = new Dictionary<AttributeType, float>();
        public Dictionary<BonusType, TimedBonusData> timed_bonus_effects = new Dictionary<BonusType, TimedBonusData>();

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref player_id);
            serializer.SerializeValue(ref gold);
            NetworkTool.SerializeDictionaryEnum(serializer, ref attributes);
            NetworkTool.SerializeDictionaryEnumObject(serializer, ref timed_bonus_effects);
        }

        public static RefreshPlayerAttributes Get(PlayerData pdata)
        {
            RefreshPlayerAttributes attr = new RefreshPlayerAttributes();
            attr.player_id = pdata.player_id;
            attr.gold = pdata.gold;
            attr.attributes = pdata.attributes;
            attr.timed_bonus_effects = pdata.timed_bonus_effects;
            return attr;
        }
    }

    [System.Serializable]
    public class RefreshPlayerLevels : INetworkSerializable
    {
        public int player_id;
        public Dictionary<string, PlayerLevelData> levels = new Dictionary<string, PlayerLevelData>();

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref player_id);
            NetworkTool.SerializeDictionaryObject(serializer, ref levels);
        }

        public static RefreshPlayerLevels Get(PlayerData pdata)
        {
            RefreshPlayerLevels attr = new RefreshPlayerLevels();
            attr.player_id = pdata.player_id;
            attr.levels = pdata.levels;
            return attr;
        }
    }

    [System.Serializable]
    public class RefreshGameTime : INetworkSerializable
    {
        public int day;
        public float day_time;

        public RefreshGameTime() { }
        public RefreshGameTime(int d, float dt) { day = d; day_time = dt; }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref day);
            serializer.SerializeValue(ref day_time);
        }
    }

    [System.Serializable]
    public class RefreshCustomInt : INetworkSerializable
    {
        public string uid;
        public int value;

        public RefreshCustomInt() { }
        public RefreshCustomInt(string id, int val) { uid = id; value = val; }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref uid);
            serializer.SerializeValue(ref value);
        }
    }

    [System.Serializable]
    public class RefreshCustomFloat : INetworkSerializable
    {
        public string uid;
        public float value;

        public RefreshCustomFloat() { }
        public RefreshCustomFloat(string id, float val) { uid = id; value = val; }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref uid);
            serializer.SerializeValue(ref value);
        }
    }

    [System.Serializable]
    public class RefreshCustomString : INetworkSerializable
    {
        public string uid;
        public string value;

        public RefreshCustomString() { }
        public RefreshCustomString(string id, string val) { uid = id; value = val; }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref uid);
            serializer.SerializeValue(ref value);
        }
    }
}
