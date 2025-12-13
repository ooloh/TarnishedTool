using SilkyRing.Interfaces;
using SilkyRing.Memory;

namespace SilkyRing.Services
{
    public class EventService(MemoryService memoryService, HookManager hookManager) : IEventService
    {
        
        public void SetEvent(long eventId)
        {
            
        }

        public bool GetEvent(long eventId)
        {
            return false;
        }
    }
}