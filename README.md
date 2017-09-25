# NHLGames
Tool to watch NHL games in High Definition
- Choose a date by using the calendar or the nav arrows.
- Choose a game that you want to watch.
- Click on a stream link inside the game panel.
- Enjoy the game.

![image](https://user-images.githubusercontent.com/23088305/30517831-7276e442-9b39-11e7-8faa-e8960df24e8c.png)

## Index
* [First use](#first-use)
  * [Requirements](#requirements)
  * [Message about the hosts file](#message-about-the-hosts-file)
  * [Is this app safe?](#is-this-app-safe)
  * [Setup](#setup)
     * [Hosts file](#hosts-file)  
 * [User interface](#user-interface)
   * [Games](#games)
      * [Navigation bar](#navigation-bar)
      * [Game panel](#game-panel)
   * [Settings](#settings)
     * [Show Scores](#show-scores)
     * [Players](#players)
     * [Stream Quality](#stream-quality)
     * [Content Delivery Network (CDN)](#content-delivery-network-cdn)
     * [Server's Hostname](#servers-hostname)
     * [Server Hosts Entry](#server-hosts-entry)
     * [Streamlink](#streamlink)
     * [Other options](#other-options)
   * [Console](#console)
   * [Modules](#modules)
* [Support](#support)
  * [Players issues](#players-issues)
    * [No playback](#no-playback)
    * [No audio](#no-audio)
    * [Player is lagging](#player-is-lagging)
  * [App issues](#app-issues)
    * [App won't start](#app-wont-start)
    * [Display issues](#display-issues)
    * [Games won't load](#games-wont-load)
    * [Games won't start](#games-wont-start)
    * [Hosts file issues](#hosts-file-issues)
    * [Ad Detection Modules](#ad-detection-modules)
* [Chromecast](#chromecast)
* [Contribute](#contribute)


# First use
## Requirements
NHLGames is an app built on .NET Framework 4.5. So, it's only available on Windows and works on any CPU (x86/x64). If you run NHLGames on Windows 7 or older you will probably need to install [.NET Framework 4.5](https://www.microsoft.com/en-ca/download/details.aspx?id=30653), but we can't garantee it will run on XP since Microsoft ended support on XP.

## Message about the hosts file
First time you start NHLGames it will ask if you wish to view the Hosts file. That means the app has changed a system file to let you use NHLGames without issues by adding a line like this one `XXX.XXX.XXX.XXX www.hosting.site.com` at the end of it. If you want to view the changes, then click Yes and you will have to select Notepad to view the file. If NHLGames did not succesfully changed this file, see the [Server Hosts Entry](#server-hosts-entry) section.

## Is this app safe?
So, you might wonder if this application is safe. You can look at our code to find out by yourself, you will see the reason why anti-virus don't like us. We modify your hosts file in Windows/System32/drivers/etc/ and we do that to make sure that the app will be able to get the stream links for games that are available. The other reason is that the app sends web request to a server that might be blacklisted. But don't worry, this server only responds with plain text.

## Setup
To be able to play streams properly, you have to choose a media player in the ![image](https://cloud.githubusercontent.com/assets/23088305/25557243/ce306f64-2cdb-11e7-9fa3-a4a73161c3ea.png) tab. Make sure the player that you choose has a valid path to the EXE file.

MPV player comes with NHLGames. So if you don't have or want VLC/MPC players, just use our default media player to watch games. Make sure you select mpv as the default player.

If you want to change some settings, see the [Settings](#settings) section.

### Hosts file
See the [Server Hosts Entry](#server-hosts-entry) section.

# User interface
Everytime you launch NHLGames it will search for today's games. 

## Games
### Navigation bar
If you want to watch past games, use the calendar or use the arrows to navigate through the days.
![image](https://cloud.githubusercontent.com/assets/23088305/25556865/0f4cc51e-2cd3-11e7-96db-b875a4ab716c.png)
Use the refresh button (at the right) to refresh the current day games.

### Game panel
Here are 5 differents game panels that you will see while using NHLGames.
  1. Dark Gray : Upcoming games
  2. Blue : Today's games
  3. Green : Pre game
  4. Red : In progress
  5. Gray : Final games
  
  Note : 6th picture shows the possibility to add live score or final score to the panel. You can turn it on/off in Settings tab.

![image](https://cloud.githubusercontent.com/assets/23088305/25744794/b7e037f2-3169-11e7-89d6-98fe61684158.png)

## Settings
### Show Scores
NHLGames gives you some options to change how a game panel will appear.
- Final Scores : If on, it shows final score of all past games.
- Live Scores : If on, it shows live score of all current games.
- Series Record : If on, it shows serie records of all upcoming playoffs games.

### Players
NHLGames supports 3 media players:
- MPV (default player, comes with NHLGames)
- MPC
- VLC

If you don't have or want VLC/MPC players, use our default media player to watch games. Make sure you select `MPV` as the default player.

If you had previously installed VLC or MPC, NHLGames should find it automatically if you installed it in the default folder `Program Files`, otherwise you will have to browse ![image](https://cloud.githubusercontent.com/assets/23088305/25557239/b99ec37a-2cdb-11e7-8c27-d8b563128e8d.png) your computer and get the path to the EXE file. 

If you don't have one of these players installed and you want to install it, use the links on the right to download it.

### Stream Quality
The selected value will defined which quality will be sent to your media player, from the lowest (224p) to the highest quality (720p at 60fps). Selecting the highest quality also means bigger files to download :

- 224p is about 300 MB per hour
- 228p is about 500 MB per hour
- 360p is about 700 MB per hour
- 504p is about 950 MB per hour
- 540p is about 1.30 GB per hour
- 720p is about 1.80 GB per hour
- 720p at 60fps is about 2.50 GB per hour

### Content Delivery Network (CDN)
Level 3 :
```
Level 3 owns and operates a global Tier-1 network and - logically - their CDN runs on top of it. 
Level 3 CDN has POPs on all continents and their product focus is on video and large object delivery. 
Level 3 CDN is part of the Google Cloud CDN Interconnect.
```

Akamai :
```
Akamai is one of the oldest CDNs and generally considered to be the largest global CDN. 
They have 'servers everywhere' and a wide range of products and services. 
The company is actively involved in Let's Encrypt and is pushing HTTP/2 adoption.
```

### Server's Hostname
This drop down list shows all NHLGames server hostname, so if you can't play games, try another hostname.

### Server Hosts Entry
If the selected hostname (above) can be resolve by your network, it will get and save the related IP adress into the Windows Hosts file. 

To test your Hosts file, go to Settings and click on `Test` button. It should tells you if everything is fine.

If NHLGames is not set properly, try the following options:
- Right click on NHLGames.exe and select `Run as an administrator`.
- Click on `View Hosts` or go to `C:\Windows\System32\Drivers\etc` and right click on `hosts` file, choose open it with Notepad. Go at the end of the file and make sure that our entry is there. You can find the entry in the Settings tab, there is a help button `?` on the 'Server Hosts Entry' line, click on it and you will see the line that you need to copy to your clipboard (click on 'Yes'). If you don't find the entry line in the Hosts file and if the `Add Entry` button does not work. You can paste the line from your clipboard (that you copied earlier) or you can type it manually. Hit `Ctrl+S` to save it, if a save dialog pops, make sure the file name is `hosts` and not hosts.txt, also the file type should be set to `All Files (*.*)`.

Note: If you need to remove NHLGames entry, just click on `Remove Entries` or open it with `View Hosts` and remove our entry.

### Streamlink
Streamlink is not a media player, it's an application that NHLGames use to get the stream from Internet and parse it to your media player. The path to streamlink.exe is inside NHLGames directory and it should not move, otherwise you will have to get the new path to it or you will get a message box that NHLGames lost the path and won't be able to play streams.

### Other options
If you wish to use these other options make sure to use the ON/OFF switch first.
- Output : Path where you want to save games as .mp4 files.
- Player args : If you want to add more arguments (commands) to be sent to your media player with the default args that NHLGames send.
- Streamer args :  If you want to add more arguments (commands) to be sent to streamlink with the default args that NHLGames send.

## Console
Go to this section to inspect everything that NHLGames does. Also, any error or warning will show here.

## Modules
NHLGames doesn't use any Ad Detection modules by default, but you can activate one by checking `Enabled` and by moving a module from the left box to the right. Select one and use the `>>>` arrow button to activate it, use the `<<<` arrow button to desactivate it. If you don't use any, it's better if you disable the Ad Detection module. 


# Support
## Players issues
First of all, if you have issues with one player you can try another player at any time by choosing another player in the `Settings` tab in the app under the `Players` section. Just make sure your choosen player is installed and the path is set in the settings tab. No need to download MPV, it comes with NHLGames and should work.

### No playback
#### Player used to open, but it does not anymore --> Kill old processes of this player
Sometimes a player process can still runs even if the stream ended and an old process of the same name can affect another from starting. So right click on your Windows taskbar and click on `Start Task Manager` (CTRL + Shift + ESC), go to `Processes` tab and right click on all of your player processes (.exe) that are running and click on `End Process`.

#### VLC 64 bits No playback --> use 32 bits
If you run VLC 64 bits version and VLC hangs when buffering and no playback starts or you don't have audio, use the 32 bits version (x86) instead. Since we use Streamlink, some versions of 64-bit VLC seem to be unable to read the stream. Using the 32 bits version of VLC might help.
- [x86 (VLC 2.2.4 32bits exe)](https://download.videolan.org/pub/videolan/vlc/2.2.4/win32/vlc-2.2.4-win32.exe)
- [VLC 2.2.4 path](https://download.videolan.org/pub/videolan/vlc/2.2.4/)

### No Audio
#### MPC No audio --> Increase stream analysis duration
You will need to do the following to fix audio:

`Options > Internal Filters > Splitter`

Increase `stream analysis duration` (eg. 3000)

### Player is lagging
#### Are you using a laptop on power saver mode ? --> use Performance mode
Streaming games needs processor time and network bandwidth, so your laptop has to be set to High Performance mode.

#### Enable caching in your player
Go to NHLGames Settings and Enable Players args, then add this line `--cache XXXX` inside the text box. Value `XXXX` is a value in KB. It goes from 1024, 2048, 4096 to 8192.

#### Enable multi-threaded streaming
Go to NHLGames Settings and Enable Streamer args, then add this line `--hls-segment-threads X` inside the text box.  Value `X` goes from 1 to 10, 2 or 4 should be enough.

#### Try to disable Windows auto tuning level
Open Command Prompt :

`netsh int tcp set global autotuninglevel=disabled`

If this impacts your connection and you want to return to the default value

`netsh int tcp set global autotuninglevel=normal`

#### Are you using VLC 3.0 ? --> use 2.2.4
If you run VLC 3.0 and it's stuttering, lagging or not rendering well, go back to the most stable version 2.2.4 :
- [x86 (VLC 2.2.4 32bits exe)](https://download.videolan.org/pub/videolan/vlc/2.2.4/win32/vlc-2.2.4-win32.exe)
- [x64 (VLC 2.2.4 64bits exe)](https://download.videolan.org/pub/videolan/vlc/2.2.4/win64/vlc-2.2.4-win64.exe)
- [VLC 2.2.4 path](https://download.videolan.org/pub/videolan/vlc/2.2.4/)


## App issues

### App won't start
#### Application error --> Download Microsoft .NET Framework 4.5
If you get an error like [this one](https://cloud.githubusercontent.com/assets/23088305/26311601/e75d6f9c-3ed2-11e7-8755-12075a888704.png) when trying to open NHLGames that means : 
- You have to download and install [Microsoft .NET Framework 4.5](https://www.microsoft.com/en-ca/download/details.aspx?id=30653) (at least).
- You have to run an Windows Operating System older than XP : Windows Vista, 7, 8, 10.

#### Windows protected your PC --> Run anyway
If you get an error like [this one](https://cloud.githubusercontent.com/assets/23088305/26286664/b2a00c14-3e1f-11e7-82b8-641bebb90d1f.png) that means Windows 10 wants to stop NHLGames from running because it is not an official publisher from Windows Store. Click on More Infos and click on Run anyway.

### Display issues
#### Display scaling issue on high DPI settings --> Disable DPI for NHLGames
A high DPI display (other than 100%) can cause controls to be cut off. Right click on NHLGames.exe and check this option: `Properties > Compatibility > Display scaling on high DPI settings`.

### Games won't load
#### Forbidden 403, Unauthorized 401 or Not Found 404 Error --> Use another NHLGames domain in Settings
If you can't get any games from NHLGames, try using another domain in the `Settings` tab. Use the drop down list.

#### Timeout error when trying to load games --> Restart the app
Restarting the app might help, until we fix the issue related to our downloader.

### Games won't start
#### Nothing happens after clicking on a stream --> Check your Hosts file
If you clicked on a stream and nothing starts after the loading bar has disappeared, go to `Settings` and click on `Test` button to check if your Hosts file has been modified by NHLGames. If you get a red message box that means you won't be able to see a stream until you fix your Windows Hosts file. See the [Hosts](#hosts) section.

### Hosts file issues
#### Windows can't find Hosts file --> Do it yourself
NHLGames can't have access to the Hosts file, so you will have to add the entry line from `Settings` tab > `DIY Steps` button and paste it to your Hosts file in `C:\Windows\System32\Drivers\etc`. See the [Hosts](#hosts) section.

### Ad Detection Modules
#### Tab not showing up --> Unblock DLLs
Right click on the DLL's in the Spotify/OBS folders then in `Properties` check the `unblock` button at the bottom of the `General` tab, that should get them to show up.


# Chromecast
NHLGames doesn't support Chromecast, but Google Chrome does. Follow these steps if you want to play the game on your TV.

![image](https://cloud.githubusercontent.com/assets/23088305/25557771/3570cf2a-2ce6-11e7-980a-b605b93c66dc.png)

3. Select a pc monitor you want to share. Make sure audio share is checked.

<img src="https://cloud.githubusercontent.com/assets/23088305/25556591/3145542a-2ccd-11e7-8ba4-d4947e2bb84f.png" width="460"/>

4. Use NHLGames to get a stream, once the game plays, move the media player window to the right monitor and enjoy the show.

<img src="https://cloud.githubusercontent.com/assets/23088305/25556617/a6caab1e-2ccd-11e7-89d3-c9177a997ed1.png" width="300"/>


# Contribute
NHLGames is written in Visual Basic. Ad Detection modules are written in C#. We use Visual Studio to code. 

If you want to contribute :
- Fork the project and submit a pull request to `NHLGames\master` branch.
- Open an issue and tell us what's wrong with NHLGames.

NHLGames use also these NuGet packages:
- MetroModernUI
- CSCore
- Newtonsoft JSON .NET
- Prism.Core
- SpotifyAPI-NET
