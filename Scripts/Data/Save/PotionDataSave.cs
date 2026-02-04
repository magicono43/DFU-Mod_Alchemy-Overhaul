using System;

namespace AlchemyOverhaul.Data.Save
{
    [Serializable]
    public sealed class PotionDataSave
    {
        public int Version;
        public PotionDataV1 Data;
    }
}