using System;

namespace Grach.Core.Enums
{
    [Flags]
    public enum CompilerDirectives
    {
        DEBUG = 1,
        DEBUGMOCK = 2,
        RELEASE = 4,
        ENABLE_APPCENTERLOGGER = 8,
        TEST = 16,
        RELEASE2 = 32
    }
}