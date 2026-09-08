<details>
<summary>Disclaimer</summary>
This repo primarily exists for personal use, and so projects I'm working on that have multiple programmers can share these utility/helper classes. Again, please note that these tools are built for my own projects; <b><ins>this means that they could change in functionality at any time</ins></b>. If you plan on using them long term, I strongly suggest sticking to one version/installing a packing and sticking to it, or paying very close attention to each update/commit. Feel free to use these in your own projects or base your own code off of mine, no credit needed; just don't claim it as your own.
</details>

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
