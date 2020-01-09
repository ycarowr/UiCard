[![Unity Version](https://img.shields.io/badge/Unity-2019.1.0f2%2B-blue.svg)](https://unity3d.com/get-unity/download)
![version](https://img.shields.io/badge/version-1.2-blue)
[![Twitter](https://img.shields.io/twitter/follow/LagrangeSpot.svg?label=Follow@LagrangeSpot&style=social)](https://twitter.com/intent/follow?screen_name=LagrangeSpot)

# Generic UI for card games like Hearthstone, Magic Arena and Slay the Spire, etc
Here are some images that illustrate what the repository contains and a decription of the content. 

For a playable version access [itch.io](https://ycarowr.itch.io/cardgameui) page.

|Draw Card|Hover Card|
|------------|-------------|
|<img width="382" height="210" src="https://github.com/ycarowr/UiCard/blob/master/Assets/Textures/Ui%20Card%20Gifs/v1.2/drawing.gif">|<img width="382" height="210" src="https://github.com/ycarowr/UiCard/blob/master/Assets/Textures/Ui%20Card%20Gifs/v1.2/hovering.gif">
|<b>Play Card</b>|<b> Angle Positioning</b>
|<img width="382" height="210" src="https://github.com/ycarowr/UiCard/blob/master/Assets/Textures/Ui%20Card%20Gifs/v1.2/play.gif">|<img width="382" height="210" src="https://github.com/ycarowr/UiCard/blob/master/Assets/Textures/Ui%20Card%20Gifs/v1.2/angle.gif">
|<b>Card Spacing</b>|<b> Hover Enemy Card </b>|
<img width="382" height="210" src="https://github.com/ycarowr/UiCard/blob/master/Assets/Textures/Ui%20Card%20Gifs/v1.2/spacing.gif">|<img width="382" height="210" src="/Assets/Textures/Ui%20Card%20Gifs/v1.2/hoverenemy.gif">|

The repository has the main elements of an user interface for a card game like Magic Arena, Hearthstone, Slay the Spire etc. It's made in Unity3D and might be a good "starting point" for games such as the mentioned titles.

Currently, you can:

1. Draw cards
2. Drag cards 
3. Put cards back in the hand dropping onto the "Hand Zone" (green area);
4. Play/Discard cards dropping onto the "Play Card Zone" (orange area);
5. Hover/zoom in cards on the player's hand;
6. Check cards from the enemy's hand
7. As an additional content, I've added to the demo parameters to configure the layout the way you game designer want. It can be done by enabling the checkbox named "configs", on the top-left side of the screen.

The following parameters can be changed:

1. Card spacing: space between cards;
2. Card rotation angle: rotation relative to the index of the card in the hand (cards on the left bend to left and cards on the right 3. 3. bend to the right);
4. Card height (position on Y-axis) relative to the bent angle described on item 2;
5. Card hovered size: how much a card shrinks or grows when hovered;
6. Card hovered rotation: whether it changes or not;
7. Card hovered height: how much a card moves up (Y-axis) when hovered;
8. Card hovered speed: moving speed of the card when hovered;
9. Position or pivot of the Hand: move it up and down;
10. Drop Zone's positions: move them up and down;
11. Disabled card Transparency (alpha): how much a disabled card "fades".
12. Motions speeds: scale, movement and rotation.

Thank you, any feedback is appreciated.

Illustations by: Tyler Warren https://tylerjwarren.itch.io/free-tyler-warren-rpg-battlers-favorites-30

---------------------- MIT License ----------------------

Copyright 2019 Ycarowr

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
