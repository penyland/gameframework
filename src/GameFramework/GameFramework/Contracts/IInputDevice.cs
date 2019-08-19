namespace GameFramework.Contracts
{
    public interface IInputDevice
    {
        bool IsConnected { get; }

        void Reset();

        void Update();
    }
}
