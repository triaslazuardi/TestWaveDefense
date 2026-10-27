# \# LAST CAMP



\## Game Overview

Last Camp is a 2D base builder with wave defense, set in a snowy mountain pass. You protect your Outpost from three waves of enemies.



Build phase. Enemies are not active. You spend Gold to place structures on fixed build slots around the map. 



There are three structure types with different roles:

* Wall: cheap and sturdy. Enemies stop to attack it, so it holds them in place.
* Turret: shoots the nearest enemy in range. It is your only source of damage, but it is fragile.
* Generator: does nothing during a fight, but pays bonus Gold at the end of every wave it survives.



Defend phase. Press Start Wave. Enemies spawn and walk toward the Outpost. You can no longer build, and your structures act on their own. Enemies stop and attack any structure in reach, then continue toward the Outpost once nothing is in range. When every enemy is dead you earn Gold, and the game returns to the build phase.



With no Turrets you cannot kill anything and you lose. With only Walls you only delay the enemies. Gold is limited, so every wave forces a trade-off, invest in a Generator early , or spend everything on Turrets and Walls to survive the next wave. Later waves add more enemies and a tougher enemy type, so you cannot win by spamming one structure.



Win: survive all 3 waves. 

Lose: HP camp reaches 0.



Controls (mouse only):



* Click a structure button (bottom left), then click an empty slot to place it.
* Start Wave (bottom right) begins the defend phase.



\## How to Run



1. Download and unzip the build from the link below.
2. Run testTopDown.exe (Windows, 64-bit).
3. Click Start in the main menu.
* Engine \& version used: Unity 6000.0.75f1, C#
* Build location: https://drive.google.com/file/d/1oR44QHm0nRmn3q3YHani3K8dsHb8Olaw/view?usp=sharing



\## Technical Decisions



I received this test on 21 October and did the development between 24 and 27 October. With that time, I kept the scope small and focused on the core loop: three complete build-and-defend cycles. All gameplay code is my own.



ScriptableObjects for balance. Structures, enemies and waves are data assets, so I could tune and re-test without touching code. I structured the scripts around SOLID principles





\## What I Would Do With More Time



**Enemies that follow the road.** 

Right now enemies walk in a straight line to the Outpost, ignoring the winding road. I would give them a shared array of waypoints along the road to follow in order, so Walls can block the road in a meaningful way, and add a path preview during the build phase.



**Object pooling** for projectiles and enemies. Bullet are currently instantiated and destroyed on every shot, which will cause garbage-collection spikes in longer or busier waves.





\## Known Issues

* Enemies ignore the road. 
* Enemies can stack. There is no separation between enemies, so groups of the same type overlap into one point.
* Placement is limited to fixed slots.





