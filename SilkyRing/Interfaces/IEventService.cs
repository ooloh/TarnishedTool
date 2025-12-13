// 

namespace SilkyRing.Interfaces;

public interface IEventService
{
    void SetEvent(long eventId, bool flagValue);
    bool GetEvent(long eventId);
}