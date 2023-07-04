using System;
using System.Collections.Generic;

namespace Air;

public static class Service
{
    private static readonly Dictionary<Type, object> _map = new();

    public static TServiceImpl Register<TServiceImpl>(TServiceImpl serviceImpl) where TServiceImpl : class
    {
        return Register<TServiceImpl, TServiceImpl>(serviceImpl);
    }

    public static TServiceImpl Register<TService, TServiceImpl>(TServiceImpl serviceImpl)
        where TServiceImpl : class, TService
    {
        _map[typeof(TService)] = serviceImpl;
        return serviceImpl;
    }

    public static TService Get<TService>()
    {
        return (TService)_map[typeof(TService)];
    }

    public static Lazy<TService> GetLazy<TService>()
    {
        return new Lazy<TService>(Get<TService>);
    }
}