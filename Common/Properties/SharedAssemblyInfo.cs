﻿using System;
using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyDescription("Manga Bot aims to download manga from some popular websites.")]
[assembly: AssemblyCompany("Duy Tran")]
//[assembly: AssemblyProduct("Manga Bot")]
[assembly: AssemblyCopyright("Copyright © DuyTran 2016")]
[assembly: AssemblyTrademark("dangduy2910@gmail.com")]

// Make it easy to distinguish Debug and Release (i.e. Retail) builds;
// for example, through the file properties window.
#if DEBUG

[assembly: AssemblyConfiguration("Debug")]

#else

[assembly: AssemblyConfiguration("Release")]

#endif


[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("2.3")]
[assembly: AssemblyInformationalVersion("2.3")]