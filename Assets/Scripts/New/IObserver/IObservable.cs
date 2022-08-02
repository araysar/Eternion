public interface IObservable
{
    void Suscribe(IObserver obs);
    void UnSuscribe(IObserver obs);
    void NotifyToObservers(EventManager.EventType eventType);
}
