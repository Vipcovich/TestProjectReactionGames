using System;

public static class ActionExtensions
{
    public static void SafetyInvoke<T1, T2>(this Action<T1, T2> action, T1 obj1, T2 obj2)
    {
        if (action == null)
        {
            return;
        }

        action.Invoke(obj1, obj2);
    }

    public static void SafetyInvoke<T>(this Action<T> action, T obj)
    {
        if (action == null)
        {
            return;
        }

        action.Invoke(obj);
    }

    public static void SafetyInvoke(this Action action)
    {
        if (action == null)
        {
            return;
        }

        action.Invoke();
    }
}

