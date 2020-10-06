using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Grach.Core.Enums;
using Grach.Core.Helpers;
using UIKit;

namespace Grach.iOS
{
    public class Application
    {
        static Application()
        {
#if DEBUG
            CompilerFlagHelper.CompilerDirectives |= CompilerDirectives.DEBUG;
#endif

#if DEBUGMOCK
            CompilerFlagHelper.CompilerDirectives |= CompilerDirectives.DEBUGMOCK;
#endif

#if RELEASE
            CompilerFlagHelper.CompilerDirectives |= CompilerDirectives.RELEASE;
#endif

#if ENABLE_APPCENTERLOGGER
            CompilerFlagHelper.CompilerDirectives |= CompilerDirectives.ENABLE_APPCENTERLOGGER;
#endif

#if TEST
            CompilerFlagHelper.CompilerDirectives |= CompilerDirectives.TEST;
#endif

#if RELEASE2
            CompilerFlagHelper.CompilerDirectives |= CompilerDirectives.RELEASE2;
#endif
        }
        
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
