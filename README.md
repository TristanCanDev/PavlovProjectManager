# PavlovProjectManager
A tool to help you improve your workflow when creating Pavlov maps, mods, or custom gamemodes.


### How does it work?
This tool works by installing and keeping the PavlovVR modkit up-to-date at all times and allowing you to easily create and delete Pavlov project files.

### Why was it created?
The Pavlov Project Manager was created to help newer and older mod and mapmakers manage their project files.

### What features does it have?
Currently, this program can download the current PavlovVR modkit. With that modkit, the application can create and delete projects using that modkit without ever having to worry about the original modkit being lost.

### Okay, that's cool and all, but how do I install and use it?
You can install the application using the latest installer [here](https://github.com/TristanCanDev/PavlovProjectManager/release). Go through the installer like you would any regular installer and when you're ready, you can launch the application. When you launch, windows defender may warn you about the application being from an unknown publisher. Just go ahead and hit 'More info' and 'Run anyways'. The program will then ask you for administrative priveleges. The only reason the program needs these is to create local files within the appdata/local directory. Afterwards, the program will start installing ADB (adb is installed for for a future update) and then install and extract the modkit file. Once that's complete you will be greeted with the main window where you can refresh the project list, create a new project, push a map to your oculus device (**_coming soon_**), delete a project, and change your settings (currently you can only change the UE editor path).

# Q & A
There may be some questions, so I'm going to answer them right off the bat.

## What do I need to have before installing?
You should have Unreal Engine 4.21 and .Net5.0 installed. I believe when you start the program without .net installed, it will prompt you to install it so you shouldn't have any issues there. **YOU NEED TO MAKE SURE YOU INSTALL UNREAL 4.21!! ANY OTHER VERSION WILL NOT WORK WITH THE MODKIT**

## Help! When I click on a project, I get an error saying that the UE editor file isn't found!
To fix this issue, you can hit the settings button and change the path to wherever you have UE4.21 installed.

## How do I uninstall?
On windows, you can hit the search bar and search 'Pavlov Project Manager' and hit uninstall.

### If you have any other questions, please contact [blu#3227](https://discord.com)
