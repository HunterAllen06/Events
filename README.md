# Events
A small system that allows you to bind events to functions by type or object.
```cs
GameObject _someObject;
EventBus _bus;
EventChannel _someChannel;

void Examples()
{
    EventManager.TBind<SomeType>(MyFunction);
    _someObject.Bind(SomeFunction);
    _bus.Bind(SomeOtherFunction);
    _someChannel.Bind(AnotherFunction);

    EventManger.TRaise<SomeType>(); // Calls MyFunction
    _someObject.Raise(); // Calls SomeFunction
    _bus.Raise(); // Calls SomeOtherFunction
    _someChannel.Raise(); // Calls AnotherFunction
}
```
