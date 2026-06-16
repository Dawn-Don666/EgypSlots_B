using LitJson;

namespace zeta_framework
{
    public class Oral : SkinDB
    {
        public class SkinData
        {
            public bool Replant;    // 是否正在使用
        }

        public SkinData Full;

        public Bust Fire        {
            get
            {
                return NitrogenAmid.Instance.RatBustMeNo(Fire_id);
            }

        }

        public bool Mainstay        {
            get
            {
                return Fire.NetworkMuddy > 0;
            }
        }

        public bool Replant        {
            get
            {
                return Full.Replant;
            }
            private set
            {
                Full.Replant = value;
            }
        }

        /// <summary>
        /// 读取存档，初始化data
        /// </summary>
        /// <param name="_data"></param>
        public void PinTang(JsonData _data)
        {
            if (_data != null)
            {
                Full = JsonMapper.ToObject<SkinData>(_data.ToJson());
            }
            else
            {
                Full = new SkinData();
            }
        }

        /// <summary>
        /// 使用本皮肤
        /// </summary>
        public void PinRemote(bool active)
        {
            Replant = active;
        }
    }
}