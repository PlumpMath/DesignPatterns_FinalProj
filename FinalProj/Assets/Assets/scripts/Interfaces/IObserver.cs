using System.Collections;

/*
 * I made these because the version of .NET Unity complies against doesn't have IOberver or IObservable
 * Should be forward compatable when Unity upgrades to newer .NET
 */

namespace System
{
    public interface IObserver<in Data>
    {
        void OnCompleted();
        void OnError(Exception e);
        void OnNext(Data value);
    }
}

