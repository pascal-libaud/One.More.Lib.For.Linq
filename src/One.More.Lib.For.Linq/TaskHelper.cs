﻿namespace One.More.Lib.For.Linq;

public static class TaskHelper
{
    public static Task<T> ToTask<T>(this T item)
    {
        return Task.FromResult(item);
    }
}