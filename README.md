<div id="top"></div>

<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/reden96/steam-voice-commands-net-framework/blob/master/SVC_icon_500.png">
    <img src="https://github.com/reden96/steam-voice-commands-net-framework/blob/master/SVC_icon_500.png" alt="Logo" width="200" height="200">
  </a>

<h3 align="center">Steam Voice Commands</h3>

  <p align="center">
    Windows application that allows you to control some functionality in Steam using your voice
    <br />
    <br />
    <br />
    ·
    <a href="https://github.com/reden96/steam-voice-commands-net-framework/issues">Report Bug</a>
    ·
    <a href="https://github.com/reden96/steam-voice-commands-net-framework/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

Windows application to control some functionality within Steam with your voice.

Requires .NET Framework 4.8.1 - uses Windows Speech Recognition so you will get best results if you do the initial configuration of Windows Speech Recognition to train the voice model. Will start listening for commands once you start the application.

Follows system dark/light mode theme.


<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [.NET Framework Windows System Speech Recognition API](https://learn.microsoft.com/en-us/dotnet/api/system.speech.recognition?view=netframework-4.8.1)
* [.NET Framework 4.8.1 Developer Pack](https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net481-developer-pack-offline-installer)
* WPF

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started


### Prerequisites

* [.NET Framework 4.8.1](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net481)
* Optional: train the Windows Speech Recognition model before you use this application. See here: https://support.microsoft.com/en-us/windows/use-voice-recognition-in-windows-83ff75bd-63eb-0b6c-18d4-6fae94050571

### Installation

Check the [releases](https://github.com/reden96/steam-voice-commands-net-framework/releases) page and download the latest exe file. Recommend putting this file in its own folder, as it will generate some extra files upon launch.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

Click the 'Show Voice Commands' button in the program to see what voice commands are available.

To see list of games detected on startup, click the "Show Installed Games" in the program.

Click the 'Toggle Voice Recognition' button to begin listening for commands, or bind a hotkey in the 'Settings' window and use that. There are also voice commands to start and stop voice recognition ("start voice recognition"/"start voice commands"/"stop voice recognition"/"stop voice commands")

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/reden96/steam-voice-commands-net-framework/issues) for a full list of proposed features (and known issues). Feel free to raise suggestions.

<p align="right">(<a href="#top">back to top</a>)</p>

Project Link: [https://github.com/reden96/steam-voice-commands-net-framework](https://github.com/reden96/steam-voice-commands-net-framework)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/reden96/steam-voice-commands-net-framework.svg?style=for-the-badge
[contributors-url]: https://github.com/reden96/steam-voice-commands-net-framework/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/reden96/steam-voice-commands-net-framework.svg?style=for-the-badge
[forks-url]: https://github.com/reden96/steam-voice-commands-net-framework/network/members
[stars-shield]: https://img.shields.io/github/stars/reden96/steam-voice-commands-net-framework.svg?style=for-the-badge
[stars-url]: https://github.com/reden96/steam-voice-commands-net-framework/stargazers
[issues-shield]: https://img.shields.io/github/issues/reden96/steam-voice-commands-net-framework.svg?style=for-the-badge
[issues-url]: https://github.com/reden96/steam-voice-commands-net-framework/issues
[license-shield]: https://img.shields.io/github/license/reden96/steam-voice-commands-net-framework.svg?style=for-the-badge
[license-url]: https://github.com/reden96/steam-voice-commands-net-framework/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/rowan-lees
