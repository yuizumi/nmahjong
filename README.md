Overview
--------

NMahjong provides a set of libraries for writing mahjong AI players and other tools on CLI (usually known as .NET). It is designed as a framework that aims to support a wide range of rule sets and allow one AI code to run with various game servers within each major rule variant.

At this moment NMahjong provides basic support for Japanese Riichi Mahjong and contains a driver for the [mjai game server][1]. Documentations will (hopefully) be available in future.

[1]: http://gimite.net/pukiwiki/index.php?Mjai%20%CB%E3%BF%FDAI%C2%D0%C0%EF%A5%B5%A1%BC%A5%D0



How to Build
------------

### Prerequisites

NMahjong runs on .NET Framework 3.5+ or Mono. You also need [NUnit 2.5+][2] (2.6.x is recommended) to build and run the test suite; make sure you have installed it in your system before following the steps below.

*Note: Xamarin Studio (aka. MonoDevelop) contains NUnit 2.6.x as part of built-in libraries, thus you don't have to install NUnit by yourself.*

[2]: http://www.nunit.org/


### Visual Studio 2010+

Open NMahjong.sln and then build the entire solution. To run the test suite, open tests\NMahjong.nunit in NUnit Runner.

*Note: NUnit Test Adapter may also work, but the author has had no chance to try it.*


### Xamarin Studio (MonoDevelop)

Open NMahjong.sln and then build the entire solution. Tests can be run from the [Unit Tests] pad.


### XBuild (Mono)

Mono is shipped with NUnit 2.4.x, but you need to install a newer version since the NMahjong's test suite does not run with 2.4.x. Suppose you have extracted NUnit-2.6.3.zip in your home directory (i.e. files are located in `~/NUnit-2.6.3`); set the `ReferencePath` environment variable as follows:

    export ReferencePath=~/NUnit-2.6.3/bin

Then run XBuild in the top directory of the source tree:

    xbuild

To run the test suite:

    mono --debug ~/NUnit-2.6.3/bin/nunit-console.exe tests/NMahjong.nunit



Running Sample AI
-----------------

On Windows (Command Prompt):

    src\drivers\mjai\bin\Debug\nmj-mjai client -a src\sample\bin\Debug\NMahjong.Sample.dll -t Shanten mjsonp://localhost:11600/default

On Mac or Linux:

    mono src/drivers/mjai/bin/Debug/nmj-mjai.exe client -a src/sample/bin/Debug/NMahjong.Sample.dll -t Shanten mjsonp://localhost:11600/default

For more details, see the help messages:

    nmj-mjai help
    nmj-mjai client --help



Author
------

Yusuke Izumi (yuizumi AT y5i DOT org)
