# Photon Escape
**Photon Escape** is a small game born long time ago with a bit of inspiration from the all notorious *Flappy Bird*. After many technology interations it is now based in **Unity**, written with **C#** and published under 1.0.0 version on the **Play Store** at the following link: https://play.google.com/store/apps/details?id=com.FreeDev.PhotonEscape.

# Development aspects
You will notice the file structure is a bit chaotic and the code is sometimes a mess. It all started back when I didn't know **Object Oriented Progamming** or **Unity*** script basics and I wrote it pretty procedural. A lot has been rewritten and the latest iterations have been using small **OOP** concepts. Being that I was a single developer and code wasn't intended to be maintanable by other people, during the last stage of development it became more like a rush to finish without sacrificing performance or functionality, so things got chaotic quickly both in code and in files.

You may also notice hardcoded logic which was written early and was not reiterated, may or may *not* be in the future.

There's one last thing to be noted, I had performance problems, little stutters I concluded to be **GarbageCollector***'s fault so I did my best to clean all loops of any possible additional memory allocation, this is how **Vscale** and **Vpos** methods were created, I can't really remember the specific code they replaced though, it may have been an Editor issue after all. This main script is ugly and could be rewritten to be cleaner, but not much faster I believe.

# How to run
Simply open the folder with Unity 5.5.1 (tested, you can try earlier versions too) or later.

You should have your editor asset serialization set to forced text, just to be sure, in case it is not already.

# How to build
Obviously I didn't include the **keystore**. You should be able to build for all platforms including **apk**. Build order and inclusion for scenes should be set by default and should not be touched unless you know what you are doing.

The published **apk** is built with the **il2cpp** feature.

For **Android** build you need **Android SDK** and **Java JDK**. If you want to use **il2cpp** you also need **Android NDK**.
