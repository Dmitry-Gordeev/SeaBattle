namespace SeaBattle.Common.Objects
{
    public interface ISerializableObject
    {
        bool SomethingChanged { get; set; }
        void DeSerialize(ref int position, byte[] dataBytes);
        byte[] Serialize();
    }
}
