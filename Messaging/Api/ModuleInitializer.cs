﻿using Microsoft.AspNetCore.Builder;
using Shared.Api;

namespace Messaging.Api;

public abstract class ModuleInitializer : ICoreModule
{
    public static async Task Initialize()
    {
        await MessageNotifier.Initialize();
    }

    public static async Task InjectDependencies(IServiceProvider services)
    {
        await MessageNotifier.InjectDependencies(services);
    }

    public static async Task RegisterEndpoints(WebApplication endpointsRegistry)
    {
        await MessageNotifier.RegisterEndpoints(endpointsRegistry);
    }
}