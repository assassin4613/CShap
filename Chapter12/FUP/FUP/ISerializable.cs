namespace FUP
{
    public interface ISerializable
    {
        byte[] GetBytes();
        int GetSize();
    }
}
