# SceneLoader

### What it is ?
SceneLoader is a framework for Unity which loads a scene, shows a loading screen with fade animation.
The loading screen shows progress and text(tooltips, hints, quotes) which is fetched from a google sheet.

### Third Party Plugins
- TextMeshPro - For signed distance field in VR support.

**Note: Requires Unity 2019 or higher.**
### Unity Scene Loading
Unity loading scene works in two step process,
- Load all the assets and scripts of scene
- Enable all the objects of scene

For first step AsynOperation of scene is between 0-0.9, and for second it goes from 0.9 to 1. 

### Also Contains
- Example with VR like mouse look and interactions.
- Simple HUD UI with world canvas for VR

### Know Issues
- Progress bar jumps to 0.9 instead of continous transition.
- LoadLevelAsync is not completely async.

### Development

Want to contribute? Great!
We welcome any contributions, be they bug reports, requests or pull request. 

### Todos

 - Write more tests fo different components
 - Profile and optimize for performance
 - Look for more TODOs

License
----
This is free and unencumbered software released into the public domain.
Anyone is free to copy, modify, publish, use, compile, sell, or distribute this software, either in source code form or as a compiled binary, for any purpose, commercial or non-commercial, and by any means.

For more information, please refer to http://unlicense.org/
