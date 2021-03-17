namespace DashboardComm.Logging
{
    public interface ILogServiceSerializedLoadable
    {
        void ClearLogAndLoadFromSerializedJson<T>(string jsonserializedlog);
        string GetLogAsSerializedJson();
    }
}