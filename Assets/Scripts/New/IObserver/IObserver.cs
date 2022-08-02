public interface IObserver
{
    void NotifyToEventManager(EventManager.EventType eventType);
}
