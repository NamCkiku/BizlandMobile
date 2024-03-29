﻿using Google.Maps;

namespace Bizland.Core.iOS
{
    public static class FormsGoogleMaps
    {
        public static bool IsInitialized { get; private set; }

        public static void Init(string apiKey, PlatformConfig config = null)
        {
            MapServices.ProvideAPIKey(apiKey);
            MapRenderer.Config = config ?? new PlatformConfig();
            IsInitialized = true;
        }
    }
}