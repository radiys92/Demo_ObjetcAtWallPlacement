# Product placement demo
This is demo project which created by brief:
* Design a tool that will allow a user to position a product in the centre of a wall in a room between 2 corners.
* Create a working example of the tool using primitive geometry.
* User experience should be: click the product to select, click one corner, click the other corner, confirm = result.
* Should be coded in UnityScript or C#, no use of plugins (PlayMaker, etc). 

## Platform info 
* Unity version: 5.3.3
* Target platform: WebGL


# User guide
At the start of using we see main screen:
![](https://raw.githubusercontent.com/radiys92/Demo_ObjetcAtWallPlacement/feature/screenshots/_Screenshots/0.png)

## Main screen
This screen contains main panels:
![](https://raw.githubusercontent.com/radiys92/Demo_ObjetcAtWallPlacement/feature/screenshots/_Screenshots/1.png)
#### 1 panel
We can see and switch states of controls throught this panel. Controls have 2 states:
* Mouse moving objects at wall
* Mouse moving camera by orbit around the wall

#### 2 panel
Contains functions 
* **Add new product** - show product adding dialog
* **Clear wall** - remove all spawned products from wall

#### 3 panel
Show or change magnet using. When magnet is switched on - objects stick to wall anchors when they moving.

#### 4 panel
Just info panel

#### 5 panel 
Main mechanic of application - wall! :)

## Product placement dialog
When you press button **Add new product**, the **Select product screen** will shown. You can choose product by preview in this window. To select product - just press needest image:
![](https://raw.githubusercontent.com/radiys92/Demo_ObjetcAtWallPlacement/feature/screenshots/_Screenshots/2.png)
After you choose product, program will propose you select 2 points at wall:
![](https://raw.githubusercontent.com/radiys92/Demo_ObjetcAtWallPlacement/feature/screenshots/_Screenshots/3.png)
And, after this, place object behind 2 points:
![](https://raw.githubusercontent.com/radiys92/Demo_ObjetcAtWallPlacement/feature/screenshots/_Screenshots/4.png)
![](https://raw.githubusercontent.com/radiys92/Demo_ObjetcAtWallPlacement/feature/screenshots/_Screenshots/5.png)